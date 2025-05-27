/**************************************************************************
 *                                                                        *
 *  File:        GestioneazaDoctorForm.cs                                 *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular folosit pentru a gestiona doctorii de catre     *
 *               administrator.                                           *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ClinicaMedicalaForm.components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.View
{
    /// <summary>
    /// Formular utilizat de administrator pentru gestionarea listei de doctori.
    /// Permite adaugarea si stergerea doctorilor din lista.
    /// </summary>
    class GestioneazaDoctorForm : Form
    {
        // Elemente create de VS
        private ListBox listBoxGestionareDoctor;
        private Button buttonStergeDoctor;
        private Button buttonAdaugaDoctor;
        private GroupBox groupBox1;

        private List<IUser> _doctori;

        /// <summary>
        /// Doctorul selectat pentru stergere.
        /// </summary>
        public IUser SelectedDoctorId { get; private set; }

        /// <summary>
        /// Doctorul nou creat prin formularul de adaugare.
        /// </summary>
        public Doctor DoctorNou { get; private set; }

        /// <summary>
        /// Constructor care primeste lista de doctori pentru gestionare.
        /// </summary>
        /// <param name="doctori">Lista doctorilor existenti.</param>
        public GestioneazaDoctorForm(List<IUser> doctori)
        {
            _doctori = doctori;
            InitializeComponent();
            InitForm();
        }

        /// <summary>
        /// Initializeaza continutul listei afisate in ListBox.
        /// </summary>
        private void InitForm()
        {
            listBoxGestionareDoctor.Items.Clear();
            foreach (var dr in _doctori)
            {
                listBoxGestionareDoctor.Items.Add(dr.ToString());
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStergeDoctor = new System.Windows.Forms.Button();
            this.buttonAdaugaDoctor = new System.Windows.Forms.Button();
            this.listBoxGestionareDoctor = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStergeDoctor);
            this.groupBox1.Controls.Add(this.buttonAdaugaDoctor);
            this.groupBox1.Controls.Add(this.listBoxGestionareDoctor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gestionarea doctorilor";
            // 
            // buttonStergeDoctor
            // 
            this.buttonStergeDoctor.Location = new System.Drawing.Point(237, 312);
            this.buttonStergeDoctor.Name = "buttonStergeDoctor";
            this.buttonStergeDoctor.Size = new System.Drawing.Size(225, 35);
            this.buttonStergeDoctor.TabIndex = 2;
            this.buttonStergeDoctor.Text = "Sterge Doctor";
            this.buttonStergeDoctor.UseVisualStyleBackColor = true;
            this.buttonStergeDoctor.Click += new System.EventHandler(this.buttonStergeDoctor_Click);
            // 
            // buttonAdaugaDoctor
            // 
            this.buttonAdaugaDoctor.Location = new System.Drawing.Point(6, 312);
            this.buttonAdaugaDoctor.Name = "buttonAdaugaDoctor";
            this.buttonAdaugaDoctor.Size = new System.Drawing.Size(225, 35);
            this.buttonAdaugaDoctor.TabIndex = 1;
            this.buttonAdaugaDoctor.Text = "Adauga Doctor";
            this.buttonAdaugaDoctor.UseVisualStyleBackColor = true;
            this.buttonAdaugaDoctor.Click += new System.EventHandler(this.buttonAdaugaDoctor_Click);
            // 
            // listBoxGestionareDoctor
            // 
            this.listBoxGestionareDoctor.FormattingEnabled = true;
            this.listBoxGestionareDoctor.ItemHeight = 16;
            this.listBoxGestionareDoctor.Location = new System.Drawing.Point(6, 21);
            this.listBoxGestionareDoctor.Name = "listBoxGestionareDoctor";
            this.listBoxGestionareDoctor.Size = new System.Drawing.Size(458, 292);
            this.listBoxGestionareDoctor.TabIndex = 0;
            // 
            // GestioneazaDoctorForm
            // 
            this.ClientSize = new System.Drawing.Size(494, 377);
            this.Controls.Add(this.groupBox1);
            this.Name = "GestioneazaDoctorForm";
            this.Text = "Gestionare";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Callback-uri
        private void buttonAdaugaDoctor_Click(object sender, EventArgs e)
        {
            DoctorForm form = new DoctorForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                DoctorNou = form.DoctorNou;
                DoctorNou.NotifyObs("NEW DOCTOR CREATED");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonStergeDoctor_Click(object sender, EventArgs e)
        {
            int index = listBoxGestionareDoctor.SelectedIndex;

            if (index >= 0)
            {
                IUser selectedDoctor = _doctori[index];
                SelectedDoctorId = selectedDoctor;

                // Trimite catre formul principal
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Selectează un doctor pentru a-l șterge.");
            }
        }
    }
}
