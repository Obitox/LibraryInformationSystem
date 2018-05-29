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
    public partial class IzbrisiKnjigu : Form
    {
        private DataTable dt;
        private SqlConnection sqlconnect;
        private SqlDataReader sqlr;
        private DataView dv;
        private string odgovorBaze;

        public IzbrisiKnjigu()
        {
            InitializeComponent();
            dgvKnjige.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKnjige.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            sqlconnect = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
            initGridView();
        }

        private void initGridView()
        {
            try {
                if (sqlconnect.State.Equals(ConnectionState.Closed))
                    sqlconnect.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT Knjiga.id_knjige, ime_knjige, ime, zanr FROM Knjiga, autori_i_knjige, Autori WHERE Knjiga.id_knjige = autori_i_knjige.id_knjige AND autori_i_knjige.id_autora = Autori.id_autora", sqlconnect);
                sqlDA.SelectCommand.CommandType = CommandType.Text;
                dt = new DataTable();
                sqlDA.Fill(dt);
                dgvKnjige.DataSource = dt;
                sqlconnect.Close();
            }
            catch(Exception ex)
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

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            if(numKnjigaId.Value >0 && numKnjigaId.Value != 0)
            {
                try
                {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlCommand sqlComm = new SqlCommand("sp_AzuriranjeKnjiga" , sqlconnect);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@tipazuriranja", "obrisi");
                    sqlComm.Parameters.AddWithValue("@idknjige", numKnjigaId.Value);

                    sqlr = sqlComm.ExecuteReader();

                    while (sqlr.Read())
                    {
                        odgovorBaze = sqlr[0].ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlconnect.Close();

                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlDataAdapter sqlRef = new SqlDataAdapter("SELECT Knjiga.id_knjige, ime_knjige, ime, zanr FROM Knjiga, autori_i_knjige, Autori WHERE Knjiga.id_knjige = autori_i_knjige.id_knjige AND autori_i_knjige.id_autora = Autori.id_autora", sqlconnect);
                    sqlRef.SelectCommand.CommandType = CommandType.Text;
                    dt = new DataTable();
                    sqlRef.Fill(dt);

                    dgvKnjige.DataSource = dt;
                    dgvKnjige.Update();
                    dgvKnjige.Refresh();

                    sqlconnect.Close();
                    MessageBox.Show(odgovorBaze);
                }
            }
            else
            {
                MessageBox.Show("Niste uneli id knjige.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKnjigu = new AzurirajKnjigu();
            azurirajKnjigu.Closed += (s, args) => this.Close();
            azurirajKnjigu.Show();
        }
    }
}
