using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KelimeOyunu2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            this.Close();
            f2.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //Ekle butonu
        {
            string id = textBox1.Text;
            string soru = textBox2.Text;
            string cevap = textBox3.Text;
            string harfsayisi = textBox4.Text;
            string sorgu="INSERT INTO oyundb values('"+id+"','"+soru+"','"+cevap+"','"+harfsayisi+"')";            
            VeritabanınaEkle(sorgu);

            MessageBox.Show("işlem başarılı");
        }

        MySqlConnection baglanti;
        private void Form3_Load(object sender, EventArgs e)
        {
            string bag;
            MySqlConnectionStringBuilder build = new MySqlConnectionStringBuilder();
            build.UserID = "root";
            build.Password = "1234";
            build.Database = "kelimeoyunu";
            build.Server = "localhost";

            bag = build.ToString();
            baglanti = new MySqlConnection(bag);

            VeriGoster();
        }
        public void VeriGoster()
        {
            string sql = "SELECT * FROM oyundb";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql;
            command.Connection = baglanti;
            adapter.SelectCommand = command;

            baglanti.Open();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Close();
        }

        public void VeritabanınaEkle(string sorgu)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VeriGoster();
        }
    }
}
