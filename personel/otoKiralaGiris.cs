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
    public partial class otoKiralaGiris : Form
    {
        MySqlConnection baglanti = new MySqlConnection("server=localhost; database=otoKiralama;uid=root;pwd=emre");
        string arac_adi = "";
        string garaj_adi = "";



        int arac_id = 0;
        int garaj_no = 0;
        int gun_id = 0;



        public otoKiralaGiris()
        {
            InitializeComponent();
            araclarKatalogu();
        }
        public void araclarKatalogu()
        {
            string araclariGoster = "select a_ad,a_plaka,a_model,a_km,a_id from araclar;";
            MySqlDataAdapter adaptor = new MySqlDataAdapter(araclariGoster, baglanti);
            DataTable tablo_Goster = new DataTable();
            adaptor.Fill(tablo_Goster);
            baglanti.Open();
            dataGridView1.DataSource = tablo_Goster;
            dataGridView1.Columns[4].Visible = false;
            baglanti.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            arac_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            string arac_adi = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

            string garajGoster = "select distinct(garaj_ad),garaj_kapasitesi,garaj.garaj_no from garaj,araclar,toplam where toplam.a_id = araclar.a_id and toplam.garaj_no=garaj.garaj_no and a_ad ='" + arac_adi + "'";

            MySqlDataAdapter adp = new MySqlDataAdapter(garajGoster, baglanti);
            DataTable tabloGoster = new DataTable();
            adp.Fill(tabloGoster);
            baglanti.Open();
            dataGridView2.DataSource = tabloGoster;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            garaj_no = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            string garaj_adi = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string arac_adi = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string guns = "select gunu,gun.g_id from araclar,garaj,gun,toplam where araclar.a_id = toplam.a_id and garaj.garaj_no = toplam.garaj_no and gun.g_id = toplam.g_id and a_ad = '" + arac_adi + "' and garaj_ad = '" + garaj_adi + "'";

            MySqlDataAdapter adaptor = new MySqlDataAdapter(guns, baglanti);
            DataTable tabloGoster = new DataTable();
            adaptor.Fill(tabloGoster);
            baglanti.Open();
            dataGridView3.DataSource = tabloGoster;
            dataGridView3.Columns[2].Visible = false;
            baglanti.Close();

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gun_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            arackirala a = new arackirala(arac_id, garaj_no,gun_id);
            a.Show();
            this.Hide();
        }

        private void otoKiralaGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
