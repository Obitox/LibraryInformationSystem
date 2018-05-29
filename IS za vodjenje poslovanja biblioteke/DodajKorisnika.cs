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
    public partial class DodajKorisnika : Form
    {
        private SqlConnection sqlConnection1;
        private SqlDataReader sqlr;
        private DataTable dt;
        private DataView dv;
        public DodajKorisnika()
        {
            InitializeComponent();
            sqlConnection1 = new SqlConnection("Data Source=BOJAN;Initial Catalog=biblioteka;Integrated Security=True");
            initGridView();
            dgvKorisnici.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKorisnici.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void initGridView()
        {
            try
            {
                if (sqlConnection1.State.Equals(ConnectionState.Closed))
                    sqlConnection1.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM view_Korisnici", sqlConnection1);
                sqlDA.SelectCommand.CommandType = CommandType.Text;
                dt = new DataTable();
                sqlDA.Fill(dt);
                dgvKorisnici.DataSource = dt;
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
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
                    sqlCommand.Parameters.AddWithValue("@uloga", "admin");
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

                    if (sqlConnection1.State.Equals(ConnectionState.Closed))
                        sqlConnection1.Open();

                    SqlDataAdapter sqlRef = new SqlDataAdapter("SELECT * FROM view_Korisnici", sqlConnection1);
                    sqlRef.SelectCommand.CommandType = CommandType.Text;
                    dt = new DataTable();
                    sqlRef.Fill(dt);

                    dgvKorisnici.DataSource = dt;
                    dgvKorisnici.Update();
                    dgvKorisnici.Refresh();

                    sqlConnection1.Close();
                    MessageBox.Show(odgovorBaze);

                }
            }
            else
            {
                MessageBox.Show("Niste uneli podatke u sledeće/sledeći polje/polja:\n" + prazniBoxovi);
            }
        }

        private void txtPretraga_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                dv = dt.DefaultView;
                dv.RowFilter = string.Format("{0} like '%{1}%'", comboBox1.SelectedItem.ToString(), txtPretraga.Text);
                dgvKorisnici.DataSource = dv.ToTable();
            }
            else
            {
                MessageBox.Show("Niste izabrali atribut po kojem želite da vršite pretragu.");
                txtPretraga.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPretraga.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKorisnika = new AzurirajKorisnika();
            azurirajKorisnika.Closed += (s, args) => this.Close();
            azurirajKorisnika.Show();
        }
    }
}
