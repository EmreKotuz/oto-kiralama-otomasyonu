using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace otoKiralama
{
    public partial class patron : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");

        public patron()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kullanıcıAdı.Text == "" && sifree.Text == "")
            {
                MessageBox.Show("Lütfen Boş Yerleri Doldurun", "Hata");
            }
            else
            {
                string k_ad = kullanıcıAdı.Text;
                string sifre = sifree.Text;

                string girisVeri = "select * from patron where k_adi = @k_adi and sifre = @sifre";
                MySqlCommand giris = new MySqlCommand(girisVeri, baglanti);
                giris.Parameters.AddWithValue("@k_adi", k_ad);
                giris.Parameters.AddWithValue("@sifre", sifre);
                baglanti.Open();

                MySqlDataReader oku = giris.ExecuteReader();
                if (oku.Read())
                {
                    patronAnaSayfa ne = new patronAnaSayfa();
                    ne.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifreyi kontrol ediniz!");
                    baglanti.Close();
                }
            }
        }
    }
}
