using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace veritabanlikutuphaneprojem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YILDIRIM\\SQLEXPRESS; Initial Catalog = Kutuphane; Integrated Security = True");    

        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("select * from kitaplar");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Kitaplar (Ad,Yazar,Sayfano,Basımevi,Tur) values (@adi,@yazari,@sayfanosu,@basimyeri,@turu)", baglanti);
            komut.Parameters.AddWithValue("@adi", textBox1.Text);
            komut.Parameters.AddWithValue("@yazari", textBox2.Text);
            komut.Parameters.AddWithValue("@sayfanosu", textBox3.Text);
            komut.Parameters.AddWithValue("@basimyeri", textBox4.Text);
            komut.Parameters.AddWithValue("@turu", textBox5.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select * from kitaplar");
            baglanti.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from kitaplar where ad=@adi", baglanti);
            komut.Parameters.AddWithValue("@adi", textBox6.Text);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from Kitaplar");
            baglanti.Close();

            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from Kitaplar where Ad like '%" + textBox7.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string Ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string Yazar = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string Sayfano = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string Basımevi = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string Tur = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();

            textBox1.Text = Ad;
            textBox2.Text = Yazar;
            textBox3.Text = Sayfano;
            textBox4.Text = Basımevi;
            textBox5.Text = Tur;

            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Kitaplar set Yazar='" + textBox2.Text + "',Sayfano='" + textBox3.Text + "',Basımevi='" + textBox4.Text + "',Tur='" + textBox5.Text + "' where Ad= '" + textBox1.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            verilerigoster("select * from Kitaplar");
            baglanti.Close();
            
        }

       
    }
}
