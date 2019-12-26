using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;


namespace MyHotKeys.Library
{
    /// <summary>
    /// A shorthand class to avoid long template name.
    /// </summary>
    public class HotKeyMap: Dictionary<string, HotKeyEntity>
    {
    }

    public class HotKeyManager
    {
        #region Private Variables

        SQLiteConnection conn;
        Secret secret;

        #endregion

        #region Public Properties

        public string FilePath { get; private set; }
        
        #endregion

        #region Schema Creation

        public string GetVersion()
        {
            if (conn.TableExists("settings"))
            {
                return conn.ExecuteScalar("select v from settings where k = 'schema_version'") as string;
            }
            return null;
        }

        private void SetupTables()
        {
            var version = GetVersion();
            if (version == null)
            {
                conn.Execute("create table settings(k text primary key, v, c default CURRENT_TIMESTAMP)");
                conn.Execute("create table hotkeys(name text primary key, keys text, macro, c default CURRENT_TIMESTAMP)");
                conn.Execute("insert into settings(k, v) values('schema_version', '1.0')");
            }
        }

        #endregion

        #region Constructors

        public HotKeyManager(string filepath, Secret secret)
        {
            Debug.WriteLine($"SQLite runtime version is {SQLiteConnection.SQLiteVersion}");

            this.FilePath = filepath;
            this.secret = secret;
            conn = new SQLiteConnection(string.Format("data source={0}", filepath));
            conn.Open();
            SetupTables();
        }

        #endregion

        #region Operations

        private HotKeyEntity Read(SQLiteDataReader r)
        {
            return new HotKeyEntity
            {
                Name = r["name"] as string,
                KeyText = r["keys"] as string,
                Macro = secret.Reveal(r["macro"] as byte[])
            };
        }

        public HotKeyMap Load()
        {
            using (var r = conn.ExecuteReader("select * from hotkeys"))
            {
                var map = new HotKeyMap();
                while (r.Read())
                {
                    var hotkey = Read(r);
                    map[hotkey.Name] = hotkey;
                }
                return map;
            }
        }

        public HotKeyEntity Get(string name)
        {
            using (var r = conn.ExecuteReader(
                "select * from hotkeys where name = @name",
                new Dictionary<string, object> { { "@name", name } }))
            {
                if (r.Read()) return Read(r);
            }
            return null;
        }

        public bool Add(HotKeyEntity hotKey)
        {
            var sql = "insert into hotkeys(name, keys, macro) values(@name, @keys, @macro)";
            var bag = new Dictionary<string, object>
            {
                {"@name", hotKey.Name },
                {"@keys", hotKey.KeyText },
                {"@macro", secret.Cloak(hotKey.Macro) }
            };
            return conn.Execute(sql, bag) == 1;
        }

        public bool Update(HotKeyEntity hotKey)
        {
            var sql = "update hotkeys set keys = @keys, macro = @macro where name = @name";
            var bag = new Dictionary<string, object>
            {
                {"@name", hotKey.Name },
                {"@keys", hotKey.KeyText },
                {"@macro", secret.Cloak(hotKey.Macro) }
            };
            return conn.Execute(sql, bag) == 1;
        }

        public bool UpdateName(string name, string newName)
        {
            var sql = "update hotkeys set name = @new_name where name = @name";
            var bag = new Dictionary<string, object>
            {
                {"@name", name },
                {"@new_name", newName },
            };
            return conn.Execute(sql, bag) == 1;
        }

        public bool Delete(string name)
        {
            return conn.Execute("delete from hotkeys where name = @name",
                new Dictionary<string, object> { { "@name", name } }) == 1;
        }

        public bool HasName(string name)
        {
            return (long)conn.ExecuteScalar(
                "select count(*) from hotkeys where name = @name",
                new Dictionary<string, object> { { "@name", name } }) == 1;
        }

        #endregion

        #region Settings

        public int GetDelayTime()
        {
            return Convert.ToInt32(conn.ExecuteScalar("select v from settings where k = 'delay_time'"));
        }

        public bool SetDelayTime(int time)
        {
            var sql= "insert or replace into settings(k, v) values('delay_time', @time)";
            return conn.Execute(sql,
                new Dictionary<string, object> { { "@time", time } }) == 1;
        }

        #endregion
    }
}
