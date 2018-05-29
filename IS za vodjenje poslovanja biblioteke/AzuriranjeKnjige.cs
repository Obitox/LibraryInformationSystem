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
    public partial class AzuriranjeKnjige : Form
    {
        private DataTable dt;
        private SqlConnection sqlconnect;
        private SqlDataReader sqlr;
        private DataView dv;
        private string odgovorBaze;
        public AzuriranjeKnjige()
        {
            InitializeComponent();
            dgvKnjige.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKnjige.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dpDatumIzdavanja.Format = DateTimePickerFormat.Custom;
            dpDatumIzdavanja.CustomFormat = "yyyy-MM-dd";
            sqlconnect = new SqlConnection("Data Source = BOJAN; Initial Catalog = biblioteka; Integrated Security = True");
            initGridView();
        }

        private void initGridView()
        {
            try
            {
                if (sqlconnect.State.Equals(ConnectionState.Closed))
                    sqlconnect.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM view_PodaciZaAzuriranje", sqlconnect);
                sqlDA.SelectCommand.CommandType = CommandType.Text;
                dt = new DataTable();
                sqlDA.Fill(dt);
                dgvKnjige.DataSource = dt;
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
            if(numericUpDown1.Value > 0 && numericUpDown1.Value != 0)
            {
                try {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();

                    SqlCommand comm = new SqlCommand("sp_AzuriranjeKnjiga", sqlconnect);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@tipazuriranja", "azuriraj");
                    comm.Parameters.AddWithValue("@idknjige", numericUpDown1.Value);
                    comm.Parameters.AddWithValue("@datum_izdavanja", dpDatumIzdavanja.Text);
                    comm.Parameters.AddWithValue("@zanr", txtZanr.Text);
                    comm.Parameters.AddWithValue("@kolicina", numKolicina.Value);

                    sqlr = comm.ExecuteReader();

                    while (sqlr.Read())
                    {
                        odgovorBaze = sqlr[0].ToString();
                    }

                    sqlr.Close();
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

                    SqlDataAdapter sqlRef = new SqlDataAdapter("SELECT * FROM view_PodaciZaAzuriranje", sqlconnect);
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
                MessageBox.Show("Niste uneli dobru vrednost za id knjige.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value > 0 && numericUpDown1.Value != 0)
            {
                try {
                    if (sqlconnect.State.Equals(ConnectionState.Closed))
                        sqlconnect.Open();
                    SqlCommand myCommand = new SqlCommand("SELECT * FROM fun_PodaciZaAzuriranje(@idknjige)", sqlconnect);
                    myCommand.CommandType = CommandType.Text;
                    myCommand.Parameters.AddWithValue("@idknjige", numericUpDown1.Value);

                    sqlr = myCommand.ExecuteReader();

                    while (sqlr.Read())
                    {
                        numKolicina.Value = int.Parse(sqlr[0].ToString());
                        dpDatumIzdavanja.Text = sqlr[1].ToString();
                        txtZanr.Text = sqlr[2].ToString();
                    }

                    sqlr.Close();
                }
                catch(Exception ex)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKnjigu = new AzurirajKnjigu();
            azurirajKnjigu.Closed += (s, args) => this.Close();
            azurirajKnjigu.Show();
        }
    }
}
