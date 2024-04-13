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
    public partial class PersonelSiparisler : Form
    {
        public PersonelSiparisler()
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

        private void Tarifler_Click(object sender, EventArgs e)
        {
            /* empty */
        }

        private void Stok_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelStok gitPersonelStok = new PersonelStok();
            gitPersonelStok.Show();
            this.Hide();
        }

        private void Tarifler_Click_1(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            PersonelTarifler gitPersonelTarifler = new PersonelTarifler();
            gitPersonelTarifler.Show();
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

        private void PersonelSiparisler_Load(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlCommand profil = new SqlCommand("select * from Employee where TC='" + LoginBilgi.tc + "'", baglantim);
            SqlDataReader drprofil = profil.ExecuteReader();
            drprofil.Read();
            byte[] resim = (byte[])drprofil["Resim"];
            MemoryStream memorystream = new MemoryStream(resim);
            LogoPersonel.BackgroundImage = Image.FromStream(memorystream);
            drprofil.Close();

            SqlCommand siparisver = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drsiparisver = siparisver.ExecuteReader();
            drsiparisver.Read();

            if (Convert.ToBoolean(drsiparisver["SiparisVer"]) == true)
            {
                button11.Visible = true;
                button12.Visible = false;
            }
            else
            {
                button11.Visible = false;
                button12.Visible = true;
            }
            drsiparisver.Close();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Siparisler", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Tarih"].DisplayIndex = 0;
            dataGridView1.Columns["Siparis"].DisplayIndex = 1;
            dataGridView1.Columns["Tutar"].DisplayIndex = 2;
            dataGridView1.Columns["Odeme"].DisplayIndex = 3;
            dataGridView1.Columns["Dipnot"].DisplayIndex = 4;
            dataGridView1.Columns["Adres"].DisplayIndex = 5;
            dataGridView1.Columns["Isim"].DisplayIndex = 6;
            dataGridView1.Columns["TC"].DisplayIndex = 7;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 230;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 300;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 130;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 100;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 150;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 200;
            DataGridViewColumn column7 = dataGridView1.Columns[7];
            column7.Width = 200;
            DataGridViewColumn column8 = dataGridView1.Columns[8];
            column8.Width = 200;

            this.dataGridView1.Sort(this.dataGridView1.Columns["ID"], ListSortDirection.Descending);
            dataGridView1.ClearSelection();

            SqlDataAdapter adapter2 = new SqlDataAdapter("select * from SiparislerinDurumu", baglantim);
            var data2 = new DataSet();
            adapter2.Fill(data2);
            dataGridView2.DataSource = data2.Tables[0];

            dataGridView2.Columns["ID"].Visible = false;
            dataGridView2.Columns["Tarih"].Visible = false;

            DataGridViewColumn column2a = dataGridView2.Columns[2];
            column2a.Width = 160;
            DataGridViewColumn column3a = dataGridView2.Columns[3];
            column3a.Width = 300;
            DataGridViewColumn column4a = dataGridView2.Columns[4];
            column4a.Width = 100;
            DataGridViewColumn column5a = dataGridView2.Columns[5];
            column5a.Width = 120;
            DataGridViewColumn column6a = dataGridView2.Columns[6];
            column6a.Width = 200;
            DataGridViewColumn column7a = dataGridView2.Columns[7];
            column7a.Width = 200;
            DataGridViewColumn column8a = dataGridView2.Columns[8];
            column8a.Width = 200;
            DataGridViewColumn column9a = dataGridView2.Columns[9];
            column9a.Width = 150;

            this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Descending);
            dataGridView2.ClearSelection();

            SqlDataAdapter adapter3 = new SqlDataAdapter("select * from SiparisGecmisleri", baglantim);
            var data3 = new DataSet();
            adapter3.Fill(data3);
            dataGridView3.DataSource = data3.Tables[0];

            dataGridView3.Columns["ID"].Visible = false;

            DataGridViewColumn column1b = dataGridView3.Columns[1];
            column1b.Width = 230;
            DataGridViewColumn column2b = dataGridView3.Columns[2];
            column2b.Width = 130;
            DataGridViewColumn column3b = dataGridView3.Columns[3];
            column3b.Width = 300;
            DataGridViewColumn column4b = dataGridView3.Columns[4];
            column4b.Width = 100;
            DataGridViewColumn column5b = dataGridView3.Columns[5];
            column5b.Width = 120;
            DataGridViewColumn column6b = dataGridView3.Columns[6];
            column6b.Width = 200;
            DataGridViewColumn column7b = dataGridView3.Columns[7];
            column7b.Width = 200;
            DataGridViewColumn column8b = dataGridView3.Columns[8];
            column8b.Width = 200;
            DataGridViewColumn column9b = dataGridView3.Columns[9];
            column9b.Width = 150;

            this.dataGridView3.Sort(this.dataGridView3.Columns["ID"], ListSortDirection.Descending);
            dataGridView3.ClearSelection();

            SqlDataAdapter adapter4 = new SqlDataAdapter("select * from SiparisIptal", baglantim);
            var data4 = new DataSet();
            adapter4.Fill(data4);
            dataGridView4.DataSource = data4.Tables[0];

            dataGridView4.Columns["ID"].Visible = false;

            DataGridViewColumn column1c = dataGridView4.Columns[1];
            column1c.Width = 230;
            DataGridViewColumn column2c = dataGridView4.Columns[2];
            column2c.Width = 130;
            DataGridViewColumn column3c = dataGridView4.Columns[3];
            column3c.Width = 300;
            DataGridViewColumn column4c = dataGridView4.Columns[4];
            column4c.Width = 100;
            DataGridViewColumn column5c = dataGridView4.Columns[5];
            column5c.Width = 120;
            DataGridViewColumn column6c = dataGridView4.Columns[6];
            column6c.Width = 200;
            DataGridViewColumn column7c = dataGridView4.Columns[7];
            column7c.Width = 200;
            DataGridViewColumn column8c = dataGridView4.Columns[8];
            column8c.Width = 200;
            DataGridViewColumn column9c = dataGridView4.Columns[9];
            column9c.Width = 150;

            this.dataGridView4.Sort(this.dataGridView4.Columns["ID"], ListSortDirection.Descending);
            dataGridView4.ClearSelection();

            baglantim.Close();
        }

        // ID numarası harf yok
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char q = e.KeyChar;
            if (!Char.IsDigit(q) && q != 8)
            {
                e.Handled = true;
            }
        }

        // Siparişi onayla
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    baglantim.Open();

                    SqlCommand move = new SqlCommand("select * from Siparisler where ID=" + cellValue, baglantim);
                    SqlCommand durum = new SqlCommand("insert into SiparislerinDurumu (Tarih,Durum,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Durum,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                    SqlDataReader drmove = move.ExecuteReader();
                    drmove.Read();
                    durum.Parameters.AddWithValue("@Tarih", (string)drmove["Tarih"]);
                    durum.Parameters.AddWithValue("@Durum", "Hazırlanıyor");
                    durum.Parameters.AddWithValue("@Siparis", (string)drmove["Siparis"]);
                    durum.Parameters.AddWithValue("@Odeme", (string)drmove["Odeme"]);
                    durum.Parameters.AddWithValue("@Tutar", (string)drmove["Tutar"]);
                    durum.Parameters.AddWithValue("@Dipnot", (string)drmove["Dipnot"]);
                    durum.Parameters.AddWithValue("@Adres", (string)drmove["Adres"]);
                    durum.Parameters.AddWithValue("@Isim", (string)drmove["Isim"]);
                    durum.Parameters.AddWithValue("@TC", (string)drmove["TC"]);
                    drmove.Close();
                    durum.ExecuteNonQuery();


                    SqlCommand sil = new SqlCommand("delete from Siparisler where ID=" + cellValue, baglantim);
                    sil.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter("select * from Siparisler", baglantim);
                    var data = new DataSet();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];
                    dataGridView1.Refresh();
                    this.dataGridView1.Sort(this.dataGridView1.Columns["ID"], ListSortDirection.Descending);
                    dataGridView1.ClearSelection();

                    SqlDataAdapter adapter2 = new SqlDataAdapter("select * from SiparislerinDurumu", baglantim);
                    var data2 = new DataSet();
                    adapter2.Fill(data2);
                    dataGridView2.DataSource = data2.Tables[0];
                    dataGridView2.Refresh();
                    this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Descending);
                    dataGridView2.ClearSelection();

                    notifyIcon1.ShowBalloonTip(3000, "Onaylandı", "Seçtiğiniz sipariş onaylanmıştır.", ToolTipIcon.Info);

                    baglantim.Close();
                }
                else if (dataGridView1.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Sipariş Onaylanamadı", "Lütfen siparişleri tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen onaylamak için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Henüz siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Siparişi iptal et
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string tcValue = Convert.ToString(selectedRow.Cells["TC"].Value);

                    DialogResult dialog = MessageBox.Show("Siparişi iptal etmek istediğinize emin misiniz?.", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand siparis = new SqlCommand("update Customer set Siparis=@Siparis where TC='" + tcValue + "'", baglantim);
                        siparis.Parameters.AddWithValue("@Siparis", 0);
                        siparis.ExecuteNonQuery();

                        SqlCommand move = new SqlCommand("select * from Siparisler where ID=" + cellValue, baglantim);
                        SqlCommand durum = new SqlCommand("insert into SiparisIptal (Tarih,Durum,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Durum,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                        SqlDataReader drmove = move.ExecuteReader();
                        drmove.Read();
                        durum.Parameters.AddWithValue("@Tarih", (string)drmove["Tarih"]);
                        durum.Parameters.AddWithValue("@Durum", "İptal Edildi");
                        durum.Parameters.AddWithValue("@Siparis", (string)drmove["Siparis"]);
                        durum.Parameters.AddWithValue("@Odeme", (string)drmove["Odeme"]);
                        durum.Parameters.AddWithValue("@Tutar", (string)drmove["Tutar"]);
                        durum.Parameters.AddWithValue("@Dipnot", (string)drmove["Dipnot"]);
                        durum.Parameters.AddWithValue("@Adres", (string)drmove["Adres"]);
                        durum.Parameters.AddWithValue("@Isim", (string)drmove["Isim"]);
                        durum.Parameters.AddWithValue("@TC", (string)drmove["TC"]);
                        drmove.Close();
                        durum.ExecuteNonQuery();


                        SqlCommand sil = new SqlCommand("delete from Siparisler where ID=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Siparisler", baglantim);
                        var data = new DataSet();
                        adapter.Fill(data);
                        dataGridView1.DataSource = data.Tables[0];
                        dataGridView1.Refresh();
                        this.dataGridView1.Sort(this.dataGridView1.Columns["ID"], ListSortDirection.Descending);
                        dataGridView1.ClearSelection();

                        SqlDataAdapter adapter4 = new SqlDataAdapter("select * from SiparisIptal", baglantim);
                        var data4 = new DataSet();
                        adapter4.Fill(data4);
                        dataGridView4.DataSource = data4.Tables[0];
                        dataGridView4.Refresh();
                        this.dataGridView4.Sort(this.dataGridView4.Columns["ID"], ListSortDirection.Descending);
                        dataGridView4.ClearSelection();

                        notifyIcon1.ShowBalloonTip(3000, "İptal Edildi", "Seçtiğiniz sipariş iptal edilmiştir.", ToolTipIcon.Info);

                        baglantim.Close();
                    }
                }
                else if (dataGridView1.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Sipariş Onaylanamadı", "Lütfen siparişleri tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen onaylamak için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Henüz siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Durumu güncelle
        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (comboBox1.Text != "")
                {
                    if (dataGridView2.SelectedRows.Count == 1)
                    {
                        int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                        int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                        baglantim.Open();
                        SqlCommand update = new SqlCommand("update SiparislerinDurumu set Durum=@Durum where ID=" + cellValue, baglantim);
                        update.Parameters.AddWithValue("@Durum", comboBox1.Text);
                        update.ExecuteNonQuery();

                        comboBox1.Text = "";

                        SqlDataAdapter adapter2 = new SqlDataAdapter("select * from SiparislerinDurumu", baglantim);
                        var data2 = new DataSet();
                        adapter2.Fill(data2);
                        dataGridView2.DataSource = data2.Tables[0];
                        dataGridView2.Refresh();
                        this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Descending);
                        dataGridView2.ClearSelection();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Güncellendi", "Seçili siparişin durumu güncellenmiştir.", ToolTipIcon.Info);
                    }
                    else
                    {
                        notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen siparişleri tek tek seçiniz.", ToolTipIcon.Warning);
                    }
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Durum Seçiniz", "Lütfen güncellenmesi için durum seçimi yapınız.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Henüz siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Durum güncellemesi klavye engeli
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Siparişi tamamla
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string tcValue = Convert.ToString(selectedRow.Cells["TC"].Value);

                    baglantim.Open();

                    SqlCommand siparis = new SqlCommand("update Customer set Siparis=@Siparis,Yorum=@Yorum where TC='" + tcValue + "'", baglantim);
                    siparis.Parameters.AddWithValue("@Siparis", 0);
                    siparis.Parameters.AddWithValue("@Yorum", 1);
                    siparis.ExecuteNonQuery();

                    SqlCommand move = new SqlCommand("select * from SiparislerinDurumu where ID=" + cellValue, baglantim);
                    SqlCommand durum = new SqlCommand("insert into SiparisGecmisleri (Tarih,Durum,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Durum,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                    SqlDataReader drmove = move.ExecuteReader();
                    drmove.Read();
                    durum.Parameters.AddWithValue("@Tarih", (string)drmove["Tarih"]);
                    durum.Parameters.AddWithValue("@Durum", "Teslim Edildi");
                    durum.Parameters.AddWithValue("@Siparis", (string)drmove["Siparis"]);
                    durum.Parameters.AddWithValue("@Odeme", (string)drmove["Odeme"]);
                    durum.Parameters.AddWithValue("@Tutar", (string)drmove["Tutar"]);
                    durum.Parameters.AddWithValue("@Dipnot", (string)drmove["Dipnot"]);
                    durum.Parameters.AddWithValue("@Adres", (string)drmove["Adres"]);
                    durum.Parameters.AddWithValue("@Isim", (string)drmove["Isim"]);
                    durum.Parameters.AddWithValue("@TC", (string)drmove["TC"]);
                    drmove.Close();
                    durum.ExecuteNonQuery();


                    SqlCommand sil = new SqlCommand("delete from SiparislerinDurumu where ID=" + cellValue, baglantim);
                    sil.ExecuteNonQuery();

                    SqlDataAdapter adapter2 = new SqlDataAdapter("select * from SiparislerinDurumu", baglantim);
                    var data2 = new DataSet();
                    adapter2.Fill(data2);
                    dataGridView2.DataSource = data2.Tables[0];
                    dataGridView2.Refresh();
                    this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Descending);
                    dataGridView2.ClearSelection();

                    SqlDataAdapter adapter3 = new SqlDataAdapter("select * from SiparisGecmisleri", baglantim);
                    var data3 = new DataSet();
                    adapter3.Fill(data3);
                    dataGridView3.DataSource = data3.Tables[0];
                    dataGridView3.Refresh();
                    this.dataGridView3.Sort(this.dataGridView3.Columns["ID"], ListSortDirection.Descending);
                    dataGridView3.ClearSelection();

                    baglantim.Close();

                    notifyIcon1.ShowBalloonTip(3000, "Tamamlandı", "Seçili sipariş tamamlandı olarak kayıtlara eklenmiştir.", ToolTipIcon.Info);
                }
                else if (dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen siparişleri tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen tamamlanması için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Henüz siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Siparişi iptal et (sipariş durumları)
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string tcValue = Convert.ToString(selectedRow.Cells["TC"].Value);

                    DialogResult dialog = MessageBox.Show("Siparişi iptal etmek istediğinize emin misiniz?.", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand siparis = new SqlCommand("update Customer set Siparis=@Siparis where TC='" + tcValue + "'", baglantim);
                        siparis.Parameters.AddWithValue("@Siparis", 0);
                        siparis.ExecuteNonQuery();

                        SqlCommand move = new SqlCommand("select * from SiparislerinDurumu where ID=" + cellValue, baglantim);
                        SqlCommand durum = new SqlCommand("insert into SiparisIptal (Tarih,Durum,Siparis,Odeme,Tutar,Dipnot,Adres,Isim,TC) values(@Tarih,@Durum,@Siparis,@Odeme,@Tutar,@Dipnot,@Adres,@Isim,@TC)", baglantim);

                        SqlDataReader drmove = move.ExecuteReader();
                        drmove.Read();
                        durum.Parameters.AddWithValue("@Tarih", (string)drmove["Tarih"]);
                        durum.Parameters.AddWithValue("@Durum", "İptal Edildi");
                        durum.Parameters.AddWithValue("@Siparis", (string)drmove["Siparis"]);
                        durum.Parameters.AddWithValue("@Odeme", (string)drmove["Odeme"]);
                        durum.Parameters.AddWithValue("@Tutar", (string)drmove["Tutar"]);
                        durum.Parameters.AddWithValue("@Dipnot", (string)drmove["Dipnot"]);
                        durum.Parameters.AddWithValue("@Adres", (string)drmove["Adres"]);
                        durum.Parameters.AddWithValue("@Isim", (string)drmove["Isim"]);
                        durum.Parameters.AddWithValue("@TC", (string)drmove["TC"]);
                        drmove.Close();
                        durum.ExecuteNonQuery();


                        SqlCommand sil = new SqlCommand("delete from SiparislerinDurumu where ID=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        SqlDataAdapter adapter2 = new SqlDataAdapter("select * from SiparislerinDurumu", baglantim);
                        var data2 = new DataSet();
                        adapter2.Fill(data2);
                        dataGridView2.DataSource = data2.Tables[0];
                        dataGridView2.Refresh();
                        this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Descending);
                        dataGridView2.ClearSelection();

                        SqlDataAdapter adapter4 = new SqlDataAdapter("select * from SiparisIptal", baglantim);
                        var data4 = new DataSet();
                        adapter4.Fill(data4);
                        dataGridView4.DataSource = data4.Tables[0];
                        dataGridView4.Refresh();
                        this.dataGridView4.Sort(this.dataGridView4.Columns["ID"], ListSortDirection.Descending);
                        dataGridView4.ClearSelection();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "İptal Edildi", "Seçtiğiniz sipariş iptal edilmiştir.", ToolTipIcon.Info);
                    }
                }
                else if (dataGridView2.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen siparişleri tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Sipariş Seçiniz", "Lütfen iptal etmek için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Sipariş Yok", "Henüz siparişiniz bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Kaydı Sil (geçmiş siparişler)
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                if (dataGridView3.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView3.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView3.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string tcValue = Convert.ToString(selectedRow.Cells["TC"].Value);

                    DialogResult dialog = MessageBox.Show("Kayıt silindiği zaman müşteriler de bu kayda artık erişemez ve kayıt silme işlemi geri alınamaz.", "Emin misiniz?", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from SiparisGecmisleri where ID=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        SqlDataAdapter adapter3 = new SqlDataAdapter("select * from SiparisGecmisleri", baglantim);
                        var data3 = new DataSet();
                        adapter3.Fill(data3);
                        dataGridView3.DataSource = data3.Tables[0];
                        dataGridView3.Refresh();
                        this.dataGridView3.Sort(this.dataGridView3.Columns["ID"], ListSortDirection.Descending);
                        dataGridView3.ClearSelection();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Kayıt Silindi", "Seçtiğiniz sipariş kayıtlardan silinmiştir.", ToolTipIcon.Info);
                    }
                }
                else if (dataGridView3.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen kayıtları tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen kaydını silmek için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Henüz sipariş kaydı bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Kaydı Sil (iptal edilen)
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                if (dataGridView4.SelectedRows.Count == 1)
                {
                    int selectedrowindex = dataGridView4.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView4.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string tcValue = Convert.ToString(selectedRow.Cells["TC"].Value);

                    DialogResult dialog = MessageBox.Show("Kayıt silindiği zaman müşteriler de bu kayda artık erişemez ve kayıt silme işlemi geri alınamaz.", "Emin misiniz?", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from SiparisIptal where ID=" + cellValue, baglantim);
                        sil.ExecuteNonQuery();

                        SqlDataAdapter adapter4 = new SqlDataAdapter("select * from SiparisIptal", baglantim);
                        var data4 = new DataSet();
                        adapter4.Fill(data4);
                        dataGridView4.DataSource = data4.Tables[0];
                        dataGridView4.Refresh();
                        this.dataGridView4.Sort(this.dataGridView4.Columns["ID"], ListSortDirection.Descending);
                        dataGridView4.ClearSelection();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Kayıt Silindi", "Seçtiğiniz sipariş kayıtlardan silinmiştir.", ToolTipIcon.Info);
                    }
                }
                else if (dataGridView4.SelectedRows.Count > 1)
                {
                    notifyIcon1.ShowBalloonTip(3000, "Hatalı Seçim", "Lütfen kayıtları tek tek seçiniz.", ToolTipIcon.Warning);
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız", "Lütfen kaydını silmek için sipariş seçiniz.", ToolTipIcon.Warning);
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Henüz iptal edilmiş sipariş kaydı bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Tüm kayıtları sil (geçmiş siparişler)
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Kayıt silindiği zaman müşteriler de bu kayda artık erişemez ve kayıt silme işlemi geri alınamaz.", "Emin misiniz?", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    DialogResult dialog2 = MessageBox.Show("Gerçekten TÜM Kayıtları silmek istediğinize emin misiniz?", "Onay Tekrarı", MessageBoxButtons.YesNo);

                    if (dialog2 == DialogResult.Yes)
                    {
                        baglantim.Open();
                        SqlCommand sil = new SqlCommand("delete from SiparisGecmisleri", baglantim);
                        sil.ExecuteNonQuery();
                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Kayıtlar Silindi", "Tüm sipariş kayıtları silinmiştir.", ToolTipIcon.Info);
                    }
                }

            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Henüz sipariş kaydı bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        // Tüm kayıtları sil (iptal edilen)
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Kayıt silindiği zaman müşteriler de bu kayda artık erişemez ve kayıt silme işlemi geri alınamaz.", "Emin misiniz?", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    DialogResult dialog2 = MessageBox.Show("Gerçekten TÜM Kayıtları silmek istediğinize emin misiniz?", "Onay Tekrarı", MessageBoxButtons.YesNo);

                    if (dialog2 == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from SiparisIptal", baglantim);
                        sil.ExecuteNonQuery();

                        baglantim.Close();

                        notifyIcon1.ShowBalloonTip(3000, "Kayıtlar Silindi", "Tüm iptal edilmiş sipariş kayıtları silinmiştir.", ToolTipIcon.Info);
                    }
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Kayıt Yok", "Henüz iptal edilen sipariş kaydı bulunmamaktadır.", ToolTipIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = string.Format("Tarih LIKE '%{0}%' OR Siparis LIKE '%{0}%' OR Odeme LIKE '%{0}%' OR Tutar LIKE '%{0}%' OR Dipnot LIKE '%{0}%' OR Adres LIKE '%{0}%' OR Isim LIKE '%{0}%' OR TC LIKE '%{0}%'", textBox4.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Tarih LIKE '%{0}%' OR Siparis LIKE '%{0}%' OR Odeme LIKE '%{0}%' OR Tutar LIKE '%{0}%' OR Dipnot LIKE '%{0}%' OR Adres LIKE '%{0}%' OR Isim LIKE '%{0}%' OR TC LIKE '%{0}%'", textBox3.Text);
        }

        // Sipariş Açık
        private void button11_Click(object sender, EventArgs e)
        {
            button12.Visible = true;

            baglantim.Open();

            SqlCommand siparisver = new SqlCommand("update Restoran set SiparisVer=@SiparisVer where id=1", baglantim);
            siparisver.Parameters.AddWithValue("@SiparisVer", 0);
            siparisver.ExecuteNonQuery();

            baglantim.Close();

            button11.Visible = false;
        }

        // Sipariş Kapalı
        private void button12_Click(object sender, EventArgs e)
        {
            button11.Visible = true;

            baglantim.Open();

            SqlCommand siparisver = new SqlCommand("update Restoran set SiparisVer=@SiparisVer where id=1", baglantim);
            siparisver.Parameters.AddWithValue("@SiparisVer", 1);
            siparisver.ExecuteNonQuery();

            baglantim.Close();

            button12.Visible = false;
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

        // Yenile (yeni siparişler)
        private void button13_Click(object sender, EventArgs e)
        {
            baglantim.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Siparisler", baglantim);
            var data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];
            dataGridView1.Refresh();
            this.dataGridView1.Sort(this.dataGridView1.Columns["ID"], ListSortDirection.Descending);
            dataGridView1.ClearSelection();

            baglantim.Close();

            notifyIcon1.ShowBalloonTip(3000, "Yenilendi", "Sipariş listesi güncellendi.", ToolTipIcon.Info);
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
