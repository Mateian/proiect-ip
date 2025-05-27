/**************************************************************************
 *                                                                        *
 *  File:        ClinicaMedicalaForm.cs                                   *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: MVP (Model-View-Presenter), View - interfata             *
 *               utilizatorului (UI). Afiseaza datele si captureaza       *
 *               interactiunile utilizatorului.                           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

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
using ClinicaMedicalaForm.components.Model.Exceptions;

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
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

                    foreach (Programare programare in programariIstoric)
                    {
                        listBoxPacientIstoricProgramari.Items.Add(programare.ToString());
                        IUser user = _presenter.GetUser(_user.ID);
                        if (user != null)
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
                else if (_user.Rol == "Doctor")
                {
                    tabControlUser.TabPages.Add(tabPageDoctor);
                    tabControlUser.SelectTab("tabPageDoctor");
                    tabControlUser.Visible = true;


                    listBoxDoctorPacienti.Items.Clear();
                    listBoxListaProgramari.Items.Clear();

                    labelWelcomeText.Text += "Dr. " + _user.Nume + " " + _user.Prenume + ".";

                    List<IUser> pacientiDoctor = _presenter.GetPacienti(_user.ID);
                    foreach (var pacient in pacientiDoctor)
                    {
                        listBoxDoctorPacienti.Items.Add(pacient.ToString());
                    }
                    List<Programare> l = _presenter.GetProgramariDoctor(_user.ID);
                    foreach (Programare p in l)
                    {
                        listBoxListaProgramari.Items.Add(p.ToString());
                    }
                }
                else if (_user.Rol == "Administrator")
                {
                    tabControlUser.TabPages.Add(tabPageAdmin);
                    tabControlUser.SelectTab("tabPageAdmin");
                    tabControlUser.Visible = true;
                    groupBoxAdministrator.Visible = true;

                    listBoxAdminPacienti.Items.Clear();

                    labelWelcomeText.Text += "Adm. " + _user.Nume + " " + _user.Prenume + ".";

                    listBoxComenzi.Items.Clear();

                    foreach (string s in _model.GetObserverInfo())
                    {
                        listBoxComenzi.Items.Add(s);
                    }

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
            else
            {
                groupBoxAdministrator.Visible = false;
                MessageBox.Show("Nu exista utilizatori cu datele introduse.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonDeconectare_Click(object sender, EventArgs e)
        {
            _user.NotifyObs("DISCONNECTED");
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
            if (listBoxIstoricMedical.SelectedItem != null)//in functie de ce fisa este selectata va afisa in casuta detaliile
            {
                textBoxPreviewFiles.Text = _presenter.PreviewIstoricMedical(listBoxIstoricMedical.SelectedIndex);
            }
        }
        private void listBoxPacientIstoricProgramari_Click(object sender, EventArgs e)
        {
            if (listBoxPacientIstoricProgramari.SelectedItem != null)
            {
                textBoxPreviewFiles.Text = _presenter.PreviewIstoricProgramari(listBoxPacientIstoricProgramari.SelectedItem.ToString(), _user.ID);
            }
        }
        private void buttonProgramare_Click(object sender, EventArgs e)
        {
            Pacient pacient = (Pacient)_user;
            try
            {
                if (pacient.Doctor == null)
                {
                    throw new MasterExceptionHandler("Nu aveti doctor!", 401, null);
                }
                ProgramareForm programareForm = new ProgramareForm(pacient.Doctor.Nume, pacient.Doctor.Prenume);
                if (programareForm.ShowDialog() == DialogResult.OK)
                {
                    int nextId = _model.Programari.Any() ? _model.Programari.Max(p => p.ID) + 1 : 1;
                    Programare nouaProgramare = new Programare(nextId, _user.ID, pacient.Doctor.ID, programareForm.Data, programareForm.Specializare, "In curs de validare");
                    listBoxProgramariViitoare.Items.Add(nouaProgramare.ToString());
                    listBoxPacientIstoricProgramari.Items.Add(nouaProgramare.ToString());
                    // trebuie inserata si in baza de date
                    _presenter.AdaugaProgramareViitoare(_user.ID, nouaProgramare);
                    listBoxComenzi.Items.Add($"Cerere programare pentru [{pacient.ToString()}].");
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void buttonAdaugareProgramare_Click(object sender, EventArgs e)
        {
            if (listBoxListaProgramari.SelectedItem != null)
            {
                string stringProgramare = listBoxListaProgramari.SelectedItem.ToString();
                Programare programare = _model.Programari.FirstOrDefault(p => p.ToString() == stringProgramare);
                if (programare.Valabilitate == "In curs de validare")
                {
                    Programare newProgramare = new Programare(programare.ID, programare.PacientID, programare.DoctorID, programare.Data, programare.Specializare, "Valabila");
                    _presenter.ValidareProgramare(programare);
                    _model.Programari.Remove(programare);

                    listBoxListaProgramari.Items.Remove(stringProgramare);
                    listBoxListaProgramari.Items.Add(newProgramare.ToString());
                    //listBoxComenzi.Items.Add($"Adaugare programare [{programare.ToString()}].");
                    _user.NotifyObs("APPOINTMENT VALIDATED");
                }
                else
                {
                    MessageBox.Show("Este deja valabila.");
                }
            }
        }

        private void listBoxDoctorPacienti_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxDoctorPacienti.SelectedItem != null)
            {
                if (MessageBox.Show("Sunteti sigur ca vreti sa stergeti acest pacient?", "Stergere pacient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Pacient id = _presenter.DeletePacient(listBoxDoctorPacienti.SelectedItem.ToString());
                    listBoxDoctorPacienti.Items.Remove(listBoxDoctorPacienti.SelectedItem);

                    listBoxListaProgramari.Items.Clear();

                    foreach (var programareDoctor in _presenter.GetProgramariDoctor(_user.ID))
                    {
                        listBoxListaProgramari.Items.Add(programareDoctor.ToString());
                    }
                }
            }
        }

        private void buttonAdaugaPacient_Click(object sender, EventArgs e)
        {
            AdaugaPacientForm form = new AdaugaPacientForm(_model.Utilizatori);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Pacient pacient = (Pacient)form.Pacient;
                if (pacient != null)
                {
                    try
                    {
                        _presenter.AdaugaPacient(_user.ID, pacient);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                    listBoxDoctorPacienti.Items.Add(pacient.ToString());
                    //listBoxComenzi.Items.Add($"Adaugare pacient [{pacient.ToString()}].");
                    _user.NotifyObs("PACIENT ADDED TO DOCTOR");
                }
            }
        }

        private void buttonGestioneazaDoctori_Click(object sender, EventArgs e)
        {
            try
            {
                GestioneazaDoctorForm form = new GestioneazaDoctorForm(_presenter.GetDoctori());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IUser doctorSters = form.SelectedDoctorId;
                    if (doctorSters != null)
                    {
                        listBoxComenzi.Items.Add($"Stergere doctor [{doctorSters.ToString()}].");
                        _presenter.StergeUser(doctorSters.ID);//comanda de salvat
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
                        _presenter.AdaugaDoctor(doctorNou);//comanda de salvat
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
                            listBoxAdminPacienti.Items.Add("--" + pacient.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        private void buttonGestioneazaPacienti_Click(object sender, EventArgs e)
        {
            try
            {
                GestioneazaPacientiForm form = new GestioneazaPacientiForm(_model.Utilizatori);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IUser pacientSters = form.Pacient;
                    if (pacientSters != null)
                    {
                        listBoxComenzi.Items.Add($"Stergere pacient [{pacientSters.ToString()}].");
                        _presenter.StergeUser(pacientSters.ID);//comanda de salvat
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
                                _ = _presenter.InsertUserCommand(date);//comanda de salvat
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
            catch (Exception ex)
            {
                return;
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
            if (textBoxCreatePassword.Text != textBoxCreateCheckPassword.Text)
            {
                MessageBox.Show("Parolele nu sunt identice!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBoxCreateUser.Text == "" || textBoxCreateFirstName.Text == "" || textBoxCreateLastName.Text == "" || textBoxCreatePassword.Text == "" || textBoxCreateCheckPassword.Text == "")
            {
                MessageBox.Show("Introduceti date in toate casutele.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Cont creat!", "Creare cont", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _user.NotifyObs("USER CREATED");
                        listBoxComenzi.Items.Add($"Creare user nou [{textBoxCreateLastName.Text + " " + textBoxCreateFirstName.Text}].");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void buttonStatistica_Click(object sender, EventArgs e)
        {
            //listBoxComenzi.Items.Add($"Verificare statistica.");
            int admini = 0, asisteni = 0, doctori = 0, pacienti = 0;
            _user.NotifyObs("SHOW STATISTIC");
            listBoxComenzi.Items.Clear();
            foreach (string s in _model.GetObserverInfo())
            {
                listBoxComenzi.Items.Add(s);
            }
            richTextBoxStatistica.Text = "";
            List<IUser> users = _model.Utilizatori;
            richTextBoxStatistica.AppendText("Administratorii:\n");
            foreach (IUser user in users)
            {
                if (user.Rol == "Administrator")
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
            string final = $"\nInformatii finale:\nIn total sunt {admini + asisteni + doctori} angajati in spital.\n" +
                           $"Acestia au grija de {pacienti} pacienti.\n" +
                           $"Un doctor are in medie grija de {pacienti / doctori} pacienti.";
            richTextBoxStatistica.AppendText(final);
        }

        private void listBoxProgramariViitoare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProgramariViitoare.SelectedItem != null)
            {
                textBoxPreviewFiles.Text = _presenter.PreviewCereriProgramari(listBoxProgramariViitoare.SelectedItem.ToString(), _user.ID);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sunteti sigur ca doriti sa parasiti aplicatia?", "Parasire aplicatie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void listBoxListaProgramari_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxListaProgramari.SelectedItem != null)
            {
                if (MessageBox.Show("Sunteti sigur ca vreti sa stergeti aceasta programare?", "Stergere programare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _presenter.StergeProgramare(listBoxListaProgramari.SelectedItem.ToString());
                    listBoxListaProgramari.Items.Remove(listBoxListaProgramari.SelectedItem);
                }
            }
        }

        private void listBoxListaProgramari_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listBoxListaProgramari.SelectedItem != null)
                {
                    if(MessageBox.Show("Doriti sa invalidati aceasta programare?", "Programare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string stringProgramare = listBoxListaProgramari.SelectedItem.ToString();
                        Programare programare = _model.Programari.FirstOrDefault(p => p.ToString() == stringProgramare);
                        if (programare.Valabilitate == "Valabila")
                        {
                            Programare newProgramare = new Programare(programare.ID, programare.PacientID, programare.DoctorID, programare.Data, programare.Specializare, "Nevalabila");
                            _presenter.NevalidareProgramare(programare);
                            _model.Programari.Remove(programare);

                            listBoxListaProgramari.Items.Remove(stringProgramare);
                            listBoxListaProgramari.Items.Add(newProgramare.ToString());
                            //listBoxComenzi.Items.Add($"Adaugare programare [{programare.ToString()}].");
                            _user.NotifyObs("APPOINTMENT INVALIDATED");
                        }
                        else
                        {
                            MessageBox.Show("Trebuie sa fie Valabila pentru a deveni nevalabila.");
                        }
                    }
                }
            }
        }

        private void helpToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            // trebuie un try catch
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\..\\..\\components\\Resources\\ourClinicHelp.chm");
        }
    }
}
