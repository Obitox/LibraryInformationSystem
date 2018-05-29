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
    public partial class DodajKnjigu : Form
    {
        private SqlConnection sqlconnect;
        private SqlDataReader sqlRead;
        public DodajKnjigu()
        {
            InitializeComponent();
            sqlconnect = new SqlConnection("Data Source=BOJAN;Initial Catalog=biblioteka;Integrated Security=True");
            dpDatumIzdavanja.Format = DateTimePickerFormat.Custom;
            dpDatumIzdavanja.CustomFormat = "yyyy-MM-dd";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int prazanBoxBrojac = 0;
            string odgovorBaze = "";
            string prazniBoxovi = "";

            foreach (Control controls in this.Controls)
            {
                if (controls is TextBox)
                {
                    TextBox textBox = controls as TextBox;
                    if (textBox.TextLength > 0)
                    {
                        prazanBoxBrojac++;
                    }
                    else
                    {
                        if (textBox.Text == string.Empty)
                        {
                            prazniBoxovi += "\n" + textBox.Name.Substring(3) + "\n";
                        }
                    }
                }
                if (controls is NumericUpDown)
                {
                    NumericUpDown numkol = controls as NumericUpDown;
                    if (numkol.Value > 0 && numkol.Value != 0)
                    {
                        prazanBoxBrojac++;
                    }
                    else
                    {
                        prazniBoxovi += "\n" + numkol.Name.Substring(3) + "\n";
                    }
                }
            }


            if (prazanBoxBrojac >= 5)
            {
                if (sqlconnect.State.Equals(ConnectionState.Closed))
                    sqlconnect.Open();

                try
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_AzuriranjeKnjiga", sqlconnect);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@tipazuriranja", "dodaj");
                    sqlCommand.Parameters.AddWithValue("@imeknjige", txtImeKnjige.Text);
                    sqlCommand.Parameters.AddWithValue("@datum_izdavanja", dpDatumIzdavanja.Text);
                    sqlCommand.Parameters.AddWithValue("@zanr", txtZanr.Text);
                    sqlCommand.Parameters.AddWithValue("@kolicina", numKolicina.Value);
                    sqlCommand.Parameters.AddWithValue("@imeautora", txtImeAutora.Text);
                    sqlCommand.Parameters.AddWithValue("@prezimeautora", txtPrezimeAutora.Text);

                    sqlRead = sqlCommand.ExecuteReader();

                    while (sqlRead.Read())
                    {
                        odgovorBaze = sqlRead[0].ToString();
                    }
                    odgovorBaze = Regex.Replace(odgovorBaze, @"\s+", " ");

                    sqlRead.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    MessageBox.Show(odgovorBaze);
                }
            }
            else
            {
                MessageBox.Show("Niste uneli podatke u sledeće/sledeći polje/polja:\n" + prazniBoxovi);
                Console.WriteLine(prazanBoxBrojac);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKnjigu = new AzurirajKnjigu();
            azurirajKnjigu.Closed += (s, args) => this.Close();
            azurirajKnjigu.Show();
        }
    }
}
