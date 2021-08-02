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
    public partial class yoneticiGiris : Form
    {
        MySqlConnection bağlanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");
        public yoneticiGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string k_ad = kullaniciAdi.Text;
            string sifre = sif.Text;

            string giris = "select * from yoneticiEkle where kullanici_adi = @kullanici_adi and sifre = @sifre";
            MySqlCommand komut = new MySqlCommand(giris, bağlanti);
            komut.Parameters.AddWithValue("@kullanici_adi", k_ad);
            komut.Parameters.AddWithValue("@sifre", sifre);
            bağlanti.Open();
            MySqlDataReader okuma = komut.ExecuteReader();
            if (okuma.Read()){
                otoKiralaGiris otoKirala = new otoKiralaGiris();
                otoKirala.Show();
                this.Hide();
            }
            else{
                MessageBox.Show("Kullanıcı adı veya şifreyi kontrol ediniz!","Giriş Başarısız!");
                bağlanti.Close();
            }
        }
    }
}
