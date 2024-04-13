using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RestaurantAtlantis
{
    public partial class MusteriProfil : Form
    {
        public MusteriProfil()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
            gitMusteriAnaSayfa.Show();
            this.Hide();
        }

        private void Profil_Click(object sender, EventArgs e)
        {
            /* empty */
        }

        private void Menü2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriMenu gitMusteriMenu = new MusteriMenu();
            gitMusteriMenu.Show();
            this.Hide();
        }

        private void Sepet_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriSiparişler gitMusteriSepet = new MusteriSiparişler();
            gitMusteriSepet.Show();
            this.Hide();
        }

        private void Siparisler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriYorumlar gitMusteriSiparisler = new MusteriYorumlar();
            gitMusteriSiparisler.Show();
            this.Hide();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void MusteriProfil_Load(object sender, EventArgs e)
        {
            textBox4.Enabled = false;
            textBox7.Enabled = false;
            this.panel6.Visible = false;
            this.panel5.Visible = true;
            this.panel10.Visible = false;

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();

            byte[] resim = (byte[])drprofil["Resim"];

            MemoryStream memorystream = new MemoryStream(resim);
            LogoMusteri.BackgroundImage = Image.FromStream(memorystream);
            pictureBox2.BackgroundImage = Image.FromStream(memorystream);

            if (drprofil.HasRows)
            {
                if(!DBNull.Value.Equals(drprofil["Name"]))
                {
                    textBox2.Text = (string)drprofil["Name"];
                }
                if (!DBNull.Value.Equals(drprofil["Surname"]))
                {
                    textBox3.Text = (string)drprofil["Surname"];
                }
                if (!DBNull.Value.Equals(drprofil["TC"]))
                {
                    textBox4.Text = (string)drprofil["TC"];
                }
                if (!DBNull.Value.Equals(drprofil["PhoneNumber"]))
                {
                    textBox1.Text = (string)drprofil["PhoneNumber"];
                }
                if (!DBNull.Value.Equals(drprofil["Password"]))
                {
                    textBox8.Text = (string)drprofil["Password"];
                }
                if (!DBNull.Value.Equals(drprofil["Mail"]))
                {
                    string mail = (string)drprofil["Mail"];
                    textBox9.Text = mail.Split('@').First();
                    comboBox3.Text = mail.Split('@').Last();
                }
                if (!DBNull.Value.Equals(drprofil["Cinsiyet"]))
                {
                    comboBox1.Text = (string)drprofil["Cinsiyet"];
                }
                if (textBox2.Text != null && textBox3.Text != null)
                {
                    textBox7.Text = textBox2.Text + " " + textBox3.Text;
                }
                if (!DBNull.Value.Equals(drprofil["Card_CVC"]))
                {
                    textBox19.Text = (string)drprofil["Card_CVC"];
                }
                if (!DBNull.Value.Equals(drprofil["Card_SKT1"]))
                {
                    comboBox21.Text = (string)drprofil["Card_SKT1"];
                }
                if (!DBNull.Value.Equals(drprofil["Card_SKT2"]))
                {
                    comboBox20.Text = (string)drprofil["Card_SKT2"];
                }
                if (!DBNull.Value.Equals(drprofil["Adres"]))
                {
                    textBox5.Text = (string)drprofil["Adres"];
                }
                if (!DBNull.Value.Equals(drprofil["Credit_Card"]))
                {
                    textBox6.Text = (string)drprofil["Credit_Card"];
                }
                drprofil.Close();
            }
            baglantim.Close();
        }

        // Ödeme bilgilerine git
        private void button3_Click(object sender, EventArgs e)
        {
            this.panel10.Visible = true;
            this.panel5.Visible = false;
        }

        // Kişisel bilgiler update
        private void label8_Click(object sender, EventArgs e)
        {
            if (textBox8.Text.Length > 6 || textBox8.Text.Length == 6)
            {
                if (textBox9.Text != "")
                {
                    if (!textBox9.Text.Contains("@"))
                    {
                        if (comboBox3.Text != "")
                        {
                            if (comboBox1.Text != "")
                            {
                                baglantim.Open();
                                SqlCommand kaydet = new SqlCommand("update Customer set Name=@Name,Surname=@Surname,PhoneNumber=@PhoneNumber,Password=@Password,Mail=@Mail,Cinsiyet=@Cinsiyet where TC='" + textBox4.Text + "'", baglantim);

                                kaydet.Parameters.AddWithValue("@Name", textBox2.Text);
                                kaydet.Parameters.AddWithValue("@Surname", textBox3.Text);
                                kaydet.Parameters.AddWithValue("@PhoneNumber", textBox1.Text);
                                kaydet.Parameters.AddWithValue("@Password", textBox8.Text);
                                kaydet.Parameters.AddWithValue("@Mail", textBox9.Text + "@" + comboBox3.Text);
                                kaydet.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                                kaydet.ExecuteNonQuery();
                                baglantim.Close();
                                textBox7.Text = textBox2.Text + " " + textBox3.Text;

                                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgileriniz başarıyla güncellendi.", ToolTipIcon.Info);
                            }
                            else
                            {
                                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen cinsiyetinizi seçiniz.", ToolTipIcon.Warning);
                            }
                        }
                        else
                        {
                            notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adres sağlayıcınızı seçiniz.", ToolTipIcon.Warning);
                        }
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı Mail", "Mail adresiniz '@' karakterini içeremez.", ToolTipIcon.Warning);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen mail adresinizi yazınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Şifre", "Yeni şifreniz en az 6 haneli olmalıdır.", ToolTipIcon.Warning);
            }
        }

        // Kişisel bilgilere geri dön
        private void button2_Click(object sender, EventArgs e)
        {
            this.panel10.Visible = false;
            this.panel5.Visible = true;
        }

        // Ödeme bilgileri
        private void label12_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 16)
            {
                if (textBox19.Text != "")
                {
                    if (textBox19.Text.Length == 4)
                    {
                        if (comboBox21.Text.Length == 2 && comboBox20.Text.Length == 4)
                        {
                            if (textBox5.Text != "")
                            {
                                baglantim.Open();
                                SqlCommand kaydet = new SqlCommand("update Customer set Credit_Card=@Credit_Card,Card_CVC=@Card_CVC,Card_SKT1=@Card_SKT1,Card_SKT2=@Card_SKT2,Adres=@Adres where TC='" + textBox4.Text + "'", baglantim);

                                kaydet.Parameters.AddWithValue("@Credit_Card", textBox6.Text);
                                kaydet.Parameters.AddWithValue("@Card_CVC", textBox19.Text);
                                kaydet.Parameters.AddWithValue("@Card_SKT1", comboBox21.Text);
                                kaydet.Parameters.AddWithValue("@Card_SKT2", comboBox20.Text);
                                kaydet.Parameters.AddWithValue("@Adres", textBox5.Text);
                                kaydet.ExecuteNonQuery();
                                baglantim.Close();
                                textBox7.Text = textBox2.Text + " " + textBox3.Text;

                                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgileriniz başarıyla güncellendi.", ToolTipIcon.Info);
                            }
                            else
                            {
                                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adresinizi giriniz.", ToolTipIcon.Warning);
                            }
                        }
                        else
                        {
                            notifyIcon1.ShowBalloonTip(3000, "Hatalı SKT", "Lütfen SKT bilgilerinizi doğru giriniz.", ToolTipIcon.Warning);
                        }
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı CVC", "CVC bilgisi 4 haneli olmalıdır.", ToolTipIcon.Warning);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen Kart CVC bilginizi yazınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Kart No", "Kart kumarası 16 haneli olmalıdır.", ToolTipIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = true;
        }

        int secim;

        private void button5_Click(object sender, EventArgs e)
        {
            secim = 1;

            button5.BackColor = Color.Goldenrod;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            secim = 2;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Goldenrod;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secim = 3;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Goldenrod;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            secim = 4;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Goldenrod;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            secim = 5;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Goldenrod;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            secim = 6;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Goldenrod;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            secim = 7;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Goldenrod;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            secim = 8;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Goldenrod;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            secim = 9;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Goldenrod;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            secim = 10;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Goldenrod;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            secim = 11;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Goldenrod;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            secim = 12;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Goldenrod;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            secim = 13;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Goldenrod;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            secim = 14;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Goldenrod;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            secim = 15;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Goldenrod;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            secim = 16;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Goldenrod;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Wheat;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            secim = 17;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Goldenrod;
            button24.BackColor = Color.Wheat;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            secim = 18;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
            button22.BackColor = Color.Wheat;
            button23.BackColor = Color.Wheat;
            button24.BackColor = Color.Goldenrod;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (secim == 1)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 4", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button5.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();

            }
            else if (secim == 2)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 5", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button6.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 3)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 6", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button4.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 4)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 7", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button7.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 5)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 8", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button8.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 6)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 9", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button9.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 7)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 10", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button10.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 8)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 11", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button11.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 9)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 12", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button12.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 10)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 13", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button13.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 11)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 14", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button14.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 12)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 15", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button15.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 13)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 16", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button16.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 14)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 17", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button19.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 15)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 18", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button20.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 16)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 22", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button22.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 17)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 23", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button23.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else if (secim == 18)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 24", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

                button24.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Avatarınızı değiştirmek için resim seçiniz.", ToolTipIcon.Warning);
            }
        }

        string dosyaismi = null;

        // Resim yükle
        private void button21_Click(object sender, EventArgs e)
        {
            secim = 16;

            baglantim.Open();

            OpenFileDialog resim = new OpenFileDialog();
            resim.Title = "Resmi Değiştir";
            resim.Filter = "Resim Dosyaları|*.bmp;*.png;*.jpg;*.jpeg";

            if (resim.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackgroundImage = Image.FromFile(resim.FileName);
                LogoMusteri.BackgroundImage = Image.FromFile(resim.FileName);
                dosyaismi = resim.FileName;
            }
            if (dosyaismi != null)
            {
                FileStream filestream = new FileStream(dosyaismi, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(filestream);
                byte[] resim2 = reader.ReadBytes((int)filestream.Length);

                SqlCommand yeni = new SqlCommand("update Customer set Resim=@Resim where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.Add("@Resim", SqlDbType.Image, resim2.Length).Value = resim2;
                yeni.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);
            }
            baglantim.Close();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
            button4.BackColor = Color.Wheat;
            button7.BackColor = Color.Wheat;
            button8.BackColor = Color.Wheat;
            button9.BackColor = Color.Wheat;
            button10.BackColor = Color.Wheat;
            button11.BackColor = Color.Wheat;
            button12.BackColor = Color.Wheat;
            button13.BackColor = Color.Wheat;
            button14.BackColor = Color.Wheat;
            button15.BackColor = Color.Wheat;
            button16.BackColor = Color.Wheat;
            button19.BackColor = Color.Wheat;
            button20.BackColor = Color.Wheat;
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
            Application.Exit();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            if(textBox6.Text.Length == 16 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Kart No Sınırı", "Kart Numaranız 16 haneyi geçemez.", ToolTipIcon.Warning);
            }
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox19.Text.Length == 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Kart CVC Sınırı", "CVC Numaranız 4 haneyi geçemez.", ToolTipIcon.Warning);
            }
        }

        private void comboBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox20.Text.Length == 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Kart SKT Sınırı", "SKT Yılı 4 haneyi geçemez.", ToolTipIcon.Warning);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox5.Text.Length == 200 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Adres Sınırı", "Adres bilgisi 200 karakteri geçemez.", ToolTipIcon.Warning);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        bool hareket; int mouseX; int mouseY;

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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }
    }
}
