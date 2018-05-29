using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_za_vodjenje_poslovanja_biblioteke
{
    public partial class Registracija : Form
    {
        private SqlConnection sqlConnection1;
        private SqlDataReader sqlr;

        public Registracija()
        {
            InitializeComponent();
            sqlConnection1 = new SqlConnection("Data Source=BOJAN;Initial Catalog=biblioteka;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string prazniBoxovi = "";
            string odgovorBaze = "";
            int prazanBoxBrojac = 0;
            foreach (Control controls in this.Controls)
            {
                if (controls is TextBox)
                {
                    TextBox textBox = controls as TextBox;
                    if (textBox.Text.Length > 0 && textBox.Name != txtBrojTelefona.Name)
                    {
                        prazanBoxBrojac++;
                    }
                    else
                    {
                        if (textBox.Text == string.Empty && textBox.Name != txtBrojTelefona.Name)
                        {
                            prazniBoxovi += "\n" + textBox.Name.Substring(3) + "\n";
                        }
                    }
                }
            }

            if (prazanBoxBrojac >= 7)
            {
                try
                {
                    if (sqlConnection1.State.Equals(ConnectionState.Closed))
                        sqlConnection1.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_AzurirajKorisnika", sqlConnection1);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@tipazuriranja", "dodaj");
                    sqlCommand.Parameters.AddWithValue("@ime_k", txtIme.Text);
                    sqlCommand.Parameters.AddWithValue("@prezime_k", txtPrezime.Text);
                    sqlCommand.Parameters.AddWithValue("@JMBG", txtJMBG.Text);
                    sqlCommand.Parameters.AddWithValue("@broj_telefona", txtBrojTelefona.Text);
                    sqlCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlCommand.Parameters.AddWithValue("@adresa", txtAdresa.Text);
                    sqlCommand.Parameters.AddWithValue("@uloga", "klijent");
                    sqlCommand.Parameters.AddWithValue("@korisnicko_ime", txtKorisnickoIme.Text);
                    sqlCommand.Parameters.AddWithValue("@sifra", txtSifra.Text);


                    sqlr = sqlCommand.ExecuteReader();

                    while (sqlr.Read())
                    {
                        odgovorBaze = sqlr[0].ToString();
                    }
                    odgovorBaze = Regex.Replace(odgovorBaze, @"\s+", " ");

                    sqlr.Close();

                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlConnection1.Close();
                    MessageBox.Show(odgovorBaze);
                    this.Hide();
                    var logovanje = new Logovanje();
                    logovanje.Closed += (s, args) => this.Close();
                    logovanje.Show();
                }
            }
            else
            {
                MessageBox.Show("Niste uneli podatke u sledeće/sledeći polje/polja:\n" + prazniBoxovi );
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var pocetna = new Pocetna();
            pocetna.Closed += (s, args) => this.Close();
            pocetna.Show();
        }
    }
}
