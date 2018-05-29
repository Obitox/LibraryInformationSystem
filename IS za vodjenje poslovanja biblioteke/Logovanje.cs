using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_za_vodjenje_poslovanja_biblioteke
{
    public partial class Logovanje : Form
    {
        private string uloga = "";
        private SqlConnection sqlConnection;

        public Logovanje()
        {
            InitializeComponent();
            txtSifra.PasswordChar = '*';
            sqlConnection = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string oString = "SELECT korisnicko_ime, sifra, uloga FROM Korisnici WHERE korisnicko_ime=N'" + txtKorisnickoIme.Text + "' AND sifra=N'" + txtSifra.Text + "'";
                SqlCommand sqlCmd = new SqlCommand(oString, sqlConnection);
                sqlConnection.Open();

                SqlDataReader oReader = sqlCmd.ExecuteReader();

                while (oReader.Read())
                {
                    uloga = oReader[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlConnection.Close();
            }

            if (uloga.Contains("admin"))
            {
                File.WriteAllText(@"korisnik.txt", txtKorisnickoIme.Text);
                this.Hide();
                var admin = new Administracija();
                admin.Closed += (s, args) => this.Close();
                admin.Show();
            }
            else if (uloga.Contains("klijent"))
            {
                File.WriteAllText(@"korisnik.txt", txtKorisnickoIme.Text);
                this.Hide();
                var klijent = new Klijent();
                klijent.Closed += (s, args) => this.Close();
                klijent.Show();
            }
            else
            {
                MessageBox.Show("Uneli ste pogrešno korisničko ime ili šifru, molim vas pokušajte ponovo.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            var pocetna = new Pocetna();
            pocetna.Closed += (s, args) => this.Close();
            pocetna.Show();
        }
    }
}
