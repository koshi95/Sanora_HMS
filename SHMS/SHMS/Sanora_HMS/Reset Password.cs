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
    public partial class Reset_Password : Form
    {
        string username = VerifyPassword.to;
        public Reset_Password()
        {
            try
            {
                DBConnection obj = new DBConnection();
                SqlConnection con = obj.getSQLConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting" + ex, "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuMaterialTextbox3.Text == bunifuMaterialTextbox4.Text)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.ExecuteNonQuery();
                MessageBox.Show("reset successfully");


            }
            else
            {
                MessageBox.Show("the new paasword do not match,so enter same password");

            }
        }
    }
}
