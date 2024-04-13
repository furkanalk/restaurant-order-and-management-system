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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void Form1_Load(object sender, EventArgs e)
        {
            if (LoginBilgi.mkayit == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Oldunuz", "Aramıza hoş geldiniz!", ToolTipIcon.Info);
                LoginBilgi.mkayit = false;
            }

            if (LoginBilgi.pkayit == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "Talep Gönderildi", "Başvurunuzu değerlendireceğiz, teşekkürler!.", ToolTipIcon.Info);
                LoginBilgi.pkayit = false;
            }
            
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;

            baglantim.Open();

            SqlCommand check = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drcheck = check.ExecuteReader();
            drcheck.Read();

            if(Convert.ToBoolean(drcheck["Basvuru"]) == true)
            {
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox1.Visible = true;
            }
            drcheck.Close();

            baglantim.Close();
        }

        private void Kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        bool hareket; int mouseX; int mouseY;
        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void MainScreen_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        private void TiklaMusteri_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            LoginMusteri gitLoginMusteri = new LoginMusteri();
            gitLoginMusteri.Show();
            this.Hide();
        }

        private void TiklaHesapOlustur_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            LoginBilgi.basvur = true;
            Register gitRegisterMusteri = new Register();
            gitRegisterMusteri.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand check = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drcheck = check.ExecuteReader();
            drcheck.Read();

            if (Convert.ToBoolean(drcheck["Basvuru"]) == true)
            {
                baglantim.Close();
                notifyIcon1.Visible = false;
                LoginBilgi.basvur = false;
                Register gitRegisterMusteri = new Register();
                gitRegisterMusteri.Show();
                this.Hide();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Başvurular Kapalı", "Şu anda başvuru yapamazsınız.", ToolTipIcon.Error);
            }
            drcheck.Close();

            baglantim.Close(); 
        }

        private void TiklaPersonel_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            LoginPersonel gitLoginPersonel = new LoginPersonel();
            gitLoginPersonel.Show();
            this.Hide();
        }

        private void TiklaSef_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            LoginSef gitLoginSef = new LoginSef();
            gitLoginSef.Show();
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
            Application.Exit();
        }
    }
}
