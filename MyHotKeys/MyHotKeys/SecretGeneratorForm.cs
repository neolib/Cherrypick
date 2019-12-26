using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MyHotKeys
{
    using Library;
    using System.Drawing;

    public partial class SecretGeneratorForm : Form
    {
        public SecretGeneratorForm()
        {
            InitializeComponent();
        }

        private void Generate()
        {
            var bytes = new byte[33];
            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(bytes, 0, 32);
            this.keyTextBox.Text = bytes.ToCStyleHexString(0, 32, 8);

            rng.GetBytes(bytes, 0, 16);
            this.IVTextBox.Text = bytes.ToCStyleHexString(0, 16, 8);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

        }

        private void SecretGeneratorForm_Load(object sender, EventArgs e)
        {
            this.keyTextBox.Font = new Font(FontFamily.GenericMonospace, this.Font.Size);
            this.IVTextBox.Font = this.keyTextBox.Font;
            this.filePathTextBox.Text = G.DatabaseFilePath;
            Generate();
        }
    }
}
