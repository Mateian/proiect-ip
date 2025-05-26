/**************************************************************************
 *                                                                        *
 *  File:        IModel.cs                                                *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Interfata care defineste un element de tip model din     *
 *               Model-View-Present.                                      *
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
using System.Windows.Forms;
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Model.Interfaces
{
    public interface IModel
    {
        List<IUser> Pacienti { get; }
        List<IUser> Doctori { get; }
        List<Programare> Programari { get; }
        List<IUser> Utilizatori {  get; }
        List<IUser> CitireUtilizatori();
        IUser VerificaAutentificare(string username, string parola);
        void CitireProgramari();
        void CitirePacienti();
        void CitireDoctori();
        void AdaugaProgramareViitoare(int id, Programare programare);
        void AdaugareFisaMedicala(List<string> datePacient);
        List<FisaMedicala> PreluareIstoricMedical(int userID);
        string PreviewIstoricMedical(int nrFisa);
        string PreviewIstoricProgramari(string programare, int userID);
        void AdaugaDoctor(Doctor doctor);
        void DeletePacient(int id);
        void StergeUser(int id);
        void AdaugaPacient(int id, Pacient pacient);
        void ValidareProgramare(Programare programare);
        bool CheckUserExists(List<string> data);
        IUser InsertUserCommand(List<string> data);
        IUser GetUser(int userID);
        string PreviewCereriProgramari(string programare, int userID);
        DatabaseManager GetDatabaseManager();
        List<string> GetObserverInfo();
    }
}
