using System;
using System.IO;

namespace MyHotKeys
{
    using MyHotKeys.Library;

    static class G
    {
        internal static readonly string DatabaseFileName = "MyHotKeys.db";
        internal static string DatabaseFilePath
        {
            get
            {
                var dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(dir, DatabaseFileName);
            }
        }
        internal static HotKeyManager HotKeyManager { get; private set; }

        static G()
        {
            HotKeyManager = new HotKeyManager(DatabaseFilePath, new Secret
            (
                new byte[] {
                    0xf3,0xae,0x2c,0x20,0xd0,0x5c,0x4f,0xd9,
                    0xec,0x8a,0xf3,0xf9,0xb3,0x9a,0x45,0xdb,
                    0x2e,0x7e,0x9b,0x9a,0x78,0x60,0x8c,0x6f,
                    0xf7,0x27,0x50,0x48,0xd6,0x95,0x5c,0x19
                },
                new byte[] {
                    0x28,0xd6,0x54,0x26,0x29,0x4c,0xdc,0x8e,
                    0xc7,0x2b,0x48,0x4b,0xf7,0xed,0x19,0x55
                }
            ));
        }
    }
}
