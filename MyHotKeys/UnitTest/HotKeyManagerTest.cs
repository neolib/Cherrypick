using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace UnitTest
{
    using MyHotKeys.Library;
    using static System.Console;

    [TestClass]
    public class HotKeyManagerTest
    {
        static Secret secret = new Secret(
            new byte[] {
                0xbe,0xf8,0xa2,0xb5,0x26,0x70,0xe3,0x16,
                0xf1,0xb2,0xe8,0xc0,0x05,0x2a,0xaf,0xca },
            new byte[] {
                0x13,0xdf,0x62,0x7f,0x50,0x0e,0x7b,0x9c,
                0x42,0x77,0xe5,0x07,0x9d,0x49,0xc1,0xc5 }
            );
        HotKeyManager hotKeyManager = new HotKeyManager(":memory:", secret);

        [TestMethod]
        public void TestSetDelayTime()
        {
            Assert.IsTrue(hotKeyManager.SetDelayTime(1234));
            Assert.AreEqual(hotKeyManager.GetDelayTime(), 1234);
        }

        [TestMethod]
        public void TestHotKeyCRUD()
        {
            var m = new HotKeyManager(":memory:", secret);
            Assert.IsTrue(hotKeyManager.Add(new HotKeyEntity { Name = "item1", KeyText = "CTRL+A", Macro = "item1" }));
            Assert.IsTrue(hotKeyManager.Add(new HotKeyEntity { Name = "item2", KeyText = "CTRL+F1", Macro = "item2" }));
            var items = hotKeyManager.Load();
            Assert.AreEqual(items.Count, 2);
            Assert.IsTrue(items.ContainsKey("item1"));
            Assert.IsTrue(items.ContainsKey("item2"));

            hotKeyManager.UpdateName("item1", "item1_new");
            Assert.IsNotNull(hotKeyManager.Get("item1_new"));

            Assert.IsFalse(hotKeyManager.Delete("item1"));
            Assert.IsTrue(hotKeyManager.Delete("item2"));
            Assert.IsNull(hotKeyManager.Get("item2"));

            Assert.IsTrue(hotKeyManager.Update(new HotKeyEntity { Name = "item1_new", KeyText = "SHIFT+F2", Macro = "new item1" }));
            var item = hotKeyManager.Get("item1_new");
            Assert.IsNotNull(item);
            Assert.AreEqual(item.KeyText, "SHIFT+F2");
            Assert.AreEqual(item.Macro, "new item1");
        }

        [TestMethod]
        public void TestTypeAffinity()
        {
            using (var conn = new SQLiteConnection("data source=:memory:"))
            {
                conn.Open();

                conn.Execute("create table t(k text primary key, v)");
                conn.Execute("insert into t(k, v) values('aaa', 1234)");
                conn.Execute("insert into t(k, v) values(1234, 'abcd')");
                conn.Execute("insert into t(k, v) values('blob', @bin)",
                    new Dictionary<string, object>
                    {
                        { "@bin", new byte[] { 1, 2, 3, 4, 5 } }
                    });

                using (var r = conn.ExecuteReader("select * from t"))
                {
                    while (r.Read())
                    {
                        for (int i = 0; i < r.FieldCount; i++)
                        {
                            WriteLine("{0}={1}", r.GetName(i), r[i]);
                            WriteLine("affinity: {0}, type name: {1}, type: {2}", 
                                 r.GetFieldAffinity(i), r.GetDataTypeName(i), r.GetFieldType(i));
                        }
                        WriteLine();
                    }
                }
            }
        }

        [TestMethod]
        public void TestSecret()
        {
            var cleartext = new string[] {
                "",
                "a",
                "abc",
                "abcd",
                "abcd1234",
                "abcd1234567",
                "my secret to be kept unnoticed",
                "my secret to be kept unnoticed..",
                "my secret to be kept unnoticed...",
            };
            foreach (var s in cleartext)
            {
                WriteLine("Cleartext: {0}", s);
                var secretData = secret.Cloak(s);
                WriteLine("Sizes: {0} -> {1}", s.Length, secretData.Length);
                var unreleavedText = secret.Reveal(secretData);
                Assert.AreEqual(s, unreleavedText);
                WriteLine();
            }
        }

        [TestMethod]
        public void TestByteArrayString()
        {
            var bytes = new byte[33];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var hexString = bytes.ToHexString();
            var cHexString = bytes.ToCStyleHexString();
            var cHexString8 = bytes.ToCStyleHexString(default, default, 8);
            WriteLine(hexString);
            WriteLine(cHexString);
            WriteLine(cHexString8);

            Assert.AreEqual(hexString, cHexString.Replace("0x", null).Replace(",", null));
            Assert.AreEqual(hexString, cHexString8.Replace("0x", null).Replace(",", null).Replace("\r\n", null));
        }
    }
}
