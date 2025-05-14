using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model
{
    public class DatabaseManager
    {
        private string _connectionString;
        private SQLiteConnection _connection;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SQLiteConnection(_connectionString);
        }

        private SQLiteConnection GetConnection()
        {
            return _connection;
        }

        public SQLiteDataReader ExecuteSelectQuery(string query)
        {
            _connection.Open();

            var command = new SQLiteCommand(query, _connection);
            var reader = command.ExecuteReader();

            return reader;
        }

        public void ExecuteNonQuery(string query)
        {
            _connection.Open();

            var command = new SQLiteCommand(query, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
