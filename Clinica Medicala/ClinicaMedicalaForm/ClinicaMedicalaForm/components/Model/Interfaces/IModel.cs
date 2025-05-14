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
        List<IUser> CitireUtilizatori();
        IUser VerificaAutentificare(string username, string parola);
        List<string> GetProgramariIstoric();
        List<Form> GetIstoric();
        List<string> GetCurrentProgramari();
        void CitireProgramari();
        void CitirePacienti();
        List<Programare> Programari{get;}
        List<IUser> Pacienti { get; }
    }
}
