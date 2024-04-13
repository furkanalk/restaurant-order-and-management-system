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
    public partial class LoginPersonel : Form
    {
        public LoginPersonel()
        {
            InitializeComponent();
        }

        readonly Veritabani _veri = new Veritabani();

        private void Kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ButtonGeriDon_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        private void GirisTC_Enter(object sender, EventArgs e)
        {
            if (GirisTC.Text == "T.C. Kimlik No")
            {
                GirisTC.Text = "";

            }

            if (GirisSifre.Text == "")
            {
                GirisSifre.Text = "Sifreniz";
            }
        }

        private void GirisTC_Leave(object sender, EventArgs e)
        {
            if (GirisSifre.Text == "")
            {
                GirisSifre.Text = "Sifreniz";
            }
        }

        private void GirisSifre_Enter(object sender, EventArgs e)
        {
            if (GirisSifre.Text == "Sifreniz")
            {
                GirisSifre.Text = "";
            }

            if (GirisTC.Text == "")
            {
                GirisTC.Text = "T.C. Kimlik No";
            }
        }

        private void GirisSifre_Leave(object sender, EventArgs e)
        {
            if (GirisTC.Text == "")
            {
                GirisTC.Text = "T.C. Kimlik No";
            }
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        bool Login()
        {
            _veri.Param("@TC", GirisTC.Text);
            _veri.Param("@password", GirisSifre.Text);
            _veri.Query("select count(*) from Employee where TC=@TC and Password=@Password");
            if ((int)_veri.table.Rows[0][0] == 1)
            {
                return true;
            }
            MessageBox.Show("Yanlis TC veya SIFRE girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
        private void ButtonGiris_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (Login() == true)
            {
                SqlCommand login = new SqlCommand("select * from Employee where tc=" + GirisTC.Text, baglantim);
                SqlDataReader drlogin = login.ExecuteReader();

                if (drlogin.HasRows)
                {
                    drlogin.Read();

                    if (!DBNull.Value.Equals(drlogin["Password"]))
                    {
                        LoginBilgi.sifre = (string)drlogin["Password"];
                    }
                    if (!DBNull.Value.Equals(drlogin["TC"]))
                    {
                        LoginBilgi.tc = (string)drlogin["TC"];
                    }
                }

                LoginBilgi.giris = true;
                drlogin.Close();

                notifyIcon1.Visible = false;
                this.Hide();
                PersonelAnaSayfa gitPersonelAnaSayfa = new PersonelAnaSayfa();
                gitPersonelAnaSayfa.ShowDialog();
            }
            baglantim.Close();
        }

        bool hareket; int mouseX; int mouseY;
        private void LoginPersonel_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void LoginPersonel_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void LoginPersonel_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
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
    }
}
