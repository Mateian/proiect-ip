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
        string PreviewIstoricProgramari(int nrProgramare,int userID);
        string PreviewCereriProgramari(int nrProgramare,int userID);
        IUser GetUser(int userID);
    }
}
