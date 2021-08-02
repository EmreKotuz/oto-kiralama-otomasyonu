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
    public partial class arackirala : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");

        int arac_id = 0;
        int garaj_no = 0;
        int gun_id = 0;
        int gunSayisi_no = 0;
        int otoyer_sayisi = 0;
        Button[] gunDizisi;
        int xkoordinat = 300;
        int yKoordinat = 50;

        public arackirala(int arac,int garaj,int gun)
        {
            InitializeComponent();
            arac_id = arac;
            garaj_no = garaj;
            gun_id = gun;
            toplamgun();
            kiralananAraclar();
            saatgunKontrol();
        }

        public void saatgunKontrol()
        {
            string şimdikiSaat = DateTime.Now.ToString("HH:mm");
            string seansSaati = "";            
            string gun_bilgi = "select gunu from gun where g_id = " + gun_id;
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand(gun_bilgi, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                seansSaati = okuma[0].ToString();
            }

            baglanti.Close();
            TimeSpan şimdi = TimeSpan.Parse(şimdikiSaat);
            TimeSpan seans = TimeSpan.Parse(seansSaati);
            if (şimdi.CompareTo(seans) == 1)
            {
                MessageBox.Show("Şimdiki saat daha büyüktür");
                for (int i = 0; i < otoyer_sayisi; i++)
                {
                    gunDizisi[i].Enabled = false;
                    gunDizisi[i].BackColor = Color.Blue;
                }
                MessageBox.Show("Gün saati geçmiştir");
            }
            if (şimdi.CompareTo(seans) == -1)
                MessageBox.Show("Gün saati daha büyüktür");
            if (şimdi.CompareTo(seans) == 0)
                MessageBox.Show("Saatler eşittir");



        }

        public void kiralananAraclar()
        {

            string secilentarih = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            string kiralananAraclar = "select gun_no from arac_kirala where a_id = " + arac_id + " and garaj_no = " + garaj_no + " and g_id = " + gun_id +" and tarih = "+secilentarih + "'";

            baglanti.Open();

            MySqlCommand komut = new MySqlCommand(kiralananAraclar, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                gunDizisi[Convert.ToInt32(okuma[0])].BackColor = Color.Blue;
                gunDizisi[Convert.ToInt32(okuma[0])].Enabled = false;
            }

            baglanti.Close();

        }


        public void toplamgun()
        {
            string k_sayı = "select count(gunu) from gun where garaj_no = " + garaj_no;

            MySqlCommand komut = new MySqlCommand(k_sayı, baglanti);

            baglanti.Open();

            MySqlDataReader okuma = komut.ExecuteReader();

            while (okuma.Read())
            {
                otoyer_sayisi = Convert.ToInt32(okuma[0]);
            }
            baglanti.Close();
            gunYaratma();
        }

        public void gunYaratma()
        {
            gunDizisi = new Button[otoyer_sayisi];


            for (int i = 1; i < otoyer_sayisi; i++)
                gunDizisi[i] = new Button();

            int n = 1;

            while (n < otoyer_sayisi)
            {
                gunDizisi[n].Text = n.ToString();
                gunDizisi[n].BackColor = Color.Red;
                gunDizisi[n].Height = 30;
                gunDizisi[n].Width = 50;

                gunDizisi[n].Left = xkoordinat;
                gunDizisi[n].Top = yKoordinat;

                xkoordinat = xkoordinat + gunDizisi[n].Width + 30;

                gunDizisi[n].Click += new EventHandler(gunRengi);


                Controls.Add(gunDizisi[n]);
                if (n % 5 == 0 && n == 6)
                {
                    xkoordinat = 300;
                    yKoordinat = yKoordinat + gunDizisi[n].Height + 10;
                }
                n++;
            }


        }

        public void gunRengi(object gönderici, EventArgs yakalayıcı)
        {          
            for (int i = 1; i < otoyer_sayisi; i++)
                if (gunDizisi[i].BackColor == Color.Green)
                    gunDizisi[i].BackColor = Color.Red;


            for (int i = 1; i < otoyer_sayisi; i++)
                if (gönderici.Equals(gunDizisi[i]))
                {
                    gunDizisi[i].BackColor = Color.Green;
                    gunSayisi_no = i;
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aracKirala = "insert into arac_kirala(a_id,garaj_no,g_id,gun_no,fiyat,tarih) values(@a_id,@garaj_no,@g_id,@gun_no,@fiyat,@tarih)";

            MySqlCommand komut = new MySqlCommand(aracKirala, baglanti);
            komut.Parameters.AddWithValue("@a_id", arac_id);
            komut.Parameters.AddWithValue("@garaj_no", garaj_no);
            komut.Parameters.AddWithValue("@g_id", gun_id);
            komut.Parameters.AddWithValue("@gun_no", gunSayisi_no);
            komut.Parameters.AddWithValue("@fiyat", Convert.ToInt32(label1.Text));
            komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

        }

        private void arackirala_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date < DateTime.Now.Date)
            {
                for (int i = 1; i < otoyer_sayisi; i++)
                {
                    gunDizisi[i].Enabled = false;
                    gunDizisi[i].BackColor = Color.Blue;
                }
            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                for (int i = 1; i < otoyer_sayisi; i++)
                {
                    gunDizisi[i].Enabled = true;
                    gunDizisi[i].BackColor = Color.Red;
                }
            }
            else 
            {
                for (int i = 1; i < otoyer_sayisi; i++)
                {
                    gunDizisi[i].Enabled = true;
                    gunDizisi[i].BackColor = Color.Red;
                }
                saatgunKontrol();
            }

        }

    }
}