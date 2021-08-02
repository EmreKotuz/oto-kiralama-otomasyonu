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
    public partial class personelekle : Form
    {
        MySqlConnection baglan = new MySqlConnection("server = localhost;database=otoKiralama;uid=root;pwd=emre");
        public personelekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yonetici = "insert into yoneticiEkle (y_ad, y_soyad, kullanici_adi, sifre) values(@y_ad, @y_soyad, @kullanici_adi, @sifre)";
            MySqlCommand komut = new MySqlCommand(yonetici, baglan);
            komut.Parameters.AddWithValue("@y_ad", textBox1.Text);
            komut.Parameters.AddWithValue("@y_soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@kullanici_adi", textBox3.Text);
            komut.Parameters.AddWithValue("@sifre", textBox4.Text);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
