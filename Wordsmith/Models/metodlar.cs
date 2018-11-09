using galleryArea.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Wordsmith.Models;

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
        public OperationResult<string> ExecuteReader(string sql, params SqlParameter[] sqlParameters)
                                    result.Message = Convert.ToString(reader["Message"]);
                                    result.Data = Convert.ToString(reader["Data"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccesful = false;
                result.Message = sql + "MESSAGE = " + ex.Message + "STACK TRACE = " + ex.StackTrace;
                result.Data = "-2";
            }
            return result;
        }
        public List<tPosts> getList()
        {
            SqlConnection baglanti = this.baglan();
            SqlCommand cmd = new SqlCommand("select * from tPosts ", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            List<tPosts> ts = new List<tPosts>();
            var a = "";
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //tPosts t = new tPosts();

                    a += "\n" + dr[0].ToString();
                    tPosts t = new tPosts { PostTitle = dr["PostTitle"].ToString(), PostText = dr["PostText"].ToString(), PostImage = dr["PostImage"].ToString(), IDUser = (int)dr["IDUser"], IDPost = (int)dr["IDPost"], IDCategory = (int)dr["IDCategory"], Slider = (int)dr["Slider"], PostDate = Convert.ToDateTime(dr["PostDate"]), PostRead = (int)dr["PostRead"] };
                    t.tTags = new List<tTags>();
                    t.TCategorys = new List<tCategorys>();
                    t.tUsers = new tUsers();
                    SqlCommand cmd2 = new SqlCommand("select * from tTags where IDPost=" + (int)dr["IDPost"], baglan());
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {

                        while (dr2.Read())
                        {
                            t.tTags.Add(new tTags { IDPost = t.IDPost, IDTag = (int)dr2["IDTag"], Tag = dr2["Tag"].ToString() });
                        }
                    }

                    cmd2.Dispose();
                    SqlCommand cmd3 = new SqlCommand("select * from tCategorys where IDCategory=" + t.IDCategory, baglan());
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    if (dr3.HasRows)
                    {

                        while (dr3.Read())
                        {
                            t.TCategorys.Add(new tCategorys { CategoryName = dr3["CategoryName"].ToString(), IDCategory = (int)dr3["IDCategory"], IDParentCategory = (int)dr3["IDParentCategory"] });

                        }
                    }


                    SqlCommand cmd4 = new SqlCommand("select * from tUsers where  IDUser =" + (int)dr["IDUser"], baglan());
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.HasRows)
                    {

                        while (dr4.Read())
                        {
                            //IDUser, UserName, UsernSname, UserMail, UserPass, UserImage, IDRole
                            t.tUsers.IDUser = (int)dr4["IDUser"];
                            t.tUsers.UserName = dr4["UserName"].ToString();
                            t.tUsers.UsernSname = dr4["UsernSname"].ToString();
                            t.tUsers.UserMail = dr4["UserMail"].ToString();
                            t.tUsers.UserImage = dr4["UserImage"].ToString();
                            t.tUsers.IDRole = (int)dr4["IDRole"];
                        }
                    }
                    cmd3.Dispose();
                    dr3.Close();
                    dr2.Close();
                    ts.Add(t);
                }
            }

            cmd.Dispose();
            dr.Close();
            baglanti.Close();
            baglanti.Dispose();
            return ts;
        }

        public List<tCategorys> GetCategorys()
        {
            List<tCategorys> tc = new List<tCategorys>();
            using (SqlConnection con = baglan())
            {

                using (SqlCommand cmd = new SqlCommand("select * from tCategorys", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                tc.Add(new tCategorys { CategoryName = dr["CategoryName"].ToString(), IDCategory = (int)dr["IDCategory"], IDParentCategory = (int)dr["IDParentCategory"] });

                            }
                        }
                    }

                }
            }
            return tc;
        }
    }
}