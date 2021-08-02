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
    public partial class agekle : Form
    {
        int sayi = 0;
        int arac_no = 0;
        int garaj_no = 0;
        string arac_adi = "";
        string garaj_adi = "";
        string gun_adii = "";
        int[] gunu_no;

        MySqlConnection baglanti = new MySqlConnection("server = localhost;database=otoKiralama;uid=root;pwd=emre");
        public agekle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string garaj_goster = "select * from araclar";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(garaj_goster, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string seans_goster = "select * from garaj";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(seans_goster, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string seans_goster = "select * from gun";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(seans_goster, baglanti);
            DataTable tablo = new DataTable();
            baglanti.Open();
            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //<>
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            arac_no = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
                if (dataGridView3.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                    dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            garaj_no = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Red)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            else
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            gunu_no = new int[100];
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Red)
                    gunu_no[i] = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                sayi = dataGridView1.Rows.Count;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sayi; i++)
                if (gunu_no[i] > 0){
                    //4 tane kaydediyor! SORUN VAR
                    string toplamEkle = "insert into toplam(a_id,garaj_no,g_id) values(@a_id,@garaj_no,@g_id)";
                    MySqlCommand komut = new MySqlCommand(toplamEkle, baglanti);
                    komut.Parameters.AddWithValue("@a_id", arac_no);
                    komut.Parameters.AddWithValue("@garaj_no", garaj_no);
                    komut.Parameters.AddWithValue("@g_id", gunu_no[i]);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    button5_Click(sender, e);
                    
                }
            Array.Clear(gunu_no, 0, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string arac = "select a_ad,garaj_ad,gunu from araclar,garaj,gun,toplam where araclar.a_id = toplam.a_id and garaj.garaj_no = toplam.garaj_no and gun.g_id = gun.g_id;";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(arac, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView4.DataSource = tabloGoster;
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int toplamSayi = 0;
            string topm = "select toplam_id from toplam,araclar,garaj,gun where toplam.a_id = araclar.a_id and garaj.garaj_no = toplam.garaj_no and toplam.g_id =gun.g_id and a_ad ='" + arac_adi + "' and garaj_ad = '" + garaj_adi + "' and gunu ='" + gun_adii + "'";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand(topm, baglanti);
            MySqlDataReader okuma = komut.ExecuteReader();
            while (okuma.Read())
            {
                toplamSayi = Convert.ToInt32(okuma[0]);
            }
            baglanti.Close();
            string toplamSill = "delete from toplam where toplam_id = " + toplamSayi;
            baglanti.Open();
            MySqlCommand komutt = new MySqlCommand(toplamSill, baglanti);
            komutt.ExecuteNonQuery();
            baglanti.Close();
            button5_Click(sender, e);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                if (dataGridView4.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView4.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            arac_adi = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
            garaj_adi = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
            gun_adii = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
