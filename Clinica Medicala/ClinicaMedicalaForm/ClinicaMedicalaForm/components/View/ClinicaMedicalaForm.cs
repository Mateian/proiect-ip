using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaMedicalaForm.components.Model;
using ClinicaMedicalaForm.components.Model.Interfaces;
using ClinicaMedicalaForm.components.Presenter;
using ClinicaMedicalaForm.components.Presenter.Interfaces;
using ClinicaMedicalaForm.components.View.Interfaces;
using FisaMedicalaForm;
using System.Data.SQLite;
using static FisaMedicalaForm.FisaMedicalaForm;
using ClinicaMedicalaForm.components.Model.Medical;
using ClinicaMedicalaForm.components.View;

namespace ClinicaMedicalaForm
{
    public partial class ClinicaMedicalaForm : Form, IView
    {
        private IPresenter _presenter;
        private IModel _model;
        private IUser _user;
        public ClinicaMedicalaForm()
        {
            InitializeComponent();
            InitForm();
            this.AcceptButton = buttonAutentificare;
        }

        public void InitForm()
        {
            // in caz de e nevoie de facut ceva cand se creeaza Form
            textBoxParola.UseSystemPasswordChar = true;
            _model = new Model();
        }

        private void buttonCreareFisaMedicala_Click(object sender, EventArgs e)
        {
            FisaMedicalaForm.FisaMedicalaForm fisaMedicala = new FisaMedicalaForm.FisaMedicalaForm();
            if (fisaMedicala.ShowDialog() == DialogResult.OK)
            {
                List<string> dateFisaMedicala = fisaMedicala.datePacient;
                _presenter.AdaugareFisaMedicala(dateFisaMedicala);
                listBoxComenzi.Items.Add($"Creare fisa medicala.");
            }
        }

