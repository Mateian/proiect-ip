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

using System.Collections.Generic;
using System.Linq;
using components.Model.Interfaces;
using components.Model.Medical;
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;
using components.Model.Users;

namespace ClinicaMedicalaForm.components.Presenter
{
    /// <summary>
    /// Clasa Presenter - parte a pattern-ului MVP.
    /// Face legatura intre View si Model, contine logica de prezentare si gestioneaza datele.
    /// </summary>
    public class Presenter : IPresenter
    {
        private IView _view;
        private IModel _model;

        /// <summary>
        /// Constructor pentru Presenter.
        /// Initializeaza referintele catre View si Model, apoi initializeaza datele.
        /// </summary>
        /// <param name="view">Interfata view-ului</param>
        /// <param name="model">Interfata modelului</param>
        public Presenter(IView view, IModel model)
        {
            this._view = view;
            this._model = model;
            Init();
        }


        /// <summary>
        /// Initializeaza datele din Model.
        /// Incarca utilizatorii, programarile, doctorii si pacientii.
        /// </summary>
        public void Init()
        {
            _model.CitireUtilizatori();
            _model.CitireProgramari();
            _model.CitireDoctori();
            _model.CitirePacienti();
        }

        /// <summary>
        /// Verifica autentificarea unui utilizator dupa username si parola.
        /// </summary>
        /// <param name="username">Numele de utilizator</param>
        /// <param name="parola">Parola</param>
        /// <returns>Obiectul IUser corespunzator daca autentificarea este valida, altfel null</returns>
        public IUser VerificaAutentificare(string username, string parola)
        {
            return _model.VerificaAutentificare(username, parola);
        }

        /// <summary>
        /// Obtine lista pacientilor unui doctor dupa ID-ul doctorului.
        /// </summary>
        /// <param name="doctorID">ID-ul doctorului</param>
        /// <returns>Lista de utilizatori care sunt pacienti ai doctorului</returns>
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

        /// <summary>
        /// Obtine lista programarilor pentru un doctor, dupa ID-ul doctorului.
        /// </summary>
        /// <param name="doctorID">ID-ul doctorului</param>
        /// <returns>Lista de programari ale doctorului</returns>
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

        /// <summary>
        /// Obtine lista tuturor doctorilor.
        /// </summary>
        /// <returns>Lista tuturor utilizatorilor cu rol Doctor</returns>
        public List<IUser> GetDoctori()
        {
            List<IUser> totiDoctorii = new List<IUser>();
            foreach (IUser d in _model.Doctori)
            {
                totiDoctorii.Add(d);
            }
            return totiDoctorii;
        }

        /// <summary>
        /// Obtine istoricul programarilor nevalabile ale unui pacient.
        /// </summary>
        /// <param name="pacientID">ID-ul pacientului</param>
        /// <returns>Lista programarilor cu valabilitate "Nevalabila"</returns>
        public List<Programare> GetProgramariIstoric(int pacientID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if (p.PacientID == pacientID && p.Valabilitate == "Nevalabila")
                {
                    l.Add(p);
                }
            }
            return l;
        }

        /// <summary>
        /// Obtine lista cererilor de programari valide sau in curs de validare pentru un pacient.
        /// </summary>
        /// <param name="pacientID">ID-ul pacientului</param>
        /// <returns>Lista cererilor cu valabilitate "In curs de validare" sau "Valabila"</returns>
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

        /// <summary>
        /// Adauga o programare viitoare pentru un anumit utilizator (pacient).
        /// </summary>
        /// <param name="ID">ID-ul utilizatorului</param>
        /// <param name="programare">Programarea de adaugat</param>
        public void AdaugaProgramareViitoare(int ID, Programare programare)
        {
            _model.AdaugaProgramareViitoare(ID, programare);
        }

