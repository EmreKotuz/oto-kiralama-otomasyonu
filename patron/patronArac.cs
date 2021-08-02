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
    public partial class patronArac : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");
        public patronArac()
        {
            InitializeComponent();
            
        }
        int arac_id = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||
            textBox2.Text == "" ||
            textBox3.Text == "" ||
            textBox4.Text == "" ||
            textBox5.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bıkamayınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else{
                string filmEkle = "insert into araclar (a_ad,a_plaka,a_model,a_km,a_fiyat) values(@a_ad,@a_plaka,@a_model,@a_km,@a_fiyat)";
                MySqlCommand komut = new MySqlCommand(filmEkle, baglanti);
                komut.Parameters.AddWithValue("@a_ad", textBox1.Text);
                komut.Parameters.AddWithValue("@a_plaka", textBox2.Text);
                komut.Parameters.AddWithValue("@a_model", textBox3.Text);
                komut.Parameters.AddWithValue("@a_km", textBox4.Text);
                komut.Parameters.AddWithValue("@a_fiyat", textBox5.Text);


                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button2_Click(sender, e);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string aracGoster = "select * from araclar";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(aracGoster, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView1.DataSource = tabloGoster;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string araciguncelle = "update araclar set a_ad = @a_ad, a_plaka = @a_plaka,a_model =@a_model,a_km = @a_km where a_fiyat=@a_fiyat";
            MySqlCommand komut = new MySqlCommand(araciguncelle, baglanti);
            komut.Parameters.AddWithValue("@a_ad", textBox1.Text);
            komut.Parameters.AddWithValue("@a_plaka", textBox2.Text);
            komut.Parameters.AddWithValue("@a_model", textBox3.Text);
            komut.Parameters.AddWithValue("@a_km", textBox4.Text);
            komut.Parameters.AddWithValue("@a_fiyat", textBox5.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button2_Click(sender, e);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string arac_sil = "delete from araclar where a_id = @a_id";
            MySqlCommand komut = new MySqlCommand(arac_sil, baglanti);
            komut.Parameters.AddWithValue("@a_id",arac_id);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            button2_Click(sender, e);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            arac_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //<>
            //kımrmızı var ise
            for (int i = 0; i < dataGridView1.Rows.Count; i++)

                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Pink)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;

            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
        }
    }
}
