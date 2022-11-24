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
    public partial class log : Form
    {
        SqlConnection sqlcon;
        public log()
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

        public int count;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon.Open();
                bool valid = true;
                if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(cmbtype.Text))
                {
                    MessageBox.Show("Need to fill all the values ", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valid = false;
                }
                if (valid)
                {
                    String UserType = null;
                    string qry = "Select UserType from  User_TB where StaffID ='" + username.Text + "' and " +
                        "Password ='" + password.Text + "'and UserType ='" + cmbtype.Text + "' ";
                    SqlCommand cmd = new SqlCommand(qry, sqlcon);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read()) 
                        {
                            UserType = dr[0].ToString();
                        }
                        if (UserType.Equals("Admin"))
                        {
                            MessageBox.Show("Admin Login Success ", "Welcome to Sanora Hotel Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ManageUser obj = new ManageUser();
                            obj.Show();
                            this.Hide();
                        }
                        else if (UserType.Equals("Receptionist")) 
                        {
                            MessageBox.Show("Receptionist Login Success ", "Welcome to Sanora Hotel Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Main_Menu obj = new Main_Menu();
                            obj.Show();
                            this.Hide();
                        } 
                    }
                    else
                    {
                        MessageBox.Show("Invalid login", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlcon.Close();
        }

        private void show_Click(object sender, EventArgs e)
        {
            show.Hide();
            password.UseSystemPasswordChar = false;
            hide.Show();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Hide();
            password.UseSystemPasswordChar = true;
            show.Show();
        }

        private void hide_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(hide, "Hide Password");
        }

        private void show_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(show, "Show Password");
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(minimize, "minimize");
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(close, "close");
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Room obj = new Room();
            obj.Show();
            this.Hide();
        }
    }
}
