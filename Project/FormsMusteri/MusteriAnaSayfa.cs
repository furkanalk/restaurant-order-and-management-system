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
    public partial class MusteriAnaSayfa : Form
    {
        public MusteriAnaSayfa()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            /*empty*/
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
            notifyIcon1.Visible = false;
            MusteriMenu gitMusteriMenu = new MusteriMenu();
            gitMusteriMenu.Show();
            this.Hide();
        }

        private void Odeme_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriYorumlar gitMusteriYorumlar = new MusteriYorumlar();
            gitMusteriYorumlar.Show();
            this.Hide();  
        }

        private void Siparisler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriSiparişler gitMusteriSiparişler = new MusteriSiparişler();
            gitMusteriSiparişler.Show();
            this.Hide();
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

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void MusteriAnaSayfa_Load(object sender, EventArgs e)
        {
            baglantim.Open();

            if (LoginBilgi.giris == true)
            {
                SqlCommand login = new SqlCommand("select * from Customer where tc=" + LoginBilgi.tc, baglantim);
                SqlDataReader drlogin = login.ExecuteReader();
                drlogin.Read();
                notifyIcon1.ShowBalloonTip(3000, "Hoş Geldiniz", drlogin["Name"] + " " + drlogin["Surname"] + " , sizi görmek güzel.", ToolTipIcon.Info);
                drlogin.Close();

                LoginBilgi.giris = false;
            }

            SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();

            byte[] resim = (byte[])drprofil["Resim"];
            drprofil.Close();
            MemoryStream memorystream = new MemoryStream(resim);
            LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

            baglantim.Close();
        }

        // Sipariş
        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Sipariş ver!", button3);
        }

        // Yorum
        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Yorum yap!", button4);
        }

        // Kurye
        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Hemen öğren!", button2);           
        }

        // Online sipariş ver
        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriMenu gitMusteriMenu = new MusteriMenu();
            gitMusteriMenu.Show();
            this.Hide();
        }

        // Durum öğren
        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriSiparişler gitMusteriSiparişler = new MusteriSiparişler();
            gitMusteriSiparişler.Show();
            this.Hide();
        }

        // Yorum yap
        private void button4_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MusteriYorumlar gitMusteriYorumlar = new MusteriYorumlar();
            gitMusteriYorumlar.Show();
            this.Hide();
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
