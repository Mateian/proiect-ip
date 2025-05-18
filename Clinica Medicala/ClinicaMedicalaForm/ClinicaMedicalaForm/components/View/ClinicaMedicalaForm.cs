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
                _model.AdaugareFisaMedicala(dateFisaMedicala);
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
                    listBoxPacientIstoricProgramari.Items.Clear();
                    listBoxProgramariViitoare.Items.Clear();
                    listBoxIstoricMedical.Items.Clear();
                    labelWelcomeText.Text += _user.Nume + " " + _user.Prenume + ".";
                    tabControlUser.Visible = true;
                    //loadPrograms(_model.GetProgramariIstoric());
                    //loadIstoric(_model.GetIstoric());
                    //loadProgramari(_model.GetCurrentProgramari());

                    List<Programare> programariIstoric = _presenter.GetProgramariIstoric(_user.ID);
                    foreach(Programare programare in programariIstoric)
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
                else if(_user.Rol == "Doctor")
                {
                    tabControlUser.TabPages.Add(tabPageDoctor);
                    tabControlUser.SelectTab("tabPageDoctor");
                    labelWelcomeText.Text += "Dr. " + _user.Nume + " " + _user.Prenume + ".";
                    tabControlUser.Visible = true;
                    listBoxDoctorPacienti.Items.Clear();
                    listBoxListaProgramari.Items.Clear();

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
                    listBoxAdminPacienti.Items.Clear();
                    labelWelcomeText.Text += "Adm. " + _user.Nume + " " + _user.Prenume + ".";

                    tabControlUser.Visible = true;
                    groupBoxAdministrator.Visible = true;

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
                tabControlUser.Visible = false;
                tabPagePacient.Visible = false;
                tabPageDoctor.Visible = false;
                tabPageAdmin.Visible = false;
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
                textBoxPreviewFiles.Text = _model.PreviewIstoric(listBoxIstoricMedical.SelectedIndex);
            }
        }

        private void buttonProgramare_Click(object sender, EventArgs e)
        {
            Pacient pacient = (Pacient)_user;
            ProgramareForm programareForm = new ProgramareForm(pacient.Doctor.Nume, pacient.Doctor.Prenume);
            if(programareForm.ShowDialog() == DialogResult.OK)
            {
                Programare nouaProgramare = new Programare(_user.ID, pacient.Doctor.ID, programareForm.Data, programareForm.Specializare, "In curs de validare");
                listBoxProgramariViitoare.Items.Add(nouaProgramare.ToString());
                listBoxPacientIstoricProgramari.Items.Add(nouaProgramare.ToString());
                // trebuie inserata si in baza de date
                _presenter.AdaugaProgramareViitoare(nouaProgramare);
            }
        }

        private void buttonAdaugareProgramare_Click(object sender, EventArgs e)
        {

        }

        private void listBoxPacientProgramari_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxProgramari_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxDoctorPacienti_DoubleClick(object sender, EventArgs e)
        {
            if(listBoxDoctorPacienti.SelectedItem != null)
            {
                if (MessageBox.Show("Sunteti sigur ca vreti sa stergeti acest pacient?", "Stergere pacient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Pacient id = _presenter.DeletePacient(listBoxDoctorPacienti.SelectedItem.ToString());
                    listBoxDoctorPacienti.Items.Remove(listBoxDoctorPacienti.SelectedItem);
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
                }
            }
        }
    }
}
