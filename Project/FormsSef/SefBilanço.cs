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

namespace RestaurantAtlantis
{
    public partial class SefBilanço : Form
    {
        public SefBilanço()
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

        private void Yorumlar_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            SefYorumlar gitSefYorumlar = new SefYorumlar();
            gitSefYorumlar.Show();
            this.Hide();
        }

        readonly SqlConnection baglantim = new SqlConnection(@"Data Source = (local); Initial Catalog = Restaurant; Integrated Security = True");
        
        string tarih;
        string ay;
        double siparisler = 0;
        double stok = 0;
        double maaslar = 0;

        private void SefBilanço_Load(object sender, EventArgs e)
        {
            int baslangic = 2022;
            comboBox2.Items.Add(baslangic);
            int yilekle = Convert.ToInt32(DateTime.Now.Year) - 2022;

            for(int i = yilekle; i > 0; i--)
            {
                comboBox2.Items.Add(baslangic += 1);
            }

            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;

            baglantim.Open();

            SqlDataAdapter maas = new SqlDataAdapter("select * from Employee", baglantim);
            DataTable table2 = new DataTable();
            maas.Fill(table2);

            foreach (DataRow dr in table2.Rows)
            {
                if (!DBNull.Value.Equals(dr["Maas"]))
                {
                    maaslar += Convert.ToDouble(dr["Maas"]);
                }
            }

            switch (DateTime.Now.Month.ToString())
            {
                case "1":
                    tarih = "Oca";
                    ay = "Ocak";
                    break;
                case "2":
                    tarih = "Şub";
                    ay = "Şubat";
                    break;
                case "3":
                    tarih = "Mar";
                    ay = "Mart";
                    break;
                case "4":
                    tarih = "Nis";
                    ay = "Nisan";
                    break;
                case "5":
                    tarih = "May";
                    ay = "Mayıs";
                    break;
                case "6":
                    tarih = "Haz";
                    ay = "Haziran";
                    break;
                case "7":
                    tarih = "Tem";
                    ay = "Temmuz";
                    break;
                case "8":
                    tarih = "Ağu";
                    ay = "Ağustos";
                    break;
                case "9":
                    tarih = "Eyl";
                    ay = "Eylül";
                    break;
                case "10":
                    tarih = "Eki";
                    ay = "Ekim";
                    break;
                case "11":
                    tarih = "Kas";
                    ay = "Kasım";
                    break;
                case "12":
                    tarih = "Ara";
                    ay = "Aralık";
                    break;
                default:
                    break;
            }

            comboBox2.Text = DateTime.Now.Year.ToString();
            comboBox1.Text = ay;

            // Siparisler

            SqlDataAdapter topla = new SqlDataAdapter("select * from SiparisGecmisleri", baglantim);
            DataTable table = new DataTable();
            topla.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                string year = Convert.ToString(dr["Tarih"]);
                year = year.Remove(0, 18);

                string year2 = Convert.ToString(dr["Tarih"]);
                year2 = year2.Remove(0, 17);

                if (year == DateTime.Now.Year.ToString() || year2 == DateTime.Now.Year.ToString())
                {
                    string date = Convert.ToString(dr["Tarih"]);
                    date = date.Remove(date.Length - 8);
                    date = date.Remove(0, 11);

                    string date2 = Convert.ToString(dr["Tarih"]);
                    date2 = date2.Remove(date2.Length - 7);
                    date2 = date2.Remove(0, 11);

                    if (date == tarih || date2 == tarih)
                    {
                        siparisler += Convert.ToDouble(dr["Tutar"]);
                    }
                }
            }

            // Stok

            SqlDataAdapter topla2 = new SqlDataAdapter("select * from StokGecmisi", baglantim);
            DataTable table3 = new DataTable();
            topla2.Fill(table3);

            foreach (DataRow dr in table3.Rows)
            {
                string year = Convert.ToString(dr["TARIH"]);
                year = year.Remove(0, 18);

                string year2 = Convert.ToString(dr["TARIH"]);
                year2 = year2.Remove(0, 17);

                if (year == DateTime.Now.Year.ToString() || year2 == DateTime.Now.Year.ToString())
                {
                    string date = Convert.ToString(dr["TARIH"]);
                    date = date.Remove(date.Length - 8);
                    date = date.Remove(0, 11);

                    string date2 = Convert.ToString(dr["TARIH"]);
                    date2 = date2.Remove(date2.Length - 7);
                    date2 = date2.Remove(0, 11);

                    if (date == tarih || date2 == tarih)
                    {
                        string stoklar = (string)(dr["FIYAT"]);
                        stoklar = stoklar.Remove(stoklar.Length - 3);
                        stok += Convert.ToDouble(stoklar);
                    }
                }
            }

