using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.Model.Interfaces
{
    public interface IModel
    {
        List<IUser> CitireUtilizatori();
        int VerificaAutentificare(string username, string parola);
        List<string> GetProgramariIstoric();
        List<Form> GetIstoric();
        List<string> GetCurrentProgramari();
    }
}
