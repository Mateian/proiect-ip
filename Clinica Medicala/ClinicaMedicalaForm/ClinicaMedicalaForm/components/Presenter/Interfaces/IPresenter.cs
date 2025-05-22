/**************************************************************************
 *                                                                        *
 *  File:        IPresenter.cs                                            *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Interfata care defineste un element de tip presenter     *
 *               din Model-View-Presenter.                                *
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
using ClinicaMedicalaForm.components.Model;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Model.Medical;

namespace ClinicaMedicalaForm.components.Presenter.Interfaces
{
    public interface IPresenter
    {
        IUser VerificaAutentificare(string username, string parola);
        List<IUser> GetPacienti(int doctorID);
        List<IUser> GetDoctori();
        List<Programare> GetProgramariDoctor(int id);
        List<Programare> GetProgramariIstoric(int pacientID);
        void AdaugaProgramareViitoare(int ID, Programare programare);
        List<Programare> GetCereriProgramari(int pacientID);
        void AdaugaPacient(int id, Pacient pacient);
        Pacient DeletePacient(string v);
        void AdaugaDoctor(Doctor doctor);
        void StergeUser(int id);
        void ValidareProgramare(Programare newProgramare);
        bool CheckUserExists(List<string> data);
        IUser InsertUserCommand(List<string> data);
        void AdaugareFisaMedicala(List<string> dateFisaMedicala);
        string PreviewIstoricMedical(int nrFisa);
        string PreviewIstoricProgramari(string programare, int userID);
        string PreviewCereriProgramari(string programare, int userID);
        IUser GetUser(int userID);
    }
}
