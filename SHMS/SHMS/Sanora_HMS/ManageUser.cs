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
    public partial class ManageUser : Form
    {
        SqlConnection sqlcon;
        public ManageUser()
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
                string sql = "select * from User_TB";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UserGV.DataSource = ds.Tables[0];
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                UserFill();
            }

        }
        void UserFill()
        {

            string sql = "select *from User_TB";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("StaffID", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbstaffid.ValueMember = "StaffID";
                cmbstaffid.DataSource = dt;
                sqlcon.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        void Fillsearchcmb()
        {
            string sql = "select *from User_TB where StaffID='" + cmbstaffid.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("StaffID", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbstaffid.ValueMember = "StaffID";
                cmbstaffid.DataSource = dt;

                sqlcon.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        void Userfilter()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from User_TB where StaffID='" + cmbstaffid.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UserGV.DataSource = ds.Tables[0];
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        void AutoID()
        {
            try
            {
                sqlcon.Open();
                SqlCommand cd = new SqlCommand("select StaffID from User_TB", sqlcon);
                SqlDataReader dr = cd.ExecuteReader();
                string id = "";
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr[0].ToString();
                    }
                    string idstring = id.Substring(1); //001
                    int CTR = Int32.Parse(idstring);

                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        staffid.Text = "S00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        staffid.Text = "S0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        staffid.Text = "S" + CTR;
                    }
                }

                else
                {
                    staffid.Text = "S001";
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
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            bool valid = true;


            if (String.IsNullOrEmpty(staffid.Text) || String.IsNullOrEmpty(fname.Text) || String.IsNullOrEmpty(lname.Text) || String.IsNullOrEmpty(address.Text) || String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(tp.Text) || String.IsNullOrEmpty(password.Text) || String.IsNullOrEmpty(cmbgender.Text) || String.IsNullOrEmpty(cmbUserType.Text))
            {
                MessageBox.Show("Need to fill all the values", "Manage User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            try
            {
                sqlcon.Open();
                if (valid)
                {

                    DialogResult confirm = MessageBox.Show("Are You Sure to Add the User", "Manage User", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into User_TB values ('" + staffid.Text + "','" + fname.Text + "','" + lname.Text + "','" + address.Text + "' ,'" + email.Text + "','" + tp.Text + "','" + password.Text + "','" + cmbgender.SelectedItem.ToString()+ "','" + cmbUserType.SelectedItem.ToString() + "')", sqlcon);
                        cmd.Connection = sqlcon;
                        int NoRows = cmd.ExecuteNonQuery();
                        

                        if (NoRows > 0)
                        {
                            MessageBox.Show("User Added Successfully !", "Manage User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user!", "Add Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
                populate();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (String.IsNullOrEmpty(staffid.Text) || String.IsNullOrEmpty(fname.Text) || String.IsNullOrEmpty(lname.Text) || String.IsNullOrEmpty(address.Text) || String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(tp.Text) || String.IsNullOrEmpty(password.Text) || String.IsNullOrEmpty(cmbgender.Text) || String.IsNullOrEmpty(cmbUserType.Text))
            {
                MessageBox.Show("Need to fill all the values", "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (valid)
            {

                DialogResult confirm = MessageBox.Show("Are You Sure to Update the User", "ManageUser", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        sqlcon.Open();
                        /*  SqlCommand cmd = new SqlCommand("update FlowerDB set FlwName='" + FlowerName.Text
                      + "', FlwQty ='" + Qty.Text + "', FlwPrice ='" + Price.Text + "', FlwDes ='"
                      + Description.Text + "', FlwCat ='" + Categorycmb.SelectedValue.ToString()
                      + "' where FlwId='" + FlowerID.Text + "'", con);
                          cmd.ExecuteNonQuery();*/

                        //var vv = cmbgender;

                        SqlCommand cmd = new SqlCommand("Update User_TB set  Fname='" + fname.Text + "',Lname='" + lname.Text + "'," +
                            "Address='" + address.Text + "',Email='" + email.Text + "',TP ='" + tp.Text + "',Password= '" + password.Text + "', " +
                            "Gender = '" + cmbgender.SelectedText + "',UserType = '" + cmbUserType.SelectedText + "' where StaffID= '" + staffid.Text + "'", sqlcon);
                       
                        int numberOfRecords = cmd.ExecuteNonQuery();
                        if (numberOfRecords > 0)
                        {
                            MessageBox.Show("Updated User details Succesfully", "Manage User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data is Not updated" + ex, "Manage User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlcon.Close();
                        populate();
                    }
                }
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
            Userfilter();
            
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("select * from User_TB where StaffID='" + staffid.Text + "'", sqlcon);
                SqlDataReader dr = cmd.ExecuteReader();

                MessageBox.Show("User Details found!", "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //if (dr.Read())
                //{

                //    MessageBox.Show("Data Not Found", "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                //    MessageBox.Show("Data Not Found", "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
               // dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "ManageUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void ManageUser_Load(object sender, EventArgs e)
        {
            populate();
            UserFill();
            Datelbl.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //staffid.Text = UserGV.SelectedRows[0].Cells[0].Value.ToString();
            //fname.Text = UserGV.SelectedRows[0].Cells[1].Value.ToString();
            //lname.Text = UserGV.SelectedRows[0].Cells[2].Value.ToString();
            //address.Text = UserGV.SelectedRows[0].Cells[3].Value.ToString();
            //email.Text = UserGV.SelectedRows[0].Cells[4].Value.ToString();
            //tp.Text = UserGV.SelectedRows[0].Cells[5].Value.ToString();
            //password.Text = UserGV.SelectedRows[0].Cells[6].Value.ToString();
            //cmbgender.SelectedIndex = cmbgender.FindString(UserGV.SelectedRows[0].Cells[7].Value.ToString());
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void cmbstaffid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void UserGV_Click(object sender, EventArgs e)
        {
            staffid.Text = UserGV.SelectedRows[0].Cells[0].Value.ToString();
            fname.Text = UserGV.SelectedRows[0].Cells[1].Value.ToString();
            lname.Text = UserGV.SelectedRows[0].Cells[2].Value.ToString();
            address.Text = UserGV.SelectedRows[0].Cells[3].Value.ToString();
            email.Text = UserGV.SelectedRows[0].Cells[4].Value.ToString();
            tp.Text = UserGV.SelectedRows[0].Cells[5].Value.ToString();
            password.Text = UserGV.SelectedRows[0].Cells[6].Value.ToString();
            cmbgender.SelectedIndex = cmbgender.FindString(UserGV.SelectedRows[0].Cells[7].Value.ToString());
            cmbUserType.SelectedIndex = cmbUserType.FindString(UserGV.SelectedRows[0].Cells[8].Value.ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Auto_Click(object sender, EventArgs e)
        {
            AutoID();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            log menu= new log();
            menu.Show();
            this.Hide();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            staffid.Text = "";
            fname.Text = "";
            lname.Text = "";
            address.Text = "";
            email.Text = "";
            tp.Text = "";
            password.Text = "";
            cmbgender.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
