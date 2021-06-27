using ProyectoFinal;
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

namespace ProyectoPrueba
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
        private void AddLogin()
        {
            var db = new UCA_ContextContext();
            var query = (from Employee in db.Employees
                         from Administrator in db.Administrators
                         where Administrator.Id == Employee.IdAdministrator
                         && Employee.IdBooth == Int32.Parse(txtBooth.Text)
                         orderby Administrator.NameUser
                         select Employee.Id).Last();
            string idEmployee = query;
            DateTime dateTime = DateTime.Now;
            var login = new Login
            {
                DateTime = dateTime,
                IdEmployee = query,
                IdBooth = Int32.Parse(txtBooth.Text)
            };

            db.Add(login);
            db.SaveChanges();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var db = new UCA_ContextContext();
            List<Administrator> administrators = db.Administrators
                .ToList();
            string user = txtUser.Text;
            string pass = txtPassword.Text;
            List<Administrator> result1 = administrators
                .Where(a => a.NameUser == user &&
                            a.PasswordUser == pass)
                .ToList();

            var query1 = (from Employee in db.Employees
                          from Administrator in db.Administrators
                          from Booth in db.Booths
                          where Administrator.Id == Employee.IdAdministrator &&
                          Booth.Id == Employee.IdBooth && Administrator.NameUser == txtUser.Text
                          orderby Booth.Id
                          select Booth.Id).Last();
            int id = query1;

            List<Booth> booths = db.Booths
                .ToList();
            int number = Int32.Parse(txtBooth.Text);
            List<Booth> result2 = booths
                .Where(a => id == number)
                .ToList();

            if (result1.Count() > 0 && result2.Count() > 0)
            {
                DateTime localDate = DateTime.Now;
                DialogResult dataEmployee = MessageBox.Show("Fecha y hora de inicio de sesión: "
                + localDate + " \nCabina en que sea iniciado sesión: " + number,
                "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddLogin();
                this.Hide();
                using (frmPrincipal window = new frmPrincipal())
                {
                    window.ShowDialog();
                }
            }
            //Mensaje de error
            else
            {
                MessageBox.Show("El Usuario y contraseña son incorrectos o el numero de cabina no existe!",
                "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Irán validaciones para verificar que existe el usuario, que no pueda iniciar sesión sin haber ingresado algo
            }
        }
    }
}