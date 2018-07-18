using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace DBToFile.Service
{
   public  class TestSetvice
    {
        public List<string> test()
        {
            string constr = "server=47.106.100.79;User Id=root;password=qh18723361304;Database=EmsDb";
            using (IDbConnection connection = new MySqlConnection(constr))
            {
                var result = connection.Query<string>("select Table_name  as classname from information_schema.tables where table_schema='EmsDb'");
                return result.ToList();
            }
        }

        public List<string> testss()
        {
            string constr = "server=47.106.100.79;User Id=root;password=qh18723361304;Database=EmsDb";
            using (IDbConnection connection = new MySqlConnection(constr))
            {
                var result = connection.Query<string>("select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where table_name = 'Area' and table_schema = 'EmsDb'");
                return result.ToList();
            }
        }
    }
}
