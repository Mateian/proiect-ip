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
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            var reader = command.ExecuteReader();

            return reader;
        }

        public void ExecuteNonQuery(string query)
        {
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            command.ExecuteNonQuery();

            CloseConnection();
        }
        private void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        private void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public void InsertCommand(List<string> data)
        {
            OpenConnection();

            var query = @"INSERT INTO FisaMedicalaDB (
            NUMEPACIENT, DATANASTERII, ADRESA, DATACONSULT, CNP, TELEFON, SEX, NUMEMEDIC,
            EXAMENCLINIC, DIAGNOSTICPREZUMTIV, RECOMANDARI, INVESTIGATIIRECOMANDATE,
            TRATAMENTPRESCRIS, MOTIV) 
            VALUES (
            @NUMEPACIENT, @DATANASTERII, @ADRESA, @DATACONSULT, @CNP, @TELEFON, @SEX, @NUMEMEDIC,
            @EXAMENCLINIC, @DIAGNOSTICPREZUMTIV, @RECOMANDARI, @INVESTIGATIIRECOMANDATE,
            @TRATAMENTPRESCRIS, @MOTIV
            )";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@NUMEPACIENT", data[0]);
                command.Parameters.AddWithValue("@DATANASTERII", data[2]);
                command.Parameters.AddWithValue("@ADRESA", data[4]);
                command.Parameters.AddWithValue("@DATACONSULT", data[6]);
                command.Parameters.AddWithValue("@CNP", data[1]);
                command.Parameters.AddWithValue("@TELEFON", data[3]);
                command.Parameters.AddWithValue("@SEX", data[5]);
                command.Parameters.AddWithValue("@NUMEMEDIC", data[7]);
                command.Parameters.AddWithValue("@EXAMENCLINIC", data[8]);
                command.Parameters.AddWithValue("@DIAGNOSTICPREZUMTIV", data[10]);
                command.Parameters.AddWithValue("@RECOMANDARI", data[12]);
                command.Parameters.AddWithValue("@INVESTIGATIIRECOMANDATE", data[9]);
                command.Parameters.AddWithValue("@TRATAMENTPRESCRIS", data[11]);
                command.Parameters.AddWithValue("@MOTIV", data[13]);

                command.ExecuteNonQuery();
            }
        }
    }
}
