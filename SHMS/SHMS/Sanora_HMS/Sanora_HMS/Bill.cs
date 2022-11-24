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

namespace Sanora_HMS
{
    public partial class Bill : Form
    {
        SqlConnection sqlcon;
        public Bill()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlcon = obj.getSQLConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting" + ex, "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            populate();
        }

        void populate()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from RoomTB WHERE Room_Type='Luxary'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();

                da.Fill(ds);

                string ss = "";

                CheckBox box;
                for (int i = 0; i < 10; i++)
                {
                    box = new CheckBox();
                    box.Tag = i.ToString();
                    box.Text = "a";
                    box.AutoSize = true;
                    box.Location = new Point(10, i * 50); //vertical
                                                          //box.Location = new Point(i * 50, 10); //horizontal
                    this.Controls.Add(box);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ss = dr["Room_No"].ToString();

                    box = new CheckBox();
                    box.Text = ss;


                    //MessageBox.Show(dr["Room_No"].ToString());
                }
                //MessageBox.Show(ss);




                //DataTable t = new DataTable();
                //da.Fill(t);
                //RoomGV.DataSource = ds.Tables[0];

                //t.Rows[0].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                //Fillsearchcmb();
            }

        }
    }
}
