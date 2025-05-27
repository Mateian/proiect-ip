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
    /// <summary>
    /// Clasa pentru crearea unui manager al bazei de date
    /// </summary>
    public class DatabaseManager
    {
        private string _connectionString;
        private SQLiteConnection _connection;

        /// <summary>
        /// Constructorul clasei pentru configurarea managerului de baze de date
        /// </summary>
        /// <param name="connectionString">String folosit pentru conectarea la baza de date.</param>
        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SQLiteConnection(_connectionString);
        }

        /// <summary>
        /// Metoda folosita pentru executarea unei comenzi de tipul SELECT.
        /// </summary>
        /// <param name="query">Query-ul care se va executa.</param>
        /// <returns>Returneaza readerul care contine rezultatul in urma interogarii.</returns>
        public SQLiteDataReader ExecuteSelectQuery(string query)
        {
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            var reader = command.ExecuteReader(); // Reader-ul trebui inchis de apelant

            return reader;
        }

        /// <summary>
        /// Metoda folosita pentru executarea unei comenzi SQL care nu returneaza nimic (DELETE FROM, INSERT etc).
        /// </summary>
        /// <param name="query">Query-ul care se va executa.</param>
        /// <param name="parameters">Parametrii stringului query identificati dupa perechi de tip string-object.</param>
        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            OpenConnection();

            var command = new SQLiteCommand(query, _connection);
            
            // Adauga parametrii la query
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            command.ExecuteNonQuery();

            CloseConnection(); // Inchide imediat conexiunea dupa executie
        }

        /// <summary>
        /// Metoda folosita pentru inchiderea conexiunii cu baza de date.
        /// </summary>
        private void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Metoda folosita pentru pornirea conexiunii cu baza de date.
        /// </summary>
        private void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// Metoda folosita pentru inserarea datelor unei fise medicale.
        /// </summary>
        /// <param name="data">Lista cu stringuri care contine datele de pe fisa medicala.</param>
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
                // Mapare simpla de parametri la coloane
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

        /// <summary>
        /// Metoda folosita pentru inserarea unui utilizator.
        /// </summary>
        /// <param name="data">Lista de stringuri cu datele utilizatorului dorit a fi introdus.</param>
        /// <returns></returns>
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
            return -1; // In caz ca nu a gasit utilizatorul
        }

        /// <summary>
        /// Metoda care verifica daca exista un utilizator.
        /// </summary>
        /// <param name="data">Lista de stringuri formata din username si parola.</param>
        /// <returns></returns>
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
                    return true; // daca exista user
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
