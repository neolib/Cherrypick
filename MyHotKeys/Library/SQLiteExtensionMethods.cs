using System.Collections.Generic;
using System.Data.SQLite;


namespace MyHotKeys.Library
{
    public static class SQLiteExtensionMethods
    {
        public static SQLiteCommand CreateCommand(this SQLiteConnection self, 
            string sql, Dictionary<string, object> paramBag = null)
        {
            var cmd = self.CreateCommand();
            cmd.CommandText = sql;
            if (paramBag != null)
            {
                foreach (var entry in paramBag)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
            }
            return cmd;

        }

        public static bool TableExists(this SQLiteConnection self, string tableName)
        {
            var sql = "select count(*) from sqlite_master where type = 'table' and name = @name";
            using (var cmd = self.CreateCommand(sql))
            {
                cmd.Parameters.AddWithValue("@name", tableName);
                return (long)cmd.ExecuteScalar() == 1;
            }
        }

        public static int Execute(this SQLiteConnection self,
            string sql, Dictionary<string, object> paramBag = null)
        {
            using (var cmd = self.CreateCommand(sql, paramBag))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(this SQLiteConnection self, 
            string sql, Dictionary<string, object> paramBag = null)
        {
            using (var cmd = self.CreateCommand(sql, paramBag))
            {
                return cmd.ExecuteScalar();
            }
        }

        public static SQLiteDataReader ExecuteReader(this SQLiteConnection self, 
            string sql, Dictionary<string, object> paramBag = null)
        {
            using (var cmd = self.CreateCommand(sql, paramBag))
            {
                return cmd.ExecuteReader();
            }
        }
    }
}
