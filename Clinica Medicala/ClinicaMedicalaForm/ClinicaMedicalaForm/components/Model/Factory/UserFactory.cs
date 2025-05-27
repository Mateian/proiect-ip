/**************************************************************************
 *                                                                        *
 *  File:        UserFactory.cs                                           *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Ajuta la creare unui nou utilizator in functie de        *
 *               datele primite.                                          *
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaMedicalaForm.components.Model.Users;
using ClinicaMedicalaForm.components.Observer;

namespace ClinicaMedicalaForm.components.Model.Factory
{
    /// <summary>
    /// Clasa UserFactory are rolul de a crea obiecte de tip
    /// utilizator (Administrator, Pacient, Doctor) pe baza
    /// unui qarray de informatii.
    /// </summary>
    public class UserFactory
    {
        /// <summary>
        /// Creeaza un obiect utilizator in functie de rolul specificat in infoArray.
        /// </summary>
        /// <param name="infoArray">Array de stringuri care contine detalii despre utilizator:
        /// [0] ID, [1] Rol, [2] Username, [3] Parola, [4] Nume, [5] Prenume.</param>
        /// <param name="obs">Observerul ce urmeaza a fi atasat utilizatorului.</param>
        /// <returns>Un obiect care implementeaza interfata IUser sau null daca rolul nu este recunoscut..</returns>
        public IUser CreateUser(string[] infoArray,Observe obs)
        {
            // Parsarea datelor din vectorul de stringuri
            int.TryParse(infoArray[0], out int ID);
            string rol = infoArray[1];
            string username = infoArray[2];
            string parola = infoArray[3];
            string nume = infoArray[4];
            string prenume = infoArray[5];

            // Crearea unei instante in functie de rol
            switch(rol)
            {
                case "Administrator":
                    return new Administrator(ID, username, parola, nume, prenume, obs);
                case "Pacient":
                    return new Pacient(ID, username, parola, nume, prenume, obs);
                case "Doctor":
                    return new Doctor(ID, username, parola, nume, prenume, obs);
            }

            return null; // In cazul in care nu se recunoaste rolul
        }
    }
}
