namespace MyHotKeys
{
    partial class SecretGeneratorForm
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
            this.keyLabel = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.IVLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.filePathLabel = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.IVTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(20, 66);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(36, 20);
            this.keyLabel.TabIndex = 0;
            this.keyLabel.Text = "Key:";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyTextBox.Location = new System.Drawing.Point(20, 89);
            this.keyTextBox.Multiline = true;
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.ReadOnly = true;
            this.keyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keyTextBox.Size = new System.Drawing.Size(467, 104);
            this.keyTextBox.TabIndex = 2;
            // 
            // IVLabel
            // 
            this.IVLabel.AutoSize = true;
            this.IVLabel.Location = new System.Drawing.Point(20, 196);
            this.IVLabel.Name = "IVLabel";
            this.IVLabel.Size = new System.Drawing.Size(25, 20);
            this.IVLabel.TabIndex = 0;
            this.IVLabel.Text = "IV:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(100, 339);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(195, 31);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Generate New  Secret";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.updateButton.Enabled = false;
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updateButton.Location = new System.Drawing.Point(301, 339);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(186, 31);
            this.updateButton.TabIndex = 0;
            this.updateButton.Text = "Update Database";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Location = new System.Drawing.Point(20, 13);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(102, 20);
            this.filePathLabel.TabIndex = 0;
            this.filePathLabel.Text = "Database File:";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTextBox.Location = new System.Drawing.Point(20, 36);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(467, 27);
            this.filePathTextBox.TabIndex = 1;
            // 
            // IVTextBox
            // 
            this.IVTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IVTextBox.Location = new System.Drawing.Point(20, 219);
            this.IVTextBox.Multiline = true;
            this.IVTextBox.Name = "IVTextBox";
            this.IVTextBox.ReadOnly = true;
            this.IVTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.IVTextBox.Size = new System.Drawing.Size(467, 104);
            this.IVTextBox.TabIndex = 3;
            // 
            // SecretGeneratorForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 382);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.filePathLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.IVTextBox);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.IVLabel);
            this.Controls.Add(this.keyLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecretGeneratorForm";
            this.Text = "Secret Generator";
            this.Load += new System.EventHandler(this.SecretGeneratorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label IVLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label filePathLabel;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.TextBox IVTextBox;
    }
}