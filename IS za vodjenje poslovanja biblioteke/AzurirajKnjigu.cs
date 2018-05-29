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
    public partial class AzurirajKnjigu : Form
    {
        public AzurirajKnjigu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var dodajKnjigu = new DodajKnjigu();
            dodajKnjigu.Closed += (s, args) => this.Close();
            dodajKnjigu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var izbrisiKnjigu = new IzbrisiKnjigu();
            izbrisiKnjigu.Closed += (s, args) => this.Close();
            izbrisiKnjigu.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azuriranjeKnjige = new AzuriranjeKnjige();
            azuriranjeKnjige.Closed += (s, args) => this.Close();
            azuriranjeKnjige.Show();
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
