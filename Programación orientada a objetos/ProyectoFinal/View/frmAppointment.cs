using ProyectoFinal.ContextUCA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.View
{
    public partial class frmAppointment : Form
    {
        private Citizen citizen { get; set; }
        public frmAppointment(Citizen citizen)
        {
            InitializeComponent();
            this.citizen = citizen;
        }

        private void frmAppointment_Load(object sender, EventArgs e)
        {
            var db = new UCA_ContextContext();

            //Combobox del lugar de vacunacion
            List<PlaceVaccination> placeVaccinations = db.PlaceVaccinations
                .ToList();
            cmbPlace_Vaccination.DataSource = placeVaccinations;
            cmbPlace_Vaccination.DisplayMember = "PlaceVaccination1";
            cmbPlace_Vaccination.ValueMember = "Id";

            //Generando una fecha aleatorio
            int day = 0;
            int hour = 0;
            Random number = new Random();
            day = number.Next(7, 20);
            Random number2 = new Random();
            hour = number2.Next(1, 10);

            dtpDate.Value = DateTime.Today.AddDays(day);//generando fecha de la primera cita
            dtpTime.Value = DateTime.Today.AddHours(hour); //generando una hora de la primera cita
        }

        private void btnAdd_Appointment_Click(object sender, EventArgs e)
        {
            PlaceVaccination u = new PlaceVaccination();// id del lugar de la vacunacion
            u.Id = ((PlaceVaccination)cmbPlace_Vaccination.SelectedItem).Id;

            var db = new UCA_ContextContext();

            PlaceVaccination place = db.Set<PlaceVaccination>()
                .SingleOrDefault(m => m.Id == u.Id);

            //Unimos la fecha y la hora
            DateTime Date = dtpDate.Value.Date + dtpTime.Value.TimeOfDay;
            dtpTime.Format = DateTimePickerFormat.Custom;
            dtpTime.CustomFormat = "hh:mm:tt";

                Appointment appointment = new Appointment
                {
                    DateTime = Date,
                    IdPlaceVaccination = place.Id,
                    IdCitizen = citizen.Id
                };
                db.Add(appointment);//agregamos la nueva cita del ciudadano registrado
                db.SaveChanges();


                MessageBox.Show("La cita se ha almacenado correctamente", "Datos almacenados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        
    }
}
