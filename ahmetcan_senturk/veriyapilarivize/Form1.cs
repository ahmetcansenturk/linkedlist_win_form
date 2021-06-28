using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace veriyapilarivize
{
   

    public partial class Form1 : Form
    {
        public class urun
        {
            public int kod;
            public string ad;
            public float fiyat;
            public urun onceki;
            public urun sonraki;

            public urun(int k, string a, float f)
            {
                kod = k;
                ad = a;
                fiyat = f;

            }
            public urun()
            {
             

            }
        }

        public class stok
        {
            public urun ilk;
            public urun son;
            public stok()
            {
                ilk = null;
                son = null;
            }
            public Boolean bosmu()
            {


                return ilk == null;
            }
            public void ekle(int k, string a, float f)
            {
                urun yeniurun = new urun(k, a, f);
  

                if (bosmu())
                {
                    ilk = yeniurun;
                    son = yeniurun;
                    son.sonraki = null;
                    MessageBox.Show("EKLENDİ");
                }


                else
                {

                    if (!esizMi(k))
                    {

                        son.sonraki = yeniurun;
                    yeniurun.onceki = son;
                    son = yeniurun;
                    son.sonraki = null;

                    MessageBox.Show("EKLENDİ");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Eşsiz Bir Değer Giriniz.");
                    }




                }

            }
            public void guncelle(int k, string a, float f)
            {
                urun yeniurun = new urun(k, a, f);
                if (bosmu())
                {
                    ilk = yeniurun;
                    son = yeniurun;
                   son.sonraki = null;
                    MessageBox.Show("Güncellendi.");
                }


                else
                {

                    son.sonraki = yeniurun;
                    yeniurun.onceki = son;
                    son = yeniurun;
                    son.sonraki = null;

                    MessageBox.Show("Güncellendi.");
                

                }

            }
            public Boolean esizMi(int kod)
            {

                urun kontrol = ilk;

                while (kontrol != null)
                {

                    if (kontrol.kod == kod)
                    {
                        return true;
                    }
                    kontrol = kontrol.sonraki;
                }
                return false;
            }

            public DataTable veriler()
            {
                urun g = ilk;
                DataTable dt = new DataTable();
                dt.Columns.Add("ÜRÜN KODU");
                dt.Columns.Add("ÜRÜN ADI");
                dt.Columns.Add("ÜRÜN FİYATI");

                while (g != null)
                {
                    DataRow drow = dt.NewRow();
                    drow["ÜRÜN KODU"] = g.kod;
                    drow["ÜRÜN ADI"] = g.ad;
                    drow["ÜRÜN FİYATI"] = g.fiyat;
                    dt.Rows.Add(drow);
                    g = g.sonraki;
                

                }
                return dt;



            }
            public urun seciliSil(int anahtar) 
            {
                urun aktif = ilk;
                while (aktif.kod != anahtar) 
                {
                    aktif = aktif.sonraki;
                    if (aktif == null)  return null; 
                
                }
                if (aktif == ilk) {
                    ilk = aktif.sonraki;
                }
               else if (aktif == son)
                {
                    son = aktif.onceki;
                }
                else {

                    aktif.onceki.sonraki = aktif.sonraki;
                    
                 //   aktif.sonraki.onceki = aktif.onceki;
                }
                return aktif; 
            }
            public urun bul(int anahtar) 
            {
                urun aktif = ilk;
                while (aktif.kod != anahtar) 
                {
                    aktif = aktif.sonraki; 
                    if (aktif == null)
                        return null; 
                }
          
                return aktif; 
            }

        }
        public Form1()
        {
            InitializeComponent();
        }

        urun gecici = new urun();
        stok st = new stok();
        int temp;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(textBox1.Text);
                string b = textBox2.Text;
                float c = float.Parse(textBox3.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                st.ekle(a, b, c);
             dataGridView1.DataSource = st.veriler();
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz değerleri kontrol ediniz.");
             
            }
     
               

  



        }
   

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text != "")
            {
                int ara = int.Parse(textBox7.Text);
                temp = int.Parse(textBox7.Text);

                urun bulunan = st.bul(ara);
                textBox7.Text = bulunan.kod.ToString();
                textBox8.Text = bulunan.ad.ToString();
                textBox9.Text = bulunan.fiyat.ToString();
            }
            else
            {
                MessageBox.Show("Geçersiz kod");
            }



           
              
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz değerleri kontrol ediniz.");

            }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                dataGridView1.DataSource = st.veriler();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir sorunla karşılaştık.");

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(textBox4.Text);
                st.seciliSil(a);

            
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                dataGridView1.DataSource = st.veriler();    
                MessageBox.Show(temp + "- Kodlu ürün silindi.");
            }
            catch (Exception)
            {

                MessageBox.Show("Girdiğiniz değerleri kontrol ediniz.");
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != "")
                {
                    temp = int.Parse(textBox4.Text);

                    urun bulunan = st.bul(temp);
                    if (bulunan != null)
                    {
                        textBox4.Text = bulunan.kod.ToString();
                        textBox5.Text = bulunan.ad.ToString();
                        textBox6.Text = bulunan.fiyat.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz kod");
                    }

                }
                else
                {
                    MessageBox.Show("Geçersiz kod");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Girdiğiniz değerleri kontrol ediniz.");
            }





        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                st.seciliSil(temp);
                st.guncelle(int.Parse(textBox7.Text), textBox8.Text, float.Parse(textBox9.Text));
                dataGridView1.DataSource = st.veriler();
            }
            catch (Exception)
            {

                MessageBox.Show("Girdiğiniz değerleri kontrol ediniz.");

            }
      
        

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
