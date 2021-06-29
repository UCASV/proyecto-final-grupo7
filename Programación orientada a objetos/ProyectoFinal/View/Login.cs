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
            // Lista de datos de usuario: nombre de usuario y contraseña.
            List<Administrator> userData = administrators
                .Where(a => a.NameUser == user && a.PasswordUser == pass).ToList();
            // Lista de nombre de usuario.
            List<Administrator> userFound = administrators
                .Where(a => a.NameUser == user).ToList();

            var query1 = (from Employee in db.Employees
                          from Administrator in db.Administrators
                          from Booth in db.Booths
                          where Administrator.Id == Employee.IdAdministrator &&
                          Booth.Id == Employee.IdBooth && Administrator.NameUser == txtUser.Text
                          orderby Booth.Id
                          select Booth.Id).LastOrDefault();

            int id = query1;// Consulta que obtiene número de cabina, segun el nombre de usuario introducido.

            List<Booth> booths = db.Booths
              .ToList();
            int number = Int32.Parse(txtBooth.Text);
            // Lista de numero de cabina.
            List<Booth> boothId = booths
                .Where(a => id == number)
                .ToList();

            //primera condicional evalua si el nombre y contraseña de usuario y número de cabina
            // son correctos.
            if (userData.Count() > 0 && boothId.Count() > 0)
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
            //segunda condicional evalua si el nombre de usuario no existe o si ha sido ingresado
            //incorrectamente.
            else if (userFound.Count == 0)
            {
                MessageBox.Show("El nombre de usuario introducido no existe!",
                  "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //tercera condicional evalua si la contraseña de usuario o el numero de cabina no
            //existen o han sido ingresados incorrectamente.
            else
            {
                MessageBox.Show("La contraseña de usuario o el numero de cabina son incorrectos!",
                "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}