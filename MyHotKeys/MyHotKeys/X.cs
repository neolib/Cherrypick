using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyHotKeys
{
    public static class X
    {
        public static string ToText(this Keys key)
        {
            var sb = new StringBuilder();
            if (key.HasFlag(Keys.Alt)) sb.Append("ALT+");
            if (key.HasFlag(Keys.Control)) sb.Append("CTRL+");
            if (key.HasFlag(Keys.Shift)) sb.Append("SHIFT+");
            var keyCode = key & Keys.KeyCode;
            sb.Append(Enum.GetName(typeof(Keys), keyCode));
            return sb.ToString();
        }

        public static Keys KeyFromText(string text)
        {
            var key = Keys.None;
            var a = text.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length > 0)
            {
                for (int i = 0; i < a.Length - 1; i++)
                {
                    if (a[i] == "ALT") key |= Keys.Alt;
                    if (a[i] == "CTRL") key |= Keys.Control;
                    if (a[i] == "SHIFT") key |= Keys.Shift;
                }
                if (Enum.TryParse(a[a.Length - 1], out Keys keyCode))
                {
                    key |= keyCode;
                }
            }
            return key;
        }
    }
}
