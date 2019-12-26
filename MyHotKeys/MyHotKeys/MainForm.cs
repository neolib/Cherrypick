using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyHotKeys
{
    using MyHotKeys.Properties;

    public partial class MainForm : Form
    {
        #region Member Variables

        int invokeDelaySeconds;
        bool keyboardStateSaved;
        byte[] keyboardStateBuffer = new byte[256];
        int hotkeyIdSequence = 1;
        Dictionary<int, HotKeyItem> hotkeyIdBag = new Dictionary<int, HotKeyItem>();

        bool windowShown;
        internal bool startMinized;

        #endregion

        #region Private Methods

        ListViewItem GetSelectedItem()
        {
            if (hotKeyListView.SelectedItems.Count > 0)
                return hotKeyListView.SelectedItems[0];
            return null;
        }

        void LoadList()
        {
            hotKeyListView.Items.Clear();
            var hotkeys = G.HotKeyManager.Load();
            foreach (var entry in hotkeys)
            {
                var hotkey = new HotKeyItem
                {
                    Name = entry.Value.Name,
                    KeyText = entry.Value.KeyText,
                    Macro = entry.Value.Macro,
                    KeyStrokes = X.KeyFromText(entry.Value.KeyText)
                };
                var lvi = new ListViewItem(new[] { hotkey.Name, hotkey.KeyText });
                lvi.Tag = hotkey;
                hotKeyListView.Items.Add(lvi);
            }
        }

        void RegisterHotKeys()
        {
            foreach (ListViewItem lvi in hotKeyListView.Items)
            {
                var hotkey = (HotKeyItem)lvi.Tag;
                if (RegisterHotKey(hotkey, hotkeyIdSequence))
                {
                    hotkeyIdBag[hotkeyIdSequence] = hotkey;
                    hotkey.RegisteredId = hotkeyIdSequence;
                    hotkeyIdSequence++;
                }
            }
        }

        bool RegisterHotKey(HotKeyItem hotkey, int id)
        {
            var flags = GetFlags_(hotkey.KeyStrokes);
            var vk = (int)(hotkey.KeyStrokes & Keys.KeyCode);
            if (!Z.RegisterHotKey(Handle, hotkeyIdSequence, flags, vk))
            {
                var prompt = $"Failed to register hot key \"{hotkey.Name}\": {hotkey.KeyText}.";
                Program.AlertBox(this, prompt);
                return false;
            }
            return true;

            uint GetFlags_(Keys key)
            {
                uint flags_ = 0;
                if (key.HasFlag(Keys.Alt)) flags_ |= Z.MOD_ALT;
                if (key.HasFlag(Keys.Control)) flags_ |= Z.MOD_CONTROL;
                if (key.HasFlag(Keys.Shift)) flags_ |= Z.MOD_SHIFT;
                return flags_;
            }
        }

        void InvokeHotKey(HotKeyItem hotkey)
        {
            keyboardStateSaved = Z.SaveKeyboardState(keyboardStateBuffer);
            try
            {
                SendKeys.Send(hotkey.Macro);
            }
            catch (Exception ex)
            {
                Program.ErrorBox(this, $"Error invoking hot key \"{hotkey.Name}\".\n{ex}", "Error");
            }
            if (keyboardStateSaved) Z.RestoreKeyboardState(keyboardStateBuffer);
        }

        #endregion

        #region Infrastructure

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Z.WM_HOTKEY)
            {
                var id = m.WParam.ToInt32();
                var hotkey = hotkeyIdBag[id];
                InvokeHotKey(hotkey);
                return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            Icon = Resources.IconCtrl;
            notifyIcon.Icon = Icon;

            LoadList();
            RegisterHotKeys();

            invokeDelaySeconds = G.HotKeyManager.GetDelayTime();
            delayTimeEdit.Text = invokeDelaySeconds.ToString();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var form = new EditForm
            {
                HotKey = new HotKeyItem(),
                IsNewMode = true
            };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var lvi = new ListViewItem(new[] { form.HotKey.Name, form.HotKey.KeyText });
                lvi.Tag = form.HotKey;
                hotKeyListView.Items.Add(lvi);

                if (RegisterHotKey(form.HotKey, hotkeyIdSequence))
                {
                    hotkeyIdBag[hotkeyIdSequence] = form.HotKey;
                    form.HotKey.RegisteredId = hotkeyIdSequence;
                    hotkeyIdSequence++;
                }
            }
        }
 
        private void editButton_Click(object sender, EventArgs e)
        {
            var lvi = GetSelectedItem();
            if (lvi == null) return;

            var form = new EditForm
            {
                HotKey = (HotKeyItem)lvi.Tag
            };
            var keystrokes = form.HotKey.KeyStrokes;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                lvi.Text = form.HotKey.Name;
                lvi.SubItems[1].Text = form.HotKey.KeyText;

                if (keystrokes != form.HotKey.KeyStrokes)
                {
                    Z.UnregisterHotKey(Handle, form.HotKey.RegisteredId);
                    RegisterHotKey(form.HotKey, form.HotKey.RegisteredId);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var lvi = GetSelectedItem();
            if (lvi == null) return;

            var hotkey = (HotKeyItem)lvi.Tag;
            var prompt = $"Delete hotkey with name \"{hotkey.Name}\"?";
            if (Program.ConfirmBox(this, prompt, "Delete"))
            {
                if (G.HotKeyManager.Delete(hotkey.Name))
                {
                    Z.UnregisterHotKey(Handle, hotkey.RegisteredId);
                    LoadList();
                }
            }
        }

        private void invokeButton_Click(object sender, EventArgs e)
        {
            var lvi = GetSelectedItem();
            if (lvi == null) return;

            var hotkey = (HotKeyItem)lvi.Tag;
            Task.Run(async () =>
            {
                await Task.Delay(invokeDelaySeconds);
                Invoke((Action)(() => InvokeHotKey(hotkey)));
            });
        }

        private void hotKeyListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var f = hotKeyListView.SelectedItems.Count > 0;
            editButton.Enabled = f;
            deleteButton.Enabled = f;
            invokeButton.Enabled = f;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var entry in hotkeyIdBag)
            {
                Z.UnregisterHotKey(Handle, entry.Value.RegisteredId);
            }
        }

        private void hotKeyListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label)) return;

            if (G.HotKeyManager.HasName(e.Label))
            {
                e.CancelEdit = true;
                Program.AlertBox(this, "Duplicate name found.");
            }
            else
            {
                var hotkey = (HotKeyItem)hotKeyListView.Items[e.Item].Tag;
                G.HotKeyManager.UpdateName(hotkey.Name, e.Label);
            }
        }

        private void hotKeyListView_DoubleClick(object sender, EventArgs e)
        {
            editButton_Click(sender, e);
        }

        private void hotKeyListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0d) editButton_Click(sender, e);
        }

        private void delayTimeEdit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void delayTimeEdit_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(delayTimeEdit.Text, out var seconds))
            {
                invokeDelaySeconds = seconds;
                G.HotKeyManager.SetDelayTime(seconds);
            }
            else
            {
                delayTimeEdit.Focus();
                delayTimeEdit.SelectAll();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                notifyIcon.Text = Text;
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon.Tag = WindowState;
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Visible = true;
            try
            {
                WindowState = (FormWindowState)notifyIcon.Tag;
            }
            catch
            {
                WindowState = FormWindowState.Normal;
            }
            BringToFront();
            Activate();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!windowShown)
            {
                windowShown = true;
                if (startMinized) WindowState = FormWindowState.Minimized;
            }
        }

        private void hotKeyListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this.hotKeyListView.SelectedItems.Count > 0)
                    this.hotKeyListView.SelectedItems[0].BeginEdit();
                e.Handled = true;
            }
        }

        #endregion

    }
}
