using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicaMedicalaForm.components.View.Interfaces;

namespace ClinicaMedicalaForm
{
    public partial class ProgramareForm : Form
    {
        private string _doctorNume, _doctorPrenume;
        public string Data { get; set; }
        public string Specializare { get; set; }
        private TextBox textBoxData;
        private TextBox textBoxSpecializare;
        private Button buttonProgramare;
        private Label label1;
        private Label label2;
        private Label labelDoctor;

        public ProgramareForm(string doctorNume, string doctorPrenume)
        {
            _doctorNume = doctorNume;
            _doctorPrenume = doctorPrenume;
            InitializeComponent();
            labelDoctor.Text += _doctorNume + " " + _doctorPrenume;
        }

        private void InitializeComponent()
        {
            this.labelDoctor = new System.Windows.Forms.Label();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.textBoxSpecializare = new System.Windows.Forms.TextBox();
            this.buttonProgramare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDoctor
            // 
            this.labelDoctor.AutoSize = true;
            this.labelDoctor.Location = new System.Drawing.Point(12, 46);
            this.labelDoctor.Name = "labelDoctor";
            this.labelDoctor.Size = new System.Drawing.Size(27, 16);
            this.labelDoctor.TabIndex = 0;
            this.labelDoctor.Text = "Dr. ";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(53, 123);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(100, 22);
            this.textBoxData.TabIndex = 1;
            // 
            // textBoxSpecializare
            // 
            this.textBoxSpecializare.Location = new System.Drawing.Point(53, 175);
            this.textBoxSpecializare.Name = "textBoxSpecializare";
            this.textBoxSpecializare.Size = new System.Drawing.Size(100, 22);
            this.textBoxSpecializare.TabIndex = 2;
            // 
            // buttonProgramare
            // 
            this.buttonProgramare.Location = new System.Drawing.Point(53, 230);
            this.buttonProgramare.Name = "buttonProgramare";
            this.buttonProgramare.Size = new System.Drawing.Size(100, 23);
            this.buttonProgramare.TabIndex = 3;
            this.buttonProgramare.Text = "Programare";
            this.buttonProgramare.UseVisualStyleBackColor = true;
            this.buttonProgramare.Click += new System.EventHandler(this.buttonProgramare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Specializare";
            // 
            // ProgramareForm
            // 
            this.ClientSize = new System.Drawing.Size(209, 321);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonProgramare);
            this.Controls.Add(this.textBoxSpecializare);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.labelDoctor);
            this.Name = "ProgramareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonProgramare_Click(object sender, EventArgs e)
        {
            Data = textBoxData.Text;
            Specializare = textBoxSpecializare.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
