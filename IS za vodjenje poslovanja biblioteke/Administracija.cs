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
    public partial class Administracija : Form
    {
        public Administracija()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKnjigu = new AzurirajKnjigu();
            azurirajKnjigu.Closed += (s, args) => this.Close();
            azurirajKnjigu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var azurirajKorisnika = new AzurirajKorisnika();
            azurirajKorisnika.Closed += (s, args) => this.Close();
            azurirajKorisnika.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var resavanjeRezervacije = new ResavanjeRezervacije();
            resavanjeRezervacije.Closed += (s, args) => this.Close();
            resavanjeRezervacije.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var logovanje = new Logovanje();
            logovanje.Closed += (s, args) => this.Close();
            logovanje.Show();
        }
    }
}
