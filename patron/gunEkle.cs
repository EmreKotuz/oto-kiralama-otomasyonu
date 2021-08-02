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
    public partial class gunEkle : Form
    {
        int gun_id = 0;
        MySqlConnection baglanti = new MySqlConnection("server=localhost;database=otoKiralama;uid=root;pwd=emre");
        public gunEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tarihEkle = "insert into gun (gunu) values(@gunu)";
            MySqlCommand komut = new MySqlCommand(tarihEkle, baglanti);
            komut.Parameters.AddWithValue("@gunu", textBox1.Text);


            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gunGoster = "select * from gun";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(gunGoster, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView1.DataSource = tabloGoster;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string seansGuncelle = "update gun set gunu = @gunu where g_id = @g_id";
            MySqlCommand komut = new MySqlCommand(seansGuncelle, baglanti);
            komut.Parameters.AddWithValue("@g_id", gun_id);
            komut.Parameters.AddWithValue("@gunu", textBox1.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Text = "";
            button2_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gun_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string seans_sil = "delete from gun where g_id = @g_id";
            MySqlCommand komut = new MySqlCommand(seans_sil, baglanti);
            komut.Parameters.AddWithValue("@g_id", gun_id);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            button2_Click(sender, e);
        }
    }
}
