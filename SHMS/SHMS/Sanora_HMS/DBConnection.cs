using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sanora_HMS
{
    class DBConnection
    {
        SqlConnection con;
        public SqlConnection getSQLConnection()
        {
            try
            {
                con = new SqlConnection(@"Data Source=pencil;Initial Catalog=Sanoradb;Integrated Security=True");
                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return con;
        }
    }
}