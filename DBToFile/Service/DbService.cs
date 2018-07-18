using Dapper;
using DBToFile.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.Service
{
    public class DbService
    {
        private Dictionary<string, List<DBFiled>> DbTable = new Dictionary<string, List<DBFiled>>();

        public Dictionary<string, List<DBFiled>> ConvertToCode(Dictionary<string, List<DBFiled>> dbTable)
        {
            foreach (var item in dbTable.Keys)
            {
                var propies = new List<DBFiled>();
                if (!dbTable.TryGetValue(item, out propies))
                    continue;

            }
            return dbTable;
        }

        public string GetType(string type)
        {
            switch (type)
            {
                case "int": return "int";
                case "char":
                case "text":
                case "varchar": return "string";
                default: return string.Empty;
            }
        }
        public Dictionary<string, List<DBFiled>> GetDataTable()
        {
            Dictionary<string, List<DBFiled>> dic = new Dictionary<string, List<DBFiled>>();
            var tables = GetTableName();
            tables.ForEach(m =>
            {
                var propies = GetPropies(m);
                dic.Add(m, propies);
            });
            return dic;
        }
        public List<string> GetTableName()
        {
            var connect = BaseDataConnect.GetMySqlDbConnection();
            var dbName = connect.Database;
            var sql = string.Format("select Table_name  as classname from information_schema.tables where table_schema='{0}'", dbName);
            return connect.Query<string>(sql).ToList();
        }
        public List<DBFiled> GetPropies(string table)
        {
            var connect = BaseDataConnect.GetMySqlDbConnection();
            var dbName = connect.Database;
            var sql = string.Format("select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}'", table, dbName);
            return connect.Query<DBFiled>(sql).ToList();
        }
    }
}
