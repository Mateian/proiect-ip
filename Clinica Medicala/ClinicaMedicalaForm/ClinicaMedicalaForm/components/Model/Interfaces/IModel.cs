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
        void AdaugaProgramareViitoare(Programare programare);
        void AdaugareFisaMedicala(List<string> datePacient);
        List<FisaMedicala> PreluareIstoricMedical(int userID);
        string PreviewIstoric(int nrFisa);
        void DeletePacient(int id);
        void AdaugaPacient(int id, Pacient pacient);
    }
}
