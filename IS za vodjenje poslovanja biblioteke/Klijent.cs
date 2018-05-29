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
    public partial class Klijent : Form
    {
        private DataTable dt;
        private SqlConnection sqlconnect;
        private SqlDataReader sqlr;
        private string odgovorBaze;

        public Klijent()
        {
            InitializeComponent();
            dgvKnjige.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKnjige.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            sqlconnect = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
            initGridView();
        }

        private void initGridView()
        {
            if (sqlconnect.State.Equals(ConnectionState.Closed))
                sqlconnect.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT Knjiga.id_knjige, ime_knjige, ime, zanr FROM Knjiga, autori_i_knjige, Autori WHERE Knjiga.id_knjige = autori_i_knjige.id_knjige AND autori_i_knjige.id_autora = Autori.id_autora", sqlconnect);
            sqlDA.SelectCommand.CommandType = CommandType.Text;
            dt = new DataTable();
            sqlDA.Fill(dt);
            dgvKnjige.DataSource = dt;
            sqlconnect.Close();
        }

        private void txtPretraga_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("{0} like '%{1}%'", comboBox1.SelectedItem.ToString(), txtPretraga.Text);
                dgvKnjige.DataSource = dv.ToTable();
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

        private void btnRezervisi_Click(object sender, EventArgs e)
        {
            string ulogovaniKorisnik = File.ReadAllText(@"korisnik.txt").ToString();

            if (numericUpDown1.Value > 0)
            {
                try
                {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlCommand sqlCommand = new SqlCommand("sp_RezervacijaKnjige", sqlconnect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@korisnickoime", ulogovaniKorisnik);
                    sqlCommand.Parameters.AddWithValue("@idknjige", numericUpDown1.Value);
                    sqlCommand.Parameters.AddWithValue("@datum_rezervacije", DateTime.Now.ToString("yyyy-MM-dd"));
                    sqlCommand.Parameters.AddWithValue("@datum_isteka", DateTime.Now.AddDays(20).ToString("yyyy-MM-dd"));

                    sqlr = sqlCommand.ExecuteReader();

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
                    MessageBox.Show(odgovorBaze);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(@"korisnik.txt");
            this.Hide();
            var azurirajKorisnika = new Logovanje();
            azurirajKorisnika.Closed += (s, args) => this.Close();
            azurirajKorisnika.Show();
        }
    }
}
