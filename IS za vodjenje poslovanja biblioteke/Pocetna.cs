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
    public partial class Pocetna : Form
    {
        public Pocetna()
        {
            InitializeComponent();
        }

        private void btnLogovanje_Click(object sender, EventArgs e)
        {
            this.Hide();
            var logovanje = new Logovanje();
            logovanje.Closed += (s, args) => this.Close();
            logovanje.Show();
        }

        private void btnRegistracija_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registracija = new Registracija();
            registracija.Closed += (s, args) => this.Close();
            registracija.Show();
        }
    }
}
