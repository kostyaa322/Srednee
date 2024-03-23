using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Srednee
{
    public class ReportM
    {
        private string connectionString = (@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");

        public DataTable ExecuteQuery(string sqlQuery)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    using (SqlDataAdapter dp = new SqlDataAdapter(cmd))
                    {
                        dp.Fill(dt);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return dt;
                
               



        }
        
         
    }
}
