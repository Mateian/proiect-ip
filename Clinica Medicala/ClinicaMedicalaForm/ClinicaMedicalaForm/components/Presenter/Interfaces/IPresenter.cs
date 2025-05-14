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
<<<<<<< HEAD
        List<IUser> GetPacienti(int doctorID);
=======
        List<Programare> GetProgramari(int id);
>>>>>>> main
    }
}
