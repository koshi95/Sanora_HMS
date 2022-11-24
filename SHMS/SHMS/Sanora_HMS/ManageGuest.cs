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
    public partial class ManageGuest : Form
    {
        SqlConnection sqlcon;
        public ManageGuest()
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
                string sql = "select * from Guest_TB";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                GuestGV.DataSource = ds.Tables[0];
               
            }
            catch (Exception )
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                FillRoomcmb();
                GuestFill();
            }

        }
        void GuestFill()//search drop down filler
        {

            string sql = "select * from Guest_TB";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Passport_NIC", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbPassport.ValueMember = "Passport_NIC";
                cmbPassport.DataSource = dt;
                sqlcon.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        void FillRoomcmb()
        {
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("select RoomNo from Guest_TB ", sqlcon);
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("RoomNo", typeof(string));
                dt.Load(rd);
                cmbroomno.ValueMember = "RoomNo";
                cmbroomno.DataSource = dt;


            }
            catch (Exception)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
            }
        }

        //void FillRoomTypecmb()
        //{
        //    try
        //    {
        //        sqlcon.Open();
        //        SqlCommand cmd = new SqlCommand("select Room_Type from RoomTB ", sqlcon);
        //        SqlDataReader rd;
        //        rd = cmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Room_Type ", typeof(string));
        //        dt.Load(rd);
        //        roomtype.ValueMember = "Room_Type ";
        //        roomtype.DataSource = dt;
        //        sqlcon.Close();


        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }
        //}

        void Guestfilter()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from Guest_TB where Passport_NIC='" + cmbPassport.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                GuestGV.DataSource = ds.Tables[0];
                
            }
            catch (Exception )
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
            }

        }
        

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            /*bool valid = true;


            if (String.IsNullOrEmpty(PassportNIC.Text) || String.IsNullOrEmpty(fname.Text) || String.IsNullOrEmpty(lname.Text) || String.IsNullOrEmpty(address.Text) || String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(tp.Text) ||  String.IsNullOrEmpty(noofmembers.Text) || String.IsNullOrEmpty(cmbroomtype.Text) || String.IsNullOrEmpty(cmbgender.Text) || String.IsNullOrEmpty(cmbroomno.Text))
            {
                MessageBox.Show("Need to fill all the values", "Manage Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            try
            {
                sqlcon.Open();
                if (valid)
                {


                    DialogResult confirm = MessageBox.Show("Are You Sure to Add the Guest", "Manage Guest", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        //var vv = cmbroomno;
                         SqlCommand cmd = new SqlCommand("insert into Guest_TB values ('" + PassportNIC.Text + "','" + fname.Text + 
                            "','" + lname.Text + "','" + address.Text + "' ,'" + email.Text + "','" + tp.Text + "','" + noofmembers.Text +
                            "', '" + cmbgender.SelectedItem.ToString() + "', '" + cmbroomno.SelectedValue.ToString() + 
                            "', '" + cmbroomtype.SelectedItem.ToString() + "')", sqlcon);

                        cmd.Connection = sqlcon;

                        int NoRows = cmd.ExecuteNonQuery();

                        if (NoRows > 0)
                        {
                            MessageBox.Show("Managr_Guest Added", "Manage Guest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Record Fail to Add", "Add Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
                populate();
            }*/

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand("insert into Guest_TB(Passport_NIC, Fname," +
                            "Lname, Address, Email, TP, NoOfMembers," +
                            "Gender, RoomNo," +
                            "RoomType) Values (@Passport_NIC, @Fname," +
                            "@Lname, @Address, @Email, @TP, @NoOfMembers," +
                            "@Gender, @RoomNo," +
                            "@RoomType) ", sqlcon);

            cmd.Parameters.AddWithValue("Passport_NIC", PassportNIC.Text);
            cmd.Parameters.AddWithValue("Fname", fname.Text);
            cmd.Parameters.AddWithValue("Lname", lname.Text);
            cmd.Parameters.AddWithValue("Address", address.Text);
            cmd.Parameters.AddWithValue("Email", email.Text);
            cmd.Parameters.AddWithValue("TP", tp.Text);
            cmd.Parameters.AddWithValue("NoOfMembers", noofmembers.Text);
            cmd.Parameters.AddWithValue("Gender", cmbgender.Text);
            cmd.Parameters.AddWithValue("RoomNo", cmbroomno.Text);
            cmd.Parameters.AddWithValue("RoomType", cmbroomtype.Text);
            cmd.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Submitted sucessfully");
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (String.IsNullOrEmpty(PassportNIC.Text) || String.IsNullOrEmpty(fname.Text) || 
                String.IsNullOrEmpty(lname.Text) || String.IsNullOrEmpty(address.Text) ||
                String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(tp.Text) || 
                String.IsNullOrEmpty(noofmembers.Text) || String.IsNullOrEmpty(cmbgender.Text) 
                || String.IsNullOrEmpty(cmbroomno.Text) || String.IsNullOrEmpty(cmbroomtype.Text))
            {
                MessageBox.Show("Need to fill all the values", "Manage Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (valid)
            {

                DialogResult confirm = MessageBox.Show("Are You Sure to Update the Guest", "Manage_Guest", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        sqlcon.Open();
                       // string q = "Update Guest_TB set  Fname='" + fname.Text + "',Lname='" + lname.Text + "',Address='" + address.Text + "',Email='" + email.Text + "',TP ='" + tp.Text + "',NoOfMembers= '" + noofmembers.Text + "', Gender = '" + cmbgender.SelectedItem.ToString() + "',Room No = '" + cmbroomno.SelectedValue.ToString() + "',Room_Type = '" + cmbroomtype.SelectedItem.ToString() + "' where Passport_NIC= '" + PassportNIC.Text + "'";
                        SqlCommand cmd = new SqlCommand("Update Guest_TB set  Fname = '" + fname.Text + "', Lname = '" + lname.Text + "', Address = '" + address.Text + "', Email = '" + email.Text + "', TP = '" + tp.Text + "', NoOfMembers = '" + noofmembers.Text + "', Gender = '" + cmbgender.SelectedItem.ToString() + "', RoomNo = '" + cmbroomno.SelectedValue.ToString() + "', RoomType = '" + cmbroomtype.SelectedItem.ToString() + "' where Passport_NIC = '" + PassportNIC.Text + "'", sqlcon);

                        /*SqlCommand cmd = new SqlCommand("Update UserTB set  Fname='" + fname.Text + "',Lname='" + lname.Text + "'," +"Address='" + address.Text + "',Email='" + email.Text + "',TP ='" + tp.Text + "',Password= '" + password.Text + "', " +"Gender = '" + cmbgender.SelectedText + "' where StaffID= '" + staffid.Text + "'", sqlcon);*/

                        int numberOfRecords = cmd.ExecuteNonQuery();
                        if (numberOfRecords > 0)
                        {
                            MessageBox.Show("Updated Succesfully", "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data is Not updated" + ex, "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlcon.Close();
                        populate();
                    }
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Guestfilter();
          
            //try
            //{
            //    sqlcon.Open();
            //    SqlCommand cmd = new SqlCommand("select * from Guest_TB where Passport_NIC='" + PassportNIC.Text + "'", sqlcon);
            //    SqlDataReader dr = cmd.ExecuteReader();

            //    if (dr.Read())
            //    {

            //        MessageBox.Show("Guest Details found!", "Manage Guest", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //    else
            //    {
            //        //MessageBox.Show("Data Not Found", "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    dr.Close();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error" + ex, "Manage_Guest", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    sqlcon.Close();
            //}
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

            populate();
        }

        private void ManageGuest_Load(object sender, EventArgs e)
        {
            populate();
            FillRoomcmb();
            Datelbl.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbPassport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GuestGV_Click(object sender, EventArgs e)
        {
            PassportNIC.Text= GuestGV.SelectedRows[0].Cells[0].Value.ToString();
            fname.Text = GuestGV.SelectedRows[0].Cells[1].Value.ToString();
            lname.Text = GuestGV.SelectedRows[0].Cells[2].Value.ToString();
            address.Text = GuestGV.SelectedRows[0].Cells[3].Value.ToString();
            email.Text = GuestGV.SelectedRows[0].Cells[4].Value.ToString();
            tp.Text = GuestGV.SelectedRows[0].Cells[5].Value.ToString();
            noofmembers.Text = GuestGV.SelectedRows[0].Cells[6].Value.ToString();
            cmbgender.SelectedIndex = cmbgender.FindString(GuestGV.SelectedRows[0].Cells[7].Value.ToString());
            cmbroomno.SelectedIndex = cmbroomno.FindString(GuestGV.SelectedRows[0].Cells[8].Value.ToString());
            cmbroomtype.SelectedIndex = cmbroomtype.FindString(GuestGV.SelectedRows[0].Cells[9].Value.ToString());
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Main_Menu menu = new Main_Menu();
            menu.Show();
            this.Hide();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            PassportNIC.Text = "";
            fname.Text = "";
            lname.Text = "";
            address.Text = "";
            email.Text = "";
            tp.Text = "";
            noofmembers.Text = "";
            //cmbgender.Text = "";
            //cmbroomno.Text = "";
            //cmbroomtype.Text = "";
        }

        private void cmbroomno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
