using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace otoKiralama.personel
{
    public partial class ciro : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");
        public ciro()
        {
            InitializeComponent();
            arac();
        }
        public void arac()
        {
            string arac = "select a_ad from arac";
            MySqlCommand komut = new MySqlCommand(arac, baglanti);

            baglanti.Open();

            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                comboBox1.Items.Add(Convert.ToString(okuma[0]));
            }
            baglanti.Close();
            comboBox1.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int aylikciro = 0;
            string aylik_ciro = "select sum(fiyat)from arac_kirala where tarih > date_sub(curdate(),interval 1 month)";

            MySqlCommand komut = new MySqlCommand(aylik_ciro, baglanti);
            baglanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();


            while (okuma.Read())
            {
                aylikciro = Convert.ToInt32(okuma[0]);
            }

            baglanti.Close();
            textBox1.Text = aylikciro.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int yillikCiro = 0;
            string yıllık_ciro = "select sum(fiyat)from arac_kirala where tarih > date_sub(curdate(),interval 1 year)";

            MySqlCommand komut = new MySqlCommand(yıllık_ciro, baglanti);

            baglanti.Open();

            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                yillikCiro = Convert.ToInt32(okuma[0]);
            }

            baglanti.Close();
            textBox2.Text = yillikCiro.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ciro = 0;
            string yıllık_ciro = "select sum(fiyat)from arac_kirala where tarih > @bir and tarih < @iki)";

            MySqlCommand komut = new MySqlCommand(yıllık_ciro, baglanti);
            komut.Parameters.AddWithValue("@bir", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@iki", dateTimePicker2.Value);
            baglanti.Open();

            MySqlDataReader okuma = komut.ExecuteReader();


            while (okuma.Read())
            {
                ciro = Convert.ToInt32(okuma[0]);
            }
            baglanti.Close();
            textBox3.Text = ciro.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ciroaylik = 0;
            string aylikciro="select sum(fiyat)from arac_kirala,araclar where araclar.a_id=arac_kirala.a_id and a_ad ="+comboBox1.Text+" and tarih > date_sub(curdate(),interval 1 month)";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand(aylikciro, baglanti); 
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                ciroaylik = Convert.ToInt32(okuma[0]);
            }
            textBox4.Text = ciroaylik.ToString();
            baglanti.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int ciroaylik = 0;
            string aylikciro = "select sum(fiyat)from arac_kirala,araclar where araclar.a_id=arac_kirala.a_id and a_ad =" + comboBox1.Text + " and tarih > date_sub(curdate(),interval 1 year)";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand(aylikciro, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                ciroaylik = Convert.ToInt32(okuma[0]);
            }
            textBox4.Text = ciroaylik.ToString();
            baglanti.Close();
        }
    }
}
