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
    public partial class PersonelProfil : Form
    {
        public PersonelProfil()
        {
            InitializeComponent();
        }

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
            /*empty*/
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelTarifler gitPersonelTarifler = new PersonelTarifler();
            gitPersonelTarifler.Show();
            this.Hide();
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

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void PersonelProfil_Load(object sender, EventArgs e)
        {
            textBox7.Text = textBox2.Text + " " + textBox3.Text;
            this.panel5.Visible = true;
            this.panel6.Visible = false;
            this.panel10.Visible = false;

            comboBox1.Enabled = false;
            textBox19.Enabled = false;
            textBox4.Enabled = false;
            textBox7.Enabled = false;

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Employee where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();
            byte[] resim = (byte[])drprofil["Resim"];
            MemoryStream memorystream = new MemoryStream(resim);
            pictureBox3.BackgroundImage = Image.FromStream(memorystream);
            LogoPersonel.BackgroundImage = Image.FromStream(memorystream);

            if (drprofil.HasRows)
            {
                if (!DBNull.Value.Equals(drprofil["Name"]))
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
                if (textBox2.Text != null && textBox3.Text != null)
                {
                    textBox7.Text = textBox2.Text + " " + textBox3.Text;
                }
                if (!DBNull.Value.Equals(drprofil["Mail"]))
                {
                    string mail = (string)drprofil["Mail"];
                    textBox6.Text = mail.Split('@').First();
                    comboBox2.Text = mail.Split('@').Last();
                }
                if (!DBNull.Value.Equals(drprofil["Pozisyon"]))
                {
                    comboBox1.Text = (string)drprofil["Pozisyon"];
                }
                if (!DBNull.Value.Equals(drprofil["Maas"]))
                {
                    textBox19.Text = (string)drprofil["Maas"] + " ₺";
                }
                if (!DBNull.Value.Equals(drprofil["Banka"]))
                {
                    comboBox21.Text = (string)drprofil["Banka"];
                }
                if (!DBNull.Value.Equals(drprofil["IBAN"]))
                {
                    textBox5.Text = (string)drprofil["IBAN"];
                }               
            }
            drprofil.Close();
            baglantim.Close();
        }

        // Kişisel bilgilere git
        private void button3_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = true;
            this.panel10.Visible = false;
        }

        // Ödeme bilgierini kaydet
        private void label15_Click(object sender, EventArgs e)
        {          
            if (comboBox21.Text != "")
            {
                if (textBox5.Text != "")
                {
                    if (textBox5.Text.Length != 24)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı IBAN", "IBAN TR + 24 haneden oluşmalıdır.", ToolTipIcon.Warning);
                    }
                    else
                    {
                        baglantim.Open();

                        SqlCommand yenitel = new SqlCommand("update Employee set Banka=@Banka,IBAN=@IBAN where TC='" + textBox4.Text + "'", baglantim);
                        yenitel.Parameters.AddWithValue("@IBAN", textBox5.Text);
                        yenitel.Parameters.AddWithValue("@Banka", comboBox21.Text);
                        yenitel.ExecuteNonQuery();

                        baglantim.Close();
                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Maaş bilgileriniz başarıyla güncellendi.", ToolTipIcon.Info);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen IBAN giriniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen Bankanızı seçiniz.", ToolTipIcon.Warning);
            } 
        }

        // Maaş bilgilerine git
        private void button2_Click(object sender, EventArgs e)
        {
            this.panel10.Visible = true;
            this.panel5.Visible = false;
        }

        // Banka yazı yazılamaz
        private void comboBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Mail yazı yazılamaz
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Kaydet
        private void button4_Click(object sender, EventArgs e)
        {
            // İsim update

            if (textBox8.Text.Length > 6 || textBox8.Text.Length == 6) 
            {
                if (textBox6.Text != "")
                {
                    if (comboBox2.Text != "")
                    {
                        baglantim.Open();
                        SqlCommand kaydet = new SqlCommand("update Employee set Name=@Name,Surname=@Surname,PhoneNumber=@PhoneNumber,Password=@Password,Mail=@Mail where TC='" + textBox4.Text + "'", baglantim);

                        kaydet.Parameters.AddWithValue("@Name", textBox2.Text);
                        kaydet.Parameters.AddWithValue("@Surname", textBox3.Text);
                        kaydet.Parameters.AddWithValue("@PhoneNumber", textBox1.Text);
                        kaydet.Parameters.AddWithValue("@Password", textBox8.Text);
                        kaydet.Parameters.AddWithValue("@Mail", textBox6.Text + "@" + comboBox2.Text);
                        kaydet.ExecuteNonQuery();
                        baglantim.Close();
                        textBox7.Text = textBox2.Text + " " + textBox3.Text;

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgileriniz başarıyla güncellendi.", ToolTipIcon.Info);
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adres sağlayıcınızı seçiniz.", ToolTipIcon.Warning);
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

        // Resim
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = true;       
        }

        int resim = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            resim = 1;
            button5.BackColor = Color.Goldenrod;
            button6.BackColor = Color.Wheat;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            resim = 2;
            button6.BackColor = Color.Goldenrod;
            button5.BackColor = Color.Wheat;
        }

        // Resim Kaydet
        private void button17_Click(object sender, EventArgs e)
        {
            if(resim == 1)
            {
                baglantim.Open();
                
                SqlCommand update = new SqlCommand("select * from Resim where id = 1", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Employee set Resim=@Resim,Cinsiyet=@Cinsiyet where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox3.BackgroundImage = Image.FromStream(memorystream);
                LogoPersonel.BackgroundImage = Image.FromStream(memorystream);

                button5.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();

            }
            else if(resim == 2)
            {
                baglantim.Open();

                SqlCommand update = new SqlCommand("select * from Resim where id = 2", baglantim);
                SqlDataReader drupdate = update.ExecuteReader();
                drupdate.Read();

                byte[] resim = (byte[])drupdate["RESIM"];
                drupdate.Close();

                SqlCommand yeni = new SqlCommand("update Employee set Resim=@Resim,Cinsiyet=@Cinsiyet where TC=" + LoginBilgi.tc, baglantim);
                yeni.Parameters.AddWithValue("@Resim", resim);
                yeni.Parameters.AddWithValue("@Cinsiyet", "Kadın");
                yeni.ExecuteNonQuery();

                MemoryStream memorystream = new MemoryStream(resim);
                pictureBox3.BackgroundImage = Image.FromStream(memorystream);
                LogoPersonel.BackgroundImage = Image.FromStream(memorystream);

                button6.BackColor = Color.Wheat;
                this.panel6.Visible = false;

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Avatarınız başarıyla değiştirildi.", ToolTipIcon.Info);

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Avatarınızı değiştirmek için resim seçiniz.", ToolTipIcon.Warning);
            }
        }

        // İptal resim
        private void button18_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;

            button5.BackColor = Color.Wheat;
            button6.BackColor = Color.Wheat;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBox5.Text.Length == 24)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    notifyIcon1.ShowBalloonTip(3000, "IBAN Sınırı", "24 Haneden fazla yazamazsınız.", ToolTipIcon.Warning);
                }
            }
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
