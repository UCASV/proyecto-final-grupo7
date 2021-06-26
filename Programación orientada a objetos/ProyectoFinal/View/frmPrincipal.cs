using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ProyectoFinal.ContextUCA;
using ProyectoFinal.Controller;
using ProyectoFinal.View;

namespace ProyectoFinal
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            tabControl.ItemSize = new Size(0, 1);
            //Conectando el combobox con la base de datos
            var db = new UCA_ContextContext();
            var sideEffects = from EffectSecondary in db.EffectSecondaries
                              select EffectSecondary;
            cmbSideEffects.DataSource = sideEffects.ToList();
            cmbSideEffects.DisplayMember = "EffectSecondary1";
            cmbSideEffects.ValueMember = "Id";

            //Combobox de las Instituciones
            List<Institution> institutions = db.Institutions
                .ToList();
            cmbInstitution.DataSource = institutions;
            cmbInstitution.DisplayMember = "Institution1";
            cmbInstitution.ValueMember = "Id";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCritizenName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumer2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDirection_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSickness_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInstitution_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check =
                txtCitizenName.Text.Length > 4 &&//verificando datos esten correctos
                txtPhoneNumer2.Text.Length > 7 &&
                txtDirection.Text.Length > 4 &&
                txtEmail.Text.Length > 4 &&
                txtAge.Text.Length > 0;

            Institution u = new Institution();//La opcion seleccionada en el combobox de institucion
            u.Id = ((Institution)cmbInstitution.SelectedItem).Id;

            var db = new UCA_ContextContext();

            Institution institute = db.Set<Institution>()
                .SingleOrDefault(m => m.Id == u.Id);//Al Id de la institucion

            
            if (check && Check.CheckDui(txtDuiRU.Text) ) //validando DUI y los demas parametros
            {
                var n = new Citizen();
                var disease = new List<Disease>();
                int age = Convert.ToInt32(txtAge.Text);

                //Si el ciudadano pertenece a una Institucion con prioridad
                if (Check.Check_institution(institute.Id))
                {
                    AddCitizen(); //agregamos ciudadano a BD
                    return;
                }
                //Si el ciudadano tiene 60 años para arriba
                if(age >= 60)
                {
                    AddCitizen(); // agregamos ciudadano a BD
                    return;
                }
                //Si el ciudadano tiene 18 años y padece de muchas enfermedades
                if(n.Age >=18 && disease.Count > 0)
                {
                    AddCitizen(); //agregamos ciudadano a BD
                    return;
                }

                else
                {
                    MessageBox.Show("No eres una persona con prioridad, Espera tu etapa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                   
                //Posteriormente se le genera un mensaje para que aparezca la fecha de su cita

                //funcion para mostrar la fecha de la cita
            }

            else
            {
               MessageBox.Show("Los datos ingresado no son validos", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //El ciudadano no es una persona con prioridad
            }

            //Se redirige a inicio porque no se sabe si se quiere hacer seguimiento de cita o registrar otro ciudadano
            tabControl.SelectedIndex = 0;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("¿Esta seguro que quiere cancelar?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                tabControl.SelectedIndex = 0;
                txtDuiRU.Text = "";
                txtCitizenName.Text = "";
                txtPhoneNumer2.Text = "";
                txtDirection.Text = "";
                txtEmail.Text = "";
                txtDisease.Text = "";
                txtAge.Text = "";              
            }
        }
        private void toolStripHome_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void toolStripRegisterUser_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void toolStripDateProcess_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
        }

        private void toolStriptabVaccinationProcess_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
        }

        private void toolStripPostVaccination_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
        }

        private void btnCancelSC_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("¿Esta seguro que quiere cancelar?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(answer == DialogResult.Yes)
            {
                Clear();
            }
            
        }
        private void Clear()
        {
            tabControl.SelectedIndex = 0;
            txtDui.Clear();
            lblNameShow.Text = "";
            lblPhoneNumber.Text = "";
            lblEmail.Text = "";
            lblDirection.Text = "";
            lblDiseases.Text = "";
            lblInstitution.Text = "";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Se hará validación y si no retorna nada se deberá de sugerir si desea registrarse para que se le asigne una cita
        }

        private void btnNextSC_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("¿Esta seguro que quiere aplicarse la vacuna?", "Consentimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                tabControl.SelectedIndex = 3;
                pgrProgress.Value = pgrProgress.Minimum;
                tmrTimer.Enabled = true;
                btnNextPV.Enabled = false;
            }
            else
            {
                MessageBox.Show("El ciudadano no esta disupuesto a vacunarse", "Consentimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDui.Clear();
                lblNameShow.Text = "";
                lblPhoneNumber.Text = "";
                lblEmail.Text = "";
                lblDirection.Text = "";
                lblDiseases.Text = "";
                lblInstitution.Text = "";
                tabControl.SelectedIndex = 0;
            }
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            if (pgrProgress.Value == pgrProgress.Maximum)
            {
                //Detiene el temorizador
                tmrTimer.Enabled = false;
                btnNextPV.Enabled = true;

                //Cambia el lbl
                lblToolStrip.Text = "El ciudadano ha pasado a vacunarse";
            }
            else
            {
                pgrProgress.Value++;
            }
        }

        private void btnNextPV_Click(object sender, EventArgs e)
        {
            pgrProgress.Value = pgrProgress.Minimum;
            lblToolStrip.Text = "El ciudadano se encuentra en la fila de espera";
            tabControl.SelectedIndex = 4;
            MessageBox.Show("El ciudadano se ha aplicado la vacuna, ahora se digire " +
                "ser vigilado por 30 minutos", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelPV_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("¿Esta seguro que quiere cancelar?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                tabControl.SelectedIndex = 0;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            var db = new UCA_ContextContext();
            AddingData();
            
            MessageBox.Show("Se ha almacenado la información, el ciudadano " +
                "ya puede retirarse", "Última etapa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clear();
        }
        private void AddingData()
        {
            var db = new UCA_ContextContext();
            //Creando una nueva entidad de referencia que ya existe
            EffectSecondary sideEffect = (EffectSecondary)cmbSideEffects.SelectedItem;
            EffectSecondary sideEffectReference = db.Set<EffectSecondary>()
                    .SingleOrDefault(s => s.Id == sideEffect.Id);
            //Obteniendo id del appointmet
            var query4 = (from Appointment in db.Appointments
                         from Citizen in db.Citizens
                         where Appointment.IdCitizen == Citizen.Id
                         && Citizen.Dui == txtDui.Text
                         orderby Appointment.Id
                         select Appointment.Id).Last();
            int a = Convert.ToInt32(query4);
            var postVaccination = new Vaccination
            {
                DateTimeApplication = dtpDateApplication.Value,
                DateTimeProcess = dtpDate.Value,
                IdAppointment = a,
               // IdPlaceVaccination = 1,   ---> campo ya no existente
                IdEffectSecondary = sideEffectReference.Id,
                TimeSecondaryEffect = txtMinutes.Text.Trim()
            };
            db.Vaccinations.Add(postVaccination);
            db.SaveChanges();
            SecondAppointment(postVaccination);
        }
        private void SecondAppointment(Vaccination vaccination )
        {
            //Genera hora y minuto random
            int h = 0;
            int minutes = 0;
            Random number = new Random();
            h = number.Next(7, 17);
            Random number2 = new Random();
            minutes = number2.Next(0, 45);  
           //Se crea la cita para segunda dosis
            DateTime secondAppointment = Convert.ToDateTime(dtpDateApplication.Value.ToString());
            secondAppointment = secondAppointment.AddDays(42);
            TimeSpan hour = new TimeSpan( h, minutes, 0);
            secondAppointment = secondAppointment.Add(hour);

            var db = new UCA_ContextContext();
            //Consulta para obtene el id del lugar de vacunación
            var query = (from Appointment in db.Appointments
                          from PlaceVaccination in db.PlaceVaccinations
                         from Citizen in db.Citizens
                         where Appointment.IdCitizen == Citizen.Id && Appointment.IdPlaceVaccination == PlaceVaccination.Id
                         && Citizen.Dui == txtDui.Text
                         orderby Appointment.IdPlaceVaccination
                          select Appointment.IdPlaceVaccination).Last();
            int idPlace = Convert.ToInt32(query);
            var query5 = (from Citizen in db.Citizens
                          where Citizen.Dui == txtDui.Text
                          orderby Citizen.Id
                          select Citizen.Id).Last();
            int idCitizen = Convert.ToInt32(query5);
            var appointmentConect = new Appointment
            {
                DateTime = secondAppointment,
                IdCitizen = idCitizen,
                IdPlaceVaccination = idPlace
                //IdVaccination = vaccination.Id -----> Modificacion campo ya no existente
            };
            db.Appointments.Add(appointmentConect);
            db.SaveChanges();
            GeneratePDF(secondAppointment, vaccination);
        }
        private void GeneratePDF(DateTime secondAppointment, Vaccination vaccination)
        {
            var db = new UCA_ContextContext();
            var query = from EffectSecondary in db.EffectSecondaries
                        where EffectSecondary.Id == vaccination.IdEffectSecondary
                        select EffectSecondary.EffectSecondary1;

            FileStream fs = new FileStream(@"C:\Users\IncoMex\Desktop\Gabriela\PDF\CitaSegundaDosis.pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
            PdfWriter pdfW = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            //titulo pdf y autor
            doc.AddAuthor("Grupo 8");
            doc.AddTitle("Proceso de vacunación");

            //definifir la fuente
            iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            // Encabezado del documento
            doc.Add(new Paragraph("Información de vacunación", standarFont));
            doc.Add(Chunk.NEWLINE);

            //Encabezado de columnas
            PdfPTable tblTry = new PdfPTable(2);
            tblTry.WidthPercentage = 100;

            //Configurando el título de nuestras columnas
            PdfPCell clDateTimeApplication = new PdfPCell(new Phrase("Fecha y hora de aplicación de vacuna", standarFont));
            clDateTimeApplication.BorderWidth = 0;
            clDateTimeApplication.BorderWidthBottom = 0.75f;

            PdfPCell clDateTimeProcess = new PdfPCell(new Phrase("Fecha y hora de aplicación", standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            PdfPCell clSideEffects = new PdfPCell(new Phrase("Efectos secundarios presentados", standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            PdfPCell clSecondAppointment = new PdfPCell(new Phrase("Fecha y hora para segunda dosis", standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            tblTry.AddCell(clDateTimeApplication);
            tblTry.AddCell(clDateTimeProcess);
            tblTry.AddCell(clSideEffects);
            tblTry.AddCell(clSecondAppointment);

            //Añadiendo datos
            PdfPCell clDateTimeApplication2 = new PdfPCell(new Phrase(dtpDateApplication.Value.ToString(), standarFont));
            clDateTimeApplication.BorderWidth = 0;
            clDateTimeApplication.BorderWidthBottom = 0.75f;

            PdfPCell clDateTimeProcess2 = new PdfPCell(new Phrase(dtpDate.Value.ToString(), standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            PdfPCell clSideEffects2 = new PdfPCell(new Phrase(query.ToString(), standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            PdfPCell clSecondAppointment2 = new PdfPCell(new Phrase(secondAppointment.ToString(), standarFont));
            clDateTimeProcess.BorderWidth = 0;
            clDateTimeProcess.BorderWidthBottom = 0.75f;

            tblTry.AddCell(clDateTimeApplication2);
            tblTry.AddCell(clDateTimeProcess2);
            tblTry.AddCell(clSideEffects2);
            tblTry.AddCell(clSecondAppointment2);

            doc.Add(tblTry);
            doc.Close();
            pdfW.Close();

            MessageBox.Show("Se ha creado exitosamente el PDF", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void AddCitizen()//Metodo para agregar ciudadano a la BD
        {
            int age = Convert.ToInt32(txtAge.Text);
            Institution u = new Institution();//La opcion seleccionada en el combobox de institucion
            u.Id = ((Institution)cmbInstitution.SelectedItem).Id;

            var db = new UCA_ContextContext();

            Institution institute = db.Set<Institution>()
                .SingleOrDefault(m => m.Id == u.Id);//Al Id de la institucion

            Citizen citizen = new Citizen //Almacenando los datos ingresados a la tabla Ciudadano de la BD
            {
                Dui = txtDuiRU.Text,
                NameCitizen = txtCitizenName.Text,
                Phone = txtPhoneNumer2.Text,
                Direction = txtDirection.Text,
                EMail = txtEmail.Text,
                Age = age,
                IdInstitution = institute.Id
            };

            db.Add(citizen);//agregamos nuevo ciudadano
            db.SaveChanges();

            Disease disease = new Disease//Almacenando la lista ingresada
            {
                Disease1 = txtDisease.Text,
                IdCitizen = citizen.Id//asignando al id del ciudadano
            };
            var result = citizen;

            db.Add(disease);//agregamos la lista de enfermedades al Id del ciudadano
            db.SaveChanges();
            MessageBox.Show("Los datos del ciudadano ha sido almacenada", "Datos almacenados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmAppointment window = new frmAppointment(result);
            window.ShowDialog();
        }

    }
}
