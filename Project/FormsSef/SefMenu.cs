using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantAtlantis
{
    public partial class SefMenu : Form
    {
        public SefMenu()
        {
            InitializeComponent();
        }

        // Baglanti
        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");
        
        private void kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefAnaSayfa gitSefAnaSayfa = new SefAnaSayfa();
            gitSefAnaSayfa.Show();
            this.Hide();
        }

        private void Profil_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefProfiller gitSefProfiller = new SefProfiller();
            gitSefProfiller.Show();
            this.Hide();
        }

        private void Yorumlar_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefYorumlar gitSefYorumlar = new SefYorumlar();
            gitSefYorumlar.Show();
            this.Hide();
        }

        private void Siparisler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefBilanço gitSefBilanço = new SefBilanço();
            gitSefBilanço.Show();
            this.Hide();
        }

        bool hareket; int mouseX; int mouseY;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*empty*/
        }

        // Şu anki KDV değerini yükler
        private void SefMenu_Load(object sender, EventArgs e)
        {
            this.panel7.Visible = false; // KDV Panelini gizler
            this.panel6.Visible = false; // Ürün Düzenleme Panelini gizler
            this.panel8.Visible = false; // Ürün İsmi Panelini gizler
            this.panel9.Visible = false; // Ürün Fiyat Panelini gizler
            this.panel10.Visible = false; // Ürün Resim Panelini gizler
            this.panel12.Visible = false; // Tarif Paneli gizlenir

            this.panel5.Visible = true; // Tercih Menüsünü gösterir

            SqlCommand kdv = new SqlCommand("select * from Restoran where id=1", baglantim);

            baglantim.Open();

            SqlDataReader drkdv = kdv.ExecuteReader();

            if (drkdv.HasRows)
            {
                drkdv.Read();
                label2.Text = (string)drkdv["KDV"];
            }
            drkdv.Close();

            baglantim.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear(); // Kaldırılırsa ürünler sürekli eklenmeye devam eder.
            comboBox2.Text = "";

            /* Seçilen Menüye göre ürünlerin listelenmesi */

            baglantim.Open();

            // Ara Sıcaklar

            if (comboBox1.Text == "Ara Sıcaklar")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from AraSicaklar", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }
                // Ana Yemekler

            if (comboBox1.Text == "Ana Yemekler")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from AnaYemekler", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }        
            }
             // Tatlılar
                    
            if (comboBox1.Text == "Tatlılar")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Tatlılar", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }

            // İçecekler

            if (comboBox1.Text == "İçecekler")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Icecekler", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }
            baglantim.Close();
        }

        // Ek Ödemeler butonu
        private void button3_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel6.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*empty*/
        }

        // KDV değeri (text sadece rakam)
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // KDV Kaydet Butonu
        private void button5_Click(object sender, EventArgs e)
        {                
            // KDV değerini girilen değerle değiştirir.
            if (textBox1.Text != "")
            {
                baglantim.Open();
                SqlCommand kdv = new SqlCommand("update Restoran set kdv=@KDV where id=1", baglantim);
                kdv.Parameters.AddWithValue("@KDV", textBox1.Text);
                kdv.ExecuteNonQuery();
                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "KDV Değeri başarıyla güncellendi.", ToolTipIcon.Info);
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir değer giriniz.", "Hatalı Girdi");
            }

            // Kaydet'ten sonra şu anki KDV değerini günceller.
            SqlCommand guncelkdv = new SqlCommand("select * from Restoran where id=1", baglantim);

            baglantim.Open();

            SqlDataReader drkdv = guncelkdv.ExecuteReader();

            if (drkdv.HasRows)
            {
                drkdv.Read();
                label2.Text = (string)drkdv["KDV"];
            }
            drkdv.Close();

            baglantim.Close();

        }

        // KDV Geri Dön butonu
        private void button4_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            textBox1.Text = "";
            this.panel5.Visible = true;
        }

        // Ürün Detayları butonu
        private void button2_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen seçim yapınız.", ToolTipIcon.Warning);
            }
            else
            {
                baglantim.Open();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    // Ara Sıcakları yükler
                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();

                        // İsim ve Fiyat
                        label6.Text = (string)dryemek["ISIM"];
                        label7.Text = (string)dryemek["FIYAT"];

                        // Resim
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        button6.BackgroundImage = Image.FromStream(stream);

                        // Menü - Menü dışı
                        bool kontrol = Convert.ToBoolean(dryemek["DURUM"]);

                        dryemek.Close();

                        if (kontrol == false)
                        {
                            this.button16.Visible = true; // Menü Dışı butonunu getirir
                            this.button9.Visible = false; // Menüde butonunu gizler
                        }
                        else
                        {
                            this.button16.Visible = false; // Menü Dışı butonunu gizler
                            this.button9.Visible = true; // Menüde butonunu getirir
                        }
                    }
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    // Ana Yemekleri yükler
                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();

                        // İsim ve Fiyat
                        label6.Text = (string)dryemek["ISIM"];
                        label7.Text = (string)dryemek["FIYAT"];

                        // Resim
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        button6.BackgroundImage = Image.FromStream(stream);

                        // Menü - Menü dışı
                        bool kontrol = Convert.ToBoolean(dryemek["DURUM"]);

                        dryemek.Close();

                        if (kontrol == false)
                        {
                            this.button16.Visible = true; // Menü Dışı butonunu getirir
                            this.button9.Visible = false; // Menüde butonunu gizler
                        }
                        else
                        {
                            this.button16.Visible = false; // Menü Dışı butonunu gizler
                            this.button9.Visible = true; // Menüde butonunu getirir
                        }
                    }
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    // Tatlıları yükler
                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();

                        // İsim ve Fiyat
                        label6.Text = (string)dryemek["ISIM"];
                        label7.Text = (string)dryemek["FIYAT"];

                        // Resim
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        button6.BackgroundImage = Image.FromStream(stream);

                        // Menü - Menü dışı
                        bool kontrol = Convert.ToBoolean(dryemek["DURUM"]);

                        dryemek.Close();

                        if (kontrol == false)
                        {
                            this.button16.Visible = true; // Menü Dışı butonunu getirir
                            this.button9.Visible = false; // Menüde butonunu gizler
                        }
                        else
                        {
                            this.button16.Visible = false; // Menü Dışı butonunu gizler
                            this.button9.Visible = true; // Menüde butonunu getirir
                        }
                    }
                }

                if (comboBox1.Text == "İçecekler")
                {
                    // İçecekleri yükler
                    SqlCommand yemek = new SqlCommand("select * from Icecekler where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();

                        // İsim ve Fiyat
                        label6.Text = (string)dryemek["ISIM"];
                        label7.Text = (string)dryemek["FIYAT"];

                        // Resim
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        button6.BackgroundImage = Image.FromStream(stream);

                        // Menü - Menü dışı
                        bool kontrol = Convert.ToBoolean(dryemek["DURUM"]);

                        dryemek.Close();

                        if (kontrol == false)
                        {
                            this.button16.Visible = true; // Menü Dışı butonunu getirir
                            this.button9.Visible = false; // Menüde butonunu gizler
                        }
                        else
                        {
                            this.button16.Visible = false; // Menü Dışı butonunu gizler
                            this.button9.Visible = true; // Menüde butonunu getirir
                        }
                    }
                }               
                this.panel5.Visible = false;
                this.panel7.Visible = true;

                baglantim.Close();
            }
        }

        // Geri Dön Butonu
        private void button8_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            textBox2.Text = "";

            this.panel8.Visible = false; // Ürün isim panelini gizler
            this.panel9.Visible = false; // Ürün fiyat panelini gizler
            this.panel10.Visible = false; // Ürün Resim panelini gizler
            pictureBox3.Image = null; // Resmi siler

            /* Seçilen Menüye göre ürünlerin listelenmesi */

            baglantim.Open();

            // Ara Sıcaklar

            if (comboBox1.Text == "Ara Sıcaklar")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from AraSicaklar", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }
            // Ana Yemekler

            if (comboBox1.Text == "Ana Yemekler")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from AnaYemekler", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }
            // Tatlılar

            if (comboBox1.Text == "Tatlılar")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Tatlılar", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }

            // İçecekler

            if (comboBox1.Text == "İçecekler")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Icecekler", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox2.Items.Add(dr["ISIM"].ToString());
                }
            }
            baglantim.Close();

            this.panel7.Visible = false;
            this.panel5.Visible = true;
        }

        // İsim Butonu (Paneli açar)
        private void button7_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = true; // Ürün İsim düzenleme panelini gösterir
            this.panel9.Visible = false; // Ürün Fiyat düzenleme panelini gizler
            this.panel10.Visible = false; // Ürün Resim düzenleme panelini gizler

            button8.Enabled = false; // Geri dön butonu devre dışı kalır

            /* pictureBox3'teki resmi varsayılan haline geri getirir */

            baglantim.Open();

            SqlCommand cresim = new SqlCommand("select * from Resimler where id=34", baglantim);
            SqlDataReader dresim = cresim.ExecuteReader();

            if (dresim.Read())
            {
                if (dresim["RESIM"] != null)
                {
                    byte[] resim2 = new byte[0];
                    resim2 = (byte[])dresim["RESIM"];
                    MemoryStream stream = new MemoryStream(resim2);
                    pictureBox3.BackgroundImage = Image.FromStream(stream);
                }
            }
            dresim.Close();

            baglantim.Close();
        }

        // İsim paneli (iptal)
        private void button13_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = false;

            this.button8.Enabled = true; // Geri dön butonu etkin hale gelir
        }

        // İsim paneli (onayla)
        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
            else
            {
                this.button8.Enabled = true; // Geri dön butonu etkin hale gelir

                baglantim.Open();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenisim = new SqlCommand("update AraSicaklar set ISIM=@ISIM where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenisim.Parameters.AddWithValue("@ISIM", textBox2.Text);
                    yenisim.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label6.Text = textBox2.Text;
                    textBox2.Text = "";
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenisim = new SqlCommand("update AnaYemekler set ISIM=@ISIM where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenisim.Parameters.AddWithValue("@ISIM", textBox2.Text);
                    yenisim.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label6.Text = textBox2.Text;
                    textBox2.Text = "";
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenisim = new SqlCommand("update Tatlılar set ISIM=@ISIM where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenisim.Parameters.AddWithValue("@ISIM", textBox2.Text);
                    yenisim.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label6.Text = textBox2.Text;
                    textBox2.Text = "";
                }

                if (comboBox1.Text == "İçecekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from Icecekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenisim = new SqlCommand("update Icecekler set ISIM=@ISIM where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenisim.Parameters.AddWithValue("@ISIM", textBox2.Text);
                    yenisim.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label6.Text = textBox2.Text;
                    textBox2.Text = "";
                }
                baglantim.Close();
            }               
        }
                

        // Fiyat Butonu (Paneli açar)
        private void button10_Click(object sender, EventArgs e)
        {
            this.panel9.Visible = true; // Ürün Fiyat düzenleme panelini gösterir
            this.panel8.Visible = false; // Ürün İsim düzenleme panelini gizler
            this.panel10.Visible = false; // Ürün Fiyat düzenleme panelini gizler
            this.panel10.Visible = false; // Ürün Resim düzenleme panelini gizler
            this.button8.Enabled = false; // Geri dön butonu devre dışı kalır

            /* pictureBox3'teki resmi varsayılan haline geri getirir */

            baglantim.Open();

            SqlCommand cresim = new SqlCommand("select * from Resimler where id=34", baglantim);
            SqlDataReader dresim = cresim.ExecuteReader();

            if (dresim.Read())
            {
                if (dresim["RESIM"] != null)
                {
                    byte[] resim2 = new byte[0];
                    resim2 = (byte[])dresim["RESIM"];
                    MemoryStream stream = new MemoryStream(resim2);
                    pictureBox3.BackgroundImage = Image.FromStream(stream);
                }
            }
            dresim.Close();

            baglantim.Close();
        }

        // Fiyat paneli (text sadece rakam)
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // Fiyat paneli (onayla)
        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Info);
            }
            else
            {
                this.button8.Enabled = true; // Geri dön butonu etkin hale gelir

                baglantim.Open();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenifiyat = new SqlCommand("update AraSicaklar set FIYAT=@FIYAT where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenifiyat.Parameters.AddWithValue("@FIYAT", textBox3.Text);
                    yenifiyat.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label7.Text = textBox3.Text;
                    textBox3.Text = "";
                    this.panel9.Visible = false; // Fiyat paneli gizlenir
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenifiyat = new SqlCommand("update AnaYemekler set FIYAT=@FIYAT where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenifiyat.Parameters.AddWithValue("@FIYAT", textBox3.Text);
                    yenifiyat.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label7.Text = textBox3.Text;
                    textBox3.Text = "";
                    this.panel9.Visible = false; // Fiyat paneli gizlenir
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenifiyat = new SqlCommand("update Tatlılar set FIYAT=@FIYAT where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenifiyat.Parameters.AddWithValue("@FIYAT", textBox3.Text);
                    yenifiyat.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label7.Text = textBox3.Text;
                    textBox3.Text = "";
                    this.panel9.Visible = false; // Fiyat paneli gizlenir
                }

                if (comboBox1.Text == "İçecekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from Icecekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    dryemek.Read();
                    SqlCommand yenifiyat = new SqlCommand("update Icecekler set FIYAT=@FIYAT where id=" + (int)dryemek["id"], baglantim);
                    dryemek.Close();

                    yenifiyat.Parameters.AddWithValue("@FIYAT", textBox3.Text);
                    yenifiyat.ExecuteNonQuery();

                    this.panel8.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün ismi başarıyla değişti.", ToolTipIcon.Info);

                    label7.Text = textBox3.Text;
                    textBox3.Text = "";
                    this.panel9.Visible = false; // Fiyat paneli gizlenir
                }
            baglantim.Close();
            }  
        }

        // Fiyat paneli (iptal)
        private void button15_Click(object sender, EventArgs e)
        {
            this.panel9.Visible = false; // Fiyat paneli gizlenir
            textBox3.Text = "";

            this.button8.Enabled = true; // Geri dön butonu etkin hale gelir
        }

        // Menüde butonu
        private void button9_Click(object sender, EventArgs e)
        {
            if(this.panel8.Visible == true || this.panel9.Visible == true || this.panel10.Visible == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "Uyarı", "İşlemi gerçekleştirmeden önce açtığınız pencereyi kapatınız.", ToolTipIcon.Warning);
            }
            else
            {
                this.button16.Visible = true; // Menü Dışı butonunu getirir
                this.button9.Visible = false; // Menüde butonunu gizler

                baglantim.Open();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    SqlCommand durum = new SqlCommand("select * from AraSicaklar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update AraSicaklar set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 0);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    SqlCommand durum = new SqlCommand("select * from AnaYemekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update AnaYemekler set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 0);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    SqlCommand durum = new SqlCommand("select * from Tatlılar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update Tatlılar set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 0);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "İçecekler")
                {
                    SqlCommand durum = new SqlCommand("select * from Icecekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update Icecekler set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 0);
                    yenidurum.ExecuteNonQuery();
                }
                baglantim.Close();
            }

        }

        // Menü Dışı butonu
        private void button16_Click(object sender, EventArgs e)
        {
            if (this.panel8.Visible == true || this.panel9.Visible == true || this.panel10.Visible == true)
            {
                MessageBox.Show("İşlemi gerçekleştirmeden önce açtığınız pencereyi kapatınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.button16.Visible = false; // Menü Dışı butonunu gizler
                this.button9.Visible = true; // Menüde butonunu getirir

                baglantim.Open();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    SqlCommand durum = new SqlCommand("select * from AraSicaklar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update AraSicaklar set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 1);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    SqlCommand durum = new SqlCommand("select * from AnaYemekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update AnaYemekler set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 1);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    SqlCommand durum = new SqlCommand("select * from Tatlılar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update Tatlılar set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 1);
                    yenidurum.ExecuteNonQuery();
                }

                if (comboBox1.Text == "İçecekler")
                {
                    SqlCommand durum = new SqlCommand("select * from Icecekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drdurum = durum.ExecuteReader();

                    drdurum.Read();
                    SqlCommand yenidurum = new SqlCommand("update Icecekler set DURUM=@DURUM where id=" + (int)drdurum["id"], baglantim);
                    drdurum.Close();

                    yenidurum.Parameters.AddWithValue("@DURUM", 1);
                    yenidurum.ExecuteNonQuery();
                }

                baglantim.Close();
            }
        }

        // Resim butonu (Resim Panelini açar)
        private void button6_Click(object sender, EventArgs e)
        {
            this.panel10.Visible = true; // Resim düzenleme panelini açar
            this.panel8.Visible = false; // Ürün İsmi Panelini gizler
            this.panel9.Visible = false; // Ürün Fiyat Panelini gizler
            this.button8.Enabled = false; // Geri dön butonu devre dışı kalır
        }

        // Resim paneli (Click)

        string dosyaismi = null;
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();
            resim.Title = "Resmi Değiştir";
            resim.Filter = "Resim Dosyaları|*.bmp;*.png;*.jpg;*.jpeg";

            if (resim.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackgroundImage = Image.FromFile(resim.FileName);
                dosyaismi = resim.FileName;
            }
        }
 
        // Resim paneli (Kaydet)
        private void button17_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (dosyaismi == null)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kaydetmeden önce bir resim yüklemelisiniz.", ToolTipIcon.Warning);
            }
            else
            {
                this.button8.Enabled = true; // Geri dön butonu etkin hale gelir

                FileStream filestream = new FileStream(dosyaismi, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(filestream);
                byte[] resim = reader.ReadBytes((int)filestream.Length);
                reader.Close(); filestream.Close();

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    // Ara Sicaklar
                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();
                        SqlCommand yeniresim = new SqlCommand("update AraSicaklar set RESIM=@RESIM where id=" + (int)dryemek["id"], baglantim);
                        dryemek.Close();

                        yeniresim.Parameters.Add("@RESIM", SqlDbType.Image, resim.Length).Value = resim;
                        yeniresim.ExecuteNonQuery();
                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün resmi başarıyla değişti.", ToolTipIcon.Info);

                        this.panel10.Visible = false; // Resim düzenleme panelini kapatır
                    }
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    // Ana Yemekler
                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    if (dryemek.HasRows)
                    {
                        dryemek.Read();
                        SqlCommand yeniresim = new SqlCommand("update AnaYemekler set RESIM=@RESIM where id=" + (int)dryemek["id"], baglantim);
                        dryemek.Close();

                        yeniresim.Parameters.Add("@RESIM", SqlDbType.Image, resim.Length).Value = resim;
                        yeniresim.ExecuteNonQuery();
                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün resmi başarıyla değişti.", ToolTipIcon.Info);

                        this.panel10.Visible = false; // Resim düzenleme panelini kapatır
                    }
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    // Tatlılar
                    SqlCommand arasicak = new SqlCommand("select * from Tatlılar where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drarasicak = arasicak.ExecuteReader();

                    if (drarasicak.HasRows)
                    {
                        drarasicak.Read();
                        SqlCommand yeniresim = new SqlCommand("update Tatlılar set RESIM=@RESIM where id=" + (int)drarasicak["id"], baglantim);
                        drarasicak.Close();

                        yeniresim.Parameters.Add("@RESIM", SqlDbType.Image, resim.Length).Value = resim;
                        yeniresim.ExecuteNonQuery();
                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün resmi başarıyla değişti.", ToolTipIcon.Info);

                        this.panel10.Visible = false; // Resim düzenleme panelini kapatır
                    }
                }

                if (comboBox1.Text == "İçecekler")
                {
                    // İçecekler
                    SqlCommand arasicak = new SqlCommand("select * from Icecekler where ISIM='" + label6.Text + "'", baglantim);
                    SqlDataReader drarasicak = arasicak.ExecuteReader();

                    if (drarasicak.HasRows)
                    {
                        drarasicak.Read();
                        SqlCommand yeniresim = new SqlCommand("update Icecekler set RESIM=@RESIM where id=" + (int)drarasicak["id"], baglantim);
                        drarasicak.Close();

                        yeniresim.Parameters.Add("@RESIM", SqlDbType.Image, resim.Length).Value = resim;
                        yeniresim.ExecuteNonQuery();
                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Ürün resmi başarıyla değişti.", ToolTipIcon.Info);

                        this.panel10.Visible = false; // Resim düzenleme panelini kapatır
                    }
                }
                button6.BackgroundImage = pictureBox3.BackgroundImage; // Seçilen resim soldaki buttonda gözükür
            }
            baglantim.Close();
        }

        // Resim paneli (İptal)
        private void button18_Click(object sender, EventArgs e)
        {
            this.panel10.Visible = false; // Resim düzenleme panelini gizler
            this.button8.Enabled = true; // Geri dön butonu etkin hale gelir

            /* pictureBox3'teki resmi varsayılan haline geri getirir */

            baglantim.Open();

            SqlCommand cresim = new SqlCommand("select * from Resimler where id=34", baglantim);
            SqlDataReader dresim = cresim.ExecuteReader();

            if (dresim.Read())
            {
                if (dresim["RESIM"] != null)
                {
                    byte[] resim = new byte[0];
                    resim = (byte[])dresim["RESIM"];
                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox3.BackgroundImage = Image.FromStream(stream);
                }
            }
            dresim.Close();

            baglantim.Close();
        }

        // Tarif Kitabı
        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen seçim yapınız.", ToolTipIcon.Warning);
            }
            else
            {
                this.panel5.Visible = false; // Ana Menü gizlenir
                this.panel12.Visible = true; // Tarif Paneli görünür
                this.button25.Visible = true; // Menü butonu görünür
                this.panel11.Visible = false; // Tarif Malzemeler paneli gizlenir
                this.panel13.Visible = false; // Tarif Detay paneli gizlenir

                /* Arka plan resmi degisir */

                baglantim.Open();

                SqlCommand cresim = new SqlCommand("select * from Resimler where id=37", baglantim);
                SqlDataReader dresim = cresim.ExecuteReader();

                if (dresim.Read())
                {
                    if (dresim["RESIM"] != null)
                    {
                        byte[] resim = new byte[0];
                        resim = (byte[])dresim["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        panel3.BackgroundImage = Image.FromStream(stream);
                    }
                }
                dresim.Close();

                // Malzemeler yüklenir

                if (comboBox1.Text == "Ara Sıcaklar")
                {
                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    label14.Text = comboBox2.Text;

                    dryemek.Read();
                    SqlCommand tarifyemek = new SqlCommand("select * from AraSicaklar where id=" + (int)dryemek["id"], baglantim);
                    
                    if (dryemek["RESIM"] != null)
                    {
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        pictureBox4.BackgroundImage = Image.FromStream(stream);
                    }
                    dryemek.Close();

                    SqlDataReader drtarifyemek = tarifyemek.ExecuteReader();

                    if (drtarifyemek.HasRows)
                    {
                        drtarifyemek.Read();
                        if (!DBNull.Value.Equals(drtarifyemek["MALZEMELER"]) && (string)drtarifyemek["MALZEMELER"] != "")
                        {
                            label16.Text = (string)drtarifyemek["MALZEMELER"];                         
                        }
                        else
                        {
                            label16.Text = "*Henüz detay girilmemiş*";
                        }
                        
                        if(!DBNull.Value.Equals(drtarifyemek["TARIF"]) && (string)drtarifyemek["TARIF"] != "")
                        {
                            label23.Text = (string)drtarifyemek["TARIF"];
                        }
                        else
                        {
                            label23.Text = "*Henüz detay girilmemiş*";
                        }
                    }
                    drtarifyemek.Close();
                }

                if (comboBox1.Text == "Ana Yemekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    label14.Text = comboBox2.Text;

                    dryemek.Read();
                    SqlCommand tarifyemek = new SqlCommand("select * from AnaYemekler where id=" + (int)dryemek["id"], baglantim);

                    if (dryemek["RESIM"] != null)
                    {
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        pictureBox4.BackgroundImage = Image.FromStream(stream);
                    }
                    dryemek.Close();

                    SqlDataReader drtarifyemek = tarifyemek.ExecuteReader();

                    if (drtarifyemek.HasRows)
                    {
                        drtarifyemek.Read();
                        if (!DBNull.Value.Equals(drtarifyemek["MALZEMELER"]) && (string)drtarifyemek["MALZEMELER"] != "")
                        {
                            label16.Text = (string)drtarifyemek["MALZEMELER"];
                        }
                        else
                        {
                            label16.Text = "*Henüz detay girilmemiş*";
                        }

                        if (!DBNull.Value.Equals(drtarifyemek["TARIF"]) && (string)drtarifyemek["TARIF"] != "")
                        {
                            label23.Text = (string)drtarifyemek["TARIF"];
                        }
                        else
                        {
                            label23.Text = "*Henüz detay girilmemiş*";
                        }
                    }
                    drtarifyemek.Close();
                }

                if (comboBox1.Text == "Tatlılar")
                {
                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    label14.Text = comboBox2.Text;

                    dryemek.Read();
                    SqlCommand tarifyemek = new SqlCommand("select * from Tatlılar where id=" + (int)dryemek["id"], baglantim);

                    if (dryemek["RESIM"] != null)
                    {
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        pictureBox4.BackgroundImage = Image.FromStream(stream);
                    }
                    dryemek.Close();

                    SqlDataReader drtarifyemek = tarifyemek.ExecuteReader();

                    if (drtarifyemek.HasRows)
                    {
                        drtarifyemek.Read();
                        if (!DBNull.Value.Equals(drtarifyemek["MALZEMELER"]) && (string)drtarifyemek["MALZEMELER"] != "")
                        {
                            label16.Text = (string)drtarifyemek["MALZEMELER"];
                        }
                        else
                        {
                            label16.Text = "*Henüz detay girilmemiş*";
                        }

                        if (!DBNull.Value.Equals(drtarifyemek["TARIF"]) && (string)drtarifyemek["TARIF"] != "")
                        {
                            label23.Text = (string)drtarifyemek["TARIF"];
                        }
                        else
                        {
                            label23.Text = "*Henüz detay girilmemiş*";
                        }
                    }
                    drtarifyemek.Close();
                }

                if (comboBox1.Text == "İçecekler")
                {
                    SqlCommand yemek = new SqlCommand("select * from Icecekler where ISIM='" + comboBox2.Text + "'", baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();

                    label14.Text = comboBox2.Text;

                    dryemek.Read();
                    SqlCommand tarifyemek = new SqlCommand("select * from Icecekler where id=" + (int)dryemek["id"], baglantim);

                    if (dryemek["RESIM"] != null)
                    {
                        byte[] resim = new byte[0];
                        resim = (byte[])dryemek["RESIM"];
                        MemoryStream stream = new MemoryStream(resim);
                        pictureBox4.BackgroundImage = Image.FromStream(stream);
                    }
                    dryemek.Close();

                    SqlDataReader drtarifyemek = tarifyemek.ExecuteReader();

                    if (drtarifyemek.HasRows)
                    {
                        drtarifyemek.Read();
                        if (!DBNull.Value.Equals(drtarifyemek["MALZEMELER"]) && (string)drtarifyemek["MALZEMELER"] != "")
                        {
                            label16.Text = (string)drtarifyemek["MALZEMELER"];
                        }
                        else
                        {
                            label16.Text = "*Henüz detay girilmemiş*";
                        }

                        if (!DBNull.Value.Equals(drtarifyemek["TARIF"]) && (string)drtarifyemek["TARIF"] != "")
                        {
                            label23.Text = (string)drtarifyemek["TARIF"];
                        }
                        else
                        {
                            label23.Text = "*Henüz detay girilmemiş*";
                        }
                    }
                    drtarifyemek.Close();
                }
                baglantim.Close();
            }
        }

        // Tarif Malzemeler Düzenle butonu
        private void button19_Click(object sender, EventArgs e)
        {
            this.button19.Enabled = false; // Malzeme düzenle butonu devre dışı
            this.button25.Enabled = false; // Menü butonu devre dışı kalır
            this.panel13.Visible = false; // Tarif detay paneli gizlenir
            this.panel15.Visible = false; // Sağ sayfa gizlenir       
            this.panel11.Visible = true; // Tarif malzeme paneli görünür hale gelir

            if(label16.Text != "*Henüz detay girilmemiş*" && label16.Text != "*malzemeler*")
            {
                textBox4.Text = label16.Text; // Malzemeleri textbox'a kopyalar   
            }
            else
            {
                textBox4.Text = "";
            }
                  
        }

        // Tarif Malzemeler Paneli (Kaydet)
        private void button22_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (comboBox1.Text == "Ara Sıcaklar")
            {
                SqlCommand id = new SqlCommand("select * from AraSicaklar where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update AraSicaklar set MALZEMELER=@MALZEMELER where id=" + (int)drid["id"], baglantim);

                if (textBox4.Text != Convert.ToString(drid["MALZEMELER"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@MALZEMELER", textBox4.Text);
                    malzemeler.ExecuteNonQuery();

                    this.panel11.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Malzeme bilgisi başarıyla değişti", ToolTipIcon.Info);

                    this.panel11.Visible = false; // Tarif malzeme paneli gizlenir
                    this.panel15.Visible = true; // Sağ sayfa gönürür hale gelir
                    this.button19.Enabled = true; // Malzeme düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label16.Text = textBox4.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Ana Yemekler")
            {
                SqlCommand id = new SqlCommand("select * from AnaYemekler where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update AnaYemekler set MALZEMELER=@MALZEMELER where id=" + (int)drid["id"], baglantim);

                if (textBox4.Text != Convert.ToString(drid["MALZEMELER"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@MALZEMELER", textBox4.Text);
                    malzemeler.ExecuteNonQuery();

                    this.panel11.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Malzeme bilgisi başarıyla değişti", ToolTipIcon.Info);

                    this.panel11.Visible = false; // Tarif malzeme paneli gizlenir
                    this.panel15.Visible = true; // Sağ sayfa gönürür hale gelir
                    this.button19.Enabled = true; // Malzeme düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label16.Text = textBox4.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Tatlılar")
            {
                SqlCommand id = new SqlCommand("select * from Tatlılar where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update Tatlılar set MALZEMELER=@MALZEMELER where id=" + (int)drid["id"], baglantim);

                if (textBox4.Text != Convert.ToString(drid["MALZEMELER"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@MALZEMELER", textBox4.Text);
                    malzemeler.ExecuteNonQuery();

                    this.panel11.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Malzeme bilgisi başarıyla değişti", ToolTipIcon.Info);
                    this.panel11.Visible = false; // Tarif malzeme paneli gizlenir
                    this.panel15.Visible = true; // Sağ sayfa gönürür hale gelir
                    this.button19.Enabled = true; // Malzeme düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label16.Text = textBox4.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Icecekler")
            {
                SqlCommand id = new SqlCommand("select * from Icecekler where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update Icecekler set MALZEMELER=@MALZEMELER where id=" + (int)drid["id"], baglantim);

                if (textBox4.Text != Convert.ToString(drid["MALZEMELER"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@MALZEMELER", textBox4.Text);
                    malzemeler.ExecuteNonQuery();

                    this.panel11.Visible = false;
                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Malzeme bilgisi başarıyla değişti", ToolTipIcon.Info);
                    this.panel11.Visible = false; // Tarif malzeme paneli gizlenir
                    this.panel15.Visible = true; // Sağ sayfa gönürür hale gelir
                    this.button19.Enabled = true; // Malzeme düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label16.Text = textBox4.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }
            baglantim.Close();
        }

        // Tarif Malzemeler Paneli (İptal)
        private void button21_Click(object sender, EventArgs e)
        {
            this.panel11.Visible = false; // Tarif malzeme paneli gizlenir
            this.panel15.Visible = true; // Sağ sayfa görünür hale gelir
            this.button19.Enabled = true; // Malzeme düzenle butonu etkin
            this.button25.Enabled = true; // Menü butonu etkin
        }

        // Tarif Detay Düzenle butonu
        private void button20_Click(object sender, EventArgs e)
        {
            this.panel14.Visible = false; // Sol sayfa gizlenir
            this.button20.Enabled = false; // Tarif düzenle butonu devre dışı
            this.button25.Enabled = false; // Menü butonu devre dışı
            this.panel13.Visible = true; // Tarif detay paneli görünür hale gelir

            if (label23.Text != "*Henüz detay girilmemiş*" && label23.Text != "*tarif*")
            {
                textBox5.Text = label23.Text; // Malzemeleri textbox'a kopyalar  
            }
            else
            {
                textBox4.Text = "";
            } 
        }

        // Tarif Detay Paneli (Kaydet)
        private void button24_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (comboBox1.Text == "Ara Sıcaklar")
            {
                SqlCommand id = new SqlCommand("select * from AraSicaklar where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update AraSicaklar set TARIF=@TARIF where id=" + (int)drid["id"], baglantim);

                if (textBox5.Text != Convert.ToString(drid["TARIF"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@TARIF", textBox5.Text);
                    malzemeler.ExecuteNonQuery();

                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Tarif bilgisi başarıyla değişti", ToolTipIcon.Info);

                    this.panel13.Visible = false; // Tarif detay paneli gizlenir
                    this.panel14.Visible = true; // Sol sayfa görünür hale gelir
                    this.button20.Enabled = true; // Tarif düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label23.Text = textBox5.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Ana Yemekler")
            {
                SqlCommand id = new SqlCommand("select * from AnaYemekler where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update AnaYemekler set TARIF=@TARIF where id=" + (int)drid["id"], baglantim);

                if (textBox5.Text != Convert.ToString(drid["TARIF"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@TARIF", textBox5.Text);
                    malzemeler.ExecuteNonQuery();

                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Tarif bilgisi başarıyla değişti", ToolTipIcon.Info);
                    this.panel13.Visible = false; // Tarif detay paneli gizlenir
                    this.panel14.Visible = true; // Sol sayfa görünür hale gelir
                    this.button20.Enabled = true; // Tarif düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label23.Text = textBox5.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Tatlılar")
            {
                SqlCommand id = new SqlCommand("select * from Tatlılar where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update Tatlılar set TARIF=@TARIF where id=" + (int)drid["id"], baglantim);

                if (textBox5.Text != Convert.ToString(drid["TARIF"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@TARIF", textBox5.Text);
                    malzemeler.ExecuteNonQuery();

                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Tarif bilgisi başarıyla değişti", ToolTipIcon.Info);
                    this.panel13.Visible = false; // Tarif detay paneli gizlenir
                    this.panel14.Visible = true; // Sol sayfa görünür hale gelir
                    this.button20.Enabled = true; // Tarif düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label23.Text = textBox5.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }

            if (comboBox1.Text == "Icecekler")
            {
                SqlCommand id = new SqlCommand("select * from Icecekler where ISIM='" + label14.Text + "'", baglantim);
                SqlDataReader drid = id.ExecuteReader();
                drid.Read();

                SqlCommand malzemeler = new SqlCommand("update Icecekler set TARIF=@TARIF where id=" + (int)drid["id"], baglantim);

                if (textBox5.Text != Convert.ToString(drid["TARIF"]))
                {
                    drid.Close();
                    malzemeler.Parameters.AddWithValue("@TARIF", textBox5.Text);
                    malzemeler.ExecuteNonQuery();

                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Tarif bilgisi başarıyla değişti", ToolTipIcon.Info);
                    this.panel13.Visible = false; // Tarif detay paneli gizlenir
                    this.panel14.Visible = true; // Sol sayfa görünür hale gelir
                    this.button20.Enabled = true; // Tarif düzenle butonu etkin
                    this.button25.Enabled = true; // Menü butonu etkin
                    label23.Text = textBox5.Text; // Yeni malzemeleri sayfaya kopyalar
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Değişiklik yok", "Herhangi bir değer değiştirilmedi.", ToolTipIcon.Warning);
                }
            }
            baglantim.Close();
        }

        // Tarif Detay Paneli (İptal)
        private void button23_Click(object sender, EventArgs e)
        {
            this.panel13.Visible = false; // Tarif detay paneli gizlenir
            this.panel14.Visible = true; // Sol sayfa görünür hale gelir
            this.button20.Enabled = true; // Tarif düzenle butonu etkin
            this.button25.Enabled = true; // Menü butonu etkin
        }

        // Tarif Paneli Menü butonu
        private void button25_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand cresim = new SqlCommand("select * from Resimler where id=35", baglantim);
            SqlDataReader dresim = cresim.ExecuteReader();

            if (dresim.Read())
            {
                if (dresim["RESIM"] != null)
                {
                    byte[] resim = new byte[0];
                    resim = (byte[])dresim["RESIM"];
                    MemoryStream stream = new MemoryStream(resim);
                    panel3.BackgroundImage = Image.FromStream(stream);
                }
            }
            dresim.Close();

            baglantim.Close();

            this.panel5.Visible = true; // Ana Menü görünür
            this.panel12.Visible = false; // Tarif paneli gizlenir
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBox5.Text.Length > 300 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Karakter Sınırı", "Maks. 300 karakter yazabilirsiniz.", ToolTipIcon.Warning);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox4.Text.Length > 200 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Karakter Sınırı", "Maks. 200 karakter yazabilirsiniz.", ToolTipIcon.Warning);
            }
        }
    }
}