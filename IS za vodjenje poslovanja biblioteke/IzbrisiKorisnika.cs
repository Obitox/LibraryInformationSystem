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
    public partial class IzbrisiKorisnika : Form
    {
        private DataTable dt;
        private SqlConnection sqlconnect;
        private SqlDataReader sqlr;
        private DataView dv;
        private string odgovorBaze;
        public IzbrisiKorisnika()
        {
            InitializeComponent();
            dgvKorisnika.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKorisnika.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            sqlconnect = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
            initGridView();
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
                dgvKorisnika.DataSource = dt;
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
                dgvKorisnika.DataSource = dv.ToTable();
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

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            if (numKorisnikId.Value > 0 && numKorisnikId.Value != 0)
            {
                try
                {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlCommand sqlComm = new SqlCommand("sp_AzurirajKorisnika", sqlconnect);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@tipazuriranja", "obrisi");
                    sqlComm.Parameters.AddWithValue("@idkorisnika", numKorisnikId.Value);

                    sqlr = sqlComm.ExecuteReader();

                    while (sqlr.Read())
                    {
                        odgovorBaze = sqlr[0].ToString();
                    }
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

                    dgvKorisnika.DataSource = dt;
                    dgvKorisnika.Update();
                    dgvKorisnika.Refresh();

                    sqlconnect.Close();
                    MessageBox.Show(odgovorBaze);
                }
            }
            else
            {
                MessageBox.Show("Niste uneli validan id korisnika.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKorisnika = new AzurirajKorisnika();
            azurirajKorisnika.Closed += (s, args) => this.Close();
            azurirajKorisnika.Show();
        }
    }
}