            if (baglantim.State == ConnectionState.Closed)
            {
                baglantim.Open();

            }

            SqlCommand restoran = new SqlCommand("select * from Restoran where id=1", baglantim);
            SqlDataReader drrestoran = restoran.ExecuteReader();
            drrestoran.Read();
            stok += Convert.ToDouble(drrestoran["KIRA"]);
            stok += Convert.ToDouble(drrestoran["SU"]);
            stok += Convert.ToDouble(drrestoran["ELEKTRIK"]);
            stok += Convert.ToDouble(drrestoran["DOGALGAZ"]);
            drrestoran.Close();

            SqlCommand kontrol = new SqlCommand(@"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
                           WHERE TABLE_NAME=@name) SELECT 1 ELSE SELECT 0", baglantim);

            kontrol.Parameters.Add("@name", SqlDbType.NVarChar).Value = "Bilanco" + DateTime.Now.Year.ToString();

            if (Convert.ToInt32(kontrol.ExecuteScalar()) == 1)
            {
                SqlCommand guncelle = new SqlCommand("update Bilanco" + DateTime.Now.Year.ToString()
                    + " set GELIR=@GELIR,GIDER=@GIDER,GIDER2=@GIDER2,TOPLAM=@TOPLAM where AY='" + ay + "'", baglantim);
                guncelle.Parameters.AddWithValue("@GELIR", Convert.ToString(siparisler));
                guncelle.Parameters.AddWithValue("@GIDER", Convert.ToString(stok));
                guncelle.Parameters.AddWithValue("@GIDER2", Convert.ToString(maaslar));
                guncelle.Parameters.AddWithValue("@TOPLAM", Convert.ToString(siparisler - (stok + maaslar)));
                guncelle.ExecuteNonQuery();

                chart2.Series[0].Points.Clear();
                chart2.Series["Oran"].Points.AddXY("Siparişler", siparisler);
                chart2.Series["Oran"].Points.AddXY("Restoran", stok);
                chart2.Series["Oran"].Points.AddXY("Personel", maaslar);

                chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
                chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
                chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

                label18.Text = comboBox1.Text + " Grafiği";
            }
            else
            {
                SqlCommand ekle = new SqlCommand("CREATE TABLE [dbo].[Bilanco" + DateTime.Now.Year.ToString() + "]("
                            + "[ID] [int] IDENTITY(1,1) NOT NULL,"
                            + "[AY] [varchar](5) NULL,"
                            + "[GELIR] [varchar](100) NULL,"
                            + "[GIDER] [varchar](100) NULL,"
                            + "[GIDER2] [varchar](100) NULL,"
                            + "[TOPLAM] [varchar](100) NULL,"
                            + "CONSTRAINT ['ID'] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[ID] ASC"
                            + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]", baglantim);
                ekle.ExecuteNonQuery();

                SqlCommand aylar1 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY1)", baglantim);
                aylar1.Parameters.AddWithValue("@AY1", "Ocak");
                aylar1.ExecuteNonQuery();

