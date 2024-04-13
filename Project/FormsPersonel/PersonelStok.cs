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
    public partial class PersonelStok : Form
    {
        public PersonelStok()
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
            PersonelAnaSayfa gitPersonelAnaSayfa = new PersonelAnaSayfa();
            gitPersonelAnaSayfa.Show();
            this.Hide();
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
            /*empty*/
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

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void PersonelStok_Load(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Employee where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();
            byte[] resim = (byte[])drprofil["Resim"];
            MemoryStream memorystream = new MemoryStream(resim);
            LogoPersonel.BackgroundImage = Image.FromStream(memorystream);
            drprofil.Close();

            this.panel6.Visible = true;
            this.panel7.Visible = false;
            this.panel9.Visible = false;
            this.panel5.Visible = false;

            this.monthCalendar1.Visible = false;

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Stok", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["NO"].Visible = false;
            dataGridView1.Columns["SIPARIS"].Visible = false;
            dataGridView1.Columns["TIP"].Visible = false;

            dataGridView1.Columns["RESIM"].DisplayIndex = 0;
            dataGridView1.Columns["MALZEME"].DisplayIndex = 1;
            dataGridView1.Columns["ADET"].DisplayIndex = 2;
            dataGridView1.Columns["BIRIM"].DisplayIndex = 3;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 70;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 185;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 90;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 100;

            DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["RESIM"];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

            DateTime today = this.monthCalendar1.TodayDate;
            DateTime pickfirstday = today.AddDays(1);
            DateTime picklastday = today.AddDays(30);

            monthCalendar1.MinDate = pickfirstday;
            monthCalendar1.SelectionRange.Start = picklastday;

            
            // Verileri tabloya ekle

            SqlDataAdapter adapter3 = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data3 = new DataSet();
            adapter3.Fill(data3);
            dataGridView2.DataSource = data3.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1a = dataGridView2.Columns[1];
            column1a.Width = 200;
            DataGridViewColumn column2a = dataGridView2.Columns[2];
            column2a.Width = 150;
            DataGridViewColumn column3a = dataGridView2.Columns[3];
            column3a.Width = 150;
            DataGridViewColumn column4a = dataGridView2.Columns[4];
            column4a.Width = 250;
            DataGridViewColumn column5a = dataGridView2.Columns[5];
            column5a.Width = 200;
            DataGridViewColumn column6a = dataGridView2.Columns[6];
            column6a.Width = 100;
            DataGridViewColumn column7a = dataGridView2.Columns[7];
            column7a.Width = 250;
            DataGridViewColumn column8a = dataGridView2.Columns[8];
            column8a.Width = 200;

            // Tarihi kontrol et

            DateTime bugun2 = DateTime.Today;

            SqlDataAdapter adapter4 = new SqlDataAdapter("select * from StokDate", baglantim);
            DataTable table4 = new DataTable();
            adapter4.Fill(table4);

            if (table4.Rows.Count > 0)
            {
                foreach (DataRow dr in table4.Rows)
                {
                    if (bugun2 > (DateTime)dr["TARIH"] || bugun2 == (DateTime)dr["TARIH"])
                    {
                        SqlCommand oku = new SqlCommand("select * from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        SqlDataReader droku = oku.ExecuteReader();

                        droku.Read();

                        SqlCommand ekle = new SqlCommand("insert into StokGecmisi (URUN,MIKTAR,FIYAT,DETAY,PERSONEL,TARIH,TESLIM) values(@URUN,@MIKTAR,@FIYAT,@DETAY,@PERSONEL,@TARIH,@TESLIM)", baglantim);
                        ekle.Parameters.AddWithValue("@URUN", (string)droku["URUN"]);
                        ekle.Parameters.AddWithValue("@MIKTAR", (string)droku["MIKTAR"]);
                        ekle.Parameters.AddWithValue("@FIYAT", (string)droku["FIYAT"]);
                        ekle.Parameters.AddWithValue("@DETAY", (string)droku["DETAY"]);
                        ekle.Parameters.AddWithValue("@PERSONEL", (string)droku["PERSONEL"]);
                        ekle.Parameters.AddWithValue("@TARIH", (string)droku["TARIH"]);
                        ekle.Parameters.AddWithValue("@TESLIM", DateTime.Now.ToString("MMM d yyyy"));
                        droku.Close();
                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        sil.ExecuteNonQuery();

                        SqlCommand plansil = new SqlCommand("delete from StokDate where SIPARISNO=" + (int)dr["SIPARISNO"], baglantim);
                        plansil.ExecuteNonQuery();
                    }
                }
            }

            // Eğer veri silindiyse tablo yeni haliyle tekrar yüklensin

            SqlDataAdapter adapter5 = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data5 = new DataSet();
            adapter5.Fill(data5);
            dataGridView2.DataSource = data5.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1b = dataGridView2.Columns[1];
            column1b.Width = 200;
            DataGridViewColumn column2b = dataGridView2.Columns[2];
            column2b.Width = 150;
            DataGridViewColumn column3b = dataGridView2.Columns[3];
            column3b.Width = 150;
            DataGridViewColumn column4b = dataGridView2.Columns[4];
            column4b.Width = 250;
            DataGridViewColumn column5b = dataGridView2.Columns[5];
            column5b.Width = 200;
            DataGridViewColumn column6b = dataGridView2.Columns[6];
            column6b.Width = 100;
            DataGridViewColumn column7b = dataGridView2.Columns[7];
            column7b.Width = 250;
            DataGridViewColumn column8b = dataGridView2.Columns[8];
            column8b.Width = 200;

            // En son ürüne kalan sürenin yazdırılması

            SqlCommand stok = new SqlCommand("select * from StokDate", baglantim);
            SqlDataAdapter adapter2 = new SqlDataAdapter(stok);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            int rows = 0;
            bool siparis = false;

            foreach (DataRow dr in table2.Rows)
            {
                rows++;
            }

            int adet = rows;
            DateTime[] tarihler = new DateTime[rows];
            DateTime enkucuk = DateTime.Today;

            if (table2.Rows.Count > 1)
            {
                siparis = true;

                rows--;
                foreach (DataRow dr in table2.Rows)
                {
                    tarihler[rows] = (DateTime)dr["TARIH"];
                    rows--;
                }

                enkucuk = tarihler[0];
                adet--;
                for (int x = 0; x < adet; x++)
                {
                    if (enkucuk > tarihler[x])
                    {
                        enkucuk = tarihler[x];
                    }
                }

            }

            if (table2.Rows.Count == 1)
            {
                siparis = true;

                foreach (DataRow dr in table2.Rows)
                {
                    enkucuk = (DateTime)dr["TARIH"];
                }
            }

            if (table2.Rows.Count == 0)
            {
                label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                label9.Text = "Planlı\nSipariş Yok";
            }

            DateTime bugun = DateTime.Today;

            if (siparis == true)
            {
                int kalangun = Convert.ToInt32((enkucuk - bugun).TotalDays);
                if(kalangun == 0)
                {
                    label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                    label9.Text = "Planlı\nSipariş Yok";
                }
                else
                {
                    label9.Font = new Font("Segoe Script", 22, FontStyle.Bold | FontStyle.Italic);
                    label9.Text = kalangun + " Gün";
                }         
            }

            baglantim.Close();
        }

        // Buzdolabı click
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel5.Visible = true;

            button3.Visible = false;
            button2.Visible = true;
            button15.Visible = true;
            button14.Visible = false;

            dataGridView1.ClearSelection();
        }

        // Stok button
        private void button10_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel5.Visible = true;

            button3.Visible = false;
            button2.Visible = true;
            button15.Visible = true;
            button14.Visible = false;

            dataGridView1.ClearSelection();
        }

        // Ürün bilgilerini getir
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            baglantim.Open();

            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);

                    SqlCommand sec = new SqlCommand("select * from Stok where NO=" + cellValue, baglantim);
                    SqlDataReader drsec = sec.ExecuteReader();
                    drsec.Read();
                    textBox1.Text = (string)drsec["MALZEME"];
                    textBox3.Text = drsec["ADET"].ToString();
                    comboBox1.Text = (string)drsec["BIRIM"];
                    comboBox4.Text = (string)drsec["TIP"];
                    drsec.Close();
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Ürün Seçilemedi", "Lütfen tek bir ürün seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Stokta ürün bulunmamaktadır.", ToolTipIcon.Error);
            }
            baglantim.Close();
        }

        // Güncelle
        private void button2_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);

            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    if (textBox1.Text.Length > 0 && comboBox1.Text.Length > 0 && textBox3.Text.Length > 0)
                    {
                        baglantim.Open();
                        SqlCommand sec = new SqlCommand("update Stok set TIP=@TIP,MALZEME=@MALZEME,ADET=@ADET,BIRIM=@BIRIM where NO=" + cellValue, baglantim);
                        sec.Parameters.AddWithValue("@TIP", comboBox4.Text);
                        sec.Parameters.AddWithValue("@MALZEME", textBox1.Text);
                        sec.Parameters.AddWithValue("@ADET", textBox3.Text);
                        sec.Parameters.AddWithValue("@BIRIM", comboBox1.Text);
                        sec.ExecuteNonQuery();
                        baglantim.Close();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Stok", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        dataGridView1.Columns["NO"].Visible = false;
                        dataGridView1.Columns["SIPARIS"].Visible = false;
                        dataGridView1.Columns["TIP"].Visible = false;

                        dataGridView1.Columns["RESIM"].DisplayIndex = 0;
                        dataGridView1.Columns["MALZEME"].DisplayIndex = 1;
                        dataGridView1.Columns["ADET"].DisplayIndex = 2;
                        dataGridView1.Columns["BIRIM"].DisplayIndex = 3;

                        DataGridViewColumn column1 = dataGridView1.Columns[1];
                        column1.Width = 70;
                        DataGridViewColumn column3 = dataGridView1.Columns[3];
                        column3.Width = 185;
                        DataGridViewColumn column4 = dataGridView1.Columns[4];
                        column4.Width = 90;
                        DataGridViewColumn column6 = dataGridView1.Columns[6];
                        column6.Width = 100;

                        DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["RESIM"];
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        dataGridView1.ClearSelection();

                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Seçili ürün bilgileri güncellenmiştir.", ToolTipIcon.Info);
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Güncelleme Başarısız", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Güncelleme Başarısız", "Lütfen tek bir ürün seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Stokta ürün bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Sil
        private void button13_Click_1(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);

            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    DialogResult dialog = MessageBox.Show("Ürünü silmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand sil = new SqlCommand("delete from Stok where NO=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Stok", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];

                        dataGridView1.Columns["NO"].Visible = false;
                        dataGridView1.Columns["SIPARIS"].Visible = false;
                        dataGridView1.Columns["TIP"].Visible = false;

                        dataGridView1.Columns["RESIM"].DisplayIndex = 0;
                        dataGridView1.Columns["MALZEME"].DisplayIndex = 1;
                        dataGridView1.Columns["ADET"].DisplayIndex = 2;
                        dataGridView1.Columns["BIRIM"].DisplayIndex = 3;

                        DataGridViewColumn column1 = dataGridView1.Columns[1];
                        column1.Width = 70;
                        DataGridViewColumn column3 = dataGridView1.Columns[3];
                        column3.Width = 185;
                        DataGridViewColumn column4 = dataGridView1.Columns[4];
                        column4.Width = 90;
                        DataGridViewColumn column6 = dataGridView1.Columns[6];
                        column6.Width = 100;

                        DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["RESIM"];
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        dataGridView1.ClearSelection();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Seçilen ürün kayıtlardan silinmiştir.", ToolTipIcon.Warning);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Ürün Silinemedi", "Lütfen tek bir ürün seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Hata", "Stokta ürün bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Ekle (değiştir)
        private void button15_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = true;

            button14.Visible = true;
            button15.Visible = false;

            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";

            dataGridView1.ClearSelection();
        }

        // Ekle button
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && comboBox1.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if (comboBox4.Text == "")
                {
                    notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen ürün tipini seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    baglantim.Open();

                    byte[] resim = new byte[0];

                    if (comboBox4.Text == "Yiyecek")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=46", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }
                    if(comboBox4.Text == "İçecek")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=47", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }
                    if (comboBox4.Text == "Servis")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=48", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }
                    if (comboBox4.Text == "Mutfak")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=49", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }
                    if (comboBox4.Text == "Temizlik")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=50", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }
                    if (comboBox4.Text == "Onarım")
                    {
                        SqlCommand resimbul = new SqlCommand("select * from Resimler where id=51", baglantim);
                        SqlDataReader drresimbul = resimbul.ExecuteReader();
                        drresimbul.Read();
                        resim = (byte[])drresimbul["RESIM"];
                        drresimbul.Close();
                    }

                    SqlCommand sec = new SqlCommand("insert into Stok (RESIM,TIP,MALZEME,ADET,SIPARIS,BIRIM) values(@RESIM,@TIP,@MALZEME,@ADET,@SIPARIS,@BIRIM)", baglantim);
                    sec.Parameters.AddWithValue("@RESIM", resim);
                    sec.Parameters.AddWithValue("@TIP", comboBox4.Text);
                    sec.Parameters.AddWithValue("@MALZEME", textBox1.Text);
                    sec.Parameters.AddWithValue("@ADET", textBox3.Text);
                    sec.Parameters.AddWithValue("@SIPARIS", 0);
                    sec.Parameters.AddWithValue("@BIRIM", comboBox1.Text);
                    sec.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter("select * from Stok", baglantim);
                    var data = new DataSet();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];

                    dataGridView1.Columns["NO"].Visible = false;
                    dataGridView1.Columns["SIPARIS"].Visible = false;
                    dataGridView1.Columns["TIP"].Visible = false;

                    dataGridView1.Columns["RESIM"].DisplayIndex = 0;
                    dataGridView1.Columns["MALZEME"].DisplayIndex = 1;
                    dataGridView1.Columns["ADET"].DisplayIndex = 2;
                    dataGridView1.Columns["BIRIM"].DisplayIndex = 3;

                    DataGridViewColumn column1 = dataGridView1.Columns[1];
                    column1.Width = 70;
                    DataGridViewColumn column3 = dataGridView1.Columns[3];
                    column3.Width = 185;
                    DataGridViewColumn column4 = dataGridView1.Columns[4];
                    column4.Width = 90;
                    DataGridViewColumn column6 = dataGridView1.Columns[6];
                    column6.Width = 100;

                    DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["RESIM"];
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                    baglantim.Close();

                    dataGridView1.ClearSelection();

                    notifyIcon1.ShowBalloonTip(3000, "Başarılı", "Yeni ürün eklenmiştir.", ToolTipIcon.Info);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Eksik Girdi", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
            }
        }

        // Güncelle (değiştir)
        private void button14_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button2.Visible = true;

            button15.Visible = true;
            button14.Visible = false;

            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";

            dataGridView1.ClearSelection();
        }

        // Ürün miktarı harf engeli
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // Birim seçimi Harf engeli
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Geri Dön Menüye
        private void button6_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel6.Visible = true;

            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";

            // En son ürüne kalan sürenin yazdırılması

            SqlCommand stok = new SqlCommand("select * from StokDate", baglantim);
            SqlDataAdapter adapter2 = new SqlDataAdapter(stok);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            int rows = 0;
            bool siparis = false;

            foreach (DataRow dr in table2.Rows)
            {
                rows++;
            }

            int adet = rows;
            DateTime[] tarihler = new DateTime[rows];
            DateTime enkucuk = DateTime.Today;

            if (table2.Rows.Count > 1)
            {
                siparis = true;

                rows--;
                foreach (DataRow dr in table2.Rows)
                {
                    tarihler[rows] = (DateTime)dr["TARIH"];
                    rows--;
                }

                enkucuk = tarihler[0];
                adet--;
                for (int x = 0; x < adet; x++)
                {
                    if (enkucuk > tarihler[x])
                    {
                        enkucuk = tarihler[x];
                    }
                }

            }

            if (table2.Rows.Count == 1)
            {
                siparis = true;

                foreach (DataRow dr in table2.Rows)
                {
                    enkucuk = (DateTime)dr["TARIH"];
                }
            }

            if (table2.Rows.Count == 0)
            {
                label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                label9.Text = "Planlı\nSipariş Yok";
            }

            DateTime bugun = DateTime.Today;

            if (siparis == true)
            {
                int kalangun = Convert.ToInt32((enkucuk - bugun).TotalDays);
                if (kalangun == 0)
                {
                    label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                    label9.Text = "Planlı\nSipariş Yok";
                }
                else
                {
                    label9.Font = new Font("Segoe Script", 22, FontStyle.Bold | FontStyle.Italic);
                    label9.Text = kalangun + " Gün";
                }
            }
        }

        // Siparişler logo click
        private void button5_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel7.Visible = true;
            this.panel8.Visible = false;

            baglantim.Open();

            // comboBox'a ürünleri getir

            comboBox3.Items.Clear();

            SqlCommand stok = new SqlCommand("select * from Stok", baglantim);
            SqlDataAdapter adapter2 = new SqlDataAdapter(stok);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            foreach (DataRow dr in table2.Rows)
            {
                comboBox3.Items.Add(dr["MALZEME"].ToString());
            }

            // Verileri tabloya ekle

            SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView2.DataSource = data.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView2.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView2.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView2.Columns[4];
            column4.Width = 250;
            DataGridViewColumn column5 = dataGridView2.Columns[5];
            column5.Width = 200;
            DataGridViewColumn column6 = dataGridView2.Columns[6];
            column6.Width = 100;
            DataGridViewColumn column7 = dataGridView2.Columns[7];
            column7.Width = 250;
            DataGridViewColumn column8 = dataGridView2.Columns[8];
            column8.Width = 200;

            dataGridView2.ClearSelection();

            // Tarihi kontrol et

            DateTime bugun = DateTime.Today;

            SqlDataAdapter adapter3 = new SqlDataAdapter("select * from StokDate", baglantim);
            DataTable table3 = new DataTable();
            adapter3.Fill(table3);

            if (table3.Rows.Count > 0)
            {
                foreach (DataRow dr in table3.Rows)
                {
                    if (bugun > (DateTime)dr["TARIH"] || bugun == (DateTime)dr["TARIH"])
                    {
                        SqlCommand oku = new SqlCommand("select * from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        SqlDataReader droku = oku.ExecuteReader();

                        droku.Read();

                        SqlCommand ekle = new SqlCommand("insert into StokGecmisi (URUN,MIKTAR,FIYAT,DETAY,PERSONEL,TARIH,TESLIM) values(@URUN,@MIKTAR,@FIYAT,@DETAY,@PERSONEL,@TARIH,@TESLIM)", baglantim);
                        ekle.Parameters.AddWithValue("@URUN", (string)droku["URUN"]);
                        ekle.Parameters.AddWithValue("@MIKTAR", (string)droku["MIKTAR"]);
                        ekle.Parameters.AddWithValue("@FIYAT", (string)droku["FIYAT"]);
                        ekle.Parameters.AddWithValue("@DETAY", (string)droku["DETAY"]);
                        ekle.Parameters.AddWithValue("@PERSONEL", (string)droku["PERSONEL"]);
                        ekle.Parameters.AddWithValue("@TARIH", (string)droku["TARIH"]);
                        ekle.Parameters.AddWithValue("@TESLIM", DateTime.Now.ToString("MMM d yyyy"));
                        droku.Close();
                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        sil.ExecuteNonQuery();

                        SqlCommand plansil = new SqlCommand("delete from StokDate where SIPARISNO=" + (int)dr["SIPARISNO"], baglantim);
                        plansil.ExecuteNonQuery();
                    }
                }
            }

            // Eğer veri silindiyse tablo yeni haliyle tekrar yüklensin

            SqlDataAdapter adapter4 = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data4 = new DataSet();
            adapter4.Fill(data4);
            dataGridView2.DataSource = data4.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1a = dataGridView2.Columns[1];
            column1a.Width = 200;
            DataGridViewColumn column2a = dataGridView2.Columns[2];
            column2a.Width = 150;
            DataGridViewColumn column3a = dataGridView2.Columns[3];
            column3a.Width = 150;
            DataGridViewColumn column4a = dataGridView2.Columns[4];
            column4a.Width = 250;
            DataGridViewColumn column5a = dataGridView2.Columns[5];
            column5a.Width = 200;
            DataGridViewColumn column6a = dataGridView2.Columns[6];
            column6a.Width = 100;
            DataGridViewColumn column7a = dataGridView2.Columns[7];
            column7a.Width = 250;
            DataGridViewColumn column8a = dataGridView2.Columns[8];
            column8a.Width = 200;

            dataGridView2.ClearSelection();

            baglantim.Close();
        }

        // Siparişler button click
        private void button8_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel7.Visible = true;
            this.panel8.Visible = false;

            baglantim.Open();

            // comboBox'a ürünleri getir

            comboBox3.Items.Clear();

            SqlCommand stok = new SqlCommand("select * from Stok", baglantim);
            SqlDataAdapter adapter2 = new SqlDataAdapter(stok);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            foreach (DataRow dr in table2.Rows)
            {
                comboBox3.Items.Add(dr["MALZEME"].ToString());
            }

            // Verileri tabloya ekle

            SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView2.DataSource = data.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView2.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView2.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView2.Columns[4];
            column4.Width = 250;
            DataGridViewColumn column5 = dataGridView2.Columns[5];
            column5.Width = 200;
            DataGridViewColumn column6 = dataGridView2.Columns[6];
            column6.Width = 100;
            DataGridViewColumn column7 = dataGridView2.Columns[7];
            column7.Width = 250;
            DataGridViewColumn column8 = dataGridView2.Columns[8];
            column8.Width = 200;

            dataGridView2.ClearSelection();

            // Tarihi kontrol et

            DateTime bugun = DateTime.Today;

            SqlDataAdapter adapter3 = new SqlDataAdapter("select * from StokDate", baglantim);
            DataTable table3 = new DataTable();
            adapter3.Fill(table3);

            if (table3.Rows.Count > 0)
            {
                foreach (DataRow dr in table3.Rows)
                {
                    if (bugun > (DateTime)dr["TARIH"] || bugun == (DateTime)dr["TARIH"])
                    {
                        SqlCommand oku = new SqlCommand("select * from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        SqlDataReader droku = oku.ExecuteReader();

                        droku.Read();

                        SqlCommand ekle = new SqlCommand("insert into StokGecmisi (URUN,MIKTAR,FIYAT,DETAY,PERSONEL,TARIH,TESLIM) values(@URUN,@MIKTAR,@FIYAT,@DETAY,@PERSONEL,@TARIH,@TESLIM)", baglantim);
                        ekle.Parameters.AddWithValue("@URUN", (string)droku["URUN"]);
                        ekle.Parameters.AddWithValue("@MIKTAR", (string)droku["MIKTAR"]);
                        ekle.Parameters.AddWithValue("@FIYAT", (string)droku["FIYAT"]);
                        ekle.Parameters.AddWithValue("@DETAY", (string)droku["DETAY"]);
                        ekle.Parameters.AddWithValue("@PERSONEL", (string)droku["PERSONEL"]);
                        ekle.Parameters.AddWithValue("@TARIH", (string)droku["TARIH"]);
                        ekle.Parameters.AddWithValue("@TESLIM", DateTime.Now.ToString("MMM d yyyy"));
                        droku.Close();
                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from StokSiparis where NO=" + (int)dr["SIPARISNO"], baglantim);
                        sil.ExecuteNonQuery();

                        SqlCommand plansil = new SqlCommand("delete from StokDate where SIPARISNO=" + (int)dr["SIPARISNO"], baglantim);
                        plansil.ExecuteNonQuery();
                    }
                }
            }

            // Eğer veri silindiyse tablo yeni haliyle tekrar yüklensin

            SqlDataAdapter adapter4 = new SqlDataAdapter("select * from StokSiparis", baglantim);
            var data4 = new DataSet();
            adapter4.Fill(data4);
            dataGridView2.DataSource = data4.Tables[0];

            dataGridView2.Columns["NO"].Visible = false;

            dataGridView2.Columns["URUN"].DisplayIndex = 0;
            dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView2.Columns["DETAY"].DisplayIndex = 3;
            dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
            dataGridView2.Columns["TARIH"].DisplayIndex = 6;
            dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

            DataGridViewColumn column1a = dataGridView2.Columns[1];
            column1a.Width = 200;
            DataGridViewColumn column2a = dataGridView2.Columns[2];
            column2a.Width = 150;
            DataGridViewColumn column3a = dataGridView2.Columns[3];
            column3a.Width = 150;
            DataGridViewColumn column4a = dataGridView2.Columns[4];
            column4a.Width = 250;
            DataGridViewColumn column5a = dataGridView2.Columns[5];
            column5a.Width = 200;
            DataGridViewColumn column6a = dataGridView2.Columns[6];
            column6a.Width = 100;
            DataGridViewColumn column7a = dataGridView2.Columns[7];
            column7a.Width = 250;
            DataGridViewColumn column8a = dataGridView2.Columns[8];
            column8a.Width = 200;

            dataGridView2.ClearSelection();

            baglantim.Close();
        }

        // Geri Dön
        private void button7_Click(object sender, EventArgs e)
        {
            this.panel7.Visible = false;
            this.panel6.Visible = true;
           
            textBox2.Font = new Font("Maindra GD", 14, FontStyle.Italic);
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "*Sipariş için bir açıklama giriniz*";

            // En son ürüne kalan sürenin yazdırılması

            SqlCommand stok = new SqlCommand("select * from StokDate", baglantim);
            SqlDataAdapter adapter2 = new SqlDataAdapter(stok);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            int rows = 0;
            bool siparis = false;

            foreach (DataRow dr in table2.Rows)
            {
                rows++;
            }

            int adet = rows;
            DateTime[] tarihler = new DateTime[rows];
            DateTime enkucuk = DateTime.Today;

            if (table2.Rows.Count > 1)
            {
                siparis = true;

                rows--;
                foreach (DataRow dr in table2.Rows)
                {
                    tarihler[rows] = (DateTime)dr["TARIH"];
                    rows--;
                }

                enkucuk = tarihler[0];
                adet--;
                for (int x = 0; x < adet; x++)
                {
                    if (enkucuk > tarihler[x])
                    {
                        enkucuk = tarihler[x];
                    }
                }

            }

            if (table2.Rows.Count == 1)
            {
                siparis = true;

                foreach (DataRow dr in table2.Rows)
                {
                    enkucuk = (DateTime)dr["TARIH"];
                }
            }

            if (table2.Rows.Count == 0)
            {
                label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                label9.Text = "Planlı\nSipariş Yok";
            }

            DateTime bugun = DateTime.Today;

            if (siparis == true)
            {
                int kalangun = Convert.ToInt32((enkucuk - bugun).TotalDays);
                if (kalangun == 0)
                {
                    label9.Font = new Font("Segoe Script", 12, FontStyle.Bold);
                    label9.Text = "Planlı\nSipariş Yok";
                }
                else
                {
                    label9.Font = new Font("Segoe Script", 22, FontStyle.Bold | FontStyle.Italic);
                    label9.Text = kalangun + " Gün";
                }
            }
        }

        string not = "(Detay yok)";
        string tarih = DateTime.Today.ToString("MMM d yyyy");

        // Siparişi ver
        private void button11_Click(object sender, EventArgs e)
        {
            if (this.panel8.Visible == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarısız", "Lütfen sipariş detayını kapatınız.", ToolTipIcon.Warning);
            }
            else if (this.monthCalendar1.Visible == true)
            {
                notifyIcon1.ShowBalloonTip(3000, "İşlem Başarısız", "Lütfen takvimi kapatınız.", ToolTipIcon.Warning);
            }
            else
            {
                if (comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
                {
                    int miktar = Convert.ToInt32(textBox4.Text);
                    double fiyat = Convert.ToDouble(textBox5.Text);

                    if (checkBox1.Checked)
                    {
                        fiyat = miktar * fiyat;
                    }
      
                    baglantim.Open();

                    string plan;
                    string teslim;
                    string tarihnow = DateTime.Now.ToString("HH:mm:ss - MMM d yyyy");

                    if (checkBox2.Checked)
                    {
                        SqlCommand tarih = new SqlCommand("insert into StokDate (TARIH,TARIH2) values(@TARIH,@TARIH2)", baglantim);
                        tarih.Parameters.AddWithValue("@TARIH", monthCalendar1.SelectionRange.Start);
                        tarih.Parameters.AddWithValue("@TARIH2", tarihnow);
                        tarih.ExecuteNonQuery();

                        plan = "Evet";
                        teslim = monthCalendar1.SelectionRange.Start.ToString("MMM d yyyy");
                    }
                    else
                    {
                        plan = "Hayır";
                        teslim = "(Belirtilmemiş)";
                    }

                    SqlCommand bul = new SqlCommand("select * from Employee where TC=" + LoginBilgi.tc, baglantim);
                    SqlDataReader drbul = bul.ExecuteReader();
                    drbul.Read();
                    string isim = (string)drbul["Name"] + " " + (string)drbul["Surname"];
                    drbul.Close();

                    SqlCommand ekle = new SqlCommand("insert into StokSiparis (URUN,MIKTAR,FIYAT,DETAY,PERSONEL,PLANLAMA,TARIH,TESLIM) values(@URUN,@MIKTAR,@FIYAT,@DETAY,@PERSONEL,@PLANLAMA,@TARIH,@TESLIM)", baglantim);
                    ekle.Parameters.AddWithValue("@URUN", comboBox3.Text);
                    ekle.Parameters.AddWithValue("@MIKTAR", Convert.ToString(miktar) + " " + comboBox2.Text);
                    ekle.Parameters.AddWithValue("@FIYAT", Convert.ToString(fiyat) + " TL");
                    ekle.Parameters.AddWithValue("@DETAY", not);
                    ekle.Parameters.AddWithValue("@PERSONEL", isim);
                    ekle.Parameters.AddWithValue("@PLANLAMA", plan);
                    ekle.Parameters.AddWithValue("@TARIH", tarihnow);
                    ekle.Parameters.AddWithValue("@TESLIM", teslim);
                    ekle.ExecuteNonQuery();

                    if(checkBox2.Checked)
                    {
                        SqlCommand oku = new SqlCommand("select * from StokSiparis where TARIH='" + tarihnow + "'", baglantim);
                        SqlCommand planlama = new SqlCommand("update StokDate set SIPARISNO=@SIPARISNO where TARIH2='" + tarihnow + "'", baglantim);
                        SqlDataReader droku = oku.ExecuteReader();
                        droku.Read();
                        planlama.Parameters.AddWithValue("@SIPARISNO", Convert.ToInt32(droku["NO"]));
                        droku.Close();
                        planlama.ExecuteNonQuery();
                    }
                    
                    textBox2.Font = new Font("Maindra GD", 14, FontStyle.Italic);
                    textBox2.ForeColor = Color.Gray;
                    textBox2.Text = "*Sipariş için bir açıklama giriniz*";

                    notifyIcon1.ShowBalloonTip(3000, "Sipariş Eklendi", "Ürün siparişiniz kayıtlara eklenmiştir.", ToolTipIcon.Info);

                    SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
                    var data = new DataSet();
                    adapter.Fill(data);
                    dataGridView2.DataSource = data.Tables[0];

                    dataGridView2.Columns["NO"].Visible = false;

                    dataGridView2.Columns["URUN"].DisplayIndex = 0;
                    dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
                    dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
                    dataGridView2.Columns["DETAY"].DisplayIndex = 3;
                    dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
                    dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
                    dataGridView2.Columns["TARIH"].DisplayIndex = 6;
                    dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

                    DataGridViewColumn column1 = dataGridView2.Columns[1];
                    column1.Width = 200;
                    DataGridViewColumn column2 = dataGridView2.Columns[2];
                    column2.Width = 150;
                    DataGridViewColumn column3 = dataGridView2.Columns[3];
                    column3.Width = 150;
                    DataGridViewColumn column4 = dataGridView2.Columns[4];
                    column4.Width = 250;
                    DataGridViewColumn column5 = dataGridView2.Columns[5];
                    column5.Width = 200;
                    DataGridViewColumn column6 = dataGridView2.Columns[6];
                    column6.Width = 100;
                    DataGridViewColumn column7 = dataGridView2.Columns[7];
                    column7.Width = 250;
                    DataGridViewColumn column8 = dataGridView2.Columns[8];
                    column8.Width = 200;

                    this.dataGridView2.Sort(this.dataGridView2.Columns["NO"], ListSortDirection.Descending);

                    dataGridView2.ClearSelection();

                    comboBox3.Text = "";
                    textBox4.Text = "";
                    comboBox2.Text = "";
                    textBox5.Text = "";
                    button16.Enabled = true;
                    checkBox3.Checked = false;
                    checkBox2.Checked = false;
                    button11.Enabled = true;
                    button12.Enabled = true;
                    button19.Enabled = true;
                    button7.Enabled = true;
                    this.monthCalendar1.Visible = false;

                    baglantim.Close();
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "İşlem Başarısız", "Lütfen boş alan bırakmayınız.", ToolTipIcon.Warning);
                }
            }
        }

        // Sipariş detayı
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked == true)
            {
                if (textBox2.Text != "" && textBox2.Text != "*Sipariş için bir açıklama giriniz*")
                {
                    not = textBox2.Text;
                }
                else
                {
                    not = "(Detay yok)";
                }
            }
            else
            {
                not = "(Detay yok)";
            }
        }

        // Sipariş detayı iptal
        private void button18_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = false;

            button7.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;

            if (textBox2.Text == "" || textBox2.Text == "*Sipariş için bir açıklama giriniz*")
            {
                checkBox3.Checked = false;
            }      
        }

        // Sipariş detayı kaydet
        private void button17_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" && textBox2.Text != "*Sipariş için bir açıklama giriniz*")
            {
                not = textBox2.Text;
            }
            else
            {
                not = "(Detay yok)";
            }

            checkBox3.Checked = true;

            this.panel8.Visible = false;
            button7.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
        }

        // Sipariş detayı kaydet text change
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == "*Sipariş için bir açıklama giriniz*")
            {
                button17.Enabled = false; // kaydet butonu
                button7.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
            }
            else
            {
                button17.Enabled = true; // kaydet butonu
            }
        }

        // Ürün ismi klavye engeli
        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Ürün miktarı harf engeli
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        // Birim klavye engeli
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Fiyat harf engeli
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                if(e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        // Sipariş detayı text click
        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "*Sipariş için bir açıklama giriniz*")
            {
                textBox2.Text = "";
                textBox2.Font = new Font("Maindra GD", 14, FontStyle.Bold);
                textBox2.ForeColor = Color.Black;
            }
        }

        // Detay logo click
        private void button16_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = true;
            this.panel8.BringToFront();
            button7.Enabled = false;

            if (textBox2.Text == "" || textBox2.Text == "*Sipariş için bir açıklama giriniz*")
            {
                button17.Enabled = false;
            }
            else
            {
                button17.Enabled = true;
            }
        }

        // Siparişi Sil
        private void button12_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);
                    string isim = Convert.ToString(selectedRow.Cells["URUN"].Value);
                    string fiyat = Convert.ToString(selectedRow.Cells["FIYAT"].Value);

                    DialogResult dialog = MessageBox.Show(isim + " isimli ürünün " + fiyat + " tutarındaki siparişini silmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {                     
                        baglantim.Open();

                        SqlCommand plansil = new SqlCommand("delete from StokDate where SIPARISNO=" + cellValue, baglantim);
                        plansil.ExecuteNonQuery();
                        SqlCommand sil = new SqlCommand("delete from StokSiparis where NO=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Seçtiğiniz sipariş silinmiştir.", ToolTipIcon.Info);

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView2.DataSource = data.Tables[0];

                        dataGridView2.Columns["NO"].Visible = false;

                        dataGridView2.Columns["URUN"].DisplayIndex = 0;
                        dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
                        dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
                        dataGridView2.Columns["DETAY"].DisplayIndex = 3;
                        dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
                        dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
                        dataGridView2.Columns["TARIH"].DisplayIndex = 6;
                        dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

                        DataGridViewColumn column1 = dataGridView2.Columns[1];
                        column1.Width = 200;
                        DataGridViewColumn column2 = dataGridView2.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView2.Columns[3];
                        column3.Width = 150;
                        DataGridViewColumn column4 = dataGridView2.Columns[4];
                        column4.Width = 250;
                        DataGridViewColumn column5 = dataGridView2.Columns[5];
                        column5.Width = 200;
                        DataGridViewColumn column6 = dataGridView2.Columns[6];
                        column6.Width = 100;
                        DataGridViewColumn column7 = dataGridView2.Columns[7];
                        column7.Width = 250;
                        DataGridViewColumn column8 = dataGridView2.Columns[8];
                        column8.Width = 200;

                        this.dataGridView2.Sort(this.dataGridView2.Columns["NO"], ListSortDirection.Descending);
                        dataGridView2.ClearSelection();

                        baglantim.Close();
                    }
                }
                else if(dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla sipariş seçmeyiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen siparişi seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Herhangi bir sipariş girilmemiş.", ToolTipIcon.Error);
            }
        }

        // Tüm kayıtları sil
        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Tüm Siparişleri silmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    DialogResult dialog2 = MessageBox.Show("Bu işlem geri alınamaz. Gerçekten TÜM Siparişleri SİLMEK istediğinizi onaylıyor musunuz?", "Onay Tekrarı", MessageBoxButtons.YesNo);

                    if (dialog2 == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand plansil = new SqlCommand("delete from StokDate", baglantim);
                        plansil.ExecuteNonQuery();
                        SqlCommand sil = new SqlCommand("delete from StokSiparis", baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Tüm siparişler silinmiştir.", ToolTipIcon.Info);

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView2.DataSource = data.Tables[0];

                        dataGridView2.Columns["NO"].Visible = false;

                        dataGridView2.Columns["URUN"].DisplayIndex = 0;
                        dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
                        dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
                        dataGridView2.Columns["DETAY"].DisplayIndex = 3;
                        dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
                        dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
                        dataGridView2.Columns["TARIH"].DisplayIndex = 6;
                        dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

                        DataGridViewColumn column1 = dataGridView2.Columns[1];
                        column1.Width = 200;
                        DataGridViewColumn column2 = dataGridView2.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView2.Columns[3];
                        column3.Width = 150;
                        DataGridViewColumn column4 = dataGridView2.Columns[4];
                        column4.Width = 250;
                        DataGridViewColumn column5 = dataGridView2.Columns[5];
                        column5.Width = 200;
                        DataGridViewColumn column6 = dataGridView2.Columns[6];
                        column6.Width = 100;
                        DataGridViewColumn column7 = dataGridView2.Columns[7];
                        column7.Width = 250;
                        DataGridViewColumn column8 = dataGridView2.Columns[8];
                        column8.Width = 200;

                        baglantim.Close();
                    }
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Herhangi bir sipariş girilmemiş.", ToolTipIcon.Error);
            }
        }

        // Siparişi tamamla
        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);
                    string isim = Convert.ToString(selectedRow.Cells["URUN"].Value);
                    string tutar = Convert.ToString(selectedRow.Cells["FIYAT"].Value);

                    DialogResult dialog = MessageBox.Show(isim + " ürünü için " + tutar + " tutarındaki siparişinizin tamamlandığını onaylıyor musunuz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand oku = new SqlCommand("select * from StokSiparis where NO=" + cellValue, baglantim);
                        SqlDataReader droku = oku.ExecuteReader();

                        droku.Read();

                        SqlCommand ekle = new SqlCommand("insert into StokGecmisi (URUN,MIKTAR,FIYAT,DETAY,PERSONEL,TARIH,TESLIM) values(@URUN,@MIKTAR,@FIYAT,@DETAY,@PERSONEL,@TARIH,@TESLIM)", baglantim);
                        ekle.Parameters.AddWithValue("@URUN", (string)droku["URUN"]);
                        ekle.Parameters.AddWithValue("@MIKTAR", (string)droku["MIKTAR"]);
                        ekle.Parameters.AddWithValue("@FIYAT", (string)droku["FIYAT"]);
                        ekle.Parameters.AddWithValue("@DETAY", (string)droku["DETAY"]);
                        ekle.Parameters.AddWithValue("@PERSONEL", (string)droku["PERSONEL"]);
                        ekle.Parameters.AddWithValue("@TARIH", (string)droku["TARIH"]);
                        ekle.Parameters.AddWithValue("@TESLIM", DateTime.Now.ToString("MMM d yyyy"));
                        droku.Close();
                        ekle.ExecuteNonQuery();

                        SqlCommand sil = new SqlCommand("delete from StokSiparis where NO=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "Sipariş Tamamlandı", "Seçtiğiniz sipariş başarıyla kayıtlara geçmiştir.", ToolTipIcon.Info);

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from StokSiparis", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView2.DataSource = data.Tables[0];

                        dataGridView2.Columns["NO"].Visible = false;

                        dataGridView2.Columns["URUN"].DisplayIndex = 0;
                        dataGridView2.Columns["MIKTAR"].DisplayIndex = 1;
                        dataGridView2.Columns["FIYAT"].DisplayIndex = 2;
                        dataGridView2.Columns["DETAY"].DisplayIndex = 3;
                        dataGridView2.Columns["PERSONEL"].DisplayIndex = 4;
                        dataGridView2.Columns["PLANLAMA"].DisplayIndex = 5;
                        dataGridView2.Columns["TARIH"].DisplayIndex = 6;
                        dataGridView2.Columns["TESLIM"].DisplayIndex = 7;

                        DataGridViewColumn column1 = dataGridView2.Columns[1];
                        column1.Width = 200;
                        DataGridViewColumn column2 = dataGridView2.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView2.Columns[3];
                        column3.Width = 150;
                        DataGridViewColumn column4 = dataGridView2.Columns[4];
                        column4.Width = 250;
                        DataGridViewColumn column5 = dataGridView2.Columns[5];
                        column5.Width = 200;
                        DataGridViewColumn column6 = dataGridView2.Columns[6];
                        column6.Width = 100;
                        DataGridViewColumn column7 = dataGridView2.Columns[7];
                        column7.Width = 250;
                        DataGridViewColumn column8 = dataGridView2.Columns[8];
                        column8.Width = 200;

                        this.dataGridView2.Sort(this.dataGridView2.Columns["NO"], ListSortDirection.Descending);
                        dataGridView2.ClearSelection();

                        baglantim.Close();
                    }
                }
                else if(dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla sipariş seçmeyiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen siparişi seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            tarih = monthCalendar1.SelectionRange.Start.ToString("MMM d yyyy");
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.Visible = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                this.monthCalendar1.Visible = true;
                this.monthCalendar1.BringToFront();
            }
            else
            {
                this.monthCalendar1.Visible = false;
            }
        }

        // Arama yeri
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = string.Format("URUN LIKE '%{0}%' OR MIKTAR LIKE '%{0}%' OR DETAY LIKE '%{0}%' OR PERSONEL LIKE '%{0}%' OR TARIH LIKE '%{0}%' OR TESLIM LIKE '%{0}%'", textBox6.Text);
        }

        // Sipariş geçmişleri logo
        private void button4_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel9.Visible = true;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from StokGecmisi", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView3.DataSource = data.Tables[0];

            dataGridView3.Columns["NO"].Visible = false;

            dataGridView3.Columns["URUN"].DisplayIndex = 0;
            dataGridView3.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView3.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView3.Columns["DETAY"].DisplayIndex = 3;
            dataGridView3.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView3.Columns["TARIH"].DisplayIndex = 5;
            dataGridView3.Columns["TESLIM"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView3.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView3.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView3.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView3.Columns[4];
            column4.Width = 250;
            DataGridViewColumn column5 = dataGridView3.Columns[5];
            column5.Width = 200;
            DataGridViewColumn column6 = dataGridView3.Columns[6];
            column6.Width = 250;
            DataGridViewColumn column7 = dataGridView3.Columns[7];
            column7.Width = 200;

            this.dataGridView3.Sort(this.dataGridView3.Columns["NO"], ListSortDirection.Descending);
            dataGridView3.ClearSelection();

            baglantim.Close();
        }

        // sipariş geçmişleri label
        private void button9_Click(object sender, EventArgs e)
        {
            this.panel6.Visible = false;
            this.panel9.Visible = true;

            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from StokGecmisi", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView3.DataSource = data.Tables[0];

            dataGridView3.Columns["NO"].Visible = false;

            dataGridView3.Columns["URUN"].DisplayIndex = 0;
            dataGridView3.Columns["MIKTAR"].DisplayIndex = 1;
            dataGridView3.Columns["FIYAT"].DisplayIndex = 2;
            dataGridView3.Columns["DETAY"].DisplayIndex = 3;
            dataGridView3.Columns["PERSONEL"].DisplayIndex = 4;
            dataGridView3.Columns["TARIH"].DisplayIndex = 5;
            dataGridView3.Columns["TESLIM"].DisplayIndex = 6;

            DataGridViewColumn column1 = dataGridView3.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView3.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dataGridView3.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView3.Columns[4];
            column4.Width = 250;
            DataGridViewColumn column5 = dataGridView3.Columns[5];
            column5.Width = 200;
            DataGridViewColumn column6 = dataGridView3.Columns[6];
            column6.Width = 250;
            DataGridViewColumn column7 = dataGridView3.Columns[7];
            column7.Width = 200;

            this.dataGridView3.Sort(this.dataGridView3.Columns["NO"], ListSortDirection.Descending);
            dataGridView3.ClearSelection();

            baglantim.Close();
        }

        // Gerid dön
        private void button26_Click(object sender, EventArgs e)
        {
            this.panel9.Visible = false;
            this.panel6.Visible = true;
        }

        // Kayıt sil
        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                if (dataGridView3.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView3.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView3.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["NO"].Value);
                    string isim = Convert.ToString(selectedRow.Cells["URUN"].Value);
                    string fiyat = Convert.ToString(selectedRow.Cells["FIYAT"].Value);
                    string tarih = Convert.ToString(selectedRow.Cells["TESLIM"].Value);

                    DialogResult dialog = MessageBox.Show(isim + " isimli ürünün " + tarih + " tarihinde girilmiş "
                        + fiyat + " tutarındaki kaydını silmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from StokGecmisi where NO=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Seçtiğiniz kayıt silinmiştir.", ToolTipIcon.Info);

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from StokGecmisi", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView3.DataSource = data.Tables[0];

                        dataGridView3.Columns["NO"].Visible = false;

                        dataGridView3.Columns["URUN"].DisplayIndex = 0;
                        dataGridView3.Columns["MIKTAR"].DisplayIndex = 1;
                        dataGridView3.Columns["FIYAT"].DisplayIndex = 2;
                        dataGridView3.Columns["DETAY"].DisplayIndex = 3;
                        dataGridView3.Columns["PERSONEL"].DisplayIndex = 4;
                        dataGridView3.Columns["TARIH"].DisplayIndex = 5;
                        dataGridView3.Columns["TESLIM"].DisplayIndex = 6;

                        DataGridViewColumn column1 = dataGridView3.Columns[1];
                        column1.Width = 200;
                        DataGridViewColumn column2 = dataGridView3.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView3.Columns[3];
                        column3.Width = 150;
                        DataGridViewColumn column4 = dataGridView3.Columns[4];
                        column4.Width = 250;
                        DataGridViewColumn column5 = dataGridView3.Columns[5];
                        column5.Width = 200;
                        DataGridViewColumn column6 = dataGridView3.Columns[6];
                        column6.Width = 250;
                        DataGridViewColumn column7 = dataGridView3.Columns[7];
                        column7.Width = 200;

                        this.dataGridView3.Sort(this.dataGridView3.Columns["NO"], ListSortDirection.Descending);
                        dataGridView3.ClearSelection();

                        baglantim.Close();
                    }
                }
                else if (dataGridView3.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen aynı anda birden fazla kayıt seçmeyiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen kayıt seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Herhangi bir sipariş kaydı girilmemiş.", ToolTipIcon.Error);
            }
        }

        // Tüm kayıtları sil
        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Tüm Kayıtları silmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    DialogResult dialog2 = MessageBox.Show("Bu işlem geri alınamaz. Gerçekten TÜM Kayıtları SİLMEK istediğinizi onaylıyor musunuz?", "Onay Tekrarı", MessageBoxButtons.YesNo);

                    if (dialog2 == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from StokGecmisi", baglantim);
                        sil.ExecuteNonQuery();

                        notifyIcon1.ShowBalloonTip(3000, "İşlem Başarılı", "Tüm kayıtlar silinmiştir.", ToolTipIcon.Info);

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from StokGecmisi", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView3.DataSource = data.Tables[0];

                        dataGridView3.Columns["NO"].Visible = false;

                        dataGridView3.Columns["URUN"].DisplayIndex = 0;
                        dataGridView3.Columns["MIKTAR"].DisplayIndex = 1;
                        dataGridView3.Columns["FIYAT"].DisplayIndex = 2;
                        dataGridView3.Columns["DETAY"].DisplayIndex = 3;
                        dataGridView3.Columns["PERSONEL"].DisplayIndex = 4;
                        dataGridView3.Columns["TARIH"].DisplayIndex = 5;
                        dataGridView3.Columns["TESLIM"].DisplayIndex = 6;

                        DataGridViewColumn column1 = dataGridView3.Columns[1];
                        column1.Width = 200;
                        DataGridViewColumn column2 = dataGridView3.Columns[2];
                        column2.Width = 150;
                        DataGridViewColumn column3 = dataGridView3.Columns[3];
                        column3.Width = 150;
                        DataGridViewColumn column4 = dataGridView3.Columns[4];
                        column4.Width = 250;
                        DataGridViewColumn column5 = dataGridView3.Columns[5];
                        column5.Width = 200;
                        DataGridViewColumn column6 = dataGridView3.Columns[6];
                        column6.Width = 250;
                        DataGridViewColumn column7 = dataGridView3.Columns[7];
                        column7.Width = 200;

                        baglantim.Close();
                    }
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Herhangi bir sipariş kaydı girilmemiş.", ToolTipIcon.Error);
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
