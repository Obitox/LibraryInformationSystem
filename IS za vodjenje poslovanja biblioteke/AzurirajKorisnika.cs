using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_za_vodjenje_poslovanja_biblioteke
{
    public partial class AzurirajKorisnika : Form
    {
        public AzurirajKorisnika()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var dodajKorisnika = new DodajKorisnika();
            dodajKorisnika.Closed += (s, args) => this.Close();
            dodajKorisnika.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var izbrisiKorisnika = new IzbrisiKorisnika();
            izbrisiKorisnika.Closed += (s, args) => this.Close();
            izbrisiKorisnika.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azuriranjeKorisnika = new AzuriranjeKorisnika();
            azuriranjeKorisnika.Closed += (s, args) => this.Close();
            azuriranjeKorisnika.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var administracija = new Administracija();
            administracija.Closed += (s, args) => this.Close();
            administracija.Show();
        }
    }
}
