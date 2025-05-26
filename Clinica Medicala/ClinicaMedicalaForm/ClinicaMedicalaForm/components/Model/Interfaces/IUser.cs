/**************************************************************************
 *                                                                        *
 *  File:        IUser.cs                                                 *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Interfata pentru utilizatorii din aplicatie.             *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using ClinicaMedicalaForm.components.Model.Medical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicalaForm.components.Model
{
    public interface IUser
    {
        int ID { get; }
        string Username { get; }
        string Nume { get; }
        string Prenume { get; }
        string Rol { get; }
        string Parola { get; }
        string ToString();
        void SetProgramare(Programare programare);
        Programare GetProgramare(int index);
        void NotifyObs(string s);
    }
}
