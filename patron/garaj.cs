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
    public partial class garaj : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost;database=otoKiralama;uid=root;pwd=emre");
        public garaj()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string salon_ekle = "insert into garaj values(@garaj_no,@garaj_ad,@garaj_kapasite)";
            MySqlCommand komut = new MySqlCommand(salon_ekle, baglanti);
            komut.Parameters.AddWithValue("@garaj_no", textBox1.Text);
            komut.Parameters.AddWithValue("@garaj_ad", textBox2.Text);
            komut.Parameters.AddWithValue("@garaj_kapasite", textBox3.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            int garajKapasite = Convert.ToInt32(textBox3.Text);
            int salonNo = Convert.ToInt32(textBox1.Text);
            //<>
            for (int i = 0; i < garajKapasite; i++)
            {
                string koltukEkle = "insert into otoYer(o_no,o_durumu,garaj_no) values(@o_no,@o_durumu,@garaj_no)";
                MySqlCommand komutKoltuk = new MySqlCommand(koltukEkle, baglanti);
                komutKoltuk.Parameters.AddWithValue("@o_no", i);
                komutKoltuk.Parameters.AddWithValue("@o_durumu", 0);
                komutKoltuk.Parameters.AddWithValue("@garaj_no", salonNo);
                baglanti.Open();
                komutKoltuk.ExecuteNonQuery();
                baglanti.Close();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string salonGoster = "select * from garaj";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(salonGoster, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView1.DataSource = tabloGoster;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gGuncelle = "update garaj set garaj_ad = @garaj_ad, garaj_kapasitesi = @garaj_kapasitesi where garaj_no = @garaj_no";
            MySqlCommand komut = new MySqlCommand(gGuncelle, baglanti);
            komut.Parameters.AddWithValue("@garaj_no", textBox1.Text);
            komut.Parameters.AddWithValue("@garaj_ad", textBox2.Text);
            komut.Parameters.AddWithValue("@garaj_kapasitesi", textBox3.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button2_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string g_sil = "delete from garaj where garaj_no = @garaj_no";
            MySqlCommand komut = new MySqlCommand(g_sil, baglanti);
            komut.Parameters.AddWithValue("@garaj_no", textBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            button2_Click(sender, e);
        }
    }
}
