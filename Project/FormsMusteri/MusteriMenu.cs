using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RestaurantAtlantis
{
    public partial class MusteriMenu : Form
    {
        public MusteriMenu()
        {
            InitializeComponent();
        }

        SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
            gitMusteriAnaSayfa.Show();
            this.Hide();
        }

        private void Profil_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriProfil gitMusteriProfil = new MusteriProfil();
            gitMusteriProfil.Show();
            this.Hide();
        }

        private void Menü2_Click(object sender, EventArgs e)
        {
            /*empty*/
        }

        private void Sepet_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriYorumlar gitMusteriSiparisler = new MusteriYorumlar();
            gitMusteriSiparisler.Show();
            this.Hide();            
        }

        private void Siparisler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriSiparişler gitMusteriSepet = new MusteriSiparişler();
            gitMusteriSepet.Show();
            this.Hide();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        private void MusteriMenu_Load(object sender, EventArgs e)
        {
            MenuEkrani.TabPages.Remove(Ödeme);

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();

            byte[] resim2 = (byte[])drprofil["Resim"];
            drprofil.Close();
            MemoryStream memorystream2 = new MemoryStream(resim2);
            LogoMusteri.BackgroundImage = Image.FromStream(memorystream2);

            /* Kdv değerinin alınması */

            SqlCommand kdv = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drkdv = kdv.ExecuteReader();

            if (drkdv.HasRows)
            {
                drkdv.Read();
                label75.Text = (string)drkdv["KDV"];
            }
            drkdv.Close();

            /* Menü isim ve fiyatların yüklenmesi */

            // Ara Sicaklar

            SqlCommand arasicak1 = new SqlCommand("select * from AraSicaklar where id=1", baglantim);
            SqlDataReader drarasicak1 = arasicak1.ExecuteReader();

            if (drarasicak1.HasRows)
            {
                drarasicak1.Read();
                checkBox1.Text = (string)drarasicak1["ISIM"];
                
                if (drarasicak1["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak1["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox1.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak1["DURUM"]) == false)
                {
                    label5.Visible = false;
                    label38.Visible = false;
                    checkBox1.Visible = false;
                    comboBox10.Visible = false;
                    pictureBox1.Visible = false;
                }
            }
            drarasicak1.Close();

            SqlCommand arasicak2 = new SqlCommand("select * from AraSicaklar where id=2", baglantim);
            SqlDataReader drarasicak2 = arasicak2.ExecuteReader();

            if (drarasicak2.HasRows)
            {
                drarasicak2.Read();
                checkBox2.Text = (string)drarasicak2["ISIM"];

                if (drarasicak2["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak2["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox2.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak2["DURUM"]) == false)
                {
                    label6.Visible = false;
                    label39.Visible = false;
                    checkBox2.Visible = false;
                    comboBox11.Visible = false;
                    pictureBox2.Visible = false;
                }
            }
            drarasicak2.Close();

            SqlCommand arasicak3 = new SqlCommand("select * from AraSicaklar where id=3", baglantim);
            SqlDataReader drarasicak3 = arasicak3.ExecuteReader();

            if (drarasicak3.HasRows)
            {
                drarasicak3.Read();
                checkBox3.Text = (string)drarasicak3["ISIM"];

                if (drarasicak3["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak3["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox3.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak3["DURUM"]) == false)
                {
                    label7.Visible = false;
                    label40.Visible = false;
                    checkBox3.Visible = false;
                    comboBox12.Visible = false;
                    pictureBox3.Visible = false;
                }
            }
            drarasicak3.Close();

            SqlCommand arasicak4 = new SqlCommand("select * from AraSicaklar where id=4", baglantim);
            SqlDataReader drarasicak4 = arasicak4.ExecuteReader();

            if (drarasicak4.HasRows)
            {
                drarasicak4.Read();
                checkBox4.Text = (string)drarasicak4["ISIM"];

                if (drarasicak4["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak4["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox4.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak4["DURUM"]) == false)
                {
                    label8.Visible = false;
                    label41.Visible = false;
                    checkBox4.Visible = false;
                    comboBox13.Visible = false;
                    pictureBox4.Visible = false;
                }
            }

            drarasicak4.Close();

            SqlCommand arasicak5 = new SqlCommand("select * from AraSicaklar where id=5", baglantim);
            SqlDataReader drarasicak5 = arasicak5.ExecuteReader();

            if (drarasicak5.HasRows)
            {
                drarasicak5.Read();
                checkBox5.Text = (string)drarasicak5["ISIM"];

                if (drarasicak5["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak5["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox5.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak5["DURUM"]) == false)
                {
                    label9.Visible = false;
                    label42.Visible = false;
                    checkBox5.Visible = false;
                    comboBox14.Visible = false;
                    pictureBox5.Visible = false;
                }
            }
            drarasicak5.Close();

            SqlCommand arasicak6 = new SqlCommand("select * from AraSicaklar where id=6", baglantim);
            SqlDataReader drarasicak6 = arasicak6.ExecuteReader();

            if (drarasicak6.HasRows)
            {
                drarasicak6.Read();
                checkBox6.Text = (string)drarasicak6["ISIM"];

                if (drarasicak6["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak6["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox6.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak6["DURUM"]) == false)
                {
                    label10.Visible = false;
                    label43.Visible = false;
                    checkBox6.Visible = false;
                    comboBox15.Visible = false;
                    pictureBox6.Visible = false;
                }
            }
            drarasicak6.Close();

            SqlCommand arasicak7 = new SqlCommand("select * from AraSicaklar where id=7", baglantim);
            SqlDataReader drarasicak7 = arasicak7.ExecuteReader();

            if (drarasicak7.HasRows)
            {
                drarasicak7.Read();
                checkBox7.Text = (string)drarasicak7["ISIM"];

                if (drarasicak7["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak7["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox7.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak7["DURUM"]) == false)
                {
                    label11.Visible = false;
                    label44.Visible = false;
                    checkBox7.Visible = false;
                    comboBox16.Visible = false;
                    pictureBox7.Visible = false;
                }
            }
            drarasicak7.Close();

            SqlCommand arasicak8 = new SqlCommand("select * from AraSicaklar where id=8", baglantim);
            SqlDataReader drarasicak8 = arasicak8.ExecuteReader();

            if (drarasicak8.HasRows)
            {
                drarasicak8.Read();
                checkBox8.Text = (string)drarasicak8["ISIM"];

                if (drarasicak8["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak8["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox8.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak8["DURUM"]) == false)
                {
                    label12.Visible = false;
                    label45.Visible = false;
                    checkBox8.Visible = false;
                    comboBox17.Visible = false;
                    pictureBox8.Visible = false;
                }
            }
            drarasicak8.Close();

            SqlCommand arasicak9 = new SqlCommand("select * from AraSicaklar where id=9", baglantim);
            SqlDataReader drarasicak9 = arasicak9.ExecuteReader();

            if (drarasicak9.HasRows)
            {
                drarasicak9.Read();
                checkBox9.Text = (string)drarasicak9["ISIM"];

                if (drarasicak9["RESIM"] != null)
                {
                    byte[] resim = (byte[])drarasicak9["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox9.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drarasicak9["DURUM"]) == false)
                {
                    label13.Visible = false;
                    label46.Visible = false;
                    checkBox9.Visible = false;
                    comboBox18.Visible = false;
                    pictureBox9.Visible = false;
                }
            }
            drarasicak9.Close();

            // Ara Sıcaklar Fiyat

            SqlCommand arasicakfiyat1 = new SqlCommand("select * from AraSicaklar where id=1", baglantim);
            SqlDataReader drarasicakfiyat1 = arasicakfiyat1.ExecuteReader();

            if (drarasicakfiyat1.HasRows)
            {
                drarasicakfiyat1.Read();
                label5.Text = (string)drarasicakfiyat1["FIYAT"];
            }

            drarasicakfiyat1.Close();

            SqlCommand arasicakfiyat2 = new SqlCommand("select * from AraSicaklar where id=2", baglantim);
            SqlDataReader drarasicakfiyat2 = arasicakfiyat2.ExecuteReader();

            if (drarasicakfiyat2.HasRows)
            {
                drarasicakfiyat2.Read();
                label6.Text = (string)drarasicakfiyat2["FIYAT"];
            }

            drarasicakfiyat2.Close();

            SqlCommand arasicakfiyat3 = new SqlCommand("select * from AraSicaklar where id=3", baglantim);
            SqlDataReader drarasicakfiyat3 = arasicakfiyat3.ExecuteReader();

            if (drarasicakfiyat3.HasRows)
            {
                drarasicakfiyat3.Read();
                label7.Text = (string)drarasicakfiyat3["FIYAT"];
            }

            drarasicakfiyat3.Close();

            SqlCommand arasicakfiyat4 = new SqlCommand("select * from AraSicaklar where id=4", baglantim);
            SqlDataReader drarasicakfiyat4 = arasicakfiyat4.ExecuteReader();

            if (drarasicakfiyat4.HasRows)
            {
                drarasicakfiyat4.Read();
                label8.Text = (string)drarasicakfiyat4["FIYAT"];
            }

            drarasicakfiyat4.Close();

            SqlCommand arasicakfiyat5 = new SqlCommand("select * from AraSicaklar where id=5", baglantim);
            SqlDataReader drarasicakfiyat5 = arasicakfiyat5.ExecuteReader();

            if (drarasicakfiyat5.HasRows)
            {
                drarasicakfiyat5.Read();
                label9.Text = (string)drarasicakfiyat5["FIYAT"];
            }

            drarasicakfiyat5.Close();

            SqlCommand arasicakfiyat6 = new SqlCommand("select * from AraSicaklar where id=6", baglantim);
            SqlDataReader drarasicakfiyat6 = arasicakfiyat6.ExecuteReader();

            if (drarasicakfiyat6.HasRows)
            {
                drarasicakfiyat6.Read();
                label10.Text = (string)drarasicakfiyat6["FIYAT"];
            }

            drarasicakfiyat6.Close();

            SqlCommand arasicakfiyat7 = new SqlCommand("select * from AraSicaklar where id=7", baglantim);
            SqlDataReader drarasicakfiyat7 = arasicakfiyat7.ExecuteReader();

            if (drarasicakfiyat7.HasRows)
            {
                drarasicakfiyat7.Read();
                label11.Text = (string)drarasicakfiyat7["FIYAT"];
            }

            drarasicakfiyat7.Close();

            SqlCommand arasicakfiyat8 = new SqlCommand("select * from AraSicaklar where id=8", baglantim);
            SqlDataReader drarasicakfiyat8 = arasicakfiyat8.ExecuteReader();

            if (drarasicakfiyat8.HasRows)
            {
                drarasicakfiyat8.Read();
                label12.Text = (string)drarasicakfiyat8["FIYAT"];
            }

            drarasicakfiyat8.Close();

            SqlCommand arasicakfiyat9 = new SqlCommand("select * from AraSicaklar where id=9", baglantim);
            SqlDataReader drarasicakfiyat9 = arasicakfiyat9.ExecuteReader();

            if (drarasicakfiyat9.HasRows)
            {
                drarasicakfiyat9.Read();
                label13.Text = (string)drarasicakfiyat9["FIYAT"];
            }

            drarasicakfiyat9.Close();


            // Ana Yemekler

            SqlCommand yemek1 = new SqlCommand("select * from AnaYemekler where id=1", baglantim);
            SqlDataReader dryemek1 = yemek1.ExecuteReader();

            if (dryemek1.HasRows)
            {
                dryemek1.Read();
                checkBox10.Text = (string)dryemek1["ISIM"];

                if (dryemek1["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek1["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox18.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek1["DURUM"]) == false)
                {
                    label14.Visible = false;
                    label47.Visible = false;
                    checkBox10.Visible = false;
                    comboBox1.Visible = false;
                    pictureBox18.Visible = false;
                }
            }
            dryemek1.Close();

            SqlCommand yemek2 = new SqlCommand("select * from AnaYemekler where id=2", baglantim);
            SqlDataReader dryemek2 = yemek2.ExecuteReader();

            if (dryemek2.HasRows)
            {
                dryemek2.Read();
                checkBox11.Text = (string)dryemek2["ISIM"];

                if (dryemek2["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek2["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox17.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek2["DURUM"]) == false)
                {
                    label15.Visible = false;
                    label48.Visible = false;
                    checkBox11.Visible = false;
                    comboBox2.Visible = false;
                    pictureBox17.Visible = false;
                }
            }
            dryemek2.Close();

            SqlCommand yemek3 = new SqlCommand("select * from AnaYemekler where id=3", baglantim);
            SqlDataReader dryemek3 = yemek3.ExecuteReader();

            if (dryemek3.HasRows)
            {
                dryemek3.Read();
                checkBox12.Text = (string)dryemek3["ISIM"];

                if (dryemek3["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek3["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox16.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek3["DURUM"]) == false)
                {
                    label16.Visible = false;
                    label49.Visible = false;
                    checkBox12.Visible = false;
                    comboBox3.Visible = false;
                    pictureBox16.Visible = false;
                }
            }
            dryemek3.Close();

            SqlCommand yemek4 = new SqlCommand("select * from AnaYemekler where id=4", baglantim);
            SqlDataReader dryemek4 = yemek4.ExecuteReader();

            if (dryemek4.HasRows)
            {
                dryemek4.Read();
                checkBox13.Text = (string)dryemek4["ISIM"];

                if (dryemek4["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek4["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox15.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek4["DURUM"]) == false)
                {
                    label17.Visible = false;
                    label50.Visible = false;
                    checkBox13.Visible = false;
                    comboBox4.Visible = false;
                    pictureBox15.Visible = false;
                }
            }
            dryemek4.Close();

            SqlCommand yemek5 = new SqlCommand("select * from AnaYemekler where id=5", baglantim);
            SqlDataReader dryemek5 = yemek5.ExecuteReader();

            if (dryemek5.HasRows)
            {
                dryemek5.Read();
                checkBox14.Text = (string)dryemek5["ISIM"];

                if (dryemek5["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek5["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox14.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek5["DURUM"]) == false)
                {
                    label18.Visible = false;
                    label51.Visible = false;
                    checkBox14.Visible = false;
                    comboBox5.Visible = false;
                    pictureBox14.Visible = false;
                }
            }
            dryemek5.Close();

            SqlCommand yemek6 = new SqlCommand("select * from AnaYemekler where id=6", baglantim);
            SqlDataReader dryemek6 = yemek6.ExecuteReader();

            if (dryemek6.HasRows)
            {
                dryemek6.Read();
                checkBox15.Text = (string)dryemek6["ISIM"];

                if (dryemek6["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek6["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox13.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek6["DURUM"]) == false)
                {
                    label19.Visible = false;
                    label52.Visible = false;
                    checkBox15.Visible = false;
                    comboBox6.Visible = false;
                    pictureBox13.Visible = false;
                }
            }
            dryemek6.Close();

            SqlCommand yemek7 = new SqlCommand("select * from AnaYemekler where id=7", baglantim);
            SqlDataReader dryemek7 = yemek7.ExecuteReader();

            if (dryemek7.HasRows)
            {
                dryemek7.Read();
                checkBox16.Text = (string)dryemek7["ISIM"];

                if (dryemek7["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek7["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox12.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek7["DURUM"]) == false)
                {
                    label20.Visible = false;
                    label53.Visible = false;
                    checkBox16.Visible = false;
                    comboBox7.Visible = false;
                    pictureBox12.Visible = false;
                }
            }
            dryemek7.Close();

            SqlCommand yemek8 = new SqlCommand("select * from AnaYemekler where id=8", baglantim);
            SqlDataReader dryemek8 = yemek8.ExecuteReader();

            if (dryemek8.HasRows)
            {
                dryemek8.Read();
                checkBox17.Text = (string)dryemek8["ISIM"];

                if (dryemek8["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek8["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox11.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek8["DURUM"]) == false)
                {
                    label21.Visible = false;
                    label54.Visible = false;
                    checkBox17.Visible = false;
                    comboBox8.Visible = false;
                    pictureBox11.Visible = false;
                }
            }
            dryemek8.Close();

            SqlCommand yemek9 = new SqlCommand("select * from AnaYemekler where id=9", baglantim);
            SqlDataReader dryemek9 = yemek9.ExecuteReader();

            if (dryemek9.HasRows)
            {
                dryemek9.Read();
                checkBox18.Text = (string)dryemek9["ISIM"];

                if (dryemek9["RESIM"] != null)
                {
                    byte[] resim = (byte[])dryemek9["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox10.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dryemek9["DURUM"]) == false)
                {
                    label22.Visible = false;
                    label55.Visible = false;
                    checkBox18.Visible = false;
                    comboBox9.Visible = false;
                    pictureBox10.Visible = false;
                }
            }
            dryemek9.Close();

            // Ana Yemekler Fiyat

            SqlCommand yemekfiyat1 = new SqlCommand("select * from AnaYemekler where id=1", baglantim);
            SqlDataReader dryemekfiyat1 = yemekfiyat1.ExecuteReader();

            if (dryemekfiyat1.HasRows)
            {
                dryemekfiyat1.Read();
                label14.Text = (string)dryemekfiyat1["FIYAT"];
            }

            dryemekfiyat1.Close();

            SqlCommand yemekfiyat2 = new SqlCommand("select * from AnaYemekler where id=2", baglantim);
            SqlDataReader dryemekfiyat2 = yemekfiyat2.ExecuteReader();

            if (dryemekfiyat2.HasRows)
            {
                dryemekfiyat2.Read();
                label15.Text = (string)dryemekfiyat2["FIYAT"];
            }

            dryemekfiyat2.Close();

            SqlCommand yemekfiyat3 = new SqlCommand("select * from AnaYemekler where id=3", baglantim);
            SqlDataReader dryemekfiyat3 = yemekfiyat3.ExecuteReader();

            if (dryemekfiyat3.HasRows)
            {
                dryemekfiyat3.Read();
                label16.Text = (string)dryemekfiyat3["FIYAT"];
            }

            dryemekfiyat3.Close();

            SqlCommand yemekfiyat4 = new SqlCommand("select * from AnaYemekler where id=4", baglantim);
            SqlDataReader dryemekfiyat4 = yemekfiyat4.ExecuteReader();

            if (dryemekfiyat4.HasRows)
            {
                dryemekfiyat4.Read();
                label17.Text = (string)dryemekfiyat4["FIYAT"];
            }

            dryemekfiyat4.Close();

            SqlCommand yemekfiyat5 = new SqlCommand("select * from AnaYemekler where id=5", baglantim);
            SqlDataReader dryemekfiyat5 = yemekfiyat5.ExecuteReader();

            if (dryemekfiyat5.HasRows)
            {
                dryemekfiyat5.Read();
                label18.Text = (string)dryemekfiyat5["FIYAT"];
            }

            dryemekfiyat5.Close();

            SqlCommand yemekfiyat6 = new SqlCommand("select * from AnaYemekler where id=6", baglantim);
            SqlDataReader dryemekfiyat6 = yemekfiyat6.ExecuteReader();

            if (dryemekfiyat6.HasRows)
            {
                dryemekfiyat6.Read();
                label19.Text = (string)dryemekfiyat6["FIYAT"];
            }

            dryemekfiyat6.Close();

            SqlCommand yemekfiyat7 = new SqlCommand("select * from AnaYemekler where id=7", baglantim);
            SqlDataReader dryemekfiyat7 = yemekfiyat7.ExecuteReader();

            if (dryemekfiyat7.HasRows)
            {
                dryemekfiyat7.Read();
                label20.Text = (string)dryemekfiyat7["FIYAT"];
            }

            dryemekfiyat7.Close();

            SqlCommand yemekfiyat8 = new SqlCommand("select * from AnaYemekler where id=8", baglantim);
            SqlDataReader dryemekfiyat8 = yemekfiyat8.ExecuteReader();

            if (dryemekfiyat8.HasRows)
            {
                dryemekfiyat8.Read();
                label21.Text = (string)dryemekfiyat8["FIYAT"];
            }

            dryemekfiyat8.Close();

            SqlCommand yemekfiyat9 = new SqlCommand("select * from AnaYemekler where id=9", baglantim);
            SqlDataReader dryemekfiyat9 = yemekfiyat9.ExecuteReader();

            if (dryemekfiyat9.HasRows)
            {
                dryemekfiyat9.Read();
                label22.Text = (string)dryemekfiyat9["FIYAT"];
            }

            dryemekfiyat9.Close();

            // Tatlılar

            SqlCommand tatli1 = new SqlCommand("select * from Tatlılar where id=1", baglantim);
            SqlDataReader drtatli1 = tatli1.ExecuteReader();

            if (drtatli1.HasRows)
            {
                drtatli1.Read();
                checkBox19.Text = (string)drtatli1["ISIM"];

                if (drtatli1["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli1["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox27.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli1["DURUM"]) == false)
                {
                    label23.Visible = false;
                    label65.Visible = false;
                    checkBox19.Visible = false;
                    comboBox27.Visible = false;
                    pictureBox27.Visible = false;
                }
            }
            drtatli1.Close();

            SqlCommand tatli2 = new SqlCommand("select * from Tatlılar where id=2", baglantim);
            SqlDataReader drtatli2 = tatli2.ExecuteReader();

            if (drtatli2.HasRows)
            {
                drtatli2.Read();
                checkBox20.Text = (string)drtatli2["ISIM"];

                if (drtatli2["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli2["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox26.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli2["DURUM"]) == false)
                {
                    label24.Visible = false;
                    label66.Visible = false;
                    checkBox20.Visible = false;
                    comboBox26.Visible = false;
                    pictureBox26.Visible = false;
                }
            }
            drtatli2.Close();

            SqlCommand tatli3 = new SqlCommand("select * from Tatlılar where id=3", baglantim);
            SqlDataReader drtatli3 = tatli3.ExecuteReader();

            if (drtatli3.HasRows)
            {
                drtatli3.Read();
                checkBox21.Text = (string)drtatli3["ISIM"];

                if (drtatli3["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli3["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox25.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli3["DURUM"]) == false)
                {
                    label25.Visible = false;
                    label67.Visible = false;
                    checkBox21.Visible = false;
                    comboBox25.Visible = false;
                    pictureBox25.Visible = false;
                }
            }
            drtatli3.Close();

            SqlCommand tatli4 = new SqlCommand("select * from Tatlılar where id=4", baglantim);
            SqlDataReader drtatli4 = tatli4.ExecuteReader();

            if (drtatli4.HasRows)
            {
                drtatli4.Read();
                checkBox22.Text = (string)drtatli4["ISIM"];

                if (drtatli4["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli4["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox24.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli4["DURUM"]) == false)
                {
                    label26.Visible = false;
                    label68.Visible = false;
                    checkBox22.Visible = false;
                    comboBox24.Visible = false;
                    pictureBox24.Visible = false;
                }
            }
            drtatli4.Close();

            SqlCommand tatli5 = new SqlCommand("select * from Tatlılar where id=5", baglantim);
            SqlDataReader drtatli5 = tatli5.ExecuteReader();

            if (drtatli5.HasRows)
            {
                drtatli5.Read();
                checkBox23.Text = (string)drtatli5["ISIM"];

                if (drtatli5["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli5["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox23.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli5["DURUM"]) == false)
                {
                    label27.Visible = false;
                    label69.Visible = false;
                    checkBox23.Visible = false;
                    comboBox23.Visible = false;
                    pictureBox23.Visible = false;
                }
            }
            drtatli5.Close();

            SqlCommand tatli6 = new SqlCommand("select * from Tatlılar where id=6", baglantim);
            SqlDataReader drtatli6 = tatli6.ExecuteReader();

            if (drtatli6.HasRows)
            {
                drtatli6.Read();
                checkBox24.Text = (string)drtatli6["ISIM"];

                if (drtatli6["RESIM"] != null)
                {
                    byte[] resim = (byte[])drtatli6["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox22.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(drtatli6["DURUM"]) == false)
                {
                    label28.Visible = false;
                    label70.Visible = false;
                    checkBox24.Visible = false;
                    comboBox22.Visible = false;
                    pictureBox22.Visible = false;
                }
            }
            drtatli6.Close();

            // Tatlılar Fiyat

            SqlCommand tatlifiyat1 = new SqlCommand("select * from Tatlılar where id=1", baglantim);
            SqlDataReader drtatlifiyat1 = tatlifiyat1.ExecuteReader();

            if (drtatlifiyat1.HasRows)
            {
                drtatlifiyat1.Read();
                label23.Text = (string)drtatlifiyat1["FIYAT"];
            }

            drtatlifiyat1.Close();

            SqlCommand tatlifiyat2 = new SqlCommand("select * from Tatlılar where id=2", baglantim);
            SqlDataReader drtatlifiyat2 = tatlifiyat2.ExecuteReader();

            if (drtatlifiyat2.HasRows)
            {
                drtatlifiyat2.Read();
                label24.Text = (string)drtatlifiyat2["FIYAT"];
            }

            drtatlifiyat2.Close();

            SqlCommand tatlifiyat3 = new SqlCommand("select * from Tatlılar where id=3", baglantim);
            SqlDataReader drtatlifiyat3 = tatlifiyat3.ExecuteReader();

            if (drtatlifiyat3.HasRows)
            {
                drtatlifiyat3.Read();
                label25.Text = (string)drtatlifiyat3["FIYAT"];
            }

            drtatlifiyat3.Close();

            SqlCommand tatlifiyat4 = new SqlCommand("select * from Tatlılar where id=4", baglantim);
            SqlDataReader drtatlifiyat4 = tatlifiyat4.ExecuteReader();

            if (drtatlifiyat4.HasRows)
            {
                drtatlifiyat4.Read();
                label26.Text = (string)drtatlifiyat4["FIYAT"];
            }

            drtatlifiyat4.Close();

            SqlCommand tatlifiyat5 = new SqlCommand("select * from Tatlılar where id=5", baglantim);
            SqlDataReader drtatlifiyat5 = tatlifiyat5.ExecuteReader();

            if (drtatlifiyat5.HasRows)
            {
                drtatlifiyat5.Read();
                label27.Text = (string)drtatlifiyat5["FIYAT"];
            }

            drtatlifiyat5.Close();

            SqlCommand tatlifiyat6 = new SqlCommand("select * from Tatlılar where id=6", baglantim);
            SqlDataReader drtatlifiyat6 = tatlifiyat6.ExecuteReader();

            if (drtatlifiyat6.HasRows)
            {
                drtatlifiyat6.Read();
                label28.Text = (string)drtatlifiyat6["FIYAT"];
            }

            drtatlifiyat6.Close();

            // Icecekler

            SqlCommand icecek1 = new SqlCommand("select * from Icecekler where id=1", baglantim);
            SqlDataReader dricecek1 = icecek1.ExecuteReader();

            if (dricecek1.HasRows)
            {
                dricecek1.Read();
                checkBox25.Text = (string)dricecek1["ISIM"];

                if (dricecek1["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek1["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox36.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek1["DURUM"]) == false)
                {
                    label29.Visible = false;
                    label56.Visible = false;
                    checkBox25.Visible = false;
                    comboBox36.Visible = false;
                    pictureBox36.Visible = false;
                }
            }
            dricecek1.Close();

            SqlCommand icecek2 = new SqlCommand("select * from Icecekler where id=2", baglantim);
            SqlDataReader dricecek2 = icecek2.ExecuteReader();

            if (dricecek2.HasRows)
            {
                dricecek2.Read();
                checkBox26.Text = (string)dricecek2["ISIM"];

                if (dricecek2["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek2["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox35.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek2["DURUM"]) == false)
                {
                    label30.Visible = false;
                    label57.Visible = false;
                    checkBox26.Visible = false;
                    comboBox35.Visible = false;
                    pictureBox35.Visible = false;
                }
            }
            dricecek2.Close();

            SqlCommand icecek3 = new SqlCommand("select * from Icecekler where id=3", baglantim);
            SqlDataReader dricecek3 = icecek3.ExecuteReader();

            if (dricecek3.HasRows)
            {
                dricecek3.Read();
                checkBox27.Text = (string)dricecek3["ISIM"];

                if (dricecek3["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek3["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox34.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek3["DURUM"]) == false)
                {
                    label31.Visible = false;
                    label58.Visible = false;
                    checkBox27.Visible = false;
                    comboBox34.Visible = false;
                    pictureBox34.Visible = false;
                }
            }
            dricecek3.Close();

            SqlCommand icecek4 = new SqlCommand("select * from Icecekler where id=4", baglantim);
            SqlDataReader dricecek4 = icecek4.ExecuteReader();

            if (dricecek4.HasRows)
            {
                dricecek4.Read();
                checkBox28.Text = (string)dricecek4["ISIM"];

                if (dricecek4["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek4["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox33.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek4["DURUM"]) == false)
                {
                    label32.Visible = false;
                    label59.Visible = false;
                    checkBox28.Visible = false;
                    comboBox33.Visible = false;
                    pictureBox33.Visible = false;
                }
            }
            dricecek4.Close();

            SqlCommand icecek5 = new SqlCommand("select * from Icecekler where id=5", baglantim);
            SqlDataReader dricecek5 = icecek5.ExecuteReader();

            if (dricecek5.HasRows)
            {
                dricecek5.Read();
                checkBox29.Text = (string)dricecek5["ISIM"];

                if (dricecek5["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek5["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox32.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek5["DURUM"]) == false)
                {
                    label33.Visible = false;
                    label60.Visible = false;
                    checkBox29.Visible = false;
                    comboBox32.Visible = false;
                    pictureBox32.Visible = false;
                }
            }
            dricecek5.Close();

            SqlCommand icecek6 = new SqlCommand("select * from Icecekler where id=6", baglantim);
            SqlDataReader dricecek6 = icecek6.ExecuteReader();

            if (dricecek6.HasRows)
            {
                dricecek6.Read();
                checkBox30.Text = (string)dricecek6["ISIM"];

                if (dricecek6["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek6["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox31.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek6["DURUM"]) == false)
                {
                    label34.Visible = false;
                    label61.Visible = false;
                    checkBox30.Visible = false;
                    comboBox31.Visible = false;
                    pictureBox31.Visible = false;
                }
            }
            dricecek6.Close();

            SqlCommand icecek7 = new SqlCommand("select * from Icecekler where id=7", baglantim);
            SqlDataReader dricecek7 = icecek7.ExecuteReader();

            if (dricecek7.HasRows)
            {
                dricecek7.Read();
                checkBox31.Text = (string)dricecek7["ISIM"];

                if (dricecek7["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek7["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox30.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek7["DURUM"]) == false)
                {
                    label35.Visible = false;
                    label62.Visible = false;
                    checkBox31.Visible = false;
                    comboBox30.Visible = false;
                    pictureBox30.Visible = false;
                }
            }
            dricecek7.Close();

            SqlCommand icecek8 = new SqlCommand("select * from Icecekler where id=8", baglantim);
            SqlDataReader dricecek8 = icecek8.ExecuteReader();

            if (dricecek8.HasRows)
            {
                dricecek8.Read();
                checkBox32.Text = (string)dricecek8["ISIM"];

                if (dricecek8["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek8["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox29.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek8["DURUM"]) == false)
                {
                    label36.Visible = false;
                    label63.Visible = false;
                    checkBox32.Visible = false;
                    comboBox29.Visible = false;
                    pictureBox29.Visible = false;
                }
            }
            dricecek8.Close();

            SqlCommand icecek9 = new SqlCommand("select * from Icecekler where id=9", baglantim);
            SqlDataReader dricecek9 = icecek9.ExecuteReader();

            if (dricecek9.HasRows)
            {
                dricecek9.Read();
                checkBox33.Text = (string)dricecek9["ISIM"];

                if (dricecek9["RESIM"] != null)
                {
                    byte[] resim = (byte[])dricecek9["RESIM"];
                    MemoryStream memorystream = new MemoryStream(resim);
                    pictureBox28.BackgroundImage = Image.FromStream(memorystream);
                }

                if (Convert.ToBoolean(dricecek9["DURUM"]) == false)
                {
                    label37.Visible = false;
                    label64.Visible = false;
                    checkBox33.Visible = false;
                    comboBox28.Visible = false;
                    pictureBox28.Visible = false;
                }
            }
            dricecek9.Close();

            // Icecekler Fiyat


            SqlCommand icecekfiyat1 = new SqlCommand("select * from Icecekler where id=1", baglantim);
            SqlDataReader dricecekfiyat1 = icecekfiyat1.ExecuteReader();

            if (dricecekfiyat1.HasRows)
            {
                dricecekfiyat1.Read();
                label29.Text = (string)dricecekfiyat1["FIYAT"];
            }

            dricecekfiyat1.Close();

            SqlCommand icecekfiyat2 = new SqlCommand("select * from Icecekler where id=2", baglantim);
            SqlDataReader dricecekfiyat2 = icecekfiyat2.ExecuteReader();

            if (dricecekfiyat2.HasRows)
            {
                dricecekfiyat2.Read();
                label30.Text = (string)dricecekfiyat2["FIYAT"];
            }

            dricecekfiyat2.Close();

            SqlCommand icecekfiyat3 = new SqlCommand("select * from Icecekler where id=3", baglantim);
            SqlDataReader dricecekfiyat3 = icecekfiyat3.ExecuteReader();

            if (dricecekfiyat3.HasRows)
            {
                dricecekfiyat3.Read();
                label31.Text = (string)dricecekfiyat3["FIYAT"];
            }

            dricecekfiyat3.Close();

            SqlCommand icecekfiyat4 = new SqlCommand("select * from Icecekler where id=4", baglantim);
            SqlDataReader dricecekfiyat4 = icecekfiyat4.ExecuteReader();

            if (dricecekfiyat4.HasRows)
            {
                dricecekfiyat4.Read();
                label32.Text = (string)dricecekfiyat4["FIYAT"];
            }

            dricecekfiyat4.Close();

            SqlCommand icecekfiyat5 = new SqlCommand("select * from Icecekler where id=5", baglantim);
            SqlDataReader dricecekfiyat5 = icecekfiyat5.ExecuteReader();

            if (dricecekfiyat5.HasRows)
            {
                dricecekfiyat5.Read();
                label33.Text = (string)dricecekfiyat5["FIYAT"];
            }

            dricecekfiyat5.Close();

            SqlCommand icecekfiyat6 = new SqlCommand("select * from Icecekler where id=6", baglantim);
            SqlDataReader dricecekfiyat6 = icecekfiyat6.ExecuteReader();

            if (dricecekfiyat6.HasRows)
            {
                dricecekfiyat6.Read();
                label34.Text = (string)dricecekfiyat6["FIYAT"];
            }

            dricecekfiyat6.Close();

            SqlCommand icecekfiyat7 = new SqlCommand("select * from Icecekler where id=7", baglantim);
            SqlDataReader dricecekfiyat7 = icecekfiyat7.ExecuteReader();

            if (dricecekfiyat7.HasRows)
            {
                dricecekfiyat7.Read();
                label35.Text = (string)dricecekfiyat7["FIYAT"];
            }

            dricecekfiyat7.Close();

            SqlCommand icecekfiyat8 = new SqlCommand("select * from Icecekler where id=8", baglantim);
            SqlDataReader dricecekfiyat8 = icecekfiyat8.ExecuteReader();

            if (dricecekfiyat8.HasRows)
            {
                dricecekfiyat8.Read();
                label36.Text = (string)dricecekfiyat8["FIYAT"];
            }

            dricecekfiyat8.Close();

            SqlCommand icecekfiyat9 = new SqlCommand("select * from Icecekler where id=9", baglantim);
            SqlDataReader dricecekfiyat9 = icecekfiyat9.ExecuteReader();

            if (dricecekfiyat9.HasRows)
            {
                dricecekfiyat9.Read();
                label37.Text = (string)dricecekfiyat9["FIYAT"];
            }

            dricecekfiyat9.Close();

            baglantim.Close();
        }

        // Sepete ürünlerin eklenmesi
        private void sepetim_Click(object sender, EventArgs e)
        {
            // Kalamar Tava
            if (checkBox1.Checked == true)
            {
                if (comboBox10.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox1.Text);
                    item.SubItems.Add(comboBox10.Text);
                    int adet = Convert.ToInt32(comboBox10.Text);
                    int donustur = Convert.ToInt32(label5.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Tereyağlı karides
            if (checkBox2.Checked == true)
            {
                if (comboBox11.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox2.Text);
                    item.SubItems.Add(comboBox11.Text);
                    int adet = Convert.ToInt32(comboBox11.Text);
                    int donustur = Convert.ToInt32(label6.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Balık Kokoreç
            if (checkBox3.Checked == true)
            {
                if (comboBox12.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox3.Text);
                    item.SubItems.Add(comboBox12.Text);
                    int adet = Convert.ToInt32(comboBox12.Text);
                    int donustur = Convert.ToInt32(label7.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Yengeç Bacağı
            if (checkBox4.Checked == true)
            {
                if (comboBox13.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox4.Text);
                    item.SubItems.Add(comboBox13.Text);
                    int adet = Convert.ToInt32(comboBox13.Text);
                    int donustur = Convert.ToInt32(label8.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Balık Çorbası
            if (checkBox5.Checked == true)
            {
                if (comboBox14.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox5.Text);
                    item.SubItems.Add(comboBox14.Text);
                    int adet = Convert.ToInt32(comboBox14.Text);
                    int donustur = Convert.ToInt32(label9.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Jumbo Karides
            if (checkBox6.Checked == true)
            {
                if (comboBox15.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox6.Text);
                    item.SubItems.Add(comboBox15.Text);
                    int adet = Convert.ToInt32(comboBox15.Text);
                    int donustur = Convert.ToInt32(label10.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Kıymalı Börek
            if (checkBox7.Checked == true)
            {
                if (comboBox16.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox7.Text);
                    item.SubItems.Add(comboBox16.Text);
                    int adet = Convert.ToInt32(comboBox16.Text);
                    int donustur = Convert.ToInt32(label11.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Paçanga Böreği
            if (checkBox8.Checked == true)
            {
                if (comboBox17.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox8.Text);
                    item.SubItems.Add(comboBox17.Text);
                    int adet = Convert.ToInt32(comboBox17.Text);
                    int donustur = Convert.ToInt32(label12.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Patates Kızartması
            if (checkBox9.Checked == true)
            {
                if (comboBox18.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox9.Text);
                    item.SubItems.Add(comboBox18.Text);
                    int adet = Convert.ToInt32(comboBox18.Text);
                    int donustur = Convert.ToInt32(label13.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Hamsi Tava
            if (checkBox10.Checked == true)
            {
                if (comboBox1.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox10.Text);
                    item.SubItems.Add(comboBox1.Text);
                    int adet = Convert.ToInt32(comboBox1.Text);
                    int donustur = Convert.ToInt32(label14.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Hamsi Güveç
            if (checkBox11.Checked == true)
            {
                if (comboBox2.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox11.Text);
                    item.SubItems.Add(comboBox2.Text);
                    int adet = Convert.ToInt32(comboBox2.Text);
                    int donustur = Convert.ToInt32(label15.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Levrek
            if (checkBox12.Checked == true)
            {
                if (comboBox3.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox12.Text);
                    item.SubItems.Add(comboBox3.Text);
                    int adet = Convert.ToInt32(comboBox3.Text);
                    int donustur = Convert.ToInt32(label16.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Çupra
            if (checkBox13.Checked == true)
            {
                if (comboBox4.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox13.Text);
                    item.SubItems.Add(comboBox4.Text);
                    int adet = Convert.ToInt32(comboBox4.Text);
                    int donustur = Convert.ToInt32(label17.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Somon
            if (checkBox14.Checked == true)
            {
                if (comboBox5.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox14.Text);
                    item.SubItems.Add(comboBox5.Text);
                    int adet = Convert.ToInt32(comboBox5.Text);
                    int donustur = Convert.ToInt32(label18.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Lüfer
            if (checkBox15.Checked == true)
            {
                if (comboBox6.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox15.Text);
                    item.SubItems.Add(comboBox6.Text);
                    int adet = Convert.ToInt32(comboBox6.Text);
                    int donustur = Convert.ToInt32(label19.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Çinekop
            if (checkBox16.Checked == true)
            {
                if (comboBox7.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox16.Text);
                    item.SubItems.Add(comboBox7.Text);
                    int adet = Convert.ToInt32(comboBox7.Text);
                    int donustur = Convert.ToInt32(label20.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Palamut
            if (checkBox17.Checked == true)
            {
                if (comboBox8.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox17.Text);
                    item.SubItems.Add(comboBox8.Text);
                    int adet = Convert.ToInt32(comboBox8.Text);
                    int donustur = Convert.ToInt32(label21.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Şefin Spesiyali
            if (checkBox18.Checked == true)
            {
                if (comboBox9.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox18.Text);
                    item.SubItems.Add(comboBox9.Text);
                    int adet = Convert.ToInt32(comboBox9.Text);
                    int donustur = Convert.ToInt32(label22.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Baklava
            if (checkBox19.Checked == true)
            {
                if (comboBox27.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox19.Text);
                    item.SubItems.Add(comboBox27.Text);
                    int adet = Convert.ToInt32(comboBox27.Text);
                    int donustur = Convert.ToInt32(label23.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Künefe
            if (checkBox20.Checked == true)
            {
                if (comboBox26.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox20.Text);
                    item.SubItems.Add(comboBox26.Text);
                    int adet = Convert.ToInt32(comboBox26.Text);
                    int donustur = Convert.ToInt32(label24.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Dondurma
            if (checkBox21.Checked == true)
            {
                if (comboBox25.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox21.Text);
                    item.SubItems.Add(comboBox25.Text);
                    int adet = Convert.ToInt32(comboBox25.Text);
                    int donustur = Convert.ToInt32(label25.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Ayva Tatlısı
            if (checkBox22.Checked == true)
            {
                if (comboBox24.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox22.Text);
                    item.SubItems.Add(comboBox24.Text);
                    int adet = Convert.ToInt32(comboBox24.Text);
                    int donustur = Convert.ToInt32(label26.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // İrmik Helvası
            if (checkBox23.Checked == true)
            {
                if (comboBox23.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox23.Text);
                    item.SubItems.Add(comboBox23.Text);
                    int adet = Convert.ToInt32(comboBox23.Text);
                    int donustur = Convert.ToInt32(label27.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Kazandibi
            if (checkBox24.Checked == true)
            {
                if (comboBox22.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox24.Text);
                    item.SubItems.Add(comboBox22.Text);
                    int adet = Convert.ToInt32(comboBox22.Text);
                    int donustur = Convert.ToInt32(label28.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Su
            if (checkBox25.Checked == true)
            {
                if (comboBox36.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox25.Text);
                    item.SubItems.Add(comboBox36.Text);
                    int adet = Convert.ToInt32(comboBox36.Text);
                    int donustur = Convert.ToInt32(label29.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Ayran
            if (checkBox26.Checked == true)
            {
                if (comboBox35.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox26.Text);
                    item.SubItems.Add(comboBox35.Text);
                    int adet = Convert.ToInt32(comboBox35.Text);
                    int donustur = Convert.ToInt32(label30.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Coca-Cola
            if (checkBox27.Checked == true)
            {
                if (comboBox34.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox27.Text);
                    item.SubItems.Add(comboBox34.Text);
                    int adet = Convert.ToInt32(comboBox34.Text);
                    int donustur = Convert.ToInt32(label31.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Sprite
            if (checkBox28.Checked == true)
            {
                if (comboBox33.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox28.Text);
                    item.SubItems.Add(comboBox33.Text);
                    int adet = Convert.ToInt32(comboBox33.Text);
                    int donustur = Convert.ToInt32(label32.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Fanta
            if (checkBox29.Checked == true)
            {
                if (comboBox32.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox29.Text);
                    item.SubItems.Add(comboBox32.Text);
                    int adet = Convert.ToInt32(comboBox32.Text);
                    int donustur = Convert.ToInt32(label33.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Şalgam
            if (checkBox30.Checked == true)
            {
                if (comboBox31.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox30.Text);
                    item.SubItems.Add(comboBox31.Text);
                    int adet = Convert.ToInt32(comboBox31.Text);
                    int donustur = Convert.ToInt32(label34.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Fuse-Tea
            if (checkBox31.Checked == true)
            {
                if (comboBox30.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox31.Text);
                    item.SubItems.Add(comboBox30.Text);
                    int adet = Convert.ToInt32(comboBox30.Text);
                    int donustur = Convert.ToInt32(label35.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Meyve Suyu
            if (checkBox32.Checked == true)
            {
                if (comboBox29.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox32.Text);
                    item.SubItems.Add(comboBox29.Text);
                    int adet = Convert.ToInt32(comboBox29.Text);
                    int donustur = Convert.ToInt32(label36.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Maden Suyu
            if (checkBox33.Checked == true)
            {
                if (comboBox28.Text != "")
                {
                    ListViewItem item = new ListViewItem(checkBox33.Text);
                    item.SubItems.Add(comboBox28.Text);
                    int adet = Convert.ToInt32(comboBox28.Text);
                    int donustur = Convert.ToInt32(label37.Text);
                    double fiyat = adet * donustur;
                    string sonfiyat = fiyat.ToString();
                    item.SubItems.Add(sonfiyat);
                    listView1.Items.Add(item);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen ürün miktarını doğru seçiniz.", ToolTipIcon.Warning);
                }
            }

            // Sepettekilerin fiyat hesaplaması

            double sepetfiyati = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                sepetfiyati += Convert.ToDouble(item.SubItems[2].Text);
            }

            double kdv = sepetfiyati * Convert.ToDouble(label75.Text) / 100;
            double vergidahil = kdv + sepetfiyati;

            string kdvGoster = kdv.ToString("c2");
            string vergidahilGoster = vergidahil.ToString("c2");
            string sepetfiyatiGoster = sepetfiyati.ToString("c2");

            textBox1.Text = sepetfiyatiGoster;
            textBox2.Text = kdvGoster;
            textBox3.Text = vergidahilGoster;

        }

        // Tercihlerimi Sıfırla
        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            checkBox25.Checked = false;
            checkBox26.Checked = false;
            checkBox27.Checked = false;
            checkBox28.Checked = false;
            checkBox29.Checked = false;
            checkBox30.Checked = false;
            checkBox31.Checked = false;
            checkBox32.Checked = false;
            checkBox33.Checked = false;

            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            comboBox12.Text = "";
            comboBox13.Text = "";
            comboBox14.Text = "";
            comboBox15.Text = "";
            comboBox16.Text = "";
            comboBox17.Text = "";
            comboBox18.Text = "";
            comboBox23.Text = "";
            comboBox24.Text = "";
            comboBox25.Text = "";
            comboBox26.Text = "";
            comboBox27.Text = "";
            comboBox28.Text = "";
            comboBox29.Text = "";
            comboBox30.Text = "";
            comboBox31.Text = "";
            comboBox32.Text = "";
            comboBox33.Text = "";
            comboBox34.Text = "";
            comboBox35.Text = "";
        }

        // Son Ürünü Çıkar
        private void button1_Click(object sender, EventArgs e)
        {
            // Ürünü sepetten kaldırır
            if (listView1.Items.Count > 0)
                listView1.Items.RemoveAt(listView1.Items.Count - 1);

            // Fiyat hesaplaması tekrar yapılır
            double sepetfiyati = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                sepetfiyati += Convert.ToDouble(item.SubItems[2].Text);
            }

            double kdv = sepetfiyati * Convert.ToDouble(label75.Text) / 100;
            double vergidahil = kdv + sepetfiyati;

            string kdvGoster = kdv.ToString("c2");
            string vergidahilGoster = vergidahil.ToString("c2");
            string sepetfiyatiGoster = sepetfiyati.ToString("c2");

            textBox1.Text = sepetfiyatiGoster;
            textBox2.Text = kdvGoster;
            textBox3.Text = vergidahilGoster;
        }

        // Sepetimi Temizle
        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        // Sepetimi Onayla
        private void button4_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand yorum = new SqlCommand("select * from Customer where TC="+ LoginBilgi.tc, baglantim);
            SqlDataReader dryorum = yorum.ExecuteReader();
            dryorum.Read();

            if (Convert.ToBoolean(dryorum["Siparis"]) == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "Siparişiniz Mevcut", "Halihazırda bir siparişiniz olduğundan yenisini şu an sipariş veremezsiniz.", ToolTipIcon.Warning);
                dryorum.Close();
            }
            else
            {
                dryorum.Close();

                SqlCommand kontrol = new SqlCommand("select * from Restoran where id=1", baglantim);
                SqlDataReader drkontrol = kontrol.ExecuteReader();
                drkontrol.Read();

                if (Convert.ToBoolean(drkontrol["SiparisVer"]) == false)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Servis Dışı", "Restoranımız yeni siparişlere kapalıdır. Lütfen daha sonra tekrar deneyiniz.", ToolTipIcon.Error);
                }
                else
                {
                    if (this.listView1.Items.Count > 0)
                    {
                        MenuEkrani.TabPages.Add(Ödeme);
                        MenuEkrani.SelectTab("Ödeme");
                        MenuEkrani.TabPages.Remove(Sepetiniz);
                    }
                    else
                    {
                        MenuEkrani.TabPages.Remove(Ödeme);
                        notifyIcon1.ShowBalloonTip(3000, "Geçersiz Sipariş", "Sepetiniz boş gözüküyor.", ToolTipIcon.Warning);
                    }
                }
                drkontrol.Close();
            }
            baglantim.Close();
        }

        // Ödemeyi yap
        private void label83_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count == 0)
            {
                notifyIcon1.ShowBalloonTip(3000, "Geçersiz Ödeme", "Sepetiniz boş gözüküyor.", ToolTipIcon.Warning);
            }
            else
            {
                if(comboBox19.Text != "(Kapıda Ödeme)")
                {
                    if (comboBox19.Text == "" || comboBox20.Text == "" || comboBox21.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox19.Text == "")
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
                    }
                    else if(textBox6.Text.Length != 16)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı Kart Numarası", "Kredi kartı numaranız 16 haneli olmalıdır.", ToolTipIcon.Warning);
                    }
                    else if(textBox19.Text.Length != 4)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı CVC Numarası", "CVC numaranız 4 haneli olmalıdır.", ToolTipIcon.Warning);
                    }
                    else if(comboBox21.Text.Length != 2 || comboBox20.Text.Length != 4)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı SKT", "SKT bilginiz yanlıştır, kontrol ediniz.", ToolTipIcon.Warning);
                    }
                    else
                    {
                        // Sepettekilerin kaydedilmesi

                        baglantim.Open();

                        SqlCommand siparis = new SqlCommand("insert into Siparisler (Tarih,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                        foreach (ListViewItem item in listView1.Items)
                        {
                            Siparis.sepet += Convert.ToString(item.SubItems[0].Text);
                            Siparis.sepet += " x";
                            Siparis.sepet += Convert.ToString(item.SubItems[1].Text);
                            Siparis.sepet += ", ";
                        }

                        Siparis.tarih = DateTime.Now.ToString("HH:mm:ss - MMM d yyyy");
                        Siparis.sepet = Siparis.sepet.Remove(Siparis.sepet.Length - 2);
                        Siparis.tutar = textBox3.Text.TrimStart('₺');
                        Siparis.odeme = "Ödendi";

                        if (textBox4.Text == "*boş bırakılabilir*" || textBox4.Text == "")
                        {
                            Siparis.dipnot = "(Boş)";
                        }
                        else
                        {
                            Siparis.dipnot = textBox4.Text;
                        }

                        Siparis.adres = textBox5.Text;
                        Siparis.isim = textBox7.Text;
                        Siparis.tc = LoginBilgi.tc;

                        siparis.Parameters.AddWithValue("@Tarih", Siparis.tarih);
                        siparis.Parameters.AddWithValue("@Siparis", Siparis.sepet);
                        siparis.Parameters.AddWithValue("@Odeme", Siparis.odeme);
                        siparis.Parameters.AddWithValue("@Tutar", Siparis.tutar);
                        siparis.Parameters.AddWithValue("@Dipnot", Siparis.dipnot);
                        siparis.Parameters.AddWithValue("@Adres", Siparis.adres);
                        siparis.Parameters.AddWithValue("@Isim", Siparis.isim);
                        siparis.Parameters.AddWithValue("@TC", Siparis.tc);
                        siparis.ExecuteNonQuery();

                        SqlCommand yorum = new SqlCommand("update Customer set Siparis=@Siparis where TC=" + LoginBilgi.tc, baglantim);
                        yorum.Parameters.AddWithValue("@Siparis", 1);
                        yorum.ExecuteNonQuery();

                        baglantim.Close();

                        Siparis.sepet = string.Empty;

                        DialogResult dialog = MessageBox.Show("Bizi seçtiğiniz için teşekkür ederiz! Faturanızı almak ister misiniz?", "Siparişiniz Alınmıştır", MessageBoxButtons.YesNo);

                        if (dialog == DialogResult.Yes)
                        {
                            printPreviewDialog1.Document = printDocument1;
                            printPreviewDialog1.ShowDialog();

                            listView1.Items.Clear();

                            notifyIcon1.Visible = false;
                            MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
                            gitMusteriAnaSayfa.Show();
                            this.Hide();
                        }
                        else
                        {
                            listView1.Items.Clear();

                            notifyIcon1.Visible = false;
                            MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
                            gitMusteriAnaSayfa.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    if(textBox5.Text == "")
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adres bilgisini giriniz.", ToolTipIcon.Warning);
                    }
                    else
                    {
                        // Sepettekilerin kaydedilmesi

                        baglantim.Open();

                        SqlCommand siparis = new SqlCommand("insert into Siparisler (Tarih,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                        foreach (ListViewItem item in listView1.Items)
                        {
                            Siparis.sepet += Convert.ToString(item.SubItems[0].Text);
                            Siparis.sepet += " x";
                            Siparis.sepet += Convert.ToString(item.SubItems[1].Text);
                            Siparis.sepet += ", ";
                        }

                        Siparis.tarih = DateTime.Now.ToString("HH:mm:ss - MMM d yyyy");
                        Siparis.sepet = Siparis.sepet.Remove(Siparis.sepet.Length - 2);
                        Siparis.tutar = textBox3.Text.TrimStart('₺');
                        Siparis.odeme = "Kapıda";

                        if(textBox4.Text == "*boş bırakılabilir*" || textBox4.Text == "")
                        {
                            Siparis.dipnot = "(Boş)";
                        }
                        else
                        {
                            Siparis.dipnot = textBox4.Text;
                        }

                        Siparis.adres = textBox5.Text;

                        SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
                        SqlDataReader drprofil = profil.ExecuteReader();
                        drprofil.Read();
                        Siparis.isim = (string)drprofil["Name"] + " " + (string)drprofil["Surname"];
                        drprofil.Close();

                        Siparis.tc = LoginBilgi.tc;

                        siparis.Parameters.AddWithValue("@Tarih", Siparis.tarih);
                        siparis.Parameters.AddWithValue("@Siparis", Siparis.sepet);
                        siparis.Parameters.AddWithValue("@Odeme", Siparis.odeme);
                        siparis.Parameters.AddWithValue("@Tutar", Siparis.tutar);
                        siparis.Parameters.AddWithValue("@Dipnot", Siparis.dipnot);
                        siparis.Parameters.AddWithValue("@Adres", Siparis.adres);
                        siparis.Parameters.AddWithValue("@Isim", Siparis.isim);
                        siparis.Parameters.AddWithValue("@TC", Siparis.tc);
                        siparis.ExecuteNonQuery();

                        SqlCommand yorum = new SqlCommand("update Customer set Siparis=@Siparis where TC=" + LoginBilgi.tc, baglantim);
                        yorum.Parameters.AddWithValue("@Siparis", 1);
                        yorum.ExecuteNonQuery();

                        baglantim.Close();

                        Siparis.sepet = string.Empty;
                       
                        MessageBox.Show("Bizi seçtiğiniz için teşekkür ederiz! Faturanız ödeme yaptığınızda kesilecektir.", "Siparişiniz Alınmıştır", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        notifyIcon1.Visible = false;
                        MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
                        gitMusteriAnaSayfa.Show();
                        this.Hide();
                    }
                }
            }
        }

        // Kapıda ödeme, kredi kartı bilgilerini girilemez yapar
        private void comboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox19.Text == "(Kapıda Ödeme)")
            {
                textBox19.Enabled = false;
                comboBox20.Enabled = false;
                comboBox21.Enabled = false;
                textBox5.Enabled = true;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                checkBox34.Enabled = false;
            }
            else
            {
                textBox19.Enabled = true;
                comboBox20.Enabled = true;
                comboBox21.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                checkBox34.Enabled = true;
            }
        }

        // Sepete Dön
        private void label84_Click(object sender, EventArgs e)
        {
            MenuEkrani.TabPages.Add(Sepetiniz);
            MenuEkrani.SelectTab("Sepetiniz");
            MenuEkrani.TabPages.Remove(Ödeme);
        }

        // Yazdırma İşlemi
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Logo
            baglantim.Open();
            SqlCommand fatura = new SqlCommand("select * from Resimler where id=39", baglantim);
            SqlDataReader drfatura = fatura.ExecuteReader();
            drfatura.Read();

            byte[] resim = (byte[])drfatura["RESIM"];
            drfatura.Close();
            baglantim.Close();
            MemoryStream stream = new MemoryStream(resim);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            e.Graphics.DrawImage(Image.FromStream(stream), 20, 50, 800, 800);

            // Tarih
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(90, 405));

            // İsim
            e.Graphics.DrawString(textBox7.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(150, 500), sf);

            // TC
            e.Graphics.DrawString(LoginBilgi.tc, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(80, 565));

            // Kart No
            e.Graphics.DrawString(textBox6.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(150, 670), sf);

            // Adres
            string adres = textBox5.Text;
            using (Font font = new Font("Arial", 90, FontStyle.Regular, GraphicsUnit.Point))
            {
                Rectangle rect = new Rectangle(280, 4450, 1500, 500);

                TextFormatFlags flags = TextFormatFlags.WordBreak;
                TextRenderer.DrawText(e.Graphics, adres, font, rect, Color.Black, flags);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rect));
            }

            // Sepet

            int y = 140; // yükseklik isim
            int y1 = 140; // yükseklik adet
            int y2 = 140; // yükseklik fiyat

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                e.Graphics.DrawString(listView1.Items[i].Text, new Font("Arial", 15, FontStyle.Bold), SystemBrushes.ActiveCaptionText, new Point(320, y+=25));
                e.Graphics.DrawString(listView1.Items[i].SubItems[1].Text, new Font("Arial", 15, FontStyle.Bold), SystemBrushes.ActiveCaptionText, new Point(600, y1+=25));
                e.Graphics.DrawString(listView1.Items[i].SubItems[2].Text, new Font("Arial", 15, FontStyle.Bold), SystemBrushes.ActiveCaptionText, new Point(700, y2+=25));
            }

            // Ücret
            e.Graphics.DrawString(textBox1.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(700, 643), sf);

            // Vergi
            e.Graphics.DrawString(textBox2.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(700, 674), sf);

            // Toplam Tutar
            e.Graphics.DrawString(textBox3.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(700, 705), sf);

        }

        // isim - soyisim rakam yok
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // Kart no harf yok
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }

            if(textBox6.Text.Length > 16 && e.KeyChar != 8)
            {
                e.Handled = true;
                notifyIcon1.ShowBalloonTip(3000, "Kart No Sınırı", "Kart numaranız 16 haneyi geçemez.", ToolTipIcon.Warning);
            }
        }

        // CVC no harf yok
        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // SKT - 1 harf yok
        private void comboBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // SKT - 2 harf yok
        private void comboBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // ara sıcaklar harf engelle
        private void comboBox10_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // ana yemekler harf engelle
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // tatlılar harf engelle
        private void comboBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // içecekler harf engelle
        private void comboBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // Bilgilerimi otomatik doldur
        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox34.Checked)
            {
                baglantim.Open();

                SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
                SqlDataReader drprofil = profil.ExecuteReader();

                if (drprofil.HasRows)
                {
                    drprofil.Read();
                    if (!DBNull.Value.Equals(drprofil["Name"]))
                    {
                        textBox7.Text = (string)drprofil["Name"] + " " + (string)drprofil["Surname"];
                    }
                    if (!DBNull.Value.Equals(drprofil["Credit_Card"]))
                    {
                        textBox6.Text = (string)drprofil["Credit_Card"];
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
                }
                baglantim.Close();
            }
            else
            {
                textBox7.Text = "";
                textBox6.Text = "";
                textBox19.Text = "";
                comboBox21.Text = "";
                comboBox20.Text = "";
                textBox5.Text = "";
            }
        }

        // Dipnot

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = "";
            textBox4.Font = new Font("Maindra GD", 11, FontStyle.Bold);
            textBox4.ForeColor = Color.Black;
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
    }
}
