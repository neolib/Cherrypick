using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyHotKeys
{
    public partial class EditForm : Form
    {
        internal HotKeyItem HotKey { get; set; }
        internal bool IsNewMode { get; set; }

        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            if (!this.IsNewMode)
            {
                this.altCheck.Focus();
                this.nameEdit.ReadOnly = true;
            }

            var keysToExclude = new[] {
                Keys.ShiftKey, Keys.LShiftKey,
                Keys.ControlKey, Keys.LControlKey, Keys.RControlKey,
                Keys.Menu, Keys.Snapshot, Keys.Apps, Keys.FinalMode,
                Keys.Help, Keys.Select, Keys.Separator,
                Keys.CapsLock, Keys.Clear, Keys.Execute, Keys.Sleep
            };
            foreach (int value in Enum.GetValues(typeof(Keys)))
            {
                if (value < (int)Keys.Back || value > (int)Keys.F24) continue;
                // IME mode
                if (value >= 21 && value <= 31 && value != (int)Keys.Escape) continue;
                if (keysToExclude.Contains((Keys)value)) continue;

                var name = Enum.GetName(typeof(Keys), value);
                if (this.keyDropdown.Items.Contains(name)) continue;
                this.keyDropdown.Items.Add(name);
            }

            if (this.IsNewMode) this.HotKey = new HotKeyItem();
            else
            {
                this.nameEdit.Text = this.HotKey.Name;
                this.macroEdit.Text = this.HotKey.Macro;
                var key = this.HotKey.KeyStrokes;
                if (key.HasFlag(Keys.Alt)) this.altCheck.Checked = true;
                if (key.HasFlag(Keys.Control)) this.ctrlCheck.Checked = true;
                if (key.HasFlag(Keys.Shift)) this.shiftCheck.Checked = true;
                this.keyDropdown.SelectedItem = Enum.GetName(typeof(Keys), key & Keys.KeyCode);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //
            // Validate inputs
            //

            if (this.IsNewMode)
            {
                // name
                var name = this.nameEdit.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    this.nameEdit.Focus();
                    return;
                }

                if (G.HotKeyManager.HasName(name))
                {
                    Program.AlertBox(this, "Duplicate name found.");
                    this.nameEdit.Focus();
                    this.nameEdit.SelectAll();
                    return;
                }

                this.HotKey.Name = name;
            }

            // keys
            var keyCodeText = this.keyDropdown.Text;
            if (string.IsNullOrEmpty(keyCodeText))
            {
                this.keyDropdown.Focus();
                return;
            }

            if (!Enum.TryParse(keyCodeText, out Keys keyCode))
            {
                this.keyDropdown.Focus();
                return;
            }
            
            var keyStrokes = Keys.None;
            if (altCheck.Checked) keyStrokes |= Keys.Alt;
            if (ctrlCheck.Checked) keyStrokes |= Keys.Control;
            if (shiftCheck.Checked) keyStrokes |= Keys.Shift;
            keyStrokes |= (Keys)Enum.Parse(typeof(Keys), keyCodeText);

            //  macro
            var macro = this.macroEdit.Text;
            if (string.IsNullOrEmpty(macro))
            {
                this.macroEdit.Focus();
                return;
            }
            
            this.HotKey.KeyText = keyStrokes.ToText();
            this.HotKey.KeyStrokes = keyStrokes;
            this.HotKey.Macro = macro;
            try
            {
                if (this.IsNewMode)
                    G.HotKeyManager.Add(this.HotKey);
                else
                    G.HotKeyManager.Update(this.HotKey);
            }
            catch (Exception ex)
            {
                Program.ErrorBox(this, ex);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
