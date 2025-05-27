/**************************************************************************
 *                                                                        *
 *  File:        MasterExceptionHandler.cs                                *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Gestioneaza toate exceptiile din program.                *
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.Model.Exceptions
{
    /// <summary>
    /// Clasa MasterExceptionHandler gestioneaza diferite tipuri de exceptii
    /// aparute in aplicatie, logheaza mesajele si notifica utilizatorul.
    /// </summary>
    public class MasterExceptionHandler : Exception
    {
        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public MasterExceptionHandler() { }

        /// <summary>
        /// Constructor care gestioneaza exceptiile pe baza codului de eroare.
        /// </summary>
        /// <param name="message">Mesajul de afisat al utilizatorului.</param>
        /// <param name="err_code">Codul numeric al erorii.</param>
        /// <param name="inner">Exceptia interioara (detaliu tehnic).</param>
        public MasterExceptionHandler(string message, int err_code, Exception inner) 
        {

            switch (err_code)
            {
                case 100:
                case 101:
                case 102:
                    // Exceptii de tip SQL
                    SQLExceptionHandle(message, inner);
                    break;
                case 200:
                case 201:
                    // Obiect nul sau referinta inexistenta
                    ObjectNullHandle(message, inner);
                    break;
                case 300:
                    // Probleme cu ID-ul utilizatorului
                    UserIDHandle(message, inner);
                    break;
                case 400:
                case 401:
                case 402:
                    // Alte exceptii legate de utilizator
                    UserRelatedException(message, inner);
                    break;
            }
        }

        /// <summary>
        /// Gestioneaza exceptiile legate de ID-ul utilizatorului.
        /// Afiseaza un mesaj si salveaza eroarea intr-un fisier log.
        /// </summary>
        /// <param name="message">Mesajul dorit a fi transmis.</param>
        /// <param name="inner">Exceptia interioara.</param>
        private void UserIDHandle(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                writer.WriteLine(inner.Message);
            }
        }

        /// <summary>
        /// Gestioneaza exceptiile cauzate de obiecte nule (null).
        /// </summary>
        /// <param name="message">Mesajul de afisat in fereastra de alerta.</param>
        /// <param name="inner">Exceptia interioara, daca este disponibila.</param>
        private void ObjectNullHandle(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                if (inner != null)
                    writer.WriteLine(inner.Message);
                else
                    writer.WriteLine(message);
            }
        }

        /// <summary>
        /// Gestioneaza exceptiile de tip SQL (ex. erori de conexiune sau interogare).
        /// </summary>
        /// <param name="message">Mesajul de afisat in fereastra de alerta.</param>
        /// <param name="inner">Exceptia interioara care contine detalii despre eroarea SQL.</param>
        private void SQLExceptionHandle(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                writer.WriteLine(inner.Message);
            }
        }

        /// <summary>
        /// Gestioneaza exceptiile legate de operatiuni cu utilizatorul (ex. login, permisiuni).
        /// </summary>
        /// <param name="message">Mesajul de afisat in fereastra de alerta.</param>
        /// <param name="inner">Exceptia interioara, daca este disponibila, pentru log.</param>
        private void UserRelatedException(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                if (inner != null)
                    writer.WriteLine(inner.Message);
                else
                    writer.WriteLine(message);
            }
        }
    }
}