                SqlCommand aylar2 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY2)", baglantim);
                aylar2.Parameters.AddWithValue("@AY2", "Şubat");
                aylar2.ExecuteNonQuery();

                SqlCommand aylar3 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY3)", baglantim);
                aylar3.Parameters.AddWithValue("@AY3", "Mart");
                aylar3.ExecuteNonQuery();

                SqlCommand aylar4 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY4)", baglantim);
                aylar4.Parameters.AddWithValue("@AY4", "Nisan");
                aylar4.ExecuteNonQuery();

                SqlCommand aylar5 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY5)", baglantim);
                aylar5.Parameters.AddWithValue("@AY5", "Mayıs");
                aylar5.ExecuteNonQuery();

                SqlCommand aylar6 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY6)", baglantim);
                aylar6.Parameters.AddWithValue("@AY6", "Haziran");
                aylar6.ExecuteNonQuery();

                SqlCommand aylar7 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY7)", baglantim);
                aylar7.Parameters.AddWithValue("@AY7", "Temmuz");
                aylar7.ExecuteNonQuery();

                SqlCommand aylar8 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY8)", baglantim);
                aylar8.Parameters.AddWithValue("@AY8", "Ağustos");
                aylar8.ExecuteNonQuery();

                SqlCommand aylar9 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY9)", baglantim);
                aylar9.Parameters.AddWithValue("@AY9", "Eylül");
                aylar9.ExecuteNonQuery();

                SqlCommand aylar10 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY10)", baglantim);
                aylar10.Parameters.AddWithValue("@AY10", "Ekim");
                aylar10.ExecuteNonQuery();

                SqlCommand aylar11 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY11)", baglantim);
                aylar11.Parameters.AddWithValue("@AY11", "Kasım");
                aylar11.ExecuteNonQuery();

                SqlCommand aylar12 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY12)", baglantim);
                aylar12.Parameters.AddWithValue("@AY12", "Aralık");
                aylar12.ExecuteNonQuery();

                SqlCommand guncelle = new SqlCommand("update Bilanco" + DateTime.Now.Year.ToString()
                    + " set GELIR=@GELIR,GIDER=@GIDER,GIDER2=@GIDER2,TOPLAM=@TOPLAM where AY='" + ay + "'", baglantim);
                guncelle.Parameters.AddWithValue("@GELIR", Convert.ToString(siparisler));
                guncelle.Parameters.AddWithValue("@GIDER", Convert.ToString(stok));
                guncelle.Parameters.AddWithValue("@GIDER2", Convert.ToString(maaslar));
                guncelle.Parameters.AddWithValue("@TOPLAM", Convert.ToString(siparisler - (stok + maaslar)));
                guncelle.ExecuteNonQuery();
            }

            label14.Text = Convert.ToString(siparisler) + " ₺";
            label13.Text = Convert.ToString(stok) + " ₺";
            label11.Text = Convert.ToString(maaslar) + " ₺";

            if (siparisler < stok + maaslar)
            {
                label9.Visible = false;
                label10.Visible = false;

                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label17.Text = Convert.ToString(siparisler - (stok + maaslar)) + " ₺";
            }
            else
            {
                label9.Visible = true;
                label10.Visible = true;

                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label10.Text = Convert.ToString(siparisler - (stok + maaslar)) + " ₺";
            }

            baglantim.Close();
        }

        // Aylar klavye engeli
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Yıllar klavye engeli
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // Ay değiştirme
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (baglantim.State == ConnectionState.Closed)
            {
                baglantim.Open();
            }

            if (comboBox1.Text == "(Tüm Yıl)")
            {
                double yilboyugelir = 0;
                double yilboyuggider = 0;
                double yilboyugider2 = 0;
                double yilboyutoplam = 0;

                SqlDataAdapter hepsi = new SqlDataAdapter("select * from Bilanco" + comboBox2.Text, baglantim);
                DataTable table = new DataTable();
                hepsi.Fill(table);

                foreach (DataRow dr in table.Rows)
                {
                    if (!DBNull.Value.Equals(dr["GELIR"]) && !DBNull.Value.Equals(dr["GIDER"]) && !DBNull.Value.Equals(dr["GIDER2"]) && !DBNull.Value.Equals(dr["TOPLAM"]))
                    {
                        yilboyugelir += Convert.ToDouble((string)dr["GELIR"]);
                        yilboyuggider += Convert.ToDouble((string)dr["GIDER"]);
                        yilboyugider2 += Convert.ToDouble((string)dr["GIDER2"]);
                        yilboyutoplam += Convert.ToDouble((string)dr["TOPLAM"]);
                    }
                }

                label14.Text = yilboyugelir + " ₺";
                label13.Text = yilboyuggider + " ₺";
                label11.Text = yilboyugider2 + " ₺";

                if (yilboyutoplam > 0)
                {
                    label9.Visible = true;
                    label10.Visible = true;
                    label15.Visible = false;
                    label16.Visible = false;
                    label17.Visible = false;
                    label10.Text = yilboyutoplam + " ₺";
                }
                if(yilboyutoplam < 0)
                {
                    label9.Visible = false;
                    label10.Visible = false;
                    label15.Visible = true;
                    label16.Visible = true;
                    label17.Visible = true;
                    label17.Text = yilboyutoplam + " ₺";
                }

                chart2.Series[0].Points.Clear();
                chart2.Series["Oran"].Points.AddXY("Siparişler", yilboyugelir);
                chart2.Series["Oran"].Points.AddXY("Restoran", yilboyuggider);
                chart2.Series["Oran"].Points.AddXY("Personel", yilboyugider2);

                chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
                chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
                chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

                label18.Text = comboBox1.Text + " Grafiği";
            }
            else
            {
                SqlCommand kontrol = new SqlCommand(@"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
                           WHERE TABLE_NAME=@name) SELECT 1 ELSE SELECT 0", baglantim);

                kontrol.Parameters.Add("@name", SqlDbType.NVarChar).Value = "Bilanco" + DateTime.Now.Year.ToString();

                if (Convert.ToInt32(kontrol.ExecuteScalar()) == 1)
                {
                    SqlCommand oku = new SqlCommand("select * from Bilanco" + comboBox2.Text + " where AY='" + comboBox1.Text + "'", baglantim);
                    SqlDataReader droku = oku.ExecuteReader();
                    droku.Read();

                    if (!DBNull.Value.Equals(droku["GELIR"]) && !DBNull.Value.Equals(droku["GIDER"]) && !DBNull.Value.Equals(droku["GIDER2"]))
                    {
                        label14.Text = (string)droku["GELIR"] + " ₺";
                        label13.Text = (string)droku["GIDER"] + " ₺";
                        label11.Text = (string)droku["GIDER2"] + " ₺";

                        double siparisler = Convert.ToDouble((string)droku["GELIR"]);
                        double stok = Convert.ToDouble((string)droku["GIDER"]);
                        double maaslar = Convert.ToDouble((string)droku["GIDER2"]);

                        if (siparisler < stok + maaslar)
                        {
                            label9.Visible = false;
                            label10.Visible = false;

                            label15.Visible = true;
                            label16.Visible = true;
                            label17.Visible = true;
                            label17.Text = Convert.ToString(siparisler - (stok + maaslar)) + " ₺";

                            chart2.Series[0].Points.Clear();
                            chart2.Series["Oran"].Points.AddXY("Siparişler", siparisler);
                            chart2.Series["Oran"].Points.AddXY("Restoran", stok);
                            chart2.Series["Oran"].Points.AddXY("Personel", maaslar);

                            chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
                            chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
                            chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

                            label18.Text = comboBox1.Text + " Grafiği";
                        }
                        else
                        {
                            label9.Visible = true;
                            label10.Visible = true;

                            label15.Visible = false;
                            label16.Visible = false;
                            label17.Visible = false;
                            label10.Text = Convert.ToString(siparisler - (stok + maaslar)) + " ₺";

                            chart2.Series[0].Points.Clear();
                            chart2.Series["Oran"].Points.AddXY("Siparişler", siparisler);
                            chart2.Series["Oran"].Points.AddXY("Restoran", stok);
                            chart2.Series["Oran"].Points.AddXY("Personel", maaslar);

                            chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
                            chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
                            chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

                            label18.Text = comboBox1.Text + " Grafiği";
                        }
                    }
                    else
                    {
                        label9.Visible = true;
                        label10.Visible = true;
                        label15.Visible = false;
                        label16.Visible = false;
                        label17.Visible = false;

                        label14.Text = "(-)";
                        label13.Text = "(-)";
                        label11.Text = "(-)";
                        label10.Text = "(belli değil)";

                        chart2.Series[0].Points.Clear();
                        chart2.Series["Oran"].Points.AddXY("Siparişler", 1);
                        chart2.Series["Oran"].Points.AddXY("Restoran", 1);
                        chart2.Series["Oran"].Points.AddXY("Personel", 1);

                        chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
                        chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
                        chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

                        label18.Text = "Değer bulunamadı";
                    }
                    droku.Close();
                }
                else
                {
                    SqlCommand ekle = new SqlCommand("CREATE TABLE [dbo].[Bilanco" + DateTime.Now.Year.ToString() + "]("
                                + "[ID] [int] IDENTITY(1,1) NOT NULL,"
                                + "[AY] [varchar](10) NULL,"
                                + "[GELIR] [varchar](100) NULL,"
                                + "[GIDER] [varchar](100) NULL,"
                                + "[GIDER2] [varchar](100) NULL,"
                                + "[TOPLAM] [varchar](100) NULL,"
                                + "CONSTRAINT ['ID'] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", baglantim);
                    ekle.ExecuteNonQuery();

                    SqlCommand aylar1 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY1)", baglantim);
                    aylar1.Parameters.AddWithValue("@AY1", "Ocak");
                    aylar1.ExecuteNonQuery();

                    SqlCommand aylar2 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY2)", baglantim);
                    aylar2.Parameters.AddWithValue("@AY2", "Şubat");
                    aylar2.ExecuteNonQuery();

                    SqlCommand aylar3 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY3)", baglantim);
                    aylar3.Parameters.AddWithValue("@AY3", "Mart");
                    aylar3.ExecuteNonQuery();

                    SqlCommand aylar4 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY4)", baglantim);
                    aylar4.Parameters.AddWithValue("@AY4", "Nisan");
                    aylar4.ExecuteNonQuery();

                    SqlCommand aylar5 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY5)", baglantim);
                    aylar5.Parameters.AddWithValue("@AY5", "Mayıs");
                    aylar5.ExecuteNonQuery();

                    SqlCommand aylar6 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY6)", baglantim);
                    aylar6.Parameters.AddWithValue("@AY6", "Haziran");
                    aylar6.ExecuteNonQuery();

                    SqlCommand aylar7 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY7)", baglantim);
                    aylar7.Parameters.AddWithValue("@AY7", "Temmuz");
                    aylar7.ExecuteNonQuery();

                    SqlCommand aylar8 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY8)", baglantim);
                    aylar8.Parameters.AddWithValue("@AY8", "Ağustos");
                    aylar8.ExecuteNonQuery();

                    SqlCommand aylar9 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY9)", baglantim);
                    aylar9.Parameters.AddWithValue("@AY9", "Eylül");
                    aylar9.ExecuteNonQuery();

                    SqlCommand aylar10 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY10)", baglantim);
                    aylar10.Parameters.AddWithValue("@AY10", "Ekim");
                    aylar10.ExecuteNonQuery();

                    SqlCommand aylar11 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY11)", baglantim);
                    aylar11.Parameters.AddWithValue("@AY11", "Kasım");
                    aylar11.ExecuteNonQuery();

                    SqlCommand aylar12 = new SqlCommand("insert into Bilanco" + DateTime.Now.Year.ToString() + "(AY) values(@AY12)", baglantim);
                    aylar12.Parameters.AddWithValue("@AY12", "Aralık");
                    aylar12.ExecuteNonQuery();

                    SqlCommand guncelle = new SqlCommand("update Bilanco" + DateTime.Now.Year.ToString()
                        + " set GELIR=@GELIR,GIDER=@GIDER,GIDER2=@GIDER2,TOPLAM=@TOPLAM where AY='" + ay + "'", baglantim);
                    guncelle.Parameters.AddWithValue("@GELIR", Convert.ToString(siparisler));
                    guncelle.Parameters.AddWithValue("@GIDER", Convert.ToString(stok));
                    guncelle.Parameters.AddWithValue("@GIDER2", Convert.ToString(maaslar));
                    guncelle.Parameters.AddWithValue("@TOPLAM", Convert.ToString(siparisler - (stok + maaslar)));
                    guncelle.ExecuteNonQuery();
                }
            }
            baglantim.Close();
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Müşteri siparişlerinin geliri", pictureBox4);
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Kira,Fatura ve Stok siparişlerinin gideri ", pictureBox5);
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Personel maaşlarının gideri", pictureBox6);
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

        // Yıl değişimi
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";

            label9.Visible = true;
            label10.Visible = true;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;

            label14.Text = "(-)";
            label13.Text = "(-)";
            label11.Text = "(-)";
            label10.Text = "(Ay Seçiniz)";

            chart2.Series[0].Points.Clear();
            chart2.Series["Oran"].Points.AddXY("Siparişler", 1);
            chart2.Series["Oran"].Points.AddXY("Restoran", 1);
            chart2.Series["Oran"].Points.AddXY("Personel", 1);

            chart2.Series["Oran"].Points[0].Color = Color.DarkSeaGreen;
            chart2.Series["Oran"].Points[1].Color = Color.LightCoral;
            chart2.Series["Oran"].Points[2].Color = Color.IndianRed;

            label18.Text = "Ay Seçimi Yapınız";
        }
    }
}
