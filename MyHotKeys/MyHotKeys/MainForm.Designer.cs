namespace MyHotKeys
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.addButton = new System.Windows.Forms.Button();
            this.hotKeyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hotKeyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.invokeButton = new System.Windows.Forms.Button();
            this.delayTimeEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.settingsFrame = new System.Windows.Forms.GroupBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.settingsFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addButton.Location = new System.Drawing.Point(463, 12);
            this.addButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(86, 30);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // hotKeyListView
            // 
            this.hotKeyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.hotKeyColumnHeader});
            this.hotKeyListView.FullRowSelect = true;
            this.hotKeyListView.GridLines = true;
            this.hotKeyListView.HideSelection = false;
            this.hotKeyListView.LabelEdit = true;
            this.hotKeyListView.Location = new System.Drawing.Point(12, 12);
            this.hotKeyListView.Name = "hotKeyListView";
            this.hotKeyListView.Size = new System.Drawing.Size(443, 298);
            this.hotKeyListView.TabIndex = 1;
            this.hotKeyListView.UseCompatibleStateImageBehavior = false;
            this.hotKeyListView.View = System.Windows.Forms.View.Details;
            this.hotKeyListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.hotKeyListView_AfterLabelEdit);
            this.hotKeyListView.SelectedIndexChanged += new System.EventHandler(this.hotKeyListView_SelectedIndexChanged);
            this.hotKeyListView.DoubleClick += new System.EventHandler(this.hotKeyListView_DoubleClick);
            this.hotKeyListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotKeyListView_KeyDown);
            this.hotKeyListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hotKeyListView_KeyPress);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 222;
            // 
            // hotKeyColumnHeader
            // 
            this.hotKeyColumnHeader.Text = "Hot Key";
            this.hotKeyColumnHeader.Width = 200;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Enabled = false;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.editButton.Location = new System.Drawing.Point(463, 50);
            this.editButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(86, 30);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "&Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Enabled = false;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deleteButton.Location = new System.Drawing.Point(463, 88);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(86, 30);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "&Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // invokeButton
            // 
            this.invokeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.invokeButton.Enabled = false;
            this.invokeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.invokeButton.Location = new System.Drawing.Point(463, 126);
            this.invokeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.invokeButton.Name = "invokeButton";
            this.invokeButton.Size = new System.Drawing.Size(86, 30);
            this.invokeButton.TabIndex = 5;
            this.invokeButton.Text = "&Invoke";
            this.invokeButton.UseVisualStyleBackColor = true;
            this.invokeButton.Click += new System.EventHandler(this.invokeButton_Click);
            // 
            // delayTimeEdit
            // 
            this.delayTimeEdit.Location = new System.Drawing.Point(290, 38);
            this.delayTimeEdit.Name = "delayTimeEdit";
            this.delayTimeEdit.Size = new System.Drawing.Size(100, 27);
            this.delayTimeEdit.TabIndex = 7;
            this.delayTimeEdit.Validating += new System.ComponentModel.CancelEventHandler(this.delayTimeEdit_Validating);
            this.delayTimeEdit.Validated += new System.EventHandler(this.delayTimeEdit_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Direct invoke delay (milliseconds):";
            // 
            // settingsFrame
            // 
            this.settingsFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsFrame.Controls.Add(this.label1);
            this.settingsFrame.Controls.Add(this.delayTimeEdit);
            this.settingsFrame.Location = new System.Drawing.Point(12, 329);
            this.settingsFrame.Name = "settingsFrame";
            this.settingsFrame.Size = new System.Drawing.Size(443, 96);
            this.settingsFrame.TabIndex = 8;
            this.settingsFrame.TabStop = false;
            this.settingsFrame.Text = "Settings";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 437);
            this.Controls.Add(this.settingsFrame);
            this.Controls.Add(this.hotKeyListView);
            this.Controls.Add(this.invokeButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Hot Keys";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.settingsFrame.ResumeLayout(false);
            this.settingsFrame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView hotKeyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader hotKeyColumnHeader;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button invokeButton;
        private System.Windows.Forms.TextBox delayTimeEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox settingsFrame;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

