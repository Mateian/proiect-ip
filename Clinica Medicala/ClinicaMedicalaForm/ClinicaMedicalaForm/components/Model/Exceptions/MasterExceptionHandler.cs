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
    public class MasterExceptionHandler : Exception
    {
        public MasterExceptionHandler() { }
        public MasterExceptionHandler(string message, int err_code, Exception inner) 
        {

            switch (err_code)
            {
                case 100:
                case 101:
                case 102:
                    SQLExceptionHandle(message, inner);
                    break;
                case 200:
                case 201:
                    ObjectNullHandle(message, inner);
                    break;
                case 300:
                    UserIDHandle(message, inner);
                    break;
                case 400:
                case 401:
                case 402:
                    UserRelatedException(message, inner);
                    break;
            }
        }
        private void UserIDHandle(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                writer.WriteLine(inner.Message);
            }
        }
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
        private void SQLExceptionHandle(string message, Exception inner)
        {
            MessageBox.Show(message, "Warning - Check error log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                writer.WriteLine(inner.Message);
            }
        }
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
