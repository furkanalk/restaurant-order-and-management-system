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

namespace RestaurantAtlantis
{
    public partial class SefAnaSayfa : Form
    {
        public SefAnaSayfa()
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
            /*empty*/
        }

        private void Profiller_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefProfiller gitSefProfiller = new SefProfiller();
            gitSefProfiller.Show();
            this.Hide();
        }

        private void Menü2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefMenu gitSefMenu = new SefMenu();
            gitSefMenu.Show();
            this.Hide();
        }

        private void Tarifler_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefYorumlar gitSefTarifler = new SefYorumlar();
            gitSefTarifler.Show();
            this.Hide();
        }

        private void Yorumlar_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefBilanço gitSefYorumlar = new SefBilanço();
            gitSefYorumlar.Show();
            this.Hide();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefMenu gitSefMenu = new SefMenu();
            gitSefMenu.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefYorumlar gitSefTarifler = new SefYorumlar();
            gitSefTarifler.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefBilanço gitSefYorumlar = new SefBilanço();
            gitSefYorumlar.Show();
            this.Hide();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Tariflerine bak!", button5);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Yorumlara bak!", button4);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Bilanço durumuna bak!", button2);
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void SefAnaSayfa_Load(object sender, EventArgs e)
        {
            baglantim.Open();

            if (LoginBilgi.giris == true)
            {
                SqlCommand login = new SqlCommand("select * from Chef where tc=" + LoginBilgi.tc, baglantim);
                SqlDataReader drlogin = login.ExecuteReader();
                drlogin.Read();
                notifyIcon1.ShowBalloonTip(3000, "Hoş Geldiniz", drlogin["Name"] + " " + drlogin["Surname"] + " , sizi görmek güzel.", ToolTipIcon.Info);
                drlogin.Close();

                LoginBilgi.giris = false;
            }

            baglantim.Close();
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
