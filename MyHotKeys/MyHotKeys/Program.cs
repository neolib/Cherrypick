using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyHotKeys
{
    using Library;

    static class Program
    {
        static Mutex singletonGuard;

        enum ExitCode
        {
            Unknown = -100,
            Ok = 0,
            AlreadyRunning,
        }

        [STAThread]
        static void Main(string[] args)
        {
            Environment.ExitCode = (int)ExitCode.Unknown;
            singletonGuard = new Mutex(true, "MyHotKeys_neolib_net", out var createdNew);
            if (!createdNew)
            {
                var hwnd = Z.FindWindow(null, "My Hot Keys");
                if (hwnd != IntPtr.Zero) Z.ActivateWindow(hwnd);
                Environment.ExitCode = (int)ExitCode.AlreadyRunning;
                return;
            }

            bool startMinimized = false;
            bool startSecretForm = false;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    var s = arg.Substring(1);
                    if (s.IsSameTextAs("min"))
                    {
                        startMinimized = true;
                    }
                    else if (s.IsSameTextAs("secret"))
                    {
                        startSecretForm = true;
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            Form startupForm;

            if (startSecretForm)
                startupForm = new SecretGeneratorForm();
            else
                startupForm = new MainForm { startMinized = startMinimized };

            Application.Run(startupForm);
            Environment.ExitCode = 0;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorBox(Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null, e.Exception);
        }

        #region MessageBox helpers

        public static string ExceptionToString(Exception ex)
        {
            if (ex == null) return null;

            string seperator = "=====================================" + Environment.NewLine;
            var sb = new StringBuilder();
            for (; ex != null; ex = ex.InnerException)
            {
                sb.Append(ex.GetType().FullName + ":");
                sb.Append(Environment.NewLine);
                sb.Append(ex.Message);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(ex.Source);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(ex.StackTrace);
                sb.Append(Environment.NewLine);
                sb.Append(seperator);
            }
            if (sb.Length > seperator.Length)
            {
                sb.Remove(sb.Length - seperator.Length, seperator.Length);
            }
            return sb.ToString();
        }

        public static void AlertBox(Form form, string text)
        {
            MessageBox.Show(form, text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void InfoBox(Form form, string text)
        {
            InfoBox(form, text, "Information");
        }

        public static void InfoBox(Form form, string text, string caption)
        {
            MessageBox.Show(form, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ErrorBox(Form form, string text, string caption)
        {
            MessageBox.Show(form, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ErrorBox(Form form, Exception ex)
        {
            ErrorBox(form, ExceptionToString(ex), "Exception");
        }

        public static bool ConfirmBox(Form form, string text, string caption)
        {
            return DialogResult.OK == MessageBox.Show(form, text, caption, 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        #endregion
    }
}
