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
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace KelimeOyunu2
{
    public partial class Form2 : Form
    {                 
        public Form2()
        {
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            BaglantiKur();            
        }
       
        void SoruVer(int sorusayac)     //Soru Ver
        {
            if (sorusayac < 15)
            {
                label2.Text = quests[sorusayac];

                int harfsayisi = harfSayisiList[sorusayac - 1];
                for (int i = 0; i < harfsayisi; i++)
                {
                    AcilmayanlarList.Add(i);
                }

                label3.Text = AcilmayanlarList.Count.ToString();
                timer2.Start();

                textBox1.Text = String.Empty;
                button13.Enabled = false;     //cevapla butonu                           
                HarfButonGoster(sorusayac);
                ButonTemizle();
                button11.Enabled = true;
                timer1.Stop();
                kalansure = 20;
                label4.Text = "20";
                label4.Visible = false;
            }
            else
            {
                timer2.Stop();
                timer1.Stop();
                MessageBox.Show("Sorular tamamlandı");
                OyunuBitir();                
            }                        
        }
        void HarfAl(int sorusayac)   //Harf Al Fonksiyonu
        {
            Random rnd = new Random();

            int RandomHarf = AcilmayanlarList[rnd.Next(0,AcilmayanlarList.Count)];

            string kelime = answers[sorusayac];
            string harf1 = kelime.ToCharArray()[RandomHarf].ToString();

            if (AcilmayanlarList.Contains(RandomHarf) == true)
            {
                if (AcilmayanlarList.Count > 2)
                {
                    Yazdir(RandomHarf, harf1);
                    AcilmayanlarList.Remove(RandomHarf);                    
                }
                else if (AcilmayanlarList.Count == 2)
                {
                    Yazdir(RandomHarf, harf1);
                    AcilmayanlarList.Remove(RandomHarf);
                    
                    button11.Enabled = false;                    
                }

            }
        }

        int sorusayac = 1;
        string[] sql_sorular = new string[15];
        string[] quests = new string[15];
        string[] answers = new string[15];
        List <int> harfSayisiList=new List<int>();
        List<int> AcilmayanlarList = new List<int>();
        public void BaglantiKur()
        {            
            MySqlConnection con = new MySqlConnection("host=localhost;user=root;password=1234;database=kelimeoyunu;");
            
            for (int i = 1; i < 15; i++)
            {
                sql_sorular[i] = "SELECT * FROM oyundb where id=" + i;
                MySqlCommand cmd = new MySqlCommand(sql_sorular[i], con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    quests[i] = reader.GetString("sorular");
                    answers[i] = reader.GetString("cevaplar");                    
                    harfSayisiList.Add(Convert.ToInt32(reader.GetString("harf_sayisi")));
                }
                con.Close();
            }            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        int puan=0;
        private void button13_Click(object sender, EventArgs e)  //Cevapla Butonu
        {
            string cevap_inp = answers[sorusayac];          
            if (textBox1.Text == cevap_inp)
            {
                timer1.Stop();
                MessageBox.Show("Doğru!Tebrikler");

                puan = puan + AcilmayanlarList.Count * 100;
                label9.Text = puan.ToString();
                sorusayac++;                
                AcilmayanlarList.Clear();
                SoruVer(sorusayac);
            }
            else
            {
                MessageBox.Show("Yanlış");                
            }

        }

        private void button14_Click(object sender, EventArgs e)  //Giriş Yap Butonu
        {
            s = 60;
            m = 3;
            button11.Enabled = true;
            button12.Enabled = true;
            label3.Visible = true;
            label11.Visible = true;

            ButonKapat();

            label2.Text = quests[sorusayac];           
            label3.Text = harfSayisiList[sorusayac-1].ToString();
            int harfsayisi = harfSayisiList[0];
            for (int i = 0; i < harfsayisi; i++)
            {
                AcilmayanlarList.Add(i);
            }
            timer2.Start();
            button14.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)  //Süreyi Durdur
        {
            button13.Enabled = true;  //Cevapla butonu aç
            button11.Enabled = false;  //Harf Al butonu kapat
            label4.Visible = true;
            timer2.Stop();
            timer1.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            HarfAl(sorusayac);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        void ButonKapat()
        {
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
        }

        void Yazdir(int RandomHarf, string harf1)
        {
            if (RandomHarf == 0)
            {
                button1.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 1)
            {
                button2.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 2)
            {
                button3.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 3)
            {
                button4.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 4)
            {
                button5.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 5)
            {
                button6.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 6)
            {
                button7.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 7)
            {
                button8.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 8)
            {
                button9.Text = harf1.ToUpper();
            }
            else if (RandomHarf == 9)
            {
                button10.Text = harf1.ToUpper();
            }
        }

        void ButonTemizle()
        {
            button1.Text = String.Empty;
            button2.Text = String.Empty;
            button3.Text = String.Empty;
            button4.Text = String.Empty;
            button5.Text = String.Empty;
            button6.Text = String.Empty;
            button7.Text = String.Empty;
            button8.Text = String.Empty;
            button9.Text = String.Empty;
            button10.Text = String.Empty;
        }

        void HarfButonGoster(int sorusayac)
        {
            if (sorusayac == 3)
            {
                button5.Visible = Enabled;
            }
            else if (sorusayac == 5)
            {
                button6.Visible = Enabled;
            }
            else if (sorusayac == 7)
            {
                button7.Visible = Enabled;
            }
            else if (sorusayac == 9)
            {
                button8.Visible = Enabled;
            }
            else if (sorusayac == 11)
            {
                button9.Visible = Enabled;
            }
            else if (sorusayac == 13)
            {
                button10.Visible = Enabled;
            }
        }

        int kalansure = 20;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (kalansure > 0)
            {
                kalansure--;
            }
            else if (kalansure == 0)
            {
                puan = puan + 0;
                label9.Text = puan.ToString();

                sorusayac++;
                AcilmayanlarList.Clear();
                SoruVer(sorusayac);                                               
            }

            label4.Text = ":" + kalansure;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        int s = 60;
        int m = 3;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (s > 0)
            {
                s--;
            }
            else if (s == 0 && m == 0)
            {
                timer2.Stop();
                MessageBox.Show("Süre Bitti.");
                OyunuBitir();
            }
            else if (s == 0)
            {
                m -= 1;
                s = 59;                               
            }
            

            label7.Text = m+":"+s;
        }

        void OyunuBitir()
        {
            string oyuncu = label1.Text;
            string tarih = label10.Text;
            timer1.Stop();
            timer2.Stop();
            AcilmayanlarList.Clear();
            sorusayac = 1;
            
            button14.Visible = true;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;

            textBox1.Clear();
            ButonTemizle();

            //m = 3 - m;
            //s = 60 - s;
            //if (s == 60)
            //{
            //    s = 00;
            //    m = m + 1;
            //}

            MessageBox.Show("Oyuncu:"+oyuncu + "\n" +"Tarih:"+ tarih + "\n" +"Toplam Puan:"+ puan+ "\n" + "Kalan Süre:" +m+":"+s);
            string k_sure = m + ":" + s;
            string[] satirlar = { oyuncu , tarih , puan.ToString() , k_sure };
            System.IO.File.WriteAllLines(@"E:\deneme\oyun.txt",satirlar);

            puan = 0;

            Form1 f1 = new Form1();
            this.Close();
            f1.Show();
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            this.Close();
            f2.Show();
        }

        public void DosyaYazdir()
        {

        }
    }



    
}
