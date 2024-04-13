using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class SefProfiller : Form
    {
        public SefProfiller()
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

        private void Cikis_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            MainScreen gitMainScreen = new MainScreen();
            gitMainScreen.Show();
            this.Hide();
        }

        private void AnaSayfa_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefAnaSayfa gitSefAnaSayfa = new SefAnaSayfa();
            gitSefAnaSayfa.Show();
            this.Hide();
        }

        private void Menü2_Click_1(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefMenu gitSefMenu = new SefMenu();
            gitSefMenu.Show();
            this.Hide();
        }

        private void Yorumlar_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefYorumlar gitSefYorumlar = new SefYorumlar();
            gitSefYorumlar.Show();
            this.Hide();
        }

        private void Bilanço_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefBilanço gitSefBilanço = new SefBilanço();
            gitSefBilanço.Show();
            this.Hide();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void SefProfiller_Load(object sender, EventArgs e)
        {
            label30.Visible = false;
            this.panel6.Visible = false; // Sef düzenleme ekranı gizlenir
            this.panel7.Visible = false; // Maaş düzenleme ekranı gizlenir
            this.panel8.Visible = false; // Personel ekleme ekranı gizlenir
            this.panel9.Visible = false; // Fatura ekranı gizlenir
            label29.Visible = false; // ekle gizlenir
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
        }

        int profil = 0;

        // Şef Profilleri Resim
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            profil = 1;
            pictureBox6.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Chef", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column8 = dataGridView1.Columns[7];
            column8.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();

        }

        // Şef Profilleri Label
        private void label13_Click(object sender, EventArgs e)
        {
            profil = 1;
            pictureBox6.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Chef", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column8 = dataGridView1.Columns[7];
            column8.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();
        }

        // Personel logo
        private void LogoPersonel_Click(object sender, EventArgs e)
        {
            profil = 2;
            pictureBox5.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Banka"].Visible = false;
            dataGridView1.Columns["IBAN"].Visible = false;
            dataGridView1.Columns["Maas"].Visible = false;
            dataGridView1.Columns["Pozisyon"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column12 = dataGridView1.Columns[12];
            column12.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();
        }

        // Personel label
        private void label14_Click(object sender, EventArgs e)
        {
            profil = 2;
            pictureBox5.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Banka"].Visible = false;
            dataGridView1.Columns["IBAN"].Visible = false;
            dataGridView1.Columns["Maas"].Visible = false;
            dataGridView1.Columns["Pozisyon"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column12 = dataGridView1.Columns[12];
            column12.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();
        }

        // Müşteri logo
        private void LogoMusteri_Click(object sender, EventArgs e)
        {
            profil = 3;
            pictureBox4.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Credit_Card"].Visible = false;
            dataGridView1.Columns["Card_CVC"].Visible = false;
            dataGridView1.Columns["Card_SKT1"].Visible = false;
            dataGridView1.Columns["Card_SKT2"].Visible = false;
            dataGridView1.Columns["Siparis"].Visible = false;
            dataGridView1.Columns["Yorum"].Visible = false;
            dataGridView1.Columns["Adres"].Visible = false;
            dataGridView1.Columns["GSoru"].Visible = false;
            dataGridView1.Columns["GCevap"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column14 = dataGridView1.Columns[14];
            column14.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();
        }

        // Müşteri label
        private void label3_Click(object sender, EventArgs e)
        {
            profil = 3;
            pictureBox4.Visible = true;
            this.panel6.Visible = true;
            this.panel5.Visible = false;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Credit_Card"].Visible = false;
            dataGridView1.Columns["Card_CVC"].Visible = false;
            dataGridView1.Columns["Card_SKT1"].Visible = false;
            dataGridView1.Columns["Card_SKT2"].Visible = false;
            dataGridView1.Columns["Siparis"].Visible = false;
            dataGridView1.Columns["Yorum"].Visible = false;
            dataGridView1.Columns["Adres"].Visible = false;

            dataGridView1.Columns["Resim"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;
            dataGridView1.Columns["Surname"].DisplayIndex = 2;
            dataGridView1.Columns["Password"].DisplayIndex = 3;
            dataGridView1.Columns["TC"].DisplayIndex = 4;
            dataGridView1.Columns["PhoneNumber"].DisplayIndex = 5;
            dataGridView1.Columns["Mail"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 300;
            DataGridViewColumn column14 = dataGridView1.Columns[14];
            column14.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.ClearSelection();
            baglantim.Close();
        }

        // Görüntüle
        private void label26_Click(object sender, EventArgs e)
        {
            label29.Visible = false;
            textBox12.Enabled = true;

            baglantim.Open();

            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    if (profil == 1)
                    {

                        SqlCommand sef = new SqlCommand("select * from Chef where ID=" + cellValue, baglantim);
                        SqlDataReader drsef = sef.ExecuteReader();
                        drsef.Read();

                        if (!DBNull.Value.Equals(drsef["Name"]) || !DBNull.Value.Equals(drsef["Surname"]) || !DBNull.Value.Equals(drsef["Password"]) ||
                                !DBNull.Value.Equals(drsef["TC"]) || !DBNull.Value.Equals(drsef["PhoneNumber"]) || !DBNull.Value.Equals(drsef["Mail"]))
                        {
                            textBox5.Text = (string)drsef["Name"];
                            textBox6.Text = (string)drsef["Surname"];
                            textBox7.Text = (string)drsef["Password"];
                            textBox9.Text = (string)drsef["TC"];
                            textBox10.Text = (string)drsef["PhoneNumber"];
                            textBox11.Text = (string)drsef["Mail"];
                        }

                        drsef.Close();
                    }


                    if (profil == 2)
                    {
                        SqlCommand sef = new SqlCommand("select * from Employee where ID=" + cellValue, baglantim);
                        SqlDataReader drsef = sef.ExecuteReader();
                        drsef.Read();

                        if (!DBNull.Value.Equals(drsef["Name"]) || !DBNull.Value.Equals(drsef["Surname"]) || !DBNull.Value.Equals(drsef["Password"]) ||
                                !DBNull.Value.Equals(drsef["TC"]) || !DBNull.Value.Equals(drsef["PhoneNumber"]) || !DBNull.Value.Equals(drsef["Mail"]))
                        {
                            textBox5.Text = (string)drsef["Name"];
                            textBox6.Text = (string)drsef["Surname"];
                            textBox7.Text = (string)drsef["Password"];
                            textBox9.Text = (string)drsef["TC"];
                            textBox10.Text = (string)drsef["PhoneNumber"];
                            textBox11.Text = (string)drsef["Mail"];
                        }
                        drsef.Close();
                    }

                    if (profil == 3)
                    {
                        SqlCommand sef = new SqlCommand("select * from Customer where ID=" + cellValue, baglantim);
                        SqlDataReader drsef = sef.ExecuteReader();
                        drsef.Read();

                        if (!DBNull.Value.Equals(drsef["Name"]) || !DBNull.Value.Equals(drsef["Surname"]) || !DBNull.Value.Equals(drsef["Password"]) ||
                                !DBNull.Value.Equals(drsef["TC"]) || !DBNull.Value.Equals(drsef["PhoneNumber"]) || !DBNull.Value.Equals(drsef["Mail"]))
                        {
                            textBox5.Text = (string)drsef["Name"];
                            textBox6.Text = (string)drsef["Surname"];
                            textBox7.Text = (string)drsef["Password"];
                            textBox9.Text = (string)drsef["TC"];
                            textBox10.Text = (string)drsef["PhoneNumber"];
                            textBox11.Text = (string)drsef["Mail"];
                        }
                        drsef.Close();
                    }

                }
                else if (dataGridView1.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla kullanıcı seçmeyiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen görüntülemek istediğiniz kullanıcıyı seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Kayıtlarda hiçbir kullanıcı bilgisi bulunamadı.", ToolTipIcon.Error);
            }
            baglantim.Close();
        }

        // Kaydet
        private void label18_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "")
                {
                    if (textBox7.Text.Length < 6)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı Şifre", "Şifre 6 haneden küçük olamaz.", ToolTipIcon.Warning);
                    }
                    else if (textBox9.Text.Length != 11)
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı TC", "TC Numarası 11 haneli olmalıdır.", ToolTipIcon.Warning);
                    }
                    else
                    {
                        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                        if (profil == 1)
                        {
                            baglantim.Open();

                            SqlCommand sef = new SqlCommand("update Chef set Name=@Name,Surname=@Surname,Password=@Password,TC=@TC,PhoneNumber=@PhoneNumber,Mail=@Mail where ID=" + cellValue, baglantim);

                            sef.Parameters.AddWithValue("@Name", textBox5.Text);
                            sef.Parameters.AddWithValue("@Surname", textBox6.Text);
                            sef.Parameters.AddWithValue("@Password", textBox7.Text);
                            sef.Parameters.AddWithValue("@TC", textBox9.Text);
                            sef.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                            sef.Parameters.AddWithValue("@Mail", textBox11.Text);
                            sef.ExecuteNonQuery();

                            SqlDataAdapter adapter = new SqlDataAdapter("select * from Chef", baglantim);
                            var data = new DataSet();
                            adapter.Fill(data);
                            dataGridView1.DataSource = data.Tables[0];

                            baglantim.Close();

                            notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgiler başarıyla kaydedilmiştir.", ToolTipIcon.Info);

                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox9.Text = "";
                            textBox10.Text = "";
                            textBox11.Text = "";

                            dataGridView1.ClearSelection();
                        }

                        if (profil == 2)
                        {
                            baglantim.Open();

                            SqlCommand personel = new SqlCommand("update Employee set Name=@Name,Surname=@Surname,Password=@Password,TC=@TC,PhoneNumber=@PhoneNumber,Mail=@Mail where ID=" + cellValue, baglantim);

                            personel.Parameters.AddWithValue("@Name", textBox5.Text);
                            personel.Parameters.AddWithValue("@Surname", textBox6.Text);
                            personel.Parameters.AddWithValue("@Password", textBox7.Text);
                            personel.Parameters.AddWithValue("@TC", textBox9.Text);
                            personel.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                            personel.Parameters.AddWithValue("@Mail", textBox11.Text);
                            personel.ExecuteNonQuery();

                            SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
                            var data = new DataSet();
                            adapter.Fill(data);
                            dataGridView1.DataSource = data.Tables[0];

                            baglantim.Close();

                            notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgiler başarıyla kaydedilmiştir.", ToolTipIcon.Info);

                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox9.Text = "";
                            textBox10.Text = "";
                            textBox11.Text = "";

                            dataGridView1.ClearSelection();
                        }

                        if (profil == 3)
                        {
                            baglantim.Open();

                            SqlCommand musteri = new SqlCommand("update Customer set Name=@Name,Surname=@Surname,Password=@Password,TC=@TC,PhoneNumber=@PhoneNumber,Mail=@Mail where ID=" + cellValue, baglantim);

                            musteri.Parameters.AddWithValue("@Name", textBox5.Text);
                            musteri.Parameters.AddWithValue("@Surname", textBox6.Text);
                            musteri.Parameters.AddWithValue("@Password", textBox7.Text);
                            musteri.Parameters.AddWithValue("@TC", textBox9.Text);
                            musteri.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                            musteri.Parameters.AddWithValue("@Mail", textBox11.Text);
                            musteri.ExecuteNonQuery();

                            SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                            var data = new DataSet();
                            adapter.Fill(data);
                            dataGridView1.DataSource = data.Tables[0];

                            baglantim.Close();

                            notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgiler başarıyla kaydedilmiştir.", ToolTipIcon.Info);

                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox9.Text = "";
                            textBox10.Text = "";
                            textBox11.Text = "";

                            dataGridView1.ClearSelection();
                        }
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş bir alan bırakmayınız.", ToolTipIcon.Warning);
                }
            }
            else if (dataGridView1.SelectedRows.Count > 1)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla kullanıcı seçmeyiniz.", ToolTipIcon.Warning);
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen kullanıcı seçimi yapınız.", ToolTipIcon.Warning);
            }
        }

        // Geri
        private void label16_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel5.Visible = true;

            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;

            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            dataGridView1.ClearSelection();
        }

        // TC sadece sayı
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            if (textBox9.Text.Length == 11)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;
                    notifyIcon1.ShowBalloonTip(3000, "TC Sınırı", "11 Haneden fazla yazamazsınız.", ToolTipIcon.Warning);
                }
            }
        }

        // Ekle (Ekleyi aç)
        private void label27_Click(object sender, EventArgs e)
        {
            label30.Visible = true;
            label29.Visible = true;
            label26.Enabled = false;
            label27.Enabled = false;
            label28.Enabled = false;
            label16.Visible = false;
            label18.Visible = false;
            textBox12.Enabled = false;

            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

            dataGridView1.ClearSelection();
        }

        // Ekle (kaydet)
        private void label29_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "")
            {
                if (textBox7.Text.Length < 6)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Şifre", "Şifre 6 haneden küçük olamaz.", ToolTipIcon.Warning);
                }
                else if (textBox9.Text.Length != 11)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı TC", "TC Numarası 11 haneli olmalıdır.", ToolTipIcon.Warning);
                }
                else
                {
                    if (profil == 1)
                    {
                        baglantim.Open();

                        SqlCommand resim = new SqlCommand("select * from Resim where id=25", baglantim);
                        SqlDataReader drresim = resim.ExecuteReader();
                        drresim.Read();
                        byte[] profil = (byte[])drresim["RESIM"];
                        drresim.Close();

                        SqlCommand sef = new SqlCommand("insert into Chef (Name,Surname,Password,TC,PhoneNumber,Mail,Resim) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Resim)", baglantim);

                        sef.Parameters.AddWithValue("@Name", textBox5.Text);
                        sef.Parameters.AddWithValue("@Surname", textBox6.Text);
                        sef.Parameters.AddWithValue("@Password", textBox7.Text);
                        sef.Parameters.AddWithValue("@TC", textBox9.Text);
                        sef.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                        sef.Parameters.AddWithValue("@Mail", textBox11.Text);
                        sef.Parameters.AddWithValue("@Resim", profil);
                        sef.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Chef", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Yeni Şef başarıyla eklendi", ToolTipIcon.Info);

                        textBox12.Enabled = true;
                        label26.Enabled = true;
                        label27.Enabled = true;
                        label28.Enabled = true;
                        label29.Visible = false;
                        label30.Visible = false;
                        label16.Visible = true;
                        label18.Visible = true;

                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox9.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";

                        dataGridView1.ClearSelection();
                    }

                    if (profil == 2)
                    {
                        baglantim.Open();

                        SqlCommand resim = new SqlCommand("select * from Resim where id=1", baglantim);
                        SqlDataReader drresim = resim.ExecuteReader();
                        drresim.Read();
                        byte[] profil = (byte[])drresim["RESIM"];
                        drresim.Close();

                        SqlCommand personel = new SqlCommand("insert into Employee (Name,Surname,Password,TC,PhoneNumber,Mail,Cinsiyet,Resim) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Cinsiyet,@Resim)", baglantim);

                        personel.Parameters.AddWithValue("@Name", textBox5.Text);
                        personel.Parameters.AddWithValue("@Surname", textBox6.Text);
                        personel.Parameters.AddWithValue("@Password", textBox7.Text);
                        personel.Parameters.AddWithValue("@TC", textBox9.Text);
                        personel.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                        personel.Parameters.AddWithValue("@Mail", textBox11.Text);
                        personel.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                        personel.Parameters.AddWithValue("@Resim", profil);
                        personel.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Yeni Personel başarıyla eklendi", ToolTipIcon.Info);

                        textBox12.Enabled = true;
                        label26.Enabled = true;
                        label27.Enabled = true;
                        label28.Enabled = true;
                        label29.Visible = false;
                        label30.Visible = false;
                        label16.Visible = true;
                        label18.Visible = true;

                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox9.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";

                        dataGridView1.ClearSelection();
                    }

                    if (profil == 3)
                    {
                        baglantim.Open();

                        SqlCommand resim = new SqlCommand("select * from Resim where id=21", baglantim);
                        SqlDataReader drresim = resim.ExecuteReader();
                        drresim.Read();
                        byte[] profil = (byte[])drresim["RESIM"];
                        drresim.Close();

                        SqlCommand musteri = new SqlCommand("insert into Customer (Name,Surname,Password,TC,PhoneNumber,Mail,Siparis,Yorum,Cinsiyet,GSoru,GCevap,Resim) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Siparis,@Yorum,@Cinsiyet,@GSoru,@GCevap,@Resim)", baglantim);

                        musteri.Parameters.AddWithValue("@Name", textBox5.Text);
                        musteri.Parameters.AddWithValue("@Surname", textBox6.Text);
                        musteri.Parameters.AddWithValue("@Password", textBox7.Text);
                        musteri.Parameters.AddWithValue("@TC", textBox9.Text);
                        musteri.Parameters.AddWithValue("@PhoneNumber", textBox10.Text);
                        musteri.Parameters.AddWithValue("@Mail", textBox11.Text);
                        musteri.Parameters.AddWithValue("@Siparis", 0);
                        musteri.Parameters.AddWithValue("@Yorum", 0);
                        musteri.Parameters.AddWithValue("@GSoru", "En sevdiğiniz restoran neresidir?");
                        musteri.Parameters.AddWithValue("@GCevap", "Atlantis");
                        musteri.Parameters.AddWithValue("@Resim", profil);
                        musteri.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                        musteri.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Yeni Müşteri başarıyla eklendi", ToolTipIcon.Info);

                        textBox12.Enabled = true;
                        label26.Enabled = true;
                        label27.Enabled = true;
                        label28.Enabled = true;
                        label29.Visible = false;
                        label30.Visible = false;
                        label16.Visible = true;
                        label18.Visible = true;

                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox9.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";

                        dataGridView1.ClearSelection();
                    }
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş bir alan bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // İptal
        private void label30_Click(object sender, EventArgs e)
        {
            textBox12.Enabled = true;
            label26.Enabled = true;
            label27.Enabled = true;
            label28.Enabled = true;
            label16.Visible = true;
            label18.Visible = true;
            label29.Visible = false;
            label30.Visible = false;
        }

        // Sil
        private void label28_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                string adsoyad = Convert.ToString(selectedRow.Cells["Name"].Value) + " " + Convert.ToString(selectedRow.Cells["Surname"].Value);

                if (profil == 1)
                {
                    DialogResult dialog = MessageBox.Show(adsoyad + " isimli kullanıcıyı silmek istediğiniz emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand sef = new SqlCommand("delete from Chef where ID=" + cellValue, baglantim);
                        sef.ExecuteNonQuery();
                        baglantim.Close();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Chef", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        notifyIcon1.ShowBalloonTip(3000, "Kullanıcı Silindi", adsoyad + " isimli Şef başarıyla silindi.", ToolTipIcon.Info);

                        dataGridView1.ClearSelection();
                    }
                }

                if (profil == 2)
                {
                    DialogResult dialog = MessageBox.Show(adsoyad + " isimli kullanıcıyı silmek istediğiniz emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand sef = new SqlCommand("delete from Employee where ID=" + cellValue, baglantim);
                        sef.ExecuteNonQuery();
                        baglantim.Close();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        notifyIcon1.ShowBalloonTip(3000, "Kullanıcı Silindi", adsoyad + " isimli Personel başarıyla silindi.", ToolTipIcon.Info);

                        dataGridView1.ClearSelection();
                    }
                }

                if (profil == 3)
                {
                    DialogResult dialog = MessageBox.Show(adsoyad + " isimli kullanıcıyı silmek istediğiniz emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand sef = new SqlCommand("delete from Customer where ID=" + cellValue, baglantim);
                        sef.ExecuteNonQuery();
                        baglantim.Close();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Customer", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        notifyIcon1.ShowBalloonTip(3000, "Kullanıcı Silindi", adsoyad + " isimli Müşteri başarıyla silindi.", ToolTipIcon.Info);

                        dataGridView1.ClearSelection();
                    }
                }
            }
            else if (dataGridView1.SelectedRows.Count == 1)
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla kullanıcı seçmeyiniz.", ToolTipIcon.Warning);
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen kullanıcı seçimi yapınız.", ToolTipIcon.Warning);
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if(profil == 1)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%' OR Surname LIKE '%{0}%' OR Password LIKE '%{0}%' OR TC LIKE '%{0}%' OR PhoneNumber LIKE '%{0}%' OR Mail LIKE '%{0}%'", textBox12.Text);
            }
            if(profil == 2)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%' OR Surname LIKE '%{0}%' OR Password LIKE '%{0}%' OR TC LIKE '%{0}%' OR PhoneNumber LIKE '%{0}%' OR Mail LIKE '%{0}%' OR Cinsiyet LIKE '%{0}%'", textBox12.Text);
            }
            if (profil == 3)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%' OR Surname LIKE '%{0}%' OR Password LIKE '%{0}%' OR TC LIKE '%{0}%' OR PhoneNumber LIKE '%{0}%' OR Mail LIKE '%{0}%' OR Cinsiyet LIKE '%{0}%'", textBox12.Text);
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

        // Maaşlar logo
        private void button2_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel7.Visible = true;

            textBox14.Text = "";
            comboBox3.Text = "";

            button18.Visible = false;
            label33.Visible = false;
            button16.Visible = true;
            label31.Visible = true;

            baglantim.Open();

            comboBox1.Items.Clear();

            SqlDataAdapter adapt = new SqlDataAdapter("select * from Pozisyonlar", baglantim);
            DataTable table = new DataTable();
            adapt.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add((string)dr["Poz"]);
            }

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView2.DataSource = data.Tables[0];

            dataGridView2.Columns["ID"].Visible = false;
            dataGridView2.Columns["Banka"].Visible = false;
            dataGridView2.Columns["IBAN"].Visible = false;
            dataGridView2.Columns["Password"].Visible = false;
            dataGridView2.Columns["TC"].Visible = false;
            dataGridView2.Columns["PhoneNumber"].Visible = false;
            dataGridView2.Columns["Mail"].Visible = false;
            dataGridView2.Columns["Cinsiyet"].Visible = false;

            dataGridView2.Columns["Resim"].DisplayIndex = 0;
            dataGridView2.Columns["Name"].DisplayIndex = 1;
            dataGridView2.Columns["Surname"].DisplayIndex = 2;
            dataGridView2.Columns["Pozisyon"].DisplayIndex = 3;
            dataGridView2.Columns["Maas"].DisplayIndex = 4;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView2.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column10 = dataGridView2.Columns[10];
            column10.Width = 180;
            DataGridViewColumn column9 = dataGridView2.Columns[9];
            column9.Width = 150;
            DataGridViewColumn column12 = dataGridView2.Columns[12];
            column12.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView2.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView2.ClearSelection();
            baglantim.Close();
        }

        // Maaşlar label
        private void label2_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel7.Visible = true;

            textBox14.Text = "";
            comboBox3.Text = "";

            button18.Visible = false;
            label33.Visible = false;
            button16.Visible = true;
            label31.Visible = true;

            baglantim.Open();

            comboBox1.Items.Clear();

            SqlDataAdapter adapt = new SqlDataAdapter("select * from Pozisyonlar", baglantim);
            DataTable table = new DataTable();
            adapt.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                comboBox1.Items.Add((string)dr["Poz"]);
            }

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView2.DataSource = data.Tables[0];

            dataGridView2.Columns["ID"].Visible = false;
            dataGridView2.Columns["Banka"].Visible = false;
            dataGridView2.Columns["IBAN"].Visible = false;
            dataGridView2.Columns["Password"].Visible = false;
            dataGridView2.Columns["TC"].Visible = false;
            dataGridView2.Columns["PhoneNumber"].Visible = false;
            dataGridView2.Columns["Mail"].Visible = false;
            dataGridView2.Columns["Cinsiyet"].Visible = false;

            dataGridView2.Columns["Resim"].DisplayIndex = 0;
            dataGridView2.Columns["Name"].DisplayIndex = 1;
            dataGridView2.Columns["Surname"].DisplayIndex = 2;
            dataGridView2.Columns["Pozisyon"].DisplayIndex = 3;
            dataGridView2.Columns["Maas"].DisplayIndex = 4;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            column1.Width = 150;
            DataGridViewColumn column2 = dataGridView2.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column10 = dataGridView2.Columns[10];
            column10.Width = 180;
            DataGridViewColumn column9 = dataGridView2.Columns[9];
            column9.Width = 150;
            DataGridViewColumn column12 = dataGridView2.Columns[12];
            column12.Width = 75;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView2.Columns["Resim"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView2.ClearSelection();
            baglantim.Close();
        }

        // Geri dön
        private void button5_Click(object sender, EventArgs e)
        {
            this.panel7.Visible = false;
            this.panel5.Visible = true;

            textBox14.Text = "";
            comboBox3.Text = "";

            button18.Visible = false;
            label33.Visible = false;
            button19.Visible = false;
            comboBox3.Visible = false;
            button17.Visible = true;
            button16.Visible = true;
            label31.Visible = true;
            textBox14.Visible = true;

            comboBox1.Text = "";
            textBox3.Text = "";

            dataGridView2.ClearSelection();
        }

        // Arama yap
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%' OR Surname LIKE '%{0}%' OR Pozisyon LIKE '%{0}%' OR Maas LIKE '%{0}%'", textBox1.Text);
        }

        // Maas harf engeli
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        // Personel bilgilerini getir
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            baglantim.Open();

            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    SqlCommand sec = new SqlCommand("select * from Employee where ID=" + cellValue, baglantim);
                    SqlDataReader drsec = sec.ExecuteReader();
                    drsec.Read();

                    if (!DBNull.Value.Equals(drsec["Pozisyon"]))
                    {
                        comboBox1.Text = (string)drsec["Pozisyon"];
                    }

                    if (!DBNull.Value.Equals(drsec["Maas"]))
                    {
                        textBox3.Text = (string)drsec["Maas"];
                    }

                    drsec.Close();
                }
                else if (dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen tek bir personel seçimi yapınız.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen personel seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda personel bulunmamaktadır.", ToolTipIcon.Error);
            }
            baglantim.Close();
        }

        // Maas bilgilerini güncelle
        private void button3_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    SqlCommand personel = new SqlCommand("update Employee set Pozisyon=@POzisyon,Maas=@Maas where ID=" + cellValue, baglantim);

                    personel.Parameters.AddWithValue("@Pozisyon", comboBox1.Text);
                    personel.Parameters.AddWithValue("@Maas", textBox3.Text);
                    personel.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee", baglantim);
                    var data = new DataSet();
                    adapter.Fill(data);
                    dataGridView2.DataSource = data.Tables[0];

                    baglantim.Close();

                    notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Bilgiler başarıyla kaydedilmiştir.", ToolTipIcon.Info);

                    dataGridView2.ClearSelection();
                }
                else if (dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen tek bir personel seçimi yapınız.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen personel seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda personel bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Başvuru ekranı logo
        private void button4_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel8.Visible = true;

            baglantim.Open();

            SqlCommand basvuru= new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drbasvuru = basvuru.ExecuteReader();
            drbasvuru.Read();
            
            if(Convert.ToBoolean(drbasvuru["Basvuru"]) == true)
            {
                button14.Visible = false;
                button8.Visible = true;
            }
            else
            {
                button8.Visible = false;
                button14.Visible = true;
            }
            drbasvuru.Close();
            basvuru.ExecuteNonQuery();

            // comboBox'a başvuruları getir

            comboBox2.Items.Clear();

            SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
            SqlDataAdapter adapter = new SqlDataAdapter(stok);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
            }

            baglantim.Close();
        }

        // Başvuru ekranı text
        private void label7_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel8.Visible = true;

            baglantim.Open();

            SqlCommand basvuru = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drbasvuru = basvuru.ExecuteReader();
            drbasvuru.Read();

            if (Convert.ToBoolean(drbasvuru["Basvuru"]) == true)
            {
                button14.Visible = false;
                button8.Visible = true;
            }
            else
            {
                button8.Visible = false;
                button14.Visible = true;
            }
            drbasvuru.Close();
            basvuru.ExecuteNonQuery();

            // comboBox'a başvuruları getir

            comboBox2.Items.Clear();

            SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
            SqlDataAdapter adapter = new SqlDataAdapter(stok);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
            }

            baglantim.Close();
        }

        // Geri Dön
        private void button6_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = false;
            this.panel5.Visible = true;

            comboBox2.Text = "";
            textBox2.Text = "";
            textBox8.Text = "";
            textBox13.Text = "";
            textBox4.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand stok = new SqlCommand("select * from EmployeeBasvur ", baglantim);
            SqlDataAdapter adapter = new SqlDataAdapter(stok);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                if (comboBox2.Text == dr["Name"].ToString() + " " + dr["Surname"].ToString())
                {
                    textBox2.Text = (string)dr["Pozisyon"];
                    textBox8.Text = (string)dr["PhoneNumber"];
                    textBox13.Text = (string)dr["TC"];
                    textBox4.Text = (string)dr["Notlar"];
                }
            }

            baglantim.Close();
        }

        // Kabul logo
        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    DialogResult dialog = MessageBox.Show(comboBox2.Text + " isimli başvuranın talebini KABUL ETMEK istediğinize emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand oku = new SqlCommand("select * from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        SqlDataReader droku = oku.ExecuteReader();
                        droku.Read();
                        SqlCommand ekle = new SqlCommand("insert into Employee (Name,Surname,Password,TC,PhoneNumber,Mail,Pozisyon,Cinsiyet,Resim) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Pozisyon,@Cinsiyet,@Resim)", baglantim);
                        ekle.Parameters.AddWithValue("@Name", (string)droku["Name"]);
                        ekle.Parameters.AddWithValue("@Surname", (string)droku["Surname"]);
                        ekle.Parameters.AddWithValue("@Password", (string)droku["Password"]);
                        ekle.Parameters.AddWithValue("@TC", (string)droku["TC"]);
                        ekle.Parameters.AddWithValue("@PhoneNumber", (string)droku["PhoneNumber"]);
                        ekle.Parameters.AddWithValue("@Mail", (string)droku["Mail"]);
                        ekle.Parameters.AddWithValue("@Pozisyon", (string)droku["Pozisyon"]);
                        ekle.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                        droku.Close();

                        SqlCommand resimbul = new SqlCommand("select * from Resim where id=1", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();

                        byte[] resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                        ekle.Parameters.AddWithValue("@Resim", resim);

                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "Kabul Edildi", "Başvuru onaylanıp, kayıtlara eklenmiştir.", ToolTipIcon.Info);

                        comboBox2.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox8.Text = "";
                        textBox13.Text = "";

                        // comboBox'a başvuruları getir

                        comboBox2.Items.Clear();

                        SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
                        SqlDataAdapter adapter = new SqlDataAdapter(stok);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
                        }

                        baglantim.Close();
                    }

                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Kabul text
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    DialogResult dialog = MessageBox.Show(comboBox2.Text + " isimli başvuranın talebini KABUL ETMEK istediğinize emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand oku = new SqlCommand("select * from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        SqlDataReader droku = oku.ExecuteReader();
                        droku.Read();
                        SqlCommand ekle = new SqlCommand("insert into Employee (Name,Surname,Password,TC,PhoneNumber,Mail,Pozisyon,Cinsiyet,Resim) values(@Name,@Surname,@Password,@TC,@PhoneNumber,@Mail,@Pozisyon,@Cinsiyet,@Resim)", baglantim);
                        ekle.Parameters.AddWithValue("@Name", (string)droku["Name"]);
                        ekle.Parameters.AddWithValue("@Surname", (string)droku["Surname"]);
                        ekle.Parameters.AddWithValue("@Password", (string)droku["Password"]);
                        ekle.Parameters.AddWithValue("@TC", (string)droku["TC"]);
                        ekle.Parameters.AddWithValue("@PhoneNumber", (string)droku["PhoneNumber"]);
                        ekle.Parameters.AddWithValue("@Mail", (string)droku["Mail"]);
                        ekle.Parameters.AddWithValue("@Pozisyon", (string)droku["Pozisyon"]);
                        ekle.Parameters.AddWithValue("@Cinsiyet", "Erkek");
                        droku.Close();

                        SqlCommand resimbul = new SqlCommand("select * from Resim where id=1", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();

                        byte[] resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                        ekle.Parameters.AddWithValue("@Resim", resim);

                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "Kabul Edildi", "Başvuru onaylanıp, kayıtlara eklenmiştir.", ToolTipIcon.Info);

                        comboBox2.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox8.Text = "";
                        textBox13.Text = "";

                        // comboBox'a başvuruları getir

                        comboBox2.Items.Clear();

                        SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
                        SqlDataAdapter adapter = new SqlDataAdapter(stok);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
                        }

                        baglantim.Close();
                    }

                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Reddet logo
        private void button12_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    DialogResult dialog = MessageBox.Show(comboBox2.Text + " isimli başvuranın talebini REDDETMEK istediğinize emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        sil.ExecuteNonQuery();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Reddedildi", "Başvuru tarafınızca reddedilmiştir.", ToolTipIcon.Info);

                        comboBox2.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox8.Text = "";
                        textBox13.Text = "";

                        // comboBox'a başvuruları getir

                        comboBox2.Items.Clear();

                        SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
                        SqlDataAdapter adapter = new SqlDataAdapter(stok);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
                        }
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Reddet text
        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    DialogResult dialog = MessageBox.Show(comboBox2.Text + " isimli başvuranın talebini REDDETMEK istediğinize emin misiniz?", "Onay bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                        sil.ExecuteNonQuery();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Reddedildi", "Başvuru tarafınızca reddedilmiştir.", ToolTipIcon.Info);

                        comboBox2.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox8.Text = "";
                        textBox13.Text = "";

                        // comboBox'a başvuruları getir

                        comboBox2.Items.Clear();

                        SqlCommand stok = new SqlCommand("select * from EmployeeBasvur", baglantim);
                        SqlDataAdapter adapter = new SqlDataAdapter(stok);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            comboBox2.Items.Add(dr["Name"].ToString() + " " + dr["Surname"].ToString());
                        }
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Alımlar açık click
        private void button8_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            button14.Visible = true;

            baglantim.Open();

            SqlCommand kapali = new SqlCommand("update Restoran set Basvuru=@Basvuru where id=1", baglantim);
            kapali.Parameters.AddWithValue("@Basvuru", 0);
            kapali.ExecuteNonQuery();

            baglantim.Close();
        }

        // Alımlar kapalı click
        private void button14_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            button8.Visible = true;

            baglantim.Open();

            SqlCommand acik = new SqlCommand("update Restoran set Basvuru=@Basvuru where id=1", baglantim);
            acik.Parameters.AddWithValue("@Basvuru", 1);
            acik.ExecuteNonQuery();

            baglantim.Close();
        }

        // Alımlar kapat aç logo
        private void button15_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand oku = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader droku = oku.ExecuteReader();
            droku.Read();

            if(Convert.ToBoolean(droku["Basvuru"]) == true)
            {
                droku.Close();
                button8.Visible = false;
                button14.Visible = true;

                SqlCommand kapali = new SqlCommand("update Restoran set Basvuru=@Basvuru where id=1", baglantim);
                kapali.Parameters.AddWithValue("@Basvuru", 0);
                kapali.ExecuteNonQuery();
            }
            else if (Convert.ToBoolean(droku["Basvuru"]) == false)
            {
                droku.Close();
                button14.Visible = false;
                button8.Visible = true;

                SqlCommand acik = new SqlCommand("update Restoran set Basvuru=@Basvuru where id=1", baglantim);
                acik.Parameters.AddWithValue("@Basvuru", 1);
                acik.ExecuteNonQuery();
            }
            baglantim.Close();
        }

        // CV indir logo
        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    baglantim.Open();

                    SqlCommand stok = new SqlCommand("select * from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                    SqlDataReader drstok = stok.ExecuteReader();
                    drstok.Read();

                    Byte[] pdf = (Byte[])drstok["PDF"];
                    string dosyayolu = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    dosyayolu = Path.Combine(dosyayolu, comboBox2.Text + drstok["Uzantı"].ToString());

                    drstok.Close();
                    File.WriteAllBytes(dosyayolu, pdf);
                    Process.Start(dosyayolu);

                    baglantim.Close();
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }


        // CV indir text
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                if (comboBox2.SelectedItem != null)
                {
                    baglantim.Open();

                    SqlCommand stok = new SqlCommand("select * from EmployeeBasvur where TC='" + textBox13.Text + "'", baglantim);
                    SqlDataReader drstok = stok.ExecuteReader();
                    drstok.Read();

                    Byte[] pdf = (Byte[])drstok["PDF"];
                    string dosyayolu = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    dosyayolu = Path.Combine(dosyayolu, comboBox2.Text + drstok["Uzantı"].ToString());

                    drstok.Close();
                    File.WriteAllBytes(dosyayolu, pdf);
                    Process.Start(dosyayolu);

                    baglantim.Close();
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen başvuru seçiniz..", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Kayıtlarda başvuru bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Başvuru seçim klavye engeli
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Pozisyonu ekle
        private void button16_Click(object sender, EventArgs e)
        {
            if(textBox14.Text != "")
            {
                baglantim.Open();

                SqlCommand ekle = new SqlCommand("insert into Pozisyonlar (Poz) values(@Poz)", baglantim);
                ekle.Parameters.AddWithValue("@Poz", textBox14.Text);
                ekle.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Yeni pozisyon başarıyla eklenmiştir.", ToolTipIcon.Info);

                textBox14.Text = "";
                comboBox1.Text = "";
                comboBox1.Items.Clear();

                SqlDataAdapter adapt = new SqlDataAdapter("select * from Pozisyonlar", baglantim);
                DataTable table = new DataTable();
                adapt.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox1.Items.Add((string)dr["Poz"]);
                }

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Pozisyonu sil
        private void button18_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                baglantim.Open();

                SqlCommand ekle = new SqlCommand("delete from Pozisyonlar where Poz='" + comboBox3.Text + "'", baglantim);
                ekle.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Pozisyon başarıyla silinmiştir.", ToolTipIcon.Info);

                comboBox1.Text = "";
                comboBox3.Text = "";
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();

                SqlDataAdapter adapt = new SqlDataAdapter("select * from Pozisyonlar", baglantim);
                DataTable table = new DataTable();
                adapt.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    comboBox1.Items.Add((string)dr["Poz"]);
                    comboBox3.Items.Add((string)dr["Poz"]);
                }

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen alanı boş bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Pozisyon ekle logo
        private void button17_Click(object sender, EventArgs e)
        {
            textBox14.Text = "";
            comboBox3.Text = "";

            comboBox3.Items.Clear();

            SqlDataAdapter adapt = new SqlDataAdapter("select * from Pozisyonlar", baglantim);
            DataTable table = new DataTable();
            adapt.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                comboBox3.Items.Add((string)dr["Poz"]);
            }

            button16.Visible = false;
            label31.Visible = false;
            button17.Visible = false;
            textBox14.Visible = false;
            button19.Visible = true;         
            button18.Visible = true;
            label33.Visible = true;
            comboBox3.Visible = true;
        }

        // Pozisyon sil logo
        private void button19_Click(object sender, EventArgs e)
        {
            textBox14.Text = "";
            comboBox3.Text = "";

            button18.Visible = false;
            label33.Visible = false;
            button19.Visible = false;
            comboBox3.Visible = false;
            button17.Visible = true;
            button16.Visible = true;
            label31.Visible = true;
            textBox14.Visible = true;
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.panel9.Visible = false;
            this.panel5.Visible = true;
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if(textBox15.Text != "" && textBox16.Text != "" && textBox17.Text != "" && textBox18.Text != "")
            {
                baglantim.Open();
                SqlCommand update = new SqlCommand("update Restoran set KIRA=@KIRA,SU=@SU,ELEKTRIK=@ELEKTRIK,DOGALGAZ=@DOGALGAZ where id=1", baglantim);
                update.Parameters.AddWithValue("@KIRA", textBox15.Text);
                update.Parameters.AddWithValue("@SU", textBox16.Text);
                update.Parameters.AddWithValue("@ELEKTRIK", textBox17.Text);
                update.Parameters.AddWithValue("@DOGALGAZ", textBox18.Text);
                update.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Restoran bilgileriniz güncellenmiştir.", ToolTipIcon.Info);
                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Restoran logo
        private void button20_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel9.Visible = true;

            baglantim.Open();

            SqlCommand oku = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader droku = oku.ExecuteReader();
            droku.Read();

            textBox15.Text = (string)droku["KIRA"]; 
            textBox16.Text = (string)droku["SU"];
            textBox17.Text = (string)droku["ELEKTRIK"];
            textBox18.Text = (string)droku["DOGALGAZ"];

            droku.Close();
            baglantim.Close();
        }

        // Restoran label
        private void label34_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel9.Visible = true;
        }

        // Tel sadece sayı
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
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
