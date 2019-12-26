namespace MyHotKeys
{
    partial class EditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nameEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.altCheck = new System.Windows.Forms.CheckBox();
            this.ctrlCheck = new System.Windows.Forms.CheckBox();
            this.shiftCheck = new System.Windows.Forms.CheckBox();
            this.keyDropdown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.macroEdit = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Name:";
            // 
            // nameEdit
            // 
            this.nameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameEdit.Location = new System.Drawing.Point(89, 14);
            this.nameEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameEdit.Name = "nameEdit";
            this.nameEdit.Size = new System.Drawing.Size(467, 27);
            this.nameEdit.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Keys:";
            // 
            // altCheck
            // 
            this.altCheck.AutoSize = true;
            this.altCheck.Location = new System.Drawing.Point(91, 53);
            this.altCheck.Name = "altCheck";
            this.altCheck.Size = new System.Drawing.Size(55, 24);
            this.altCheck.TabIndex = 3;
            this.altCheck.Text = "ALT";
            this.altCheck.UseVisualStyleBackColor = true;
            // 
            // ctrlCheck
            // 
            this.ctrlCheck.AutoSize = true;
            this.ctrlCheck.Location = new System.Drawing.Point(172, 53);
            this.ctrlCheck.Name = "ctrlCheck";
            this.ctrlCheck.Size = new System.Drawing.Size(64, 24);
            this.ctrlCheck.TabIndex = 4;
            this.ctrlCheck.Text = "CTRL";
            this.ctrlCheck.UseVisualStyleBackColor = true;
            // 
            // shiftCheck
            // 
            this.shiftCheck.AutoSize = true;
            this.shiftCheck.Location = new System.Drawing.Point(262, 53);
            this.shiftCheck.Name = "shiftCheck";
            this.shiftCheck.Size = new System.Drawing.Size(69, 24);
            this.shiftCheck.TabIndex = 5;
            this.shiftCheck.Text = "SHIFT";
            this.shiftCheck.UseVisualStyleBackColor = true;
            // 
            // keyDropdown
            // 
            this.keyDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyDropdown.FormattingEnabled = true;
            this.keyDropdown.Location = new System.Drawing.Point(352, 51);
            this.keyDropdown.Name = "keyDropdown";
            this.keyDropdown.Size = new System.Drawing.Size(206, 28);
            this.keyDropdown.Sorted = true;
            this.keyDropdown.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Macro:";
            // 
            // macroEdit
            // 
            this.macroEdit.AcceptsReturn = true;
            this.macroEdit.AcceptsTab = true;
            this.macroEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroEdit.Location = new System.Drawing.Point(89, 93);
            this.macroEdit.Multiline = true;
            this.macroEdit.Name = "macroEdit";
            this.macroEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.macroEdit.Size = new System.Drawing.Size(467, 263);
            this.macroEdit.TabIndex = 8;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(377, 368);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(84, 31);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "Save";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButon
            // 
            this.cancelButon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButon.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButon.Location = new System.Drawing.Point(472, 368);
            this.cancelButon.Name = "cancelButon";
            this.cancelButon.Size = new System.Drawing.Size(84, 31);
            this.cancelButon.TabIndex = 10;
            this.cancelButon.Text = "Cancel";
            this.cancelButon.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButon;
            this.ClientSize = new System.Drawing.Size(578, 411);
            this.Controls.Add(this.cancelButon);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.macroEdit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.keyDropdown);
            this.Controls.Add(this.shiftCheck);
            this.Controls.Add(this.ctrlCheck);
            this.Controls.Add(this.altCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameEdit);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hot Key Editor";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox altCheck;
        private System.Windows.Forms.CheckBox ctrlCheck;
        private System.Windows.Forms.CheckBox shiftCheck;
        private System.Windows.Forms.ComboBox keyDropdown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox macroEdit;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButon;
    }
}