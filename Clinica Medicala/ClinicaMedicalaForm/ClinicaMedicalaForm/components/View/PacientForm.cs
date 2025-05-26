/**************************************************************************
 *                                                                        *
 *  File:        PacientForm.cs                                           *
 *  Copyright:   (c) 2025, ourClinic                                      *
 *  E-mail:      ourClinic@medic.ro                                       *
 *  Description: Formular folosit pentru a adauga datele unui pacient     *
 *               la creare.                                               *
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
using ClinicaMedicalaForm.components.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.View
{
    class PacientForm : Form
    {
        private Button buttonAdauga;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBoxPrenume;
        private TextBox textBoxNume;
        private TextBox textBoxParola;
        private TextBox textBoxUsername;

        public Pacient PacientNou { get; private set; }

        public PacientForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPrenume = new System.Windows.Forms.TextBox();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxParola = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.Location = new System.Drawing.Point(76, 296);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(101, 37);
            this.buttonAdauga.TabIndex = 17;
            this.buttonAdauga.Text = "Adauga";
            this.buttonAdauga.UseVisualStyleBackColor = true;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Prenume";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Nume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Parola";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Username";
            // 
            // textBoxPrenume
            // 
            this.textBoxPrenume.Location = new System.Drawing.Point(67, 233);
            this.textBoxPrenume.Name = "textBoxPrenume";
            this.textBoxPrenume.Size = new System.Drawing.Size(121, 22);
            this.textBoxPrenume.TabIndex = 12;
            // 
            // textBoxNume
            // 
            this.textBoxNume.Location = new System.Drawing.Point(67, 169);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(121, 22);
            this.textBoxNume.TabIndex = 11;
            // 
            // textBoxParola
            // 
            this.textBoxParola.Location = new System.Drawing.Point(67, 103);
            this.textBoxParola.Name = "textBoxParola";
            this.textBoxParola.Size = new System.Drawing.Size(121, 22);
            this.textBoxParola.TabIndex = 10;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(67, 38);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(121, 22);
            this.textBoxUsername.TabIndex = 9;
            // 
            // PacientForm
            // 
            this.ClientSize = new System.Drawing.Size(257, 361);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPrenume);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.textBoxParola);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "PacientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxParola.Text == "" || textBoxNume.Text == "" || textBoxPrenume.Text == "")
            {
                MessageBox.Show("Trebuie completate toate campurile!");
                return;
            }

            if (!Regex.IsMatch(textBoxNume.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(textBoxPrenume.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Numele/Prenumele trebuie sa contina doar litere!");
                return;
            }
            Observe o = new Observe();
            PacientNou = new Pacient(1 /*DEFAULT VALUE*/, textBoxUsername.Text, textBoxParola.Text, textBoxNume.Text, textBoxPrenume.Text, o);
            PacientNou.NotifyObs("NEW USER CREATED");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