        private void buttonAutentificare_Click(object sender, EventArgs e)
        {
            string username = textBoxNumeUtilizator.Text;
            string parola = textBoxParola.Text;
            _user = _presenter.VerificaAutentificare(username, parola);

            
            
            if (_user != null)//val cat mai mica sa nu incurce cu nimica
            {
                buttonDeconectare.Visible = true;
                groupBoxAutentificare.Enabled = false;
                labelWelcomeText.Text = "Bine ai venit, ";
                labelWelcomeText.Visible = true;
                tabControlUser.TabPages.Clear();

                if (_user.Rol == "Pacient")//_user
                {
                    tabControlUser.TabPages.Add(tabPagePacient);
                    tabControlUser.SelectTab("tabPagePacient");
                    tabControlUser.Visible = true;

                    listBoxPacientIstoricProgramari.Items.Clear();
                    listBoxProgramariViitoare.Items.Clear();
                    listBoxIstoricMedical.Items.Clear();

                    labelWelcomeText.Text += _user.Nume + " " + _user.Prenume + ".";

                    List<Programare> programariIstoric = _presenter.GetProgramariIstoric(_user.ID);

                    foreach(Programare programare in programariIstoric)
                    {
                        listBoxPacientIstoricProgramari.Items.Add(programare.ToString());
                        IUser user = _presenter.GetUser(_user.ID);
                        if(user != null)
                        {
                            user.SetProgramare(programare);
                        }
                    }

                    List<Programare> cereriProgramare = _presenter.GetCereriProgramari(_user.ID);
                    foreach (Programare programare in cereriProgramare)
                    {
                        listBoxProgramariViitoare.Items.Add(programare.ToString());
                        IUser user = _presenter.GetUser(_user.ID);
                        if (user != null)
                        {
                            user.SetProgramare(programare);
                        }
                    }

                    List<FisaMedicala> fisaMedicalaIstoric = _model.PreluareIstoricMedical(_user.ID);
                    foreach (FisaMedicala fisa in fisaMedicalaIstoric)
                    {
                        listBoxIstoricMedical.Items.Add(fisa.ToString());
                    }
                }
                else if(_user.Rol == "Doctor")
                {
                    tabControlUser.TabPages.Add(tabPageDoctor);
                    tabControlUser.SelectTab("tabPageDoctor");
                    tabControlUser.Visible = true;

                    listBoxDoctorPacienti.Items.Clear();
                    listBoxListaProgramari.Items.Clear();

                    labelWelcomeText.Text += "Dr. " + _user.Nume + " " + _user.Prenume + ".";

                    List<IUser> pacientiDoctor = _presenter.GetPacienti(_user.ID);
                    foreach(var pacient in pacientiDoctor)
                    {
                        listBoxDoctorPacienti.Items.Add(pacient.ToString());
                    }
                    List <Programare> l=_presenter.GetProgramariDoctor(_user.ID);
                    foreach (Programare p in l)
                    {
                        listBoxListaProgramari.Items.Add(p.ToString());
                    }
                }
                else if(_user.Rol == "Administrator")
                {
                    tabControlUser.TabPages.Add(tabPageAdmin);
                    tabControlUser.SelectTab("tabPageAdmin");
                    tabControlUser.Visible = true;
                    groupBoxAdministrator.Visible = true;

                    listBoxAdminPacienti.Items.Clear();

                    labelWelcomeText.Text += "Adm. " + _user.Nume + " " + _user.Prenume + ".";

                    List<IUser> doctori = _presenter.GetDoctori();
                    foreach (var dr in doctori)
                    {
                        listBoxAdminPacienti.Items.Add(dr.ToString());
                        List<IUser> pacientiDoctor = _presenter.GetPacienti(dr.ID);
                        foreach (var pacient in pacientiDoctor)
                        {
                            listBoxAdminPacienti.Items.Add("--"+pacient.ToString());
                        }
                    }
                }
                else if(_user.Rol == "Asistent")
                {
                    labelWelcomeText.Text += "Asist. " + _user.Nume + " " + _user.Prenume + ".";
                }
            }
            else
            {
                groupBoxAdministrator.Visible = false;
                MessageBox.Show("No _user that matches these credentials found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonDeconectare_Click(object sender, EventArgs e)
        {
            groupBoxAdministrator.Visible = false;
            labelWelcomeText.Visible = false;
            tabControlUser.Visible = false;
            textBoxNumeUtilizator.Clear();
            textBoxParola.Clear();
            _user = null;
            buttonDeconectare.Visible = false;
            groupBoxAutentificare.Enabled = true;
            textBoxPreviewFiles.Clear();
        }
        
        public void SetModel(IModel model)
        {
            _model = model;
        }

        public void SetPresenter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        private void tabControlUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            /*if (_user.Rol == "Pacient" && e.TabPage.Name=="tabPageDoctor")
            {
                e.Cancel = true;
            }
            if (_user.Rol == "Doctor" && e.TabPage.Name == "tabPagePacient")
            {
                e.Cancel = true;
            }*/
        }

        private void listBoxIstoricMedical_Click(object sender, EventArgs e)
        {
            if(listBoxIstoricMedical.SelectedItem!=null)//in functie de ce fisa este selectata va afisa in casuta detaliile
            {
                textBoxPreviewFiles.Text = _presenter.PreviewIstoricMedical(listBoxIstoricMedical.SelectedIndex);
            }
        }
        private void listBoxPacientIstoricProgramari_Click(object sender, EventArgs e)
        {
            if (listBoxPacientIstoricProgramari.SelectedItem != null)
            {
                textBoxPreviewFiles.Text = _presenter.PreviewIstoricProgramari(listBoxPacientIstoricProgramari.SelectedIndex,_user.ID);
            }
        }
        private void buttonProgramare_Click(object sender, EventArgs e)
        {
            Pacient pacient = (Pacient)_user;
            try
            {
                if(pacient.Doctor == null)
                {
                    throw new NoDoctorExeption("Nu aveti doctor!");
                }
                ProgramareForm programareForm = new ProgramareForm(pacient.Doctor.Nume, pacient.Doctor.Prenume);
                if (programareForm.ShowDialog() == DialogResult.OK)
                {
                    Programare nouaProgramare = new Programare(_user.ID, pacient.Doctor.ID, programareForm.Data, programareForm.Specializare, "In curs de validare");
                    listBoxProgramariViitoare.Items.Add(nouaProgramare.ToString());
                    listBoxPacientIstoricProgramari.Items.Add(nouaProgramare.ToString());
                    // trebuie inserata si in baza de date
                    _presenter.AdaugaProgramareViitoare(_user.ID, nouaProgramare);
                    listBoxComenzi.Items.Add($"Cerere programare pentru [{pacient.ToString()}].");
                }
            }
            catch (NoDoctorExeption ex)
            {
                MessageBox.Show("Nu sunteți înscris la niciun doctor.\nVă rog să vă prezentați la cabinet pentru a discuta și a vă înscrie la un doctor!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdaugareProgramare_Click(object sender, EventArgs e)
        {
            if(listBoxListaProgramari.SelectedItem != null)
            {
                string stringProgramare = listBoxListaProgramari.SelectedItem.ToString();
                Programare programare = _model.Programari.FirstOrDefault(p => p.ToString() == stringProgramare);
                if(programare.Valabilitate == "In curs de validare")
                {
                    Programare newProgramare = new Programare(programare.PacientID, programare.DoctorID, programare.Data, programare.Specializare, "Valabila");
                    _presenter.ValidareProgramare(programare);
                    _model.Programari.Remove(programare);
                    _model.Programari.Add(newProgramare);
                    
                        listBoxListaProgramari.Items.Remove(stringProgramare);
                    listBoxListaProgramari.Items.Add(newProgramare.ToString());
                    listBoxComenzi.Items.Add($"Adaugare programare [{programare.ToString()}].");
                }
                else
                {
                    MessageBox.Show("Este deja valabila.");
                }
            }
        }

        private void listBoxDoctorPacienti_DoubleClick(object sender, EventArgs e)
        {
            if(listBoxDoctorPacienti.SelectedItem != null)
            {
                if (MessageBox.Show("Sunteti sigur ca vreti sa stergeti acest pacient?", "Stergere pacient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Pacient id = _presenter.DeletePacient(listBoxDoctorPacienti.SelectedItem.ToString());
                    listBoxDoctorPacienti.Items.Remove(listBoxDoctorPacienti.SelectedItem);

                    listBoxListaProgramari.Items.Clear();

                    foreach(var programareDoctor in _presenter.GetProgramariDoctor(_user.ID))
                    {
                        listBoxListaProgramari.Items.Add(programareDoctor.ToString());
                    }
                }
            }
        }

        private void buttonAdaugaPacient_Click(object sender, EventArgs e)
        {
            AdaugaPacientForm form = new AdaugaPacientForm(_model.Utilizatori);
            if(form.ShowDialog() == DialogResult.OK)
            {
                Pacient pacient = (Pacient)form.Pacient;
                if (pacient != null)
                {
                    _presenter.AdaugaPacient(_user.ID, pacient);
                    listBoxDoctorPacienti.Items.Add(pacient.ToString());
                    listBoxComenzi.Items.Add($"Adaugare pacient [{pacient.ToString()}].");
                }
            }
        }

        private void buttonGestioneazaDoctori_Click(object sender, EventArgs e)
        {
            GestioneazaDoctorForm form = new GestioneazaDoctorForm(_presenter.GetDoctori());
            if(form.ShowDialog() == DialogResult.OK)
            {
                IUser doctorSters = form.SelectedDoctorId;
                if (doctorSters != null)
                {
                    listBoxComenzi.Items.Add($"Stergere doctor [{doctorSters.ToString()}].");
                    _presenter.StergeUser(doctorSters.ID);
                }
                Doctor doctorNou = form.DoctorNou;
                if (doctorNou != null)
                {
                    List<string> date = new List<string>();
                    date.Add(doctorNou.Username);
                    date.Add(doctorNou.Parola);
                    date.Add(doctorNou.Nume);
                    date.Add(doctorNou.Prenume);
                    if (_presenter.CheckUserExists(date))
                    {
                        MessageBox.Show("Cont deja existent!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _presenter.AdaugaDoctor(doctorNou);
                    listBoxComenzi.Items.Add($"Adaugare doctor [{doctorNou.ToString()}].");
                }

                // Pentru a da refresh la listBox-ul de sub butoanele de gestionare
                listBoxAdminPacienti.Items.Clear();
                List<IUser> doctori = _presenter.GetDoctori();
                foreach (var dr in doctori)
                {
                    listBoxAdminPacienti.Items.Add(dr.ToString());
                    List<IUser> pacientiDoctor = _presenter.GetPacienti(dr.ID);
                    foreach (var pacient in pacientiDoctor)
                    {
                        listBoxAdminPacienti.Items.Add("--"+pacient.ToString());
                    }
                }
            }
        }
        private void buttonGestioneazaPacienti_Click(object sender, EventArgs e)
        {
            GestioneazaPacientiForm form = new GestioneazaPacientiForm(_model.Utilizatori);
            if (form.ShowDialog() == DialogResult.OK)
            {
                IUser pacientSters = form.Pacient;
                if (pacientSters != null)
                {
                    listBoxComenzi.Items.Add($"Stergere pacient [{pacientSters.ToString()}].");
                    _presenter.StergeUser(pacientSters.ID);
                }
                Pacient pacientNou = form.PacientNou;
                if (pacientNou != null)
                {
                    List<string> date = new List<string>();
                    date.Add(pacientNou.Username);
                    date.Add(pacientNou.Parola);
                    date.Add(pacientNou.Nume);
                    date.Add(pacientNou.Prenume);
                    if (_presenter.CheckUserExists(date))
                    {
                        MessageBox.Show("Cont deja existent!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        try
                        {
                            _ = _presenter.InsertUserCommand(date);
                            listBoxComenzi.Items.Add($"Adaugare pacient [{pacientNou.Nume + " " + pacientNou.Prenume}].");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Pentru a da refresh la listBox-ul de sub butoanele de gestionare
                listBoxAdminPacienti.Items.Clear();
                List<IUser> doctori = _presenter.GetDoctori();
                foreach (var dr in doctori)
                {
                    listBoxAdminPacienti.Items.Add(dr.ToString());
                    List<IUser> pacientiDoctor = _presenter.GetPacienti(dr.ID);
                    foreach (var pacient in pacientiDoctor)
                    {
                        listBoxAdminPacienti.Items.Add("--" + pacient.ToString());
                    }
                }
            }
        }

        private void buttonToCreateAcc_Click(object sender, EventArgs e)
        {
            groupBoxAutentificare.Visible = false;
            groupBoxCreateAcc.Visible = true;
            textBoxNumeUtilizator.Clear();
            textBoxParola.Clear();
        }

        private void buttonCreateB2Login_Click(object sender, EventArgs e)
        {
            groupBoxCreateAcc.Visible = false;
            groupBoxAutentificare.Visible = true;
            textBoxCreateUser.Clear();
            textBoxCreatePassword.Clear();
            textBoxCreateCheckPassword.Clear();
            textBoxCreateFirstName.Clear();
            textBoxCreateLastName.Clear();
        }

        private void buttonCreateSubmit_Click(object sender, EventArgs e)
        {
            if(textBoxCreatePassword.Text != textBoxCreateCheckPassword.Text)
            {
                MessageBox.Show("Parolele nu sunt identice!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                List<string> date = new List<string>();
                date.Add(textBoxCreateUser.Text);
                date.Add(textBoxCreatePassword.Text);
                date.Add(textBoxCreateLastName.Text);
                date.Add(textBoxCreateFirstName.Text);
                if (_presenter.CheckUserExists(date))
                {
                    MessageBox.Show("Cont deja existent!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        _user = _presenter.InsertUserCommand(date);
                        listBoxComenzi.Items.Add($"Creare user nou [{textBoxCreateLastName.Text + " " + textBoxCreateFirstName.Text}].");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            buttonDeconectare.Visible = true;
            groupBoxCreateAcc.Visible = false;
            groupBoxAutentificare.Visible = true;
            groupBoxAutentificare.Enabled = false;
            textBoxNumeUtilizator.Text = textBoxCreateUser.Text;
            textBoxParola.Text = textBoxCreatePassword.Text;
            textBoxCreateUser.Clear();
            textBoxCreatePassword.Clear();
            textBoxCreateCheckPassword.Clear();
            textBoxCreateFirstName.Clear();
            textBoxCreateLastName.Clear();
            labelWelcomeText.Text = "Bine ai venit, ";
            labelWelcomeText.Visible = true;
            listBoxPacientIstoricProgramari.Items.Clear();
            listBoxProgramariViitoare.Items.Clear();
            listBoxIstoricMedical.Items.Clear();
            labelWelcomeText.Text += _user.Nume + " " + _user.Prenume + ".";
            tabControlUser.Visible = true;
            tabPagePacient.Visible = true;
            tabPageDoctor.Visible = false;
            tabControlUser.SelectTab("tabPagePacient");

            List<Programare> programariIstoric = _presenter.GetProgramariIstoric(_user.ID);
            foreach (Programare programare in programariIstoric)
            {
                listBoxPacientIstoricProgramari.Items.Add(programare.ToString());
            }

            List<Programare> cereriProgramare = _presenter.GetCereriProgramari(_user.ID);
            foreach (Programare programare in cereriProgramare)
            {
                listBoxProgramariViitoare.Items.Add(programare.ToString());
            }

            List<FisaMedicala> fisaMedicalaIstoric = _model.PreluareIstoricMedical(_user.ID);
            foreach (FisaMedicala fisa in fisaMedicalaIstoric)
            {
                listBoxIstoricMedical.Items.Add(fisa.ToString());
            }
        }

        private void buttonStatistica_Click(object sender, EventArgs e)
        {
            listBoxComenzi.Items.Add($"Verificare statistica.");
            int admini = 0, asisteni = 0, doctori = 0, pacienti = 0; 
            richTextBoxStatistica.Text = "";
            List<IUser> users = _model.Utilizatori;
            richTextBoxStatistica.AppendText("Administratorii:\n");
            foreach(IUser user in users)
            {
                if(user.Rol == "Administrator") 
                { 
                    richTextBoxStatistica.AppendText(user.ToString() + "\n");
                    admini++;
                }
            }
            richTextBoxStatistica.AppendText("\nAsistentii:\n");
            foreach (IUser user in users)
            {
                if (user.Rol == "Asistent")
                {
                    richTextBoxStatistica.AppendText(user.ToString() + "\n");
                    asisteni++;
                }
            }
            richTextBoxStatistica.AppendText("\nDoctorii:\n");
            foreach (IUser user in users)
            {
                if (user.Rol == "Doctor")
                {
                    richTextBoxStatistica.AppendText(user.ToString() + "\n");
                    doctori++;
                }
            }
            richTextBoxStatistica.AppendText("\nPacientii:\n");
            foreach (IUser user in users)
            {
                if (user.Rol == "Pacient")
                {
                    richTextBoxStatistica.AppendText(user.ToString() + "\n");
                    pacienti++;
                }
            }
            string final = $"\nInformatii finale:\nIn total sunt {admini+asisteni+doctori} angajati in spital.\n" +
                           $"Acestia au grija de {pacienti} pacienti.\n" +
                           $"Un doctor are in medie grija de {pacienti/doctori} pacienti.";
            richTextBoxStatistica.AppendText(final);
        }

        private void listBoxProgramariViitoare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProgramariViitoare.SelectedItem != null)
            {
                textBoxPreviewFiles.Text = _presenter.PreviewCereriProgramari(listBoxProgramariViitoare.SelectedIndex, _user.ID);
            }
        }
    }
}
