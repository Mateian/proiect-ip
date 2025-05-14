using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model.DatabaseManager
{
    public class DatabaseManager
    {
        private string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public SQLiteDataReader ExecuteSelectQuery(string query)
        {
            var connection = GetConnection();
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            return command.ExecuteReader();
        }
    }
}
