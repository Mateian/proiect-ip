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

            foreach(var user in totiPacientii)
            {
                Pacient pacient = user as Pacient;
                
                if(pacient != null && pacient.Doctor != null && pacient.Doctor.ID == doctorID)
                {
                    pacientiDoctor.Add(pacient);
                }
            }

            return pacientiDoctor;
        }
        //List<> GetProgramari(int doctotID);
        //List<> GetPacienti(int doctotID);
        public List<Programare> GetProgramariDoctor(int doctorID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if(p.DoctorID == doctorID)
                {
                    l.Add(p);
                }
            }
            return l;
        }
        public List<Programare> GetProgramariPacient(int pacientID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if (p.PacientID == pacientID)
                {
                    l.Add(p);
                }
            }
            return l;
        }
        public void AdaugaProgramare(Programare programare)
        {
            _model.CereProgramare(programare);
        }
    }
}
