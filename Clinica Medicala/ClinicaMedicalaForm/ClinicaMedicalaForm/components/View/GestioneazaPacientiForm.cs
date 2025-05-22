using ClinicaMedicalaForm.components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedicalaForm.components.View
{
    class GestioneazaPacientiForm : Form
    {
        private ListBox listBoxPacienti;
        private Button buttonSterge;
        private Button buttonAdauga;
        private GroupBox groupBox1;

        private List<IUser> _users;
        private List<IUser> _pacienti;
       
        public IUser Pacient { get; private set; }
        public Pacient PacientNou { get; private set; }
        public GestioneazaPacientiForm(List<IUser> pacienti)
        {
            _users = pacienti;
            _pacienti = _users.Where(u => u.Rol == "Pacient").ToList();
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            foreach(var pacient in _users)
            {
                if(pacient.Rol == "Pacient") 
                { 
                    listBoxPacienti.Items.Add(pacient.ToString());
                }
            }
        }
        private void InitializeComponent()
        {
            this.listBoxPacienti = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPacienti
            // 
            this.listBoxPacienti.FormattingEnabled = true;
            this.listBoxPacienti.ItemHeight = 16;
            this.listBoxPacienti.Location = new System.Drawing.Point(6, 21);
            this.listBoxPacienti.Name = "listBoxPacienti";
            this.listBoxPacienti.Size = new System.Drawing.Size(458, 292);
            this.listBoxPacienti.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSterge);
            this.groupBox1.Controls.Add(this.buttonAdauga);
            this.groupBox1.Controls.Add(this.listBoxPacienti);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 353);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gestionarea pacientilor";
            // 
            // buttonSterge
            // 
            this.buttonSterge.Location = new System.Drawing.Point(239, 312);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(225, 35);
            this.buttonSterge.TabIndex = 2;
            this.buttonSterge.Text = "Sterge Pacient";
            this.buttonSterge.UseVisualStyleBackColor = true;
            this.buttonSterge.Click += new System.EventHandler(this.buttonSterge_Click);
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.Location = new System.Drawing.Point(6, 312);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(225, 35);
            this.buttonAdauga.TabIndex = 1;
            this.buttonAdauga.Text = "Adauga Pacient";
            this.buttonAdauga.UseVisualStyleBackColor = true;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // GestioneazaPacientiForm
            // 
            this.ClientSize = new System.Drawing.Size(494, 377);
            this.Controls.Add(this.groupBox1);
            this.Name = "GestioneazaPacientiForm";
            this.Text = "Gestionare";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            PacientForm form = new PacientForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                PacientNou = form.PacientNou;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            int index = listBoxPacienti.SelectedIndex;

            if (index >= 0)
            {
                IUser selectedPacient = _pacienti[index];
                Pacient = selectedPacient;

                // Trimite catre formul principal
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Selectează un pacient pentru a-l șterge.");
            }
        }
    }
}
