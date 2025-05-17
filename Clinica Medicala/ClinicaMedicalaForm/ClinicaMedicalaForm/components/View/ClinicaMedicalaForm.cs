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

        private void button2_Click(object sender, EventArgs e)
        {
            
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
                labelWelcomeText.Text = "Bine ai venit, ";
                labelWelcomeText.Visible = true;
                if (_user.Rol == "Pacient")//_user
                {
                    labelWelcomeText.Text += _user.Nume + " " + _user.Prenume + ".";
                    tabControlUser.Visible = true;
                    //loadPrograms(_model.GetProgramariIstoric());
                    //loadIstoric(_model.GetIstoric());
                    //loadProgramari(_model.GetCurrentProgramari());
                    tabPagePacient.Visible = true;
                    tabPageDoctor.Visible = false;
                    tabControlUser.SelectTab("tabPagePacient");
                }
                else if(_user.Rol == "Doctor")
                {
                    labelWelcomeText.Text += "Dr. " + _user.Nume + " " + _user.Prenume + ".";
                    tabControlUser.Visible = true;
                    listBoxDoctorPacienti.Items.Clear();
                    listBoxListaProgramari.Items.Clear();

                    List<IUser> pacientiDoctor = _presenter.GetPacienti(_user.ID);
                    foreach(var pacient in pacientiDoctor)
                    {
                        listBoxDoctorPacienti.Items.Add(pacient.ToString());
                    }
                    List <Programare> l=_presenter.GetProgramari(_user.ID);
                    foreach (Programare p in l)
                    {
                        string s=p.Data.ToString()+", Pacientul: "+p.PacientID+", Specialitatea: "+p.Specializare+"\n";
                        listBoxListaProgramari.Items.Add(s);
                    }
                    tabControlUser.Visible = true;
                    tabControlUser.SelectTab("tabPageDoctor");
                }
                else if(_user.Rol == "Administrator")
                {
                    labelWelcomeText.Text += "Adm. " + _user.Nume + " " + _user.Prenume + ".";
                    tabControlUser.Visible = true;
                    groupBoxAdministrator.Visible = true;
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
            if (_user.Rol == "Pacient" && e.TabPage.Name=="tabPageDoctor")
            {
                e.Cancel = true;
            }
            if (_user.Rol == "Doctor" && e.TabPage.Name == "tabPagePacient")
            {
                e.Cancel = true;
            }
        }

        private void listBoxIstoricMedical_Click(object sender, EventArgs e)
        {
            if(listBoxIstoricMedical.SelectedItem!=null)
            {
                Form f;
                f=(FisaMedicalaForm.FisaMedicalaForm)listBoxIstoricMedical.SelectedItem;
                f.Show();
            }
        }

        private void buttonProgramare_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdaugareProgramare_Click(object sender, EventArgs e)
        {

        }
    }
}
