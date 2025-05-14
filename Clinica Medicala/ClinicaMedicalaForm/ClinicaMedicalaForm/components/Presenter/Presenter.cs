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
            _model.CitirePacienti();
        }
        public IUser VerificaAutentificare(string username, string parola)
        {
            return _model.VerificaAutentificare(username, parola);
        }

        //List<> GetPacienti(int doctotID);
        public List<Programare> GetProgramari(int doctotID)
        {
            List<Programare> l = new List<Programare>();
            foreach (Programare p in _model.Programari)
            {
                if(p.DoctorID == doctotID && !p.EsteInTrecut)
                {
                    l.Add(p);
                }
            }
            return l;
        }
    }
}
