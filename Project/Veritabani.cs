using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantAtlantis
{
    internal class Veritabani
    {
        readonly SqlConnection baglantim = new SqlConnection(@"Data Source=(local);Initial Catalog=Restaurant;Integrated Security=True");

        public SqlDataAdapter adapter;
        readonly List<SqlParameter> Params = new List<SqlParameter>();
        public DataTable table;
        public int record;
        public string exception;

        public void Query(string name)
        {
            record = 0;
            exception = null;
            try
            {
                baglantim.Open();

                SqlCommand value = new SqlCommand(name, baglantim);
                Params.ForEach(param => value.Parameters.Add(param));
                Params.Clear();

                table = new DataTable();
                adapter = new SqlDataAdapter(value);
                record = adapter.Fill(table);
            }
            catch (Exception e)
            {
                // Kullanımına gerek kalmadı
                exception = "Sorun: " + e.Message;
            }
            finally
            {
                if(baglantim.State == ConnectionState.Open)
                {
                    baglantim.Close();
                }
            }
        }

        public void Param(string name,object value)
        {
            SqlParameter newparam = new SqlParameter(name,value);
            Params.Add(newparam);
        }
    }
}
