using Dapper;
using DBToFile.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.Service
{
    public class MySqlDBService
    {
        private readonly string _conStr;
        private string _folderName;
        public MySqlDBService(string constr)
        {
            _conStr = constr;
        }

        public void HanderDbToFile()
        {
            var dt = GetDataTable();
            var classDt = ConvertToCode(dt);

            var partitioner = Partitioner.Create(0, classDt.Keys.Count);
            Parallel.ForEach(partitioner, item =>
            {
                for (var i = item.Item1; i < item.Item2; i++)
                {
                    var key = classDt.Keys.ElementAt(i);
                    WriteFile(key, classDt[key]);
                }
            });

        }

        private Dictionary<string, List<DBFiled>> ConvertToCode(Dictionary<string, List<DBFiled>> dbTable)
        {
            Dictionary<string, List<DBFiled>> classTable = new Dictionary<string, List<DBFiled>>();
            foreach (var item in dbTable.Keys)
            {
                var propies = new List<DBFiled>();
                if (!dbTable.TryGetValue(item, out propies))
                    continue;
                propies.ForEach(m =>
                {
                    m.Type = GetType(m.Type);
                    m.Name = GetName(m.Name);
                });
                var className = GetName(item);
                classTable.Add(className, propies);
            }
            return classTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propies"></param>
        private void WriteFile(string name, List<DBFiled> propies)
        {
            var content = new StringBuilder();
            content.Append("public class " + name + " { \n ");

            propies.ForEach(m =>
            {
                var comment = "\t/// <summary>\n\t///" + m.Comment + "\n\t/// </summary>\n";
                content.Append(comment);
                content.Append("\tpublic " + m.Type + " " + m.Name + "{get;set;}\n");
            });

            content.Append("}");

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + _folderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var filepath = string.Format("{1}/{0}.cs", name, path);
            File.WriteAllText(filepath, content.ToString());
        }
        private string GetName(string name)
        {
            var words = name.Trim().Split('_');
            string newName = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                newName += words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
            }
            return newName;
        }
        private string GetType(string type)
        {
            switch (type)
            {
                case "bool":
                case "boolean":
                case "bit": return "bool";
                case "tinyint": return "sbyte";
                case "smallint":
                case "year":
                case "integer":
                case "mediumint":
                case "int": return "int";
                case "bigint": return "ulong";
                case "float": return "float";
                case "double":
                case "real": return "double";
                case "decimal":
                case "numeric":
                case "dec":
                case "fixed":
                case "serial": return "decimal";
                case "time":
                case "date":
                case "timestamp":
                case "datetime":
                case "datetimeoffset": return "DateTime";
                case "char":
                case "varchar":
                case "tinytext":
                case "text":
                case "mediumtext":
                case "nchar":
                case "longtext":
                case "set":
                case "enum":
                case "nvarchar": return "string";
                case "binary":
                case "varbinary":
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob": return "byte[]";
                default: return string.Empty;
            }
        }
        private Dictionary<string, List<DBFiled>> GetDataTable()
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
        private List<string> GetTableName()
        {
            var connect = BaseDataConnect.GetMySqlDbConnection(_conStr);
            var dbName = connect.Database;
            _folderName = connect.Database;
            var sql = string.Format("select Table_name  as classname from information_schema.tables where table_schema='{0}'", dbName);
            return connect.Query<string>(sql).ToList();
        }
        private List<DBFiled> GetPropies(string table)
        {
            var connect = BaseDataConnect.GetMySqlDbConnection(_conStr);
            var dbName = connect.Database;
            var sql = string.Format("select COLUMN_NAME as Name, DATA_TYPE as Type,COLUMN_COMMENT as Comment from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}'", table, dbName);
            return connect.Query<DBFiled>(sql).ToList();
        }
    }
}
