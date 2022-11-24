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

namespace Sanora_HMS
{
    public partial class Room : Form
    {
        SqlConnection sqlcon;
        public Room()
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

        void populate()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from RoomTB";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                RoomGV.DataSource = ds.Tables[0];
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                Fillsearchcmb();
                
            }

        }

        void RoomFill()
        {

            string sql = "select *from UserTB";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Room_No", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbroomno.ValueMember = "Room_No";
                cmbroomno.DataSource = dt;
                sqlcon.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        void Fillsearchcmb()
        {

            string sql = "select * from RoomTB";

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Room_No", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbroomno.ValueMember = "Room_No";
                cmbroomno.DataSource = dt;

                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        void Roomfilter()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from RoomTB where Room_No='" + cmbroomno.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                RoomGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
            }

        }

        void AutoID()
        {
            try
            {
                sqlcon.Open();
                SqlCommand cd = new SqlCommand("select Room_No from RoomTB", sqlcon);
                SqlDataReader dr = cd.ExecuteReader();
                string id = "";
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr[0].ToString();
                    }
                    string idstring = id.Substring(2);
                    int CTR = Int32.Parse(idstring);

                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        roomno.Text = "RM00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        roomno.Text = "RM0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        roomno.Text = "RM" + CTR;
                    }
                }

                else
                {
                    roomno.Text = "RM001";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddRoomDetails(object sender, EventArgs e)
        {
         
            string facilities = "";

            if (freewifi.Checked)
                facilities += "free wifi,";

            if (minibar.Checked)
                facilities += "Mini Bar,";

            if (satallite.Checked)
                facilities += "Satalite TV,";

            if (beds.Checked)
                facilities += "Double and Single beds,";

            if (bathroom.Checked)
                facilities += "En-suiye bathroom,"; 

            if (twinbed.Checked)
                facilities += "Twin bed,";


            if (facilities[facilities.Length - 1].ToString() == ",")
            {
                facilities.Remove(facilities.Length - 1);
            }


                try
                {

                //sqlcon.Open();
                string availability;
                if (free.Checked == true)
                    availability = "Free";
                else
                    availability = "Booked";

                //var availability = "";

                if (free.Checked)
                    availability = free.Text;

                if (booked.Checked)
                    availability = booked.Text;
                //var hh = roomtype.SelectedItem;
                sqlcon.Open();
                string query = " insert into RoomTB values('" + roomno.Text + "' ,'" + tp.Text + "'," +
                                                "'" + facilities + "','" + roomtype.SelectedItem.ToString() + "','" +
                                                availability + "')";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.ExecuteNonQuery();


                MessageBox.Show("Room details Successfully added", "Room details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //sqlcon.Close();

            }
                catch
                {

                }
                finally
                {
                sqlcon.Close();
                populate();
                }
        }

        private void Room_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string facilities = "";

                if (freewifi.Checked)
                    facilities += "free wifi,";

                if (minibar.Checked)
                    facilities += "Mini Bar,";

                if (satallite.Checked)
                    facilities += "Satalite TV,";

                if (beds.Checked)
                    facilities += "Double and Single beds,";

                if (bathroom.Checked)
                    facilities += "En-suiye bathroom,";

                if (twinbed.Checked)
                    facilities += "Twin bed,";


                if (facilities[facilities.Length - 1].ToString() == ",")
                {
                    facilities.Remove(facilities.Length - 1);
                }


                string availability;
                if (free.Checked == true)
                    availability = "Free";
                else
                    availability = "Booked";

                sqlcon.Open();
                var rm = roomtype;
                string qry = "update RoomTB set T_P='" + tp.Text
                    + "', Availability ='" + availability + "', Facilities ='" + facilities + "',  Room_Type ='" + roomtype.SelectedItem.ToString()
                    + "' where Room_No='" + roomno.Text + "'";
                SqlCommand cmd = new SqlCommand(qry, sqlcon);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room details Successfully updated", "Room details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                


            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlcon.Close();
                populate();
            }
        }

        private void searchClick(object sender, EventArgs e)
        {
            Roomfilter();
            //populate();
        }

        private void Refresh(object sender, EventArgs e)
        {
            populate();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RoomGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RoomGV_Click(object sender, EventArgs e)
        {
            roomno.Text = RoomGV.SelectedRows[0].Cells[0].Value.ToString();
            tp.Text = RoomGV.SelectedRows[0].Cells[1].Value.ToString();
            string facilities = RoomGV.SelectedRows[0].Cells[2].Value.ToString();
            roomtype.SelectedIndex = roomtype.FindString(RoomGV.SelectedRows[0].Cells[3].Value.ToString());
            //availability.Text = RoomGV.SelectedRows[0].Cells[5].Value.ToString();
            string availability = RoomGV.SelectedRows[0].Cells[4].Value.ToString();

            //check boxes
            if (facilities.Contains("free wifi"))
                freewifi.Checked = true;
            else
                freewifi.Checked = false;

            if (facilities.Contains("Mini Bar"))
                minibar.Checked = true;
            else
                minibar.Checked = false;

            if (facilities.Contains("Satalite TV"))
                satallite.Checked = true;
            else
                satallite.Checked = false;

            if (facilities.Contains("Double and Single beds"))
                beds.Checked = true;
            else
                beds.Checked = false;

            if (facilities.Contains("En-suiye bathroom"))
                bathroom.Checked = true;
            else
                bathroom.Checked = false;

            if (facilities.Contains("Twin bed"))
                twinbed.Checked = true;
            else
                twinbed.Checked = false;


            //radio button
            if (availability.ToLower().Contains("free"))
                free.Checked = true;

            if (availability.ToLower().Contains("booked"))
                booked.Checked = true;

        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Main_Menu menu = new Main_Menu();
            menu.Show();
            this.Hide();
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            AutoID();
        }

        private void cmbroomno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            roomno.Text = "";
            tp.Text = "";
            string facilities = "";
            //roomtype.SelectedIndex = "";
            //availability.Text = RoomGV.SelectedRows[0].Cells[5].Value.ToString();
            string availability = "";
        }
    }
}
