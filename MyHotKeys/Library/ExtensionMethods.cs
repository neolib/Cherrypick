using System.Text;

namespace MyHotKeys.Library
{
    public static class ExtensionMethods
    {
        public static string ToHexString(this byte[] self,
            int startIndex = 0, int length = 0)
        {
            if (self?.Length == 0) return null;

            if (length == 0) length = self.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < startIndex + length; i++)
            {
                sb.AppendFormat("{0:x2}", self[i]);
            }
            return sb.ToString();
        }

        public static string ToCStyleHexString(this byte[] self, 
            int startIndex = 0, int length = 0, int nthBreak = 0)
        {
            if (self?.Length == 0) return null;

            if (length == 0) length = self.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < startIndex + length; i++)
            {
                sb.AppendFormat("0x{0:x2},", self[i]);
                if (nthBreak > 0 && ((i + 1) % nthBreak) == 0)
                {
                    sb.AppendLine();
                }
            }

            int count = 0;
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                var c = sb[i];
                if (c == 0x0a || c == 0x0d || c == ',') count++;
                else break;
            }
            if (count > 0) sb.Length -= count;
            return sb.ToString();
        }

        public static bool IsSameTextAs(this string self, string other)
        {
            return string.Compare(self, other) == 0;
        }
    }
}
