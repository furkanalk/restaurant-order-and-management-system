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
    public partial class PersonelTarifler : Form
    {
        public PersonelTarifler()
        {
            InitializeComponent();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelAnaSayfa gitPersonelAnaSayfa = new PersonelAnaSayfa();
            gitPersonelAnaSayfa.Show();
            this.Hide();
        }

        private void Profil_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelProfil gitPersonelProfil = new PersonelProfil();
            gitPersonelProfil.Show();
            this.Hide();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            /* empty */
        }

        private void Tarifler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelSiparisler gitPersonelSiparisler = new PersonelSiparisler();
            gitPersonelSiparisler.Show();
            this.Hide();
        }

        private void Stok_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelStok gitPersonelStok = new PersonelStok();
            gitPersonelStok.Show();
            this.Hide();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        // Resimler
        private void PersonelTarifler_Load(object sender, EventArgs e)
        {
            this.panel5.Visible = true;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            button25.Enabled = false;

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Employee where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();
            byte[] resim = (byte[])drprofil["Resim"];
            MemoryStream memorystream = new MemoryStream(resim);
            LogoPersonel.BackgroundImage = Image.FromStream(memorystream);
            drprofil.Close();

            baglantim.Close();
        }

        int menu = 0; // Menü tipini seçer

        // Ara Sıcaklar
        private void button2_Click(object sender, EventArgs e)
        {
            label35.Text = "IX...";
            label34.Text = "VIII..";
            label33.Text = "VII..";

            button25.Enabled = true;

            label19.Text = "Ara Sıcaklar";
            label19.ForeColor = System.Drawing.Color.Olive;
            label10.ForeColor = System.Drawing.Color.Olive;
            label11.ForeColor = System.Drawing.Color.Olive;
            label13.ForeColor = System.Drawing.Color.Olive;
            label18.ForeColor = System.Drawing.Color.Olive;
            label20.ForeColor = System.Drawing.Color.Olive;
            label21.ForeColor = System.Drawing.Color.Olive;
            label22.ForeColor = System.Drawing.Color.Olive;
            label24.ForeColor = System.Drawing.Color.Olive;
            label25.ForeColor = System.Drawing.Color.Olive;
            label26.ForeColor = System.Drawing.Color.Olive;

            this.panel7.Visible = true;
            this.panel6.Visible = false;
            this.panel5.Visible = false;

            id = 0;
            menu = 1;

            baglantim.Open();

            // Ara Sıcakları yükler
            SqlCommand arayemek1 = new SqlCommand("select * from AraSicaklar where id=1", baglantim);
            SqlDataReader drarayemek1 = arayemek1.ExecuteReader();

            drarayemek1.Read();

            // İsim
            label11.Text = (string)drarayemek1["ISIM"];
            drarayemek1.Close();

            SqlCommand arayemek2 = new SqlCommand("select * from AraSicaklar where id=2", baglantim);
            SqlDataReader drarayemek2 = arayemek2.ExecuteReader();

            drarayemek2.Read();

            // İsim
            label13.Text = (string)drarayemek2["ISIM"];

            // Resim
            byte[] resim2 = new byte[0];
            resim2 = (byte[])drarayemek2["RESIM"];
            drarayemek2.Close();
            MemoryStream stream2 = new MemoryStream(resim2);
            pictureBox7.BackgroundImage = Image.FromStream(stream2);

            SqlCommand arayemek3 = new SqlCommand("select * from AraSicaklar where id=3", baglantim);
            SqlDataReader drarayemek3 = arayemek3.ExecuteReader();

            drarayemek3.Read();

            // İsim
            label18.Text = (string)drarayemek3["ISIM"];

            // Resim
            byte[] resim3 = new byte[0];
            resim3 = (byte[])drarayemek3["RESIM"];
            drarayemek3.Close();
            MemoryStream stream3 = new MemoryStream(resim3);
            pictureBox8.BackgroundImage = Image.FromStream(stream3);

            SqlCommand arayemek4 = new SqlCommand("select * from AraSicaklar where id=4", baglantim);
            SqlDataReader drarayemek4 = arayemek4.ExecuteReader();

            drarayemek4.Read();

            // İsim
            label20.Text = (string)drarayemek4["ISIM"];

            // Resim
            byte[] resim4 = new byte[0];
            resim4 = (byte[])drarayemek4["RESIM"];
            drarayemek4.Close();
            MemoryStream stream4 = new MemoryStream(resim4);
            pictureBox9.BackgroundImage = Image.FromStream(stream4);

            SqlCommand arayemek5 = new SqlCommand("select * from AraSicaklar where id=5", baglantim);
            SqlDataReader drarayemek5 = arayemek5.ExecuteReader();

            drarayemek5.Read();

            // İsim
            label21.Text = (string)drarayemek5["ISIM"];

            // Resim
            byte[] resim5 = new byte[0];
            resim5 = (byte[])drarayemek5["RESIM"];
            drarayemek5.Close();
            MemoryStream stream5 = new MemoryStream(resim5);
            pictureBox11.BackgroundImage = Image.FromStream(stream5);


            SqlCommand arayemek6 = new SqlCommand("select * from AraSicaklar where id=6", baglantim);
            SqlDataReader drarayemek6 = arayemek6.ExecuteReader();

            drarayemek6.Read();

            // İsim
            label22.Text = (string)drarayemek6["ISIM"];
            drarayemek6.Close();

            SqlCommand arayemek7 = new SqlCommand("select * from AraSicaklar where id=7", baglantim);
            SqlDataReader drarayemek7 = arayemek7.ExecuteReader();

            drarayemek7.Read();

            // İsim
            label24.Text = (string)drarayemek7["ISIM"];
            drarayemek7.Close();

            SqlCommand arayemek8 = new SqlCommand("select * from AraSicaklar where id=8", baglantim);
            SqlDataReader drarayemek8 = arayemek8.ExecuteReader();

            drarayemek8.Read();

            // İsim
            label25.Text = (string)drarayemek8["ISIM"];
            drarayemek8.Close();

            SqlCommand arayemek9 = new SqlCommand("select * from AraSicaklar where id=9", baglantim);
            SqlDataReader drarayemek9 = arayemek9.ExecuteReader();

            drarayemek9.Read();

            // İsim
            label26.Text = (string)drarayemek9["ISIM"];

            // Resim
            byte[] resim = new byte[0];
            resim = (byte[])drarayemek9["RESIM"];
            drarayemek9.Close();
            MemoryStream stream = new MemoryStream(resim);
            pictureBox6.BackgroundImage = Image.FromStream(stream);

            baglantim.Close();
        }

        // Ana Yemekler
        private void button3_Click(object sender, EventArgs e)
        {
            label35.Text = "IX...";
            label34.Text = "VIII..";
            label33.Text = "VII..";

            button25.Enabled = true;

            label19.Text = "Ana Yemekler";
            label19.ForeColor = System.Drawing.Color.DarkCyan;
            label10.ForeColor = System.Drawing.Color.DarkCyan;
            label11.ForeColor = System.Drawing.Color.DarkCyan;
            label13.ForeColor = System.Drawing.Color.DarkCyan;
            label18.ForeColor = System.Drawing.Color.DarkCyan;
            label20.ForeColor = System.Drawing.Color.DarkCyan;
            label21.ForeColor = System.Drawing.Color.DarkCyan;
            label22.ForeColor = System.Drawing.Color.DarkCyan;
            label24.ForeColor = System.Drawing.Color.DarkCyan;
            label25.ForeColor = System.Drawing.Color.DarkCyan;
            label26.ForeColor = System.Drawing.Color.DarkCyan;

            this.panel7.Visible = true;
            this.panel6.Visible = false;
            this.panel5.Visible = false;

            id = 0;
            menu = 2;

            baglantim.Open();

            // Ana Yemekleri yükler
            SqlCommand arayemek1 = new SqlCommand("select * from AnaYemekler where id=1", baglantim);
            SqlDataReader drarayemek1 = arayemek1.ExecuteReader();

            drarayemek1.Read();

            // İsim
            label11.Text = (string)drarayemek1["ISIM"];

            // Resim
            byte[] resim = new byte[0];
            resim = (byte[])drarayemek1["RESIM"];
            drarayemek1.Close();
            MemoryStream stream = new MemoryStream(resim);
            pictureBox6.BackgroundImage = Image.FromStream(stream);

            SqlCommand arayemek2 = new SqlCommand("select * from AnaYemekler where id=2", baglantim);
            SqlDataReader drarayemek2 = arayemek2.ExecuteReader();

            drarayemek2.Read();

            // İsim
            label13.Text = (string)drarayemek2["ISIM"];

            // Resim
            byte[] resim2 = new byte[0];
            resim2 = (byte[])drarayemek2["RESIM"];
            drarayemek2.Close();
            MemoryStream stream2 = new MemoryStream(resim2);
            pictureBox7.BackgroundImage = Image.FromStream(stream2);

            SqlCommand arayemek3 = new SqlCommand("select * from AnaYemekler where id=3", baglantim);
            SqlDataReader drarayemek3 = arayemek3.ExecuteReader();

            drarayemek3.Read();

            // İsim
            label18.Text = (string)drarayemek3["ISIM"];

            // Resim
            byte[] resim3 = new byte[0];
            resim3 = (byte[])drarayemek3["RESIM"];
            drarayemek3.Close();
            MemoryStream stream3 = new MemoryStream(resim3);
            pictureBox8.BackgroundImage = Image.FromStream(stream3);

            SqlCommand arayemek4 = new SqlCommand("select * from AnaYemekler where id=4", baglantim);
            SqlDataReader drarayemek4 = arayemek4.ExecuteReader();

            drarayemek4.Read();

            // İsim
            label20.Text = (string)drarayemek4["ISIM"];

            // Resim
            byte[] resim4 = new byte[0];
            resim4 = (byte[])drarayemek4["RESIM"];
            drarayemek4.Close();
            MemoryStream stream4 = new MemoryStream(resim4);
            pictureBox9.BackgroundImage = Image.FromStream(stream4);

            SqlCommand arayemek5 = new SqlCommand("select * from AnaYemekler where id=5", baglantim);
            SqlDataReader drarayemek5 = arayemek5.ExecuteReader();

            drarayemek5.Read();

            // İsim
            label21.Text = (string)drarayemek5["ISIM"];

            // Resim
            byte[] resim5 = new byte[0];
            resim5 = (byte[])drarayemek5["RESIM"];
            drarayemek5.Close();
            MemoryStream stream5 = new MemoryStream(resim5);
            pictureBox11.BackgroundImage = Image.FromStream(stream5);


            SqlCommand arayemek6 = new SqlCommand("select * from AnaYemekler where id=6", baglantim);
            SqlDataReader drarayemek6 = arayemek6.ExecuteReader();

            drarayemek6.Read();

            // İsim
            label22.Text = (string)drarayemek6["ISIM"];
            drarayemek6.Close();

            SqlCommand arayemek7 = new SqlCommand("select * from AnaYemekler where id=7", baglantim);
            SqlDataReader drarayemek7 = arayemek7.ExecuteReader();

            drarayemek7.Read();

            // İsim
            label24.Text = (string)drarayemek7["ISIM"];
            drarayemek7.Close();

            SqlCommand arayemek8 = new SqlCommand("select * from AnaYemekler where id=8", baglantim);
            SqlDataReader drarayemek8 = arayemek8.ExecuteReader();

            drarayemek8.Read();

            // İsim
            label25.Text = (string)drarayemek8["ISIM"];
            drarayemek8.Close();

            SqlCommand arayemek9 = new SqlCommand("select * from AnaYemekler where id=9", baglantim);
            SqlDataReader drarayemek9 = arayemek9.ExecuteReader();

            drarayemek9.Read();

            // İsim
            label26.Text = (string)drarayemek9["ISIM"];
            drarayemek9.Close();

            baglantim.Close();
        }

        // Tatlılar
        private void button4_Click(object sender, EventArgs e)
        {
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";

            label35.Text = "";
            label34.Text = "";
            label33.Text = "";

            button25.Enabled = true;

            label19.Text = "Tatlılar";
            label19.ForeColor = System.Drawing.Color.DarkMagenta;
            label10.ForeColor = System.Drawing.Color.DarkMagenta;
            label11.ForeColor = System.Drawing.Color.DarkMagenta;
            label13.ForeColor = System.Drawing.Color.DarkMagenta;
            label18.ForeColor = System.Drawing.Color.DarkMagenta;
            label20.ForeColor = System.Drawing.Color.DarkMagenta;
            label21.ForeColor = System.Drawing.Color.DarkMagenta;
            label22.ForeColor = System.Drawing.Color.DarkMagenta;
            label24.ForeColor = System.Drawing.Color.DarkMagenta;
            label25.ForeColor = System.Drawing.Color.DarkMagenta;
            label26.ForeColor = System.Drawing.Color.DarkMagenta;

            this.panel7.Visible = true;
            this.panel6.Visible = false;
            this.panel5.Visible = false;

            id = 0;
            menu = 3;

            baglantim.Open();

            // Tatlıları yükler
            SqlCommand arayemek1 = new SqlCommand("select * from Tatlılar where id=1", baglantim);
            SqlDataReader drarayemek1 = arayemek1.ExecuteReader();

            drarayemek1.Read();

            // İsim
            label11.Text = (string)drarayemek1["ISIM"];

            // Resim
            byte[] resim = new byte[0];
            resim = (byte[])drarayemek1["RESIM"];
            drarayemek1.Close();
            MemoryStream stream = new MemoryStream(resim);
            pictureBox6.BackgroundImage = Image.FromStream(stream);

            SqlCommand arayemek2 = new SqlCommand("select * from Tatlılar where id=2", baglantim);
            SqlDataReader drarayemek2 = arayemek2.ExecuteReader();

            drarayemek2.Read();

            // İsim
            label13.Text = (string)drarayemek2["ISIM"];

            // Resim
            byte[] resim2 = new byte[0];
            resim2 = (byte[])drarayemek2["RESIM"];
            drarayemek2.Close();
            MemoryStream stream2 = new MemoryStream(resim2);
            pictureBox7.BackgroundImage = Image.FromStream(stream2);

            SqlCommand arayemek3 = new SqlCommand("select * from Tatlılar where id=3", baglantim);
            SqlDataReader drarayemek3 = arayemek3.ExecuteReader();

            drarayemek3.Read();

            // İsim
            label18.Text = (string)drarayemek3["ISIM"];

            // Resim
            byte[] resim3 = new byte[0];
            resim3 = (byte[])drarayemek3["RESIM"];
            drarayemek3.Close();
            MemoryStream stream3 = new MemoryStream(resim3);
            pictureBox8.BackgroundImage = Image.FromStream(stream3);

            SqlCommand arayemek4 = new SqlCommand("select * from Tatlılar where id=4", baglantim);
            SqlDataReader drarayemek4 = arayemek4.ExecuteReader();

            drarayemek4.Read();

            // İsim
            label20.Text = (string)drarayemek4["ISIM"];

            // Resim
            byte[] resim4 = new byte[0];
            resim4 = (byte[])drarayemek4["RESIM"];
            drarayemek4.Close();
            MemoryStream stream4 = new MemoryStream(resim4);
            pictureBox9.BackgroundImage = Image.FromStream(stream4);

            SqlCommand arayemek5 = new SqlCommand("select * from Tatlılar where id=5", baglantim);
            SqlDataReader drarayemek5 = arayemek5.ExecuteReader();

            drarayemek5.Read();

            // İsim
            label21.Text = (string)drarayemek5["ISIM"];

            // Resim
            byte[] resim5 = new byte[0];
            resim5 = (byte[])drarayemek5["RESIM"];
            drarayemek5.Close();
            MemoryStream stream5 = new MemoryStream(resim5);
            pictureBox11.BackgroundImage = Image.FromStream(stream5);


            SqlCommand arayemek6 = new SqlCommand("select * from Tatlılar where id=6", baglantim);
            SqlDataReader drarayemek6 = arayemek6.ExecuteReader();

            drarayemek6.Read();

            // İsim
            label22.Text = (string)drarayemek6["ISIM"];

            // Resim
            byte[] resim6 = new byte[0];
            resim6 = (byte[])drarayemek6["RESIM"];
            drarayemek5.Close();
            MemoryStream stream6 = new MemoryStream(resim6);
            pictureBox11.BackgroundImage = Image.FromStream(stream6);

            baglantim.Close();
        }

        // İçecekler
        private void button5_Click(object sender, EventArgs e)
        {
            label35.Text = "IX...";
            label34.Text = "VIII..";
            label33.Text = "VII..";

            button25.Enabled = true;

            label19.Text = "İçecekler";
            label19.ForeColor = System.Drawing.Color.SeaGreen;
            label10.ForeColor = System.Drawing.Color.SeaGreen;
            label11.ForeColor = System.Drawing.Color.SeaGreen;
            label13.ForeColor = System.Drawing.Color.SeaGreen;
            label18.ForeColor = System.Drawing.Color.SeaGreen;
            label20.ForeColor = System.Drawing.Color.SeaGreen;
            label21.ForeColor = System.Drawing.Color.SeaGreen;
            label22.ForeColor = System.Drawing.Color.SeaGreen;
            label24.ForeColor = System.Drawing.Color.SeaGreen;
            label25.ForeColor = System.Drawing.Color.SeaGreen;
            label26.ForeColor = System.Drawing.Color.SeaGreen;

            this.panel7.Visible = true;
            this.panel6.Visible = false;
            this.panel5.Visible = false;

            id = 0;
            menu = 4;

            baglantim.Open();

            // İçecekleri yükler
            SqlCommand arayemek1 = new SqlCommand("select * from Icecekler where id=1", baglantim);
            SqlDataReader drarayemek1 = arayemek1.ExecuteReader();

            drarayemek1.Read();

            // İsim
            label11.Text = (string)drarayemek1["ISIM"];
            drarayemek1.Close();

            SqlCommand arayemek2 = new SqlCommand("select * from Icecekler where id=2", baglantim);
            SqlDataReader drarayemek2 = arayemek2.ExecuteReader();

            drarayemek2.Read();

            // İsim
            label13.Text = (string)drarayemek2["ISIM"];

            // Resim
            byte[] resim2 = new byte[0];
            resim2 = (byte[])drarayemek2["RESIM"];
            drarayemek2.Close();
            MemoryStream stream2 = new MemoryStream(resim2);
            pictureBox7.BackgroundImage = Image.FromStream(stream2);

            SqlCommand arayemek3 = new SqlCommand("select * from Icecekler where id=3", baglantim);
            SqlDataReader drarayemek3 = arayemek3.ExecuteReader();

            drarayemek3.Read();

            // İsim
            label18.Text = (string)drarayemek3["ISIM"];

            // Resim
            byte[] resim3 = new byte[0];
            resim3 = (byte[])drarayemek3["RESIM"];
            drarayemek3.Close();
            MemoryStream stream3 = new MemoryStream(resim3);
            pictureBox8.BackgroundImage = Image.FromStream(stream3);

            SqlCommand arayemek4 = new SqlCommand("select * from Icecekler where id=4", baglantim);
            SqlDataReader drarayemek4 = arayemek4.ExecuteReader();

            drarayemek4.Read();

            // İsim
            label20.Text = (string)drarayemek4["ISIM"];
            drarayemek4.Close();

            SqlCommand arayemek5 = new SqlCommand("select * from Icecekler where id=5", baglantim);
            SqlDataReader drarayemek5 = arayemek5.ExecuteReader();

            drarayemek5.Read();

            // İsim
            label21.Text = (string)drarayemek5["ISIM"];

            // Resim
            byte[] resim5 = new byte[0];
            resim5 = (byte[])drarayemek5["RESIM"];
            drarayemek5.Close();
            MemoryStream stream5 = new MemoryStream(resim5);
            pictureBox11.BackgroundImage = Image.FromStream(stream5);


            SqlCommand arayemek6 = new SqlCommand("select * from Icecekler where id=6", baglantim);
            SqlDataReader drarayemek6 = arayemek6.ExecuteReader();

            drarayemek6.Read();

            // İsim
            label22.Text = (string)drarayemek6["ISIM"];

            // Resim
            byte[] resim = new byte[0];
            resim = (byte[])drarayemek6["RESIM"];
            drarayemek6.Close();
            MemoryStream stream = new MemoryStream(resim);
            pictureBox6.BackgroundImage = Image.FromStream(stream);
            
            SqlCommand arayemek7 = new SqlCommand("select * from Icecekler where id=7", baglantim);
            SqlDataReader drarayemek7 = arayemek7.ExecuteReader();

            drarayemek7.Read();

            // İsim
            label24.Text = (string)drarayemek7["ISIM"];
            drarayemek7.Close();

            SqlCommand arayemek8 = new SqlCommand("select * from Icecekler where id=8", baglantim);
            SqlDataReader drarayemek8 = arayemek8.ExecuteReader();

            drarayemek8.Read();

            // İsim
            label25.Text = (string)drarayemek8["ISIM"];

            // Resim
            byte[] resim4 = new byte[0];
            resim4 = (byte[])drarayemek8["RESIM"];
            drarayemek8.Close();
            MemoryStream stream4 = new MemoryStream(resim4);
            pictureBox9.BackgroundImage = Image.FromStream(stream4);         

            SqlCommand arayemek9 = new SqlCommand("select * from Icecekler where id=9", baglantim);
            SqlDataReader drarayemek9 = arayemek9.ExecuteReader();

            drarayemek9.Read();

            // İsim
            label26.Text = (string)drarayemek9["ISIM"];
            drarayemek9.Close();

            baglantim.Close();
        }

        int id = 0; // Ok tuşları için sayfa numaraları

        // Sağ ok (1 kere)
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.panel7.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = true;
            pictureBox10.Visible = true;
            pictureBox5.Visible = true;

            label36.Visible = true;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            label41.Visible = false;
            label42.Visible = false;
            label43.Visible = false;
            label44.Visible = false;

            id = 1;

            if (menu == 1)
            {
                baglantim.Open();

                SqlCommand yemek = new SqlCommand("select * from AraSicaklar where id=" + id, baglantim);
                SqlDataReader dryemek = yemek.ExecuteReader();
                dryemek.Read();
                byte[] resim = new byte[0];
                resim = (byte[])dryemek["RESIM"];

                MemoryStream stream = new MemoryStream(resim);
                pictureBox4.BackgroundImage = Image.FromStream(stream);

                if (!DBNull.Value.Equals(dryemek["ISIM"]))
                {
                    label14.Text = (string)dryemek["ISIM"];
                }

                if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                {
                    label16.Text = (string)dryemek["MALZEMELER"];
                }
                else
                {
                    label16.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                {
                    label23.Text = (string)dryemek["TARIF"];
                }
                else
                {
                    label23.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                {
                    label2.Text = "Evet";
                }
                else
                {
                    label2.Text = "Hayır";
                }

                dryemek.Close();
                baglantim.Close();
            }

            if (menu == 2)
            {
                baglantim.Open();

                SqlCommand yemek = new SqlCommand("select * from AnaYemekler where id=" + id, baglantim);
                SqlDataReader dryemek = yemek.ExecuteReader();
                dryemek.Read();
                byte[] resim = new byte[0];
                resim = (byte[])dryemek["RESIM"];

                MemoryStream stream = new MemoryStream(resim);
                pictureBox4.BackgroundImage = Image.FromStream(stream);

                if (!DBNull.Value.Equals(dryemek["ISIM"]))
                {
                    label14.Text = (string)dryemek["ISIM"];
                }

                if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                {
                    label16.Text = (string)dryemek["MALZEMELER"];
                }
                else
                {
                    label16.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                {
                    label23.Text = (string)dryemek["TARIF"];
                }
                else
                {
                    label23.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                {
                    label2.Text = "Evet";
                }
                else
                {
                    label2.Text = "Hayır";
                }

                dryemek.Close();
                baglantim.Close();
            }

            if (menu == 3)
            {
                baglantim.Open();

                SqlCommand yemek = new SqlCommand("select * from Tatlılar where id=" + id, baglantim);
                SqlDataReader dryemek = yemek.ExecuteReader();
                dryemek.Read();
                byte[] resim = new byte[0];
                resim = (byte[])dryemek["RESIM"];

                MemoryStream stream = new MemoryStream(resim);
                pictureBox4.BackgroundImage = Image.FromStream(stream);

                if (!DBNull.Value.Equals(dryemek["ISIM"]))
                {
                    label14.Text = (string)dryemek["ISIM"];
                }

                if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                {
                    label16.Text = (string)dryemek["MALZEMELER"];
                }
                else
                {
                    label16.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                {
                    label23.Text = (string)dryemek["TARIF"];
                }
                else
                {
                    label23.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                {
                    label2.Text = "Evet";
                }
                else
                {
                    label2.Text = "Hayır";
                }

                dryemek.Close();
                baglantim.Close();
            }

            if (menu == 4)
            {
                baglantim.Open();

                SqlCommand yemek = new SqlCommand("select * from Icecekler where id=" + id, baglantim);
                SqlDataReader dryemek = yemek.ExecuteReader();
                dryemek.Read();
                byte[] resim = new byte[0];
                resim = (byte[])dryemek["RESIM"];

                MemoryStream stream = new MemoryStream(resim);
                pictureBox4.BackgroundImage = Image.FromStream(stream);

                if (!DBNull.Value.Equals(dryemek["ISIM"]))
                {
                    label14.Text = (string)dryemek["ISIM"];
                }

                if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                {
                    label16.Text = (string)dryemek["MALZEMELER"];
                }
                else
                {
                    label16.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                {
                    label23.Text = (string)dryemek["TARIF"];
                }
                else
                {
                    label23.Text = "*Henüz detay girilmemiş*";
                }

                if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                {
                    label2.Text = "Evet";
                }
                else
                {
                    label2.Text = "Hayır";
                }

                dryemek.Close();
                baglantim.Close();
            }
        }

        // Sağ ok
        private void pictureBox5_MouseClick(object sender, EventArgs e)
        {
            if (id < 9)
            {
                this.panel6.Visible = true;
                pictureBox10.Visible = true;
                pictureBox5.Visible = true;
                id++;

                switch(id)
                {
                    case 1:
                        label36.Visible = true;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 2:
                        label36.Visible = false;
                        label37.Visible = true;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 3:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = true;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 4:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = true;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 5:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = true;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 6:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = true;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 7:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = true;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                    case 8:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = true;
                        label44.Visible = false;
                        break;
                    case 9:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = true;
                        break;
                    default:
                        label36.Visible = false;
                        label37.Visible = false;
                        label38.Visible = false;
                        label39.Visible = false;
                        label40.Visible = false;
                        label41.Visible = false;
                        label42.Visible = false;
                        label43.Visible = false;
                        label44.Visible = false;
                        break;
                }

                if (menu == 1)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 2)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 3 && id < 6)
                {
                    pictureBox5.Visible = true;

                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 3 && id == 6)
                {
                    pictureBox5.Visible = false;

                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 4)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from Icecekler where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }
                    dryemek.Close();
                    baglantim.Close();
                }
            }
            if(id == 9)
            {
                pictureBox5.Visible = false;
            }
        }

        // Ara Sıcaklar ok sol
        private void pictureBox10_MouseClick(object sender, EventArgs e)
        {
            id--;

            switch (id)
            {
                case 1:
                    label36.Visible = true;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 2:
                    label36.Visible = false;
                    label37.Visible = true;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 3:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = true;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 4:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = true;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 5:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = true;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 6:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = true;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 7:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = true;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
                case 8:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = true;
                    label44.Visible = false;
                    break;
                case 9:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = true;
                    break;
                default:
                    label36.Visible = false;
                    label37.Visible = false;
                    label38.Visible = false;
                    label39.Visible = false;
                    label40.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    label43.Visible = false;
                    label44.Visible = false;
                    break;
            }

            if (id == 8)
            {
                pictureBox5.Visible = true;
            }

            if (id == 0)
            {
                this.panel6.Visible = false;
                this.panel7.Visible = true;
            }
            else
            {
                if (menu == 1)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from AraSicaklar where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 2)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from AnaYemekler where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 3)
                {
                    pictureBox5.Visible = true;

                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from Tatlılar where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }

                if (menu == 4)
                {
                    baglantim.Open();

                    SqlCommand yemek = new SqlCommand("select * from Icecekler where id=" + id, baglantim);
                    SqlDataReader dryemek = yemek.ExecuteReader();
                    dryemek.Read();
                    byte[] resim = new byte[0];
                    resim = (byte[])dryemek["RESIM"];

                    MemoryStream stream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(stream);

                    if (!DBNull.Value.Equals(dryemek["ISIM"]))
                    {
                        label14.Text = (string)dryemek["ISIM"];
                    }

                    if (!DBNull.Value.Equals(dryemek["MALZEMELER"]) && (string)dryemek["MALZEMELER"] != "")
                    {
                        label16.Text = (string)dryemek["MALZEMELER"];
                    }
                    else
                    {
                        label16.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["TARIF"]) && (string)dryemek["TARIF"] != "")
                    {
                        label23.Text = (string)dryemek["TARIF"];
                    }
                    else
                    {
                        label23.Text = "*Henüz detay girilmemiş*";
                    }

                    if (!DBNull.Value.Equals(dryemek["DURUM"]) && Convert.ToBoolean(dryemek["DURUM"]) == true)
                    {
                        label2.Text = "Evet";
                    }
                    else
                    {
                        label2.Text = "Hayır";
                    }

                    dryemek.Close();
                    baglantim.Close();
                }
            }
        }

        // Geri butonu
        private void button25_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = true;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            button25.Enabled = false;
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

        bool hareket; int mouseX; int mouseY;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }
    }
}