        /// <summary>
        /// Sterge un pacient dupa un string care contine ID-ul pacientului.
        /// </summary>
        /// <param name="pacientString">String ce contine ID-ul pacientului la inceput</param>
        /// <returns>Pacientul sters sau null daca nu a fost gasit</returns>
        public Pacient DeletePacient(string pacientString)
        {
            int id;
            int.TryParse(pacientString.Split(' ')[0], out id);
            _model.DeletePacient(id);
            return (Pacient)_model.Pacienti.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// Adauga un pacient unui doctor specificat.
        /// </summary>
        /// <param name="doctorID">ID-ul doctorului</param>
        /// <param name="pacient">Pacientul de adaugat</param>
        public void AdaugaPacient(int doctorID, Pacient pacient)
        {
            _model.AdaugaPacient(doctorID, pacient);
        }

        /// <summary>
        /// Sterge un utilizator dupa ID.
        /// </summary>
        /// <param name="id">ID-ul utilizatorului de sters</param>
        public void StergeUser(int id)
        {
            _model.StergeUser(id);
        }

        /// <summary>
        /// Adauga un doctor nou in sistem.
        /// </summary>
        /// <param name="doctor">Doctorul de adaugat</param>
        public void AdaugaDoctor(Doctor doctor)
        {
            _model.AdaugaDoctor(doctor);
        }


        /// <summary>
        /// Marcheaza o programare ca fiind validata.
        /// </summary>
        /// <param name="programare">Programarea care trebuie validata</param>
        public void ValidareProgramare(Programare programare)
        {
            _model.ValidareProgramare(programare);
        }

        /// <summary>
        /// Verifica daca un utilizator exista in baza de date, dupa o lista de date.
        /// </summary>
        /// <param name="data">Lista cu datele utilizatorului</param>
        /// <returns>true daca utilizatorul exista, false altfel</returns>
        public bool CheckUserExists(List<string> data)
        {
            return _model.CheckUserExists(data);
        }

        /// <summary>
        /// Insereaza un utilizator nou in baza de date.
        /// </summary>
        /// <param name="data">Lista cu datele utilizatorului</param>
        /// <returns>Utilizatorul creat, sau null in caz de eroare</returns>
        public IUser InsertUserCommand(List<string> data)
        {
            return _model.InsertUserCommand(data);
        }

        /// <summary>
        /// Adauga o fisa medicala noua.
        /// </summary>
        /// <param name="dateFisaMedicala">Lista cu datele fisei medicale</param>
        public void AdaugareFisaMedicala(List<string> dateFisaMedicala)
        {
            _model.AdaugareFisaMedicala(dateFisaMedicala);
        }

        /// <summary>
        /// Returneaza o previzualizare a istoricului medical pentru o fisa medicala data.
        /// </summary>
        /// <param name="nrFisa">Numarul fisei medicale</param>
        /// <returns>String cu istoricul medical</returns>
        public string PreviewIstoricMedical(int nrFisa)
        {
            return _model.PreviewIstoricMedical(nrFisa);
        }

        /// <summary>
        /// Returneaza o previzualizare a istoricului programarilor pentru un utilizator.
        /// </summary>
        /// <param name="programare">Detalii programare (string)</param>
        /// <param name="userID">ID-ul utilizatorului</param>
        /// <returns>String cu istoricul programarilor</returns>
        public string PreviewIstoricProgramari(string programare, int userID)
        {
            return _model.PreviewIstoricProgramari(programare, userID);
        }

        /// <summary>
        /// Returneaza o previzualizare a cererilor de programari pentru un utilizator.
        /// </summary>
        /// <param name="programare">Detalii programare (string)</param>
        /// <param name="userID">ID-ul utilizatorului</param>
        /// <returns>String cu cererile de programari</returns>
        public string PreviewCereriProgramari(string programare, int userID)
        {
            return _model.PreviewCereriProgramari(programare, userID);
        }

        /// <summary>
        /// Returneaza utilizatorul cu ID-ul specificat.
        /// </summary>
        /// <param name="userID">ID-ul utilizatorului</param>
        /// <returns>Obiectul IUser corespunzator</returns>
        public IUser GetUser(int userID)
        {
            return _model.GetUser(userID);
        }

        /// <summary>
        /// Sterge o programare identificata printr-un string.
        /// </summary>
        /// <param name="programareString">String ce contine detalii despre programare</param>
        public void StergeProgramare(string programareString)
        {
            _model.DeleteAppointment(programareString);
        }

        /// <summary>
        /// Marcheaza o programare ca nevalidata.
        /// </summary>
        /// <param name="programare">Programarea de nevalidat</param>
        public void NevalidareProgramare(Programare programare)
        {
            _model.NevalidareProgramare(programare);
        }

        /// <summary>
        /// Returneaza o previzualizare a programarilor unui doctor.
        /// </summary>
        /// <param name="v">Detalii suplimentare pentru previzualizare (string)</param>
        /// <param name="iD">ID-ul doctorului</param>
        /// <returns>String cu programarile doctorului</returns>
        public string PreviewProgramariDoctor(string v, int iD)
        {
            return _model.PreviewProgramariDoctor(v, iD);
        }
    }
}
