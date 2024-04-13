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
    public partial class PersonelAnaSayfa : Form
    {
        public PersonelAnaSayfa()
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

        private void Profil_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelProfil gitPersonelProfil = new PersonelProfil();
            gitPersonelProfil.Show();
            this.Hide();
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

        private void PersonelAnaSayfa_Load(object sender, EventArgs e)
        {
            baglantim.Open();

            if (LoginBilgi.giris == true)
            {
                SqlCommand login = new SqlCommand("select * from Employee where tc=" + LoginBilgi.tc, baglantim);
                SqlDataReader drlogin = login.ExecuteReader();
                drlogin.Read();
                notifyIcon1.ShowBalloonTip(3000, "Hoş Geldiniz", drlogin["Name"] + " " + drlogin["Surname"] + " , sizi görmek güzel.", ToolTipIcon.Info);
                drlogin.Close();

                LoginBilgi.giris = false;
            }

            SqlCommand profil = new SqlCommand("select * from Employee where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();
            byte[] resim = (byte[])drprofil["Resim"];
            MemoryStream memorystream = new MemoryStream(resim);
            LogoPersonel.BackgroundImage = Image.FromStream(memorystream);

            baglantim.Close();
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

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelTarifler gitPersonelTarifler = new PersonelTarifler();
            gitPersonelTarifler.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelSiparisler gitPersonelSiparisler = new PersonelSiparisler();
            gitPersonelSiparisler.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelStok gitPersonelStok = new PersonelStok();
            gitPersonelStok.Show();
            this.Hide();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Tariflere bak!", button3);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Siparişlere bak!", button2);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Stoklara bak!", button4);
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
