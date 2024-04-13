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
    public partial class SefYorumlar : Form
    {
        public SefYorumlar()
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

        private void Profil_Click(object sender, EventArgs e)
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

        private void Bilanço_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefBilanço gitSefBilanço = new SefBilanço();
            gitSefBilanço.Show();
            this.Hide();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");

        private void SefYorumlar_Load(object sender, EventArgs e)
        {
            baglantim.Open();

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
            dataGridView1.ClearSelection();

            baglantim.Close();
        }


        // Kalp
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                baglantim.Open();

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                SqlCommand emote = new SqlCommand("update Yorumlar set Emote=@Emote where ID=" + cellValue, baglantim);
                SqlCommand bul = new SqlCommand("select * from Resimler where id=41", baglantim);
                SqlDataReader drbul = bul.ExecuteReader();

                drbul.Read();
                byte[] resim = (byte[])drbul["RESIM"];
                drbul.Close();

                emote.Parameters.AddWithValue("@Emote", resim);
                emote.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Başarılı.", "Yoruma ifade bıraktınız!.", ToolTipIcon.Info);

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
                dataGridView1.ClearSelection();

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız.", "Lütfen tepki vereceğiniz yorumu seçiniz.", ToolTipIcon.Warning);
            }
        }

        // Beğen
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                baglantim.Open();

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                SqlCommand emote = new SqlCommand("update Yorumlar set Emote=@Emote where ID=" + cellValue, baglantim);
                SqlCommand bul = new SqlCommand("select * from Resimler where id=42", baglantim);
                SqlDataReader drbul = bul.ExecuteReader();

                drbul.Read();
                byte[] resim = (byte[])drbul["RESIM"];
                drbul.Close();

                emote.Parameters.AddWithValue("@Emote", resim);
                emote.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Başarılı.", "Yoruma ifade bıraktınız!.", ToolTipIcon.Info);

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
                dataGridView1.ClearSelection();

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız.", "Lütfen tepki vereceğiniz yorumu seçiniz.", ToolTipIcon.Warning);
            }
        }

        // Gülücük
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                baglantim.Open();

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                SqlCommand emote = new SqlCommand("update Yorumlar set Emote=@Emote where ID=" + cellValue, baglantim);
                SqlCommand bul = new SqlCommand("select * from Resimler where id=43", baglantim);
                SqlDataReader drbul = bul.ExecuteReader();

                drbul.Read();
                byte[] resim = (byte[])drbul["RESIM"];
                drbul.Close();

                emote.Parameters.AddWithValue("@Emote", resim);
                emote.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Başarılı.", "Yoruma ifade bıraktınız!.", ToolTipIcon.Info);

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
                dataGridView1.ClearSelection();

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız.", "Lütfen tepki vereceğiniz yorumu seçiniz.", ToolTipIcon.Warning);
            }
        }

        // Üzgün
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                baglantim.Open();

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                SqlCommand emote = new SqlCommand("update Yorumlar set Emote=@Emote where ID=" + cellValue, baglantim);
                SqlCommand bul = new SqlCommand("select * from Resimler where id=44", baglantim);
                SqlDataReader drbul = bul.ExecuteReader();
                
                drbul.Read();
                byte[] resim = (byte[])drbul["RESIM"];
                drbul.Close();

                emote.Parameters.AddWithValue("@Emote", resim);
                emote.ExecuteNonQuery();

                notifyIcon1.ShowBalloonTip(3000, "Başarılı.", "Yoruma ifade bıraktınız!.", ToolTipIcon.Info);

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
                dataGridView1.ClearSelection();

                baglantim.Close();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Seçim Yapınız.", "Lütfen tepki vereceğiniz yorumu seçiniz.", ToolTipIcon.Warning);
            }
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        // Yorumu Sil
        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                DialogResult dialog = MessageBox.Show("Seçtğiniz yorum kalıcı olarak silinecektir. Devam etmek istediğinize emin misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    baglantim.Open();

                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    int cellValue = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    SqlCommand sil = new SqlCommand("delete from Yorumlar where ID=" + cellValue, baglantim);
                    sil.ExecuteNonQuery();

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
                    dataGridView1.ClearSelection();

                    notifyIcon1.ShowBalloonTip(3000, "Yorum Silindi.", "Seçtiğiniz yorum başarıyla silindi.", ToolTipIcon.Info);

                    baglantim.Close();
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "Yorum Seçiniz.", "Lütfen silmek istediğiniz yorumu seçiniz.", ToolTipIcon.Warning);
            }
        }

        // tüm yorumları sil
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                DialogResult dialog = MessageBox.Show("Tüm yorumlar silindiği zaman müşteriler de bu yorumlara kalıcı olarak erişemeyecektir. Devam etmek istediğinize min misiniz?", "Onay Bekleniyor", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    DialogResult dialog2 = MessageBox.Show("Gerçekten tüm yorumları silmek istediğinize emin misiniz?", "Onay Tekrarı", MessageBoxButtons.YesNo);

                    if (dialog2 == DialogResult.Yes)
                    {
                        baglantim.Open();

                        SqlCommand sil = new SqlCommand("delete from Yorumlar", baglantim);
                        sil.ExecuteNonQuery();

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

                        //this.dataGridView1.Sort(this.dataGridView1.Columns["Tarih"], ListSortDirection.Descending);

                        notifyIcon1.ShowBalloonTip(3000, "Yorumlar Silindi.", "Yorumların hepsi başarıyla silindi.", ToolTipIcon.Info);
                    }
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(3000, "İşlem Geçersiz.", "Yorum geçmişi zaten boş gözüküyor", ToolTipIcon.Error);
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
