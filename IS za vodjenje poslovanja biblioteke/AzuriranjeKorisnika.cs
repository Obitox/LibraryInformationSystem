using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_za_vodjenje_poslovanja_biblioteke
{
    public partial class AzuriranjeKorisnika : Form
    {
        private DataTable dt;
        private SqlConnection sqlconnect;
        private SqlDataReader sqlr;
        private DataView dv;
        private string odgovorBaze;
        public AzuriranjeKorisnika()
        {
            InitializeComponent();
            dgvKorisnici.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKorisnici.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            sqlconnect = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
            initGridView();
            this.Size = new Size(750, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void initGridView()
        {
            try
            {
                if (sqlconnect.State.Equals(ConnectionState.Closed))
                    sqlconnect.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM view_Korisnici", sqlconnect);
                sqlDA.SelectCommand.CommandType = CommandType.Text;
                dt = new DataTable();
                sqlDA.Fill(dt);
                dgvKorisnici.DataSource = dt;
                sqlconnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0 && numericUpDown1.Value != 0)
            {
                try
                {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlCommand comm = new SqlCommand("sp_AzurirajKorisnika", sqlconnect);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@tipazuriranja", "azuriraj");
                    comm.Parameters.AddWithValue("@ime_k", txtIme.Text);
                    comm.Parameters.AddWithValue("@prezime_k", txtPrezime.Text);
                    comm.Parameters.AddWithValue("@JMBG", txtJMBG.Text);
                    comm.Parameters.AddWithValue("@broj_telefona", txtBrojTelefona.Text);
                    comm.Parameters.AddWithValue("@email", txtEmail.Text);
                    comm.Parameters.AddWithValue("@adresa", txtAdresa.Text);
                    comm.Parameters.AddWithValue("@uloga", txtUloga.Text);
                    comm.Parameters.AddWithValue("@korisnicko_ime", txtKorisnickoIme.Text);
                    comm.Parameters.AddWithValue("@sifra", txtSifra.Text);
                    comm.Parameters.AddWithValue("@idkorisnika", numericUpDown1.Value);

                    sqlr = comm.ExecuteReader();

                    while (sqlr.Read())
                    {
                        odgovorBaze = sqlr[0].ToString();
                    }

                    sqlr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlconnect.Close();

                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlDataAdapter sqlRef = new SqlDataAdapter("SELECT * FROM view_Korisnici", sqlconnect);
                    sqlRef.SelectCommand.CommandType = CommandType.Text;
                    dt = new DataTable();
                    sqlRef.Fill(dt);

                    dgvKorisnici.DataSource = dt;
                    dgvKorisnici.Update();
                    dgvKorisnici.Refresh();

                    sqlconnect.Close();
                    MessageBox.Show(odgovorBaze);
                }
            }
            else
            {
                MessageBox.Show("Niste uneli dobru vrednost za id korisnika.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0 && numericUpDown1.Value != 0)
            {
                try
                {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();
                    SqlCommand myCommand = new SqlCommand("SELECT * FROM fun_PodaciZaAzuriranjeKorisnika(@idkorisnika)", sqlconnect);
                    myCommand.CommandType = CommandType.Text;
                    myCommand.Parameters.AddWithValue("@idkorisnika", numericUpDown1.Value);

                    sqlr = myCommand.ExecuteReader();

                    while (sqlr.Read())
                    {
                        txtKorisnickoIme.Text = sqlr[0].ToString();
                        txtSifra.Text = sqlr[1].ToString();
                        txtIme.Text = sqlr[2].ToString();
                        txtPrezime.Text = sqlr[3].ToString();
                        txtJMBG.Text = sqlr[4].ToString();
                        txtBrojTelefona.Text = sqlr[5].ToString();
                        txtEmail.Text = sqlr[6].ToString();
                        txtAdresa.Text = sqlr[7].ToString();
                        txtUloga.Text = sqlr[8].ToString();
                    }

                    sqlr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlconnect.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste uneli dobru vrednost u id knjige.");
            }
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
