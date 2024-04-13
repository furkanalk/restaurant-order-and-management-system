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
    public partial class MusteriSiparişler : Form
    {
        public MusteriSiparişler()
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

        private void Sepet_Click(object sender, EventArgs e)
        {
            /* empty */
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

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void MusteriSiparişler_Load(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel5.Visible = true;

            label15.Visible = false;

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();

            byte[] resim = (byte[])drprofil["Resim"];
            drprofil.Close();
            MemoryStream memorystream = new MemoryStream(resim);
            LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

            SqlCommand takip = new SqlCommand("select * from Siparisler where TC=" + LoginBilgi.tc, baglantim);
            SqlDataReader drtakip = takip.ExecuteReader();

            if(drtakip.Read())
            {
                label2.Text = "Onay Bekleniyor";               
            }
            else
            {
                drtakip.Close();

                label2.Text = "Siparis Yok";

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if(drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ "+(string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            baglantim.Close();
        }

        // Sipariş detayı logo
        private void button5_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Onay Bekleniyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz onay beklemektedir.", ToolTipIcon.Warning);
            }

            baglantim.Open();

            if (label2.Text == "Hazırlanıyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz hazırlanmaktadır.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            if (label2.Text == "Paketleniyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz paketlenmektedir.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            if (label2.Text == "Kuryede")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz kuryede yola çıkmıştır.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            baglantim.Close();

            if (label2.Text == "Siparis Yok")
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Şu anda siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }      
        }

        // Sipariş detayı button
        private void button6_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (label2.Text == "Onay Bekleniyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz onay beklemektedir.", ToolTipIcon.Warning);
            }
            if (label2.Text == "Hazırlanıyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz hazırlanmaktadır.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            if (label2.Text == "Paketleniyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz paketlenmektedir.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            if (label2.Text == "Kuryede")
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatırlatma", "Siparişiniz kuryede yola çıkmıştır.", ToolTipIcon.Info);
                this.panel5.Visible = false;
                this.panel6.Visible = true;

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];

                        label9.Text = (string)drdurum["Tarih"];
                        label10.Text = (string)drdurum["Siparis"];
                        label11.Text = "₺ " + (string)drdurum["Tutar"];
                        label13.Text = (string)drdurum["Dipnot"];

                        if ((string)drdurum["Odeme"] == "Kapıda")
                        {
                            label14.Text = "(Ödenecek)";
                        }
                        else
                        {
                            label14.Text = "(Ödendi)";
                        }
                    }
                }
                else
                {
                    label2.Text = "Siparis Yok";
                }
                drdurum.Close();
            }
            baglantim.Close();

            if (label2.Text == "Siparis Yok")
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Şu anda siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }


        // Geri buttonu (sipariş detayı)
        private void button3_Click(object sender, EventArgs e)
        {       
            this.panel6.Visible = false;
            this.panel5.Visible = true;
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

        // Yenile
        private void button1_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand takip = new SqlCommand("select * from Siparisler where TC=" + LoginBilgi.tc, baglantim);
            SqlDataReader drtakip = takip.ExecuteReader();

            if (drtakip.Read())
            {
                label2.Text = "Onay Bekleniyor";
            }
            else
            {
                drtakip.Close();

                label2.Text = "Siparis Yok";

                SqlCommand durum = new SqlCommand("select * from SiparislerinDurumu where TC=" + LoginBilgi.tc, baglantim);
                SqlDataReader drdurum = durum.ExecuteReader();

                if (drdurum.Read())
                {
                    if (drdurum["TC"] != null)
                    {
                        label2.Text = (string)drdurum["Durum"];
                        notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Siparişiniz '" + label2.Text + "'", ToolTipIcon.Info);
                    }
                }
            }
            baglantim.Close();

            if (label2.Text == "Siparis Yok")
            {
                notifyIcon1.ShowBalloonTip(3000, "Durum Güncellenemedi", "Şu anda siparişiniz bulunmamaktadır.", ToolTipIcon.Warning);
            }
            if (label2.Text == "Onay Bekleniyor")
            {
                notifyIcon1.ShowBalloonTip(3000, "Lütfen Bekleyiniz", "Siparişiniz hala onay beklemektedir.", ToolTipIcon.Info);
            }
        }

        // Sipariş Geçmişi (logo)
        private void button2_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel7.Visible = true;

            button9.Visible = false;
            button8.Visible = true;

            label15.Visible = false;

            listView3.Items.Clear();

            baglantim.Open();

            SqlCommand sepet4 = new SqlCommand("select * from SiparisGecmisleri where TC=" + LoginBilgi.tc, baglantim);
            SqlDataAdapter adapter4 = new SqlDataAdapter(sepet4);
            DataTable table4 = new DataTable();
            adapter4.Fill(table4);

            foreach (DataRow dr in table4.Rows)
            {
                bool kontrol = false;

                foreach (ListViewItem item2 in listView3.Items)
                {
                    if (item2.Text == dr["ID"].ToString())
                    {
                        kontrol = true;
                        break;
                    }
                }

                if (!kontrol)
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["Tarih"].ToString());
                    item.SubItems.Add(dr["Durum"].ToString());
                    item.SubItems.Add(dr["Siparis"].ToString());
                    item.SubItems.Add(dr["Odeme"].ToString());
                    item.SubItems.Add("₺" + dr["Tutar"].ToString());
                    item.SubItems.Add(dr["Dipnot"].ToString());
                    item.SubItems.Add(dr["Adres"].ToString());
                    item.SubItems.Add(dr["Isim"].ToString());
                    item.SubItems.Add(dr["TC"].ToString());
                    listView3.Items.Add(item);
                }
            }

            baglantim.Close();

            listView3.Columns[0].Width = 0;

            if (listView3.Items.Count == 0)
            {
                label15.Visible = true;
            }
        }

        // Sipariş Geçmişi (label)
        private void button4_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel7.Visible = true;

            button9.Visible = false;
            button8.Visible = true;

            label15.Visible = false;

            listView3.Items.Clear();

            baglantim.Open();

            SqlCommand sepet4 = new SqlCommand("select * from SiparisGecmisleri where TC=" + LoginBilgi.tc, baglantim);
            SqlDataAdapter adapter4 = new SqlDataAdapter(sepet4);
            DataTable table4 = new DataTable();
            adapter4.Fill(table4);

            foreach (DataRow dr in table4.Rows)
            {
                bool kontrol = false;

                foreach (ListViewItem item2 in listView3.Items)
                {
                    if (item2.Text == dr["ID"].ToString())
                    {
                        kontrol = true;
                        break;
                    }
                }

                if (!kontrol)
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["Tarih"].ToString());
                    item.SubItems.Add(dr["Durum"].ToString());
                    item.SubItems.Add(dr["Siparis"].ToString());
                    item.SubItems.Add(dr["Odeme"].ToString());
                    item.SubItems.Add("₺" + dr["Tutar"].ToString());
                    item.SubItems.Add(dr["Dipnot"].ToString());
                    item.SubItems.Add(dr["Adres"].ToString());
                    item.SubItems.Add(dr["Isim"].ToString());
                    item.SubItems.Add(dr["TC"].ToString());
                    listView3.Items.Add(item);
                }
            }

            baglantim.Close();

            listView3.Columns[0].Width = 0;

            if (listView3.Items.Count == 0)
            {
                label15.Visible = true;
            }
        }

        // Teslim Edilmiş
        private void button8_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            button9.Visible = true;

            label15.Visible = false;

            listView3.Items.Clear();

            baglantim.Open();

            SqlCommand sepet3 = new SqlCommand("select * from SiparisIptal where TC="+ LoginBilgi.tc, baglantim);
            SqlDataAdapter adapter3 = new SqlDataAdapter(sepet3);
            DataTable table3 = new DataTable();
            adapter3.Fill(table3);

            foreach (DataRow dr in table3.Rows)
            {
                bool kontrol = false;

                foreach (ListViewItem item2 in listView3.Items)
                {
                    if (item2.Text == dr["ID"].ToString())
                    {
                        kontrol = true;
                        break;
                    }
                }

                if (!kontrol)
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["Tarih"].ToString());
                    item.SubItems.Add(dr["Durum"].ToString());
                    item.SubItems.Add(dr["Siparis"].ToString());
                    item.SubItems.Add(dr["Odeme"].ToString());
                    item.SubItems.Add("₺" + dr["Tutar"].ToString());
                    item.SubItems.Add(dr["Dipnot"].ToString());
                    item.SubItems.Add(dr["Adres"].ToString());
                    item.SubItems.Add(dr["Isim"].ToString());
                    item.SubItems.Add(dr["TC"].ToString());
                    listView3.Items.Add(item);
                }
            }
            baglantim.Close();

            listView3.Columns[0].Width = 0;

            if (listView3.Items.Count == 0)
            {
                label15.Visible = true;
            }
        }

        // İptal Edilmiş
        private void button9_Click(object sender, EventArgs e)
        {
            button9.Visible = false;
            button8.Visible = true;

            label15.Visible = false;

            listView3.Items.Clear();

            baglantim.Open();

            SqlCommand sepet4 = new SqlCommand("select * from SiparisGecmisleri where TC=" + LoginBilgi.tc, baglantim);
            SqlDataAdapter adapter4 = new SqlDataAdapter(sepet4);
            DataTable table4 = new DataTable();
            adapter4.Fill(table4);

            foreach (DataRow dr in table4.Rows)
            {
                bool kontrol = false;

                foreach (ListViewItem item2 in listView3.Items)
                {
                    if (item2.Text == dr["ID"].ToString())
                    {
                        kontrol = true;
                        break;
                    }
                }

                if (!kontrol)
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["Tarih"].ToString());
                    item.SubItems.Add(dr["Durum"].ToString());
                    item.SubItems.Add(dr["Siparis"].ToString());
                    item.SubItems.Add(dr["Odeme"].ToString());
                    item.SubItems.Add("₺" + dr["Tutar"].ToString());
                    item.SubItems.Add(dr["Dipnot"].ToString());
                    item.SubItems.Add(dr["Adres"].ToString());
                    item.SubItems.Add(dr["Isim"].ToString());
                    item.SubItems.Add(dr["TC"].ToString());
                    listView3.Items.Add(item);
                }
            }
            baglantim.Close();

            listView3.Columns[0].Width = 0;

            if (listView3.Items.Count == 0)
            {
                label15.Visible = true;
            }
        }

        // Geri (sipariş geçmişi)
        private void button7_Click(object sender, EventArgs e)
        {
            this.panel7.Visible = false;
            this.panel5.Visible = true;
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
