/**************************************************************************
 *                                                                        *
 *  File:        DatabaseManager.cs                                       *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Gestioneaza conexiunile si comenzile trimise catre       *
 *               baza de date.                                            *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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

        public SQLiteDataReader ExecuteSelectQuery(string query)
        {
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            var reader = command.ExecuteReader();

            return reader;
        }

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

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
        public int InsertUserCommand(List<string> data)
        {
            OpenConnection();
            var query = @"INSERT INTO Users (
            Role, Username, HashPassword, Nume, Prenume ) 
            VALUES (
            @Role, @Username, @Password, @Nume, @Prenume
            )";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Role", "Pacient");
                command.Parameters.AddWithValue("@Username", data[0]);
                command.Parameters.AddWithValue("@Password", data[1]);
                command.Parameters.AddWithValue("@Nume", data[2]);
                command.Parameters.AddWithValue("@Prenume", data[3]);

                command.ExecuteNonQuery();
            }
            query = @"SELECT ID FROM Users WHERE Username = @Username AND HashPassword = @Password";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Username", data[0]);
                command.Parameters.AddWithValue("@Password", data[1]);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    return id;
                }
            }
            return -1;
        }
        public bool CheckUserExists(List<string> data)
        {
            OpenConnection();
            var query = @"SELECT COUNT(*) FROM Users WHERE Username = @Username AND HashPassword = @Password";
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Username", data[0]);
                command.Parameters.AddWithValue("@Password", data[1]);
                var count = (long)command.ExecuteScalar();
                if(count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
