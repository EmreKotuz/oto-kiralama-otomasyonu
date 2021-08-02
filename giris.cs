using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace otoKiralama
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            patron patron = new patron();
            patron.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            personel.yoneticiGiris yonetici = new personel.yoneticiGiris();
            yonetici.Show();
            this.Hide();
        }
    }
}
