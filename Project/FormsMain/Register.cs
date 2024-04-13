using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RestaurantAtlantis
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Buraya_tikla_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            LoginMusteri gitLoginMusteri = new LoginMusteri();
            gitLoginMusteri.Show();
            this.Hide();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void ButtonKayit_Click(object sender, EventArgs e)
        {
            if (KayitAd.Text == "" || KayitSoyad.Text == "" || KayitTelno.Text == "" || KayitTC.Text == "" || textBox8.Text == "" || KayitSifre.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
            }
            else if (KayitTC.Text.Length != 11)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı TC", "TC numarası 11 haneli olmalıdır.", ToolTipIcon.Warning);
            }
            else if (KayitSifre.Text.Length < 6)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Şifre", "Şifreniz en az 6 haneli olmalıdır.", ToolTipIcon.Warning);
            }
            else if(textBox8.Text.Contains("@"))
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Mail", "Mail adresiniz '@' karakterini içeremez.", ToolTipIcon.Warning);
            }
            else if (comboBox3.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adres sağlayıcınızı seçiniz.", ToolTipIcon.Warning);
            }
            else if (comboBox4.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Güvenlik Sorusu", "Lütfen bir soru seçiniz.", ToolTipIcon.Warning);
            }
            else if (textBox9.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Güvenlik Sorusu", "Lütfen seçtiğiniz soruyu cevaplayınız.", ToolTipIcon.Warning);
            }
            else
            {
                baglantim.Open();

                bool kontrol = true;

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    if (KayitTC.Text == (string)dr["TC"])
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz TC Numarası sistemde kayıtlıdır.", ToolTipIcon.Warning);
                        kontrol = false;
                    }
                }

                if (kontrol == true)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        if (textBox8.Text + "@" + comboBox3.Text == (string)dr["Mail"])
                        {
                            notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz Mail Adresi sistemde kayıtlıdır.", ToolTipIcon.Warning);
                            kontrol = false;
                        }
                    }
                }

                if (kontrol == true)
                {
                    SqlCommand resimbul = new SqlCommand("select * from Resim where id=21", baglantim);
                    SqlDataReader drresimbul = resimbul.ExecuteReader();
                    drresimbul.Read();
                    byte[] resim = (byte[])drresimbul["RESIM"];
                    drresimbul.Close();

                    SqlCommand musteri = new SqlCommand("insert into Customer  "
                        + "(Name,Surname,Password,TC,PhoneNumber,Mail,Siparis,Yorum,GSoru,GCevap,Cinsiyet,Resim) "
                        + "values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Siparis,@Yorum,@GSoru,@GCevap,@Cinsiyet,@Resim)", baglantim);
                    musteri.Parameters.AddWithValue("@Name", KayitAd.Text);
                    musteri.Parameters.AddWithValue("@Surname", KayitSoyad.Text);
                    musteri.Parameters.AddWithValue("@Password", KayitSifre.Text);
                    musteri.Parameters.AddWithValue("@TC", KayitTC.Text);
                    musteri.Parameters.AddWithValue("@PhoneNumber", KayitTelno.Text);
                    musteri.Parameters.AddWithValue("@Mail", textBox8.Text + "@" + comboBox3.Text);
                    musteri.Parameters.AddWithValue("@Siparis", 0);
                    musteri.Parameters.AddWithValue("@Yorum", 0);
                    musteri.Parameters.AddWithValue("@GSoru", comboBox4.Text);
                    musteri.Parameters.AddWithValue("@GCevap", textBox9.Text);
                    musteri.Parameters.AddWithValue("@Resim", resim);
                    musteri.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                    musteri.ExecuteNonQuery();

                    LoginBilgi.mkayit = true;

                    baglantim.Close();

                    notifyIcon1.Visible = false;
                    MainScreen gitMainScreen = new MainScreen();
                    gitMainScreen.Show();
                    this.Hide();
                }
                baglantim.Close();
            }
        }

        bool hareket; int mouseX; int mouseY;
        private void RegisterMusteri_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void RegisterMusteri_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = false;
        }

        private void RegisterMusteri_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == true)
            {
                SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        // Kapat
        private void Kapat_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        // Ekranı Küçült
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

        private void RegisterMusteri_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            if (LoginBilgi.basvur == true)
            {
                this.panel5.Visible = false;
                this.panelGiris.Visible = true;
            }
            else
            {
                this.panel5.Visible = true;
                this.panelGiris.Visible = false;

                SqlCommand stok = new SqlCommand("select * from Pozisyonlar", baglantim);
                SqlDataAdapter adapter = new SqlDataAdapter(stok);
                DataTable table = new DataTable();
                adapter.Fill(table);

                comboBox1.Items.Clear();

                foreach (DataRow dr in table.Rows)
                {
                    comboBox1.Items.Add((string)dr["Poz"]);
                }
            }
        }

        // Geri Dön
        private void button1_Click(object sender, EventArgs e)
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

        // Başvur
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox7.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
            }
            else if(textBox4.Text.Length < 6)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Şifre", "Şifreniz en az 6 haneli olmalıdır.", ToolTipIcon.Warning);
            }
            else if(textBox2.Text.Length != 11)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı TC", "TC numarası 11 haneli olmalıdır.", ToolTipIcon.Warning);
            }
            else if(comboBox2.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen adres sağlayıcınızı seçiniz.", ToolTipIcon.Warning);
            }
            else if(comboBox1.Text == "")
            {
                notifyIcon1.ShowBalloonTip(3000, "Pozisyon Seçiniz", "Lütfen ilgili olduğunuz pozisyonu seçiniz.", ToolTipIcon.Warning);
            }

            else
            {
                bool kontrol = true;

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    if (textBox2.Text == (string)dr["TC"])
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz TC Numarası sistemde kayıtlıdır.", ToolTipIcon.Warning);
                        kontrol = false;
                    }
                }

                if(kontrol == true)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        if (textBox2.Text == (string)dr["TC"])
                        {
                            notifyIcon1.ShowBalloonTip(3000, "Hata", "Girmiş olduğunuz TC Numarası ile başvuru yapılmıştır.", ToolTipIcon.Warning);
                            kontrol = false;
                        }
                    }
                }
                
                if (kontrol == true)
                {
                    baglantim.Open();

                    string not = textBox5.Text;

                    if (textBox5.Text == "")
                    {
                        not = "(Detay yok)";
                    }

                    if (pdfkontrol == true)
                    {
                        byte[] pdf = File.ReadAllBytes(dosyaismi);

                        SqlCommand personel = new SqlCommand("insert into EmployeeBasvur (Name,Surname,Password,TC,PhoneNumber,Mail,Pozisyon,Notlar,PDF,Uzantı) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Pozisyon,@Notlar,@PDF,@Uzantı)", baglantim);
                        personel.Parameters.AddWithValue("@Name", textBox6.Text);
                        personel.Parameters.AddWithValue("@Surname", textBox7.Text);
                        personel.Parameters.AddWithValue("@Password", textBox4.Text);
                        personel.Parameters.AddWithValue("@TC", textBox2.Text);
                        personel.Parameters.AddWithValue("@PhoneNumber", textBox1.Text);
                        personel.Parameters.AddWithValue("@Mail", textBox3.Text + "@" + comboBox2.Text);
                        personel.Parameters.AddWithValue("@Pozisyon", comboBox1.Text);
                        personel.Parameters.AddWithValue("@Notlar", not);
                        personel.Parameters.Add("@PDF", SqlDbType.VarBinary, pdf.Length).Value = pdf;
                        personel.Parameters.AddWithValue("@Uzantı", uzanti);
                        personel.ExecuteNonQuery();

                        LoginBilgi.pkayit = true;

                        notifyIcon1.Visible = false;
                        MainScreen gitMainScreen = new MainScreen();
                        gitMainScreen.Show();
                        this.Hide();
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "CV Eksik", "Lütfen CV yüklemesi yapınız.", ToolTipIcon.Warning);
                    }
                    baglantim.Close();
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        string dosyaismi;
        string uzanti;
        bool pdfkontrol = false;
        // CV logo
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            OpenFileDialog cv = new OpenFileDialog();
            cv.Title = "CV Yükle";
            cv.Filter = "PDF Dosyaları|*.pdf";

            if (cv.ShowDialog() == DialogResult.OK)
            {
                pdfkontrol = true;
                dosyaismi = cv.FileName;
                uzanti = new FileInfo(dosyaismi).Extension;
            }
        }

        // CV yükle button
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog cv = new OpenFileDialog();
            cv.Title = "CV Yükle";
            cv.Filter = "PDF Dosyaları|*.pdf";

            if (cv.ShowDialog() == DialogResult.OK)
            {
                pdfkontrol = true;
                dosyaismi = cv.FileName;
                uzanti = new FileInfo(dosyaismi).Extension;
            }
        }

        // TC klayve engeli
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            if (textBox2.Text.Length == 11)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    notifyIcon1.ShowBalloonTip(3000, "TC Sınırı", "11 Haneden fazla yazamazsınız.", ToolTipIcon.Warning);
                }
            }
        }

        // TC klavye engeli
        private void KayitTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            if (KayitTC.Text.Length == 11)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    notifyIcon1.ShowBalloonTip(3000, "TC Sınırı", "11 Haneden fazla yazamazsınız.", ToolTipIcon.Warning);
                }
            }
        }

        // Tel harf engeli
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        // Tel harf engeli
        private void KayitTelno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        // Eklemek istedikleriniz
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox5.Text.Length == 200)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    notifyIcon1.ShowBalloonTip(3000, "Not Sınırı", "Maks. 200 karakter yazabilirsiniz.", ToolTipIcon.Warning);
                }
            }
        }

        // Güvenlik sorusu klavye engeli
        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
