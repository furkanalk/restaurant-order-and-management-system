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
using System.Collections;
using System.IO;

namespace RestaurantAtlantis
{
    public partial class MusteriYorumlar : Form
    {
        public MusteriYorumlar()
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
            notifyIcon1.Visible = false;
            MusteriSiparişler gitMusteriSepet = new MusteriSiparişler();
            gitMusteriSepet.Show();
            this.Hide();
        }
        private void Siparisler_Click(object sender, EventArgs e)
        {
            /* empty */
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void MusteriYorumlar_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            label1.BringToFront();
            label2.BringToFront();
            label3.BringToFront();

            textBox1.Enabled = false;

            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Customer where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();

            byte[] resim2 = (byte[])drprofil["Resim"];
            drprofil.Close();
            MemoryStream memorystream = new MemoryStream(resim2);
            LogoMusteri.BackgroundImage = Image.FromStream(memorystream);

            SqlCommand musteri = new SqlCommand("select * from Customer where TC=" + LoginBilgi.tc, baglantim);
            SqlDataReader drmusteri = musteri.ExecuteReader();

            if (drmusteri.HasRows)
            {
                drmusteri.Read();

                // Resim
                byte[] resim = new byte[0];
                resim = (byte[])drmusteri["Resim"];
                MemoryStream stream = new MemoryStream(resim);
                pictureBox2.BackgroundImage = Image.FromStream(stream);

                if (Convert.ToBoolean(drmusteri["Siparis"]) == false)
                {
                    label1.Visible = true;

                    if (Convert.ToBoolean(drmusteri["Yorum"]) == false)
                    {     
                        label1.Visible = true;
                    }
                    else
                    {
                        label1.Visible = false;
                        textBox1.Enabled = true;
                    }
                }
                else
                {
                    label2.Visible = true;
                }
                drmusteri.Close();
            }

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Yorumlar", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Durum"].Visible = false;
            dataGridView1.Columns["Date"].Visible = false;

            dataGridView1.Columns["ResimMusteri"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Emote"].DisplayIndex = 2;
            dataGridView1.Columns["Yorum"].DisplayIndex = 3;
            dataGridView1.Columns["Tarih"].DisplayIndex = 4;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 100;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 120;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 500;
            DataGridViewColumn column4 = dataGridView1.Columns[6];
            column4.Width = 30;
            DataGridViewColumn column5 = dataGridView1.Columns[4];
            column5.Width = 200;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["ResimMusteri"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            DataGridViewImageColumn imgCol2 = (DataGridViewImageColumn)dataGridView1.Columns["Emote"];
            imgCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;

            this.dataGridView1.Sort(this.dataGridView1.Columns["Date"], ListSortDirection.Descending);

            baglantim.Close();
        }

        // Yorumu Gönder
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand musteri = new SqlCommand("select * from Customer where TC=" + LoginBilgi.tc, baglantim);
            SqlDataReader drmusteri = musteri.ExecuteReader();

            if (drmusteri.HasRows)
            {
                drmusteri.Read();

                if (Convert.ToBoolean(drmusteri["Yorum"]) == false)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Yorum Yapamazsınız", "Önce sipariş verin ve teslim edilene kadar bekleyin.", ToolTipIcon.Warning);
                }
                else
                {
                    if (textBox1.Text.Length > 150)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Yorum Çok Uzun!", "Lütfen 150 karakteri geçmeyin.", ToolTipIcon.Warning);
                    }
                    else if (textBox1.Text.Length == 0 || textBox1.Text == "*Yorum yaparken 150 karakteri geçmemesine özen gösteriniz*")
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Yorum Boş!", "Lütfen yorum yapınız.", ToolTipIcon.Warning);
                    }
                    else
                    {
                        drmusteri.Close();

                        SqlCommand ekle = new SqlCommand("insert into Yorumlar (ResimMusteri,Name,Yorum,Tarih,Durum,Emote,Date) values(@ResimMusteri,@Name,@Yorum,@Tarih,@Durum,@Emote,@Date)", baglantim);
                        SqlCommand bul = new SqlCommand("select * from Customer where TC=" + LoginBilgi.tc, baglantim);
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=45", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        byte[] emote = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();

                        SqlDataReader drbul = bul.ExecuteReader();
                        drbul.Read();

                        byte[] resim = (byte[])drbul["Resim"];

                        ekle.Parameters.AddWithValue("@ResimMusteri", resim);
                        ekle.Parameters.AddWithValue("@Name", (string)drbul["Name"] + " " + (string)drbul["Surname"]);
                        drbul.Close();
                        ekle.Parameters.AddWithValue("@Yorum", textBox1.Text);
                        ekle.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString("HH:mm:ss - MMM d yyyy"));
                        ekle.Parameters.AddWithValue("@Durum", 1);
                        ekle.Parameters.AddWithValue("@Emote", emote);
                        ekle.Parameters.AddWithValue("@Date", DateTime.Now);
                        ekle.ExecuteNonQuery();                    

                        SqlCommand yorum2 = new SqlCommand("update Customer set Yorum=@Yorum where TC=" + LoginBilgi.tc, baglantim);
                        yorum2.Parameters.AddWithValue("@Yorum", 0);
                        yorum2.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Yorumlar", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        dataGridView1.Columns["ID"].Visible = false;
                        dataGridView1.Columns["Durum"].Visible = false;
                        dataGridView1.Columns["Date"].Visible = false;

                        dataGridView1.Columns["ResimMusteri"].DisplayIndex = 0;
                        dataGridView1.Columns["Name"].DisplayIndex = 1;
                        dataGridView1.Columns["Emote"].DisplayIndex = 2;
                        dataGridView1.Columns["Yorum"].DisplayIndex = 3;
                        dataGridView1.Columns["Tarih"].DisplayIndex = 4;

                        DataGridViewColumn column1 = dataGridView1.Columns[1];
                        column1.Width = 100;
                        DataGridViewColumn column2 = dataGridView1.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView1.Columns[3];
                        column3.Width = 500;
                        DataGridViewColumn column4 = dataGridView1.Columns[6];
                        column4.Width = 30;
                        DataGridViewColumn column5 = dataGridView1.Columns[4];
                        column5.Width = 200;

                        DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["ResimMusteri"];
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        DataGridViewImageColumn imgCol2 = (DataGridViewImageColumn)dataGridView1.Columns["Emote"];
                        imgCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        this.dataGridView1.Sort(this.dataGridView1.Columns["Date"], ListSortDirection.Descending);

                        label3.Visible = true;
                    }
                }
            }
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

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "*Yorum yaparken 150 karakteri geçmemesine özen gösteriniz*")
            {
                textBox1.Text = "";
                textBox1.Font = new Font("Maindra GD", 11, FontStyle.Bold);
                textBox1.ForeColor = Color.Black;
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
