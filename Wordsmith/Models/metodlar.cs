using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Wordsmith.Models
{
    public class metodlar
    {
        public metodlar()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection baglan()
        {
            string conString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

            SqlConnection baglanti = new SqlConnection(conString);
            baglanti.Open();
            SqlConnection.ClearPool(baglanti);
            SqlConnection.ClearAllPools();
            return (baglanti);
        }

        public int cmd(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand sorgu = new SqlCommand(sqlcumle, baglan);
            int sonuc = 0;

            try
            {
                sonuc = sorgu.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + " (" + sqlcumle + ")");
            }
            sorgu.Dispose();
            baglan.Close();
            baglan.Dispose();
            return (sonuc);
        }

        public DataTable GetDataTable(string sql)
        {
            SqlConnection baglanti = this.baglan();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, baglanti);
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + " (" + sql + ")");
            }
            adapter.Dispose();
            baglanti.Close();
            baglanti.Dispose();
            return dt;
        }
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                        pro.SetValue(objT, row[pro.Name]);
                }
                return objT;
            }).ToList();
        }
        public DataRow GetDataRow(string sql)
        {
            DataTable table = GetDataTable(sql);
            if (table.Rows.Count == 0) return null;
            return table.Rows[0];
        }

        public string GetDataCell(string sql)
        {
            DataTable table = GetDataTable(sql);
            if (table.Rows.Count == 0) return null;
            return table.Rows[0][0].ToString();
        }


    }
}