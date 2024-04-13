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
using System.Net;
using System.Net.Mail;
using System.IO;

namespace RestaurantAtlantis
{
    public partial class LoginMusteri : Form
    {
        public LoginMusteri()
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
            _veri.Query("select count(*) from Customer where TC=@TC and Password=@Password");
            if((int)_veri.table.Rows[0][0]==1)
            {
                return true;
            }
            MessageBox.Show("Yanlış TC veya SIFRE girdiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            return false;
        }
        private void ButtonGiris_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if(Login() == true)
            {
                SqlCommand login = new SqlCommand("select * from Customer where tc="+ GirisTC.Text, baglantim);
                SqlDataReader drlogin = login.ExecuteReader();

                if (drlogin.HasRows)
                {
                    drlogin.Read();
                    
                    if(!DBNull.Value.Equals(drlogin["Password"]))
                    {
                        LoginBilgi.sifre = (string)drlogin["Password"];
                    }
                    if(!DBNull.Value.Equals(drlogin["TC"]))
                    {
                        LoginBilgi.tc = (string)drlogin["TC"];
                    }
                }

                LoginBilgi.giris = true;
                drlogin.Close();

                notifyIcon1.Visible = false;
                this.Hide();
                MusteriAnaSayfa gitMusteriAnaSayfa = new MusteriAnaSayfa();
                gitMusteriAnaSayfa.ShowDialog();              
            }
            baglantim.Close();
        }

        bool hareket; int mouseX; int mouseY;
        private void LoginMusteri_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void LoginMusteri_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void LoginMusteri_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        private void LoginMusteri_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
        }

        private void buraya_tikla_Click(object sender, EventArgs e)
        {
            this.panelGiris.Visible = false;
            this.panel1.Visible = true;
        }

        // Şifremi unuttum (1)
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                bool kontrol = false;

                foreach (DataRow dr in table.Rows)
                {
                    if (textBox2.Text == (string)dr["TC"])
                    {
                        kontrol = true;
                    }
                }

                if (kontrol == false)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz TC Numarası sistemde kayıtlı değildir.", ToolTipIcon.Error);
                }

                if (kontrol == true)
                {
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Şifremi unuttum (1) iptal
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            this.panel1.Visible = false;
            this.panelGiris.Visible = true;
        }

        // Şifremi unuttum (2)
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                bool kontrol = false;

                foreach (DataRow dr in table.Rows)
                {
                    if (textBox1.Text == (string)dr["Mail"])
                    {
                        kontrol = true;
                    }
                }

                if (kontrol == false)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz TC Numarası sistemde kayıtlı değildir.", ToolTipIcon.Error);
                }

                if (kontrol == true)
                {
                    this.panel2.Visible = false;
                    this.panel3.Visible = true;

                    baglantim.Open();
                    SqlCommand soru = new SqlCommand("select * from Customer where TC='" + textBox2.Text + "'", baglantim);
                    SqlDataReader drsoru = soru.ExecuteReader();
                    drsoru.Read();
                    
                    label5.Text = (string)drsoru["GSoru"];
                    drsoru.Close();
                    baglantim.Close();
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Şifremi unuttum (2) iptal
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
            this.panel2.Visible = false;
            this.panelGiris.Visible = true;
        }

        // Şifremi unuttum (3)
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                bool kontrol = false;

                baglantim.Open();

                SqlCommand cevap = new SqlCommand("select * from Customer where TC='" + textBox2.Text + "'", baglantim);
                SqlDataReader drcevap = cevap.ExecuteReader();
                drcevap.Read();

                if (textBox3.Text == (string)drcevap["GCevap"])
                {
                    kontrol = true;
                }
                drcevap.Close();

                if (kontrol == false)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hata", "Yanlış yanıt.", ToolTipIcon.Error);
                }

                if (kontrol == true)
                {
                    SqlCommand bul = new SqlCommand("select * from Customer where TC='" + textBox2.Text + "'", baglantim);
                    SqlDataReader drbul = bul.ExecuteReader();
                    drbul.Read();

                    MailMessage sifre = new MailMessage("RestoranAtlantis@outlook.com", textBox1.Text);
                    sifre.IsBodyHtml = true;
                    sifre.Subject = "Şifrenizi mi Unuttunuz?";
                    sifre.Body = 
                        "<strong>" 
                        + "Merhaba, " + (string)drbul["Name"] + " " + (string)drbul["Surname"] 
                        + "<br>" 
                        + "Sizden şifrenizi unuttuğunuza dair bir bildirim aldık."
                        + "<br> <br> <font size = 5>" 
                        + "Şifreniz: " + (string)drbul["Password"]
                        + "<font size = 5> <br> <br> </strong>";
                    drbul.Close();
                    
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("RestoranAtlantis@outlook.com", "Atlantis5353");
                    smtp.EnableSsl = true;
                    smtp.Send(sifre);

                    notifyIcon1.ShowBalloonTip(3000, "Şifreniz Gönderildi", "Lütfen Posta Kutunuzu kontrol ediniz.", ToolTipIcon.Info);

                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                    this.panel3.Visible = false;
                    this.panelGiris.Visible = true;
                }
                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Şifremi unuttum (3) iptal
        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            this.panel3.Visible = false;
            this.panelGiris.Visible = true;
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
