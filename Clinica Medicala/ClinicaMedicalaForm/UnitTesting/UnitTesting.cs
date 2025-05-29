using ClinicaMedicalaForm.components.Presenter;
using components.Model.Exceptions;
using components.Model.Interfaces;
using components.Model.Medical;
using components.Model.Users;
using components.ModelClass;
using components.Presenter.Interfaces;
using components.View.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Observer;
using System;
using System.Collections.Generic;
using System.IO;
using ClinicaMedicalaFormNamespace;

namespace UnitTesting
{
    [TestClass]
    public class UnitTesting
    {
        //Teste pentru clasa Pacient
        [TestMethod]
        public void TestSetProgramarePacient()
        {
            Observe o = new Observe();
            Pacient p = new Pacient(1, "test", "test", "Rotaru", "Rares", o);
            Programare programare = new Programare(1, 1, 3, "2023-10-01", "Consultație", "In validare");
            p.SetProgramare(programare);
            Assert.AreEqual(programare, p.GetProgramare(0), "Programarea nu a fost setata corect pentru pacient.");
        }
        [TestMethod]
        public void TestGetProgramarePacient()
        {
            Observe o = new Observe();
            Pacient p = new Pacient(1, "test", "test", "Rotaru", "Rares", o);
            Programare programare = new Programare(1, 1, 3, "2023-10-01", "Consultație", "In validare");
            p.SetProgramare(programare);
            Assert.AreEqual(programare, p.GetProgramare(0), "Programarea nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestToStringPacient()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            string expectedString = "4 Marin Ioana (pacient1) Pacient  01-Jan-01 12:00:00 AM";
            Assert.AreEqual(expectedString, m.GetUser(4).ToString(), "functia nu a parsat corect.");
        }
        //Teste pentru clasa Doctor
        [TestMethod]
        public void TestSetProgramareDoctor()
        {
            Observe o = new Observe();
            Doctor d = new Doctor(1, "test", "test", "Popescu", "Ion", o);
            Programare programare = new Programare(1, 1, 3, "2023-10-01", "Consultație", "In validare");
            d.SetProgramare(programare);
            Assert.AreEqual(programare, d.GetProgramare(0), "Programarea nu a fost setata corect pentru doctor.");
        }
        [TestMethod]
        public void TestGetProgramareDoctor()
        {
            Observe o = new Observe();
            Doctor d = new Doctor(1, "test", "test", "Popescu", "Ion", o);
            Programare programare = new Programare(1, 1, 3, "2023-10-01", "Consultație", "In validare");
            d.SetProgramare(programare);
            Assert.AreEqual(programare, d.GetProgramare(0), "Programarea nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestToStringDoctor()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            string expectedString = "2 Popescu Andrei (doctor1) Doctor  ";
            Assert.AreEqual(expectedString, m.GetUser(2).ToString(), "functia nu a parsat corect.");
        }
        //Teste pentru clasa Model
        [TestMethod]
        public void TestCitireUtilizatori()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            Assert.IsNotNull(m.CitireUtilizatori(), "Citirea utilizatorilor a esuat.");
        }
        [TestMethod]
        public void TestCitireDoctori()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            m.CitireDoctori();
            Assert.IsTrue(m.Doctori.Count > 0, "Citirea doctorilor a esuat.");
        }
        [TestMethod]
        public void TestCitirePacienti()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            m.CitireDoctori();
            m.CitirePacienti();
            Assert.IsTrue(m.Pacienti.Count > 0, "Citirea pacientilor a esuat.");
        }
        [TestMethod]
        public void TestCitireProgramari()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireProgramari();
            Assert.IsNotNull(m.Programari, "Citirea programarilor a esuat.");
        }
        [TestMethod]
        public void TestVerificaAutentificare()
        {
            Pacient p = new Pacient(4, "pacient1", "pacient1", "Marin", "Ioana", new Observe());
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            Assert.AreEqual(p.ToString(), m.VerificaAutentificare("pacient1", "pacient1").ToString(), "Autentificarea nu a functionat corect pentru pacient.");
        }
        [TestMethod]
        public void TestPreluareIstoricMedical()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            m.PreluareIstoricMedical(2);
            Assert.IsNotNull(m.PreluareIstoricMedical(2), "Preluarea istoricului medical a esuat.");
        }
        [TestMethod]
        public void TestPreviewIstoricMedical()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            List<FisaMedicala> l = m.PreluareIstoricMedical(4);
            Assert.AreEqual(l[0].GeneratePreview(), m.PreviewIstoricMedical(0), "Fisa medicala nu a fost selectata corect.");
        }
        [TestMethod]
        public void TestPreviewIstoricProgramari()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            m.CitireProgramari();
            string programare = "6 18.03.2025, Pacientul: 5, Specialitatea: Neurochirurgie, Valabilitatea: Nevalabila\n";
            string expectedString = "Data programarii: 18.03.2025\r\nSpecializarea: Neurochirurgie\r\nValabilitatea: Nevalabila\r\n\r\n";
            Assert.AreEqual(expectedString, m.PreviewIstoricProgramari(programare, 5), "Preview-ul nu a fost generat corect.");
        }
        [TestMethod]
        public void TestGetUser()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            m.CitireUtilizatori();
            Pacient p = new Pacient(4, "pacient1", "pacient1", "Marin", "Ioana", new Observe());
            Assert.AreEqual(p.ToString(), m.GetUser(4).ToString(), "Utilizatorul nu a fost selectat corect.");
        }
        //Teste pentru clasa presenter
        [TestMethod]
        public void TestGetPacienti()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            List<IUser> pacienti = presenter.GetPacienti(2);
            Assert.IsTrue(pacienti.Count > 0, "Lista de pacienti nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestGetDoctori()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            List<IUser> doctori = presenter.GetDoctori();
            Assert.IsTrue(doctori.Count > 0, "Lista de doctori nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestGetProgramariDoctor()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            List<Programare> programari = presenter.GetProgramariDoctor(2);
            Assert.IsNotNull(programari, "Lista de programari nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestGetProgramariIstoricPacient()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            List<Programare> programari = presenter.GetProgramariIstoric(5);
            Assert.IsTrue(programari.Count > 0, "Lista de istoric programari nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestGetCereriProgramariPacient()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            List<Programare> programari = presenter.GetCereriProgramari(5);
            Assert.IsTrue(programari.Count > 0, "Lista de cereri programari nu a fost returnata corect.");
        }
        [TestMethod]
        public void TestCheckUserExists()
        {
            List<string> date = new List<string>();
            date.Add("pacient1");
            date.Add("pacient1");
            date.Add("pacient1");
            date.Add("pacient1");
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            Assert.IsTrue(presenter.CheckUserExists(date), "Functia trebuia sa gaseasca userul.");
        }
        [TestMethod]
        public void TestPreviewProgramariDoctor()
        {
            Model.Location = Directory.GetCurrentDirectory() + "\\..\\..\\..\\ClinicaMedicalaForm\\components\\Resources\\ClinicaMedicalaDB.db";
            Model m = new Model();
            IView view = new ClinicaMedicalaFormNamespace.ClinicaMedicalaForm();
            IPresenter presenter = new Presenter(view, m);
            string preview = presenter.PreviewProgramariDoctor("2 10.06.2025, Pacientul: 5, Specialitatea: Psihiatrie, Valabilitatea: In curs de validare\n", 3);
            string expectedPreview = "Data programarii: 10.06.2025\r\nSpecializarea: Psihiatrie\r\nValabilitatea: In curs de validare\r\n\r\n";
            Assert.AreEqual(expectedPreview,preview,"Preview-ul programarii nu a fost generat corect.");
        }
    }
}
