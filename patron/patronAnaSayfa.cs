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
    public partial class patronAnaSayfa : Form
    {
        public patronAnaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            patronArac arac = new patronArac();
            arac.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            garaj arac = new garaj();
            arac.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gunEkle arac = new gunEkle();
            arac.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            agekle arac = new agekle();
            arac.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            personelekle personel = new personelekle();
            personel.Show();
            this.Hide();
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //patron klasöründe gözükmediği için personele aldım.
            personel.ciro ea = new personel.ciro();
            ea.Show();
            this.Hide();
        }

        
    }
}
