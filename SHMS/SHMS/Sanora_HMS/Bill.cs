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

        private int tIndex = -1;
        private ToolTip toolTip1;
        public decimal TotalAmount { get; set; }
        string resNo = "";
        string passNo = "";
        List<string> roomNos = new List<string>();
        string checkIn = "";
        string checkOut = "";
        string total = "";
        public Bill(string resno, string passNo, List<string> roomNos, string checkIn, string checkOut, string total)
        {
            InitializeComponent();

            this.resNo = resno;
            this.passNo = passNo;
            this.roomNos = roomNos;
            this.checkIn = checkIn;
            this.checkOut = checkOut;
            this.total = total;

            //listView1.BackColor = System.Drawing.Color.Orange;

            txtPass.Text = passNo;
            txtCheckIn.Text = checkIn;
            txtCheckOut.Text = checkOut;
            TotalRs.Text = total;
            txtResNo.Text = resno;

            //listView1.Items.Add("Mahesh Chand");

            foreach (var rm in roomNos)
            {
                listView1.Items.Add(rm);
                //txtRoomNo.Text = txtRoomNo.Text +"," + rm;
            }


            try
            {
                DBConnection obj = new DBConnection();
                sqlcon = obj.getSQLConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting" + ex, "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            toolTip1 = new ToolTip();
            TotalAmount = decimal.Parse(total);

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
                string sql = "select * from RoomTB  WHERE Availability = 'Free'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();

                da.Fill(ds);

                /*foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["Room_Type"].ToString())
                    {
                        case "Luxary":
                            checkedListBox1.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;

                        case "Family":
                              checkedListBox2.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;

                        case "Normal":
                            checkedListBox3.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;
                    }
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                /*calculateForeignTotal();
                Fillsearchcmb();*/
            }

        }

        /*private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = checkedListBox1.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
                CalculateAmount(roomNumber, true);
            else
                CalculateAmount(roomNumber, false);
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = checkedListBox2.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
                CalculateAmount(roomNumber, true);
            else
                CalculateAmount(roomNumber, false);
        }*/

        /*private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = checkedListBox3.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
                CalculateAmount(roomNumber, true);
            else
                CalculateAmount(roomNumber, false);
        }*/

        private void CalculateAmount(string roomNo, bool isAdd)
        {
            decimal chagingValue = 0;
            //additional prices 
            decimal freeWifi = 200;
            decimal miniBar = 2000;
            decimal sateliteTV = 2000;
            decimal doubleAndSIngleBeds = 4000;
            decimal enSuiyeBathroom = 1500;
            decimal twinBed = 5000;

            decimal luxuryRoom = 50000;
            decimal familyRoom = 18000;
            decimal normalRoom = 6000;


            try
            {
                sqlcon.Open();
                string sql = "select * from RoomTB WHERE Room_No='" + roomNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();

                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["Room_Type"].ToString())
                    {
                        case "Luxary":
                            chagingValue += luxuryRoom;
                            break;

                        case "Family":
                            chagingValue += familyRoom;
                            break;

                        case "Normal":
                            chagingValue += normalRoom;
                            break;
                    }

                    //charges for facilities
                    string facilities = dr["Facilities"].ToString();

                    if (facilities.Contains("free wifi"))
                        chagingValue += freeWifi;

                    if (facilities.Contains("Mini Bar"))
                        chagingValue += miniBar;

                    if (facilities.Contains("Satalite TV"))
                        chagingValue += sateliteTV;

                    if (facilities.Contains("Double and Single beds"))
                        chagingValue += doubleAndSIngleBeds;

                    if (facilities.Contains("En-suiye bathroom"))
                        chagingValue += enSuiyeBathroom;

                    if (facilities.Contains("Twin bed"))
                        chagingValue += twinBed;

                }

                /*if (isAdd)
                    TotalAmount += chagingValue;
                else if (TotalAmount >= chagingValue)
                    TotalAmount -= chagingValue;

                TotalRs.Text = TotalAmount.ToString();

                calculateForeignTotal();*/

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

        /*private void calculateForeignTotal()
        {
            if (cmbCurrency.SelectedIndex != -1)
            {
                decimal usd = 0.005m; // 1 USD = 200 Rs
                decimal usdTotal = TotalAmount > 0 ? TotalAmount * usd : 0;

                if (cmbCurrency.SelectedItem.ToString() == "Doller")
                    txtTotal.Text = usdTotal.ToString("0.##") + " $";
                else if (cmbCurrency.SelectedItem.ToString() == "RS")
                    txtTotal.Text = " Rs" +TotalAmount.ToString("0.##") ;
            }
        }*/

        /*private void checkedListBox1_MouseHover(object sender, EventArgs e)
        {
            GetToolTip1();
        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip1();
            }
        }

        void GetToolTip1()
        {
            Point pos = checkedListBox1.PointToClient(MousePosition);

            tIndex = checkedListBox1.IndexFromPoint(pos);

            if (tIndex > -1)
            {
                string roomNo = checkedListBox1.Items[tIndex].ToString();
                string facilities = "";

                try
                {
                    sqlcon.Open();
                    string sql = "select * from RoomTB WHERE Room_No='" + roomNo + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    var ds = new DataSet();

                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        facilities = dr["Facilities"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    sqlcon.Close();
                }

                pos = this.PointToClient(MousePosition);

                toolTip1.ToolTipTitle = checkedListBox1.Items[tIndex].ToString();

                toolTip1.SetToolTip(checkedListBox1, facilities);

            }

        }*/

        private void checkedListBox2_MouseHover(object sender, EventArgs e)
        {
            /*int index = checkedListBox2.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip2();
            }*/
        }

        /*private void checkedListBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int index = checkedListBox2.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip2();
            }
        }
        void GetToolTip2()
        {
            Point pos = checkedListBox2.PointToClient(MousePosition);

            tIndex = checkedListBox2.IndexFromPoint(pos);

            if (tIndex > -1)
            {
                string roomNo = checkedListBox2.Items[tIndex].ToString();
                string facilities = "";

                try
                {
                      sqlcon.Open();
                    string sql = "select * from RoomTB WHERE Room_No='" + roomNo + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    var ds = new DataSet();

                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        facilities = dr["Facilities"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    sqlcon.Close();
                }

                pos = this.PointToClient(MousePosition);

                toolTip1.ToolTipTitle = checkedListBox2.Items[tIndex].ToString();

                toolTip1.SetToolTip(checkedListBox2, facilities);
            }
        }*/

        private void checkedListBox3_MouseHover(object sender, EventArgs e)
        {
            //GetToolTip3();
        }

        /*private void checkedListBox3_MouseMove(object sender, MouseEventArgs e)
        {
            int index = checkedListBox3.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip3();
            }
        }
        void GetToolTip3()
        {
            Point pos = checkedListBox3.PointToClient(MousePosition);

            tIndex = checkedListBox3.IndexFromPoint(pos);

            if (tIndex > -1)
            {
                string roomNo = checkedListBox3.Items[tIndex].ToString();
                string facilities = "";

                try
                {
                    sqlcon.Open();
                    string sql = "select * from RoomTB WHERE Room_No='" + roomNo + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    var ds = new DataSet();

                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        facilities = dr["Facilities"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    sqlcon.Close();
                }

                pos = this.PointToClient(MousePosition);

                toolTip1.ToolTipTitle = checkedListBox3.Items[tIndex].ToString();

                toolTip1.SetToolTip(checkedListBox3, facilities);

            }

        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //calculateForeignTotal();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            PrintBill printBill = new PrintBill();
            printBill.Show();
            this.Hide();
        }

        private void Bill_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Reservation menu = new Reservation();
            menu.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PrintBill(object sender, EventArgs e)
        {

        }

        private void btnBillCal_Click(object sender, EventArgs e)
        {
            /*bool valid = true;

            string rooms = "";

            foreach (string s in roomNos)
            {
                rooms = rooms + s + ",";
            }
            try
            {
                sqlcon.Open();
                if (valid)
                {

                    DialogResult confirm = MessageBox.Show("Are You Sure to Add the Bill", "Manage Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into BillTB values ('" +
                        txtResNo.Text + "','" + txtPass.Text + "','" + Convert.ToDateTime(txtCheckIn.Text) + "','" + Convert.ToDateTime(txtCheckOut.Text) + "','" + TotalRs.Text + "','" + listView1.Value + "','" + txtTotal.Text + "','" + cmbCurrency.SelectedItem.ToString() + "','" + cmbPayment.SelectedItem.ToString() + "',)", sqlcon);
                        cmd.Connection = sqlcon;
                        int NoRows = cmd.ExecuteNonQuery();

                        if (NoRows > 0)
                        {
                            MessageBox.Show("Bill details Added Successfully !", "Manage Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Bill!", "Add Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                sqlcon.Close();
                populate();

            }*/

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand("insert into BillTB(RNo, PNo," +
                            "Checkin, Checkout, Total, BookedRoom, Total(ByCu)," +
                            "Currency, PaymentT,) Values (@RNo, @PNo," +
                            "@Checkin, @Checkout, @Total, @BookedRoom, @Total(ByCu)," +
                            "@Currency, @PaymentT,) ", sqlcon);

            cmd.Parameters.AddWithValue("RNo", txtResNo.Text);
            cmd.Parameters.AddWithValue("PNo", txtPass.Text);
            cmd.Parameters.AddWithValue("Checkin", txtCheckIn.Text);
            cmd.Parameters.AddWithValue("Checkout", txtCheckOut.Text);
            cmd.Parameters.AddWithValue("Total", txtTotal.Text);
            cmd.Parameters.AddWithValue("BookedRoom", listView1.Text);
            cmd.Parameters.AddWithValue("Total(ByCu)", TotalRs.Text);
            cmd.Parameters.AddWithValue("Currency", cmbCurrency.Text);
            cmd.Parameters.AddWithValue("PaymentT", cmbPayment.Text);
            cmd.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Submitted sucessfully");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TotalRs_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}