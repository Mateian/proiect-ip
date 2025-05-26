/**************************************************************************
 *                                                                        *
 *  File:        IPresenter.cs                                            *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: MVP (Model-View-Presenter), Presenter - intermediar      *
 *               intre Model si View. Contine logica de prezentare        *
 *               si coordoneaza fluxul de date si interactiunile.         *
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
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;

namespace ClinicaMedicalaForm.components.Presenter
{
    public class Presenter : IPresenter
    {
        private IView _view;
        private IModel _model;

        public Presenter(IView view, IModel model)
        {
            this._view = view;
            this._model = model;
            Init();
        }
        public void Init()
        {
            _model.CitireUtilizatori();
            _model.CitireProgramari();
            _model.CitireDoctori();
            _model.CitirePacienti();
        }
        public IUser VerificaAutentificare(string username, string parola)
        {
            return _model.VerificaAutentificare(username, parola);
        }

        public List<IUser> GetPacienti(int doctorID)
        {
            List<IUser> pacientiDoctor = new List<IUser>();

            List<IUser> totiPacientii = _model.Pacienti;
            if (totiPacientii == null) return pacientiDoctor;

            foreach (var user in totiPacientii)
            {
                Pacient pacient = user as Pacient;

                if (pacient != null && pacient.Doctor != null && pacient.Doctor.ID == doctorID)
                {
                    pacientiDoctor.Add(pacient);
                }
            }

            return pacientiDoctor;
        }

        public List<Programare> GetProgramariDoctor(int doctorID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if (p.DoctorID == doctorID)
                {
                    l.Add(p);
                }
            }
            return l;
        }

        public List<IUser> GetDoctori()
        {
            List<IUser> totiDoctorii = new List<IUser>();
            foreach (IUser d in _model.Doctori)
            {
                totiDoctorii.Add(d);
            }
            return totiDoctorii;
        }
        public List<Programare> GetProgramariIstoric(int pacientID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if (p.PacientID == pacientID && p.Valabilitate == "Valabila")
                {
                    l.Add(p);
                }
            }
            return l;
        }
        public List<Programare> GetCereriProgramari(int pacientID)
        {
            List<Programare> cereri = new List<Programare>();
            foreach (Programare programare in _model.Programari)
            {
                if (programare.PacientID == pacientID && (programare.Valabilitate == "In curs de validare" || programare.Valabilitate == "Valabila"))
                {
                    cereri.Add(programare);
                }
            }
            return cereri;
        }
        public void AdaugaProgramareViitoare(int ID, Programare programare)
        {
            _model.AdaugaProgramareViitoare(ID, programare);
        }
        public Pacient DeletePacient(string pacientString)
        {
            int id;
            int.TryParse(pacientString.Split(' ')[0], out id);
            _model.DeletePacient(id);
            return (Pacient)_model.Pacienti.FirstOrDefault(p => p.ID == id);
        }

        public void AdaugaPacient(int doctorID, Pacient pacient)
        {
            _model.AdaugaPacient(doctorID, pacient);
        }

        public void StergeUser(int id)
        {
            _model.StergeUser(id);
        }

        public void AdaugaDoctor(Doctor doctor)
        {
            _model.AdaugaDoctor(doctor);
        }

        public void ValidareProgramare(Programare programare)
        {
            _model.ValidareProgramare(programare);
        }
        public bool CheckUserExists(List<string> data)
        {
            return _model.CheckUserExists(data);
        }
        public IUser InsertUserCommand(List<string> data)
        {
            return _model.InsertUserCommand(data);
        }
        public void AdaugareFisaMedicala(List<string> dateFisaMedicala)
        {
            _model.AdaugareFisaMedicala(dateFisaMedicala);
        }
        public string PreviewIstoricMedical(int nrFisa)
        {
            return _model.PreviewIstoricMedical(nrFisa);
        }
        public string PreviewIstoricProgramari(string programare, int userID)
        {
            return _model.PreviewIstoricProgramari(programare, userID);
        }
        public string PreviewCereriProgramari(string programare, int userID)
        {
            return _model.PreviewCereriProgramari(programare, userID);
        }
        public IUser GetUser(int userID)
        {
            return _model.GetUser(userID);
        }
    }
}
