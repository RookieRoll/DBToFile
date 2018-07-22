using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.Common;

namespace DBToFile.Service
{
    public abstract class BaseDataSerivce
    {
        private readonly string _constr;
        private readonly IDbConnection _connection;

        private IDataReader reader;
        private readonly IDbCommand _command;
        public BaseDataSerivce(string constr)
        {
            if (string.IsNullOrWhiteSpace(constr))
            {
                throw new Exception("Error constr");
            }
            _constr = constr;
            _connection = GetDBConnection(constr);
            _command = _connection.CreateCommand();
        }

        protected abstract IDbConnection GetDBConnection(string constr);
        public string GetDBName()
        {
            return _connection == null ? "" : _connection.Database;
        }
        private void Open()
        {
            if (_connection == null)
                throw new Exception("connection is err");
            _connection.Open();
        }

        public IDataReader ExecuteQuery(string sql)
        {
            Open();
            _command.CommandText = sql;
            _command.CommandType = CommandType.Text;

            reader = _command.ExecuteReader();
            return reader;
        }

        protected void Close()
        {
            if (_command != null)
                _command.Dispose();
            if (_connection != null)
                _connection.Dispose();


        }

    }
}
