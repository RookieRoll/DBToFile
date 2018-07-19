using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DBToFile.Service
{
    public class BaseDataConnect
    {
        public static IDbConnection GetMySqlDbConnection(string connect)
        {
            return new MySqlConnection(connect);
        }
        //public static IDbConnection GetMySqlDbConnection()
        //{
        //    return new MySqlConnection(ConStr);
        //}

        //public static IDbConnection GetSqlServerDBConnection()
        //{
        //    return new SqlConnection(ConStr);
        //}
    }
}
