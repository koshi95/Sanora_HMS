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
using System.Globalization;

namespace Sanora_HMS
{
    public partial class Reservation : Form
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
        public Reservation()
        {
            toolTip1 = new ToolTip();
            TotalAmount = 0;

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
                string sql = "select * from ReservationTB";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ReservationGV.DataSource = ds.Tables[0];


                CBox1.Visible = true;
                CBox2.Visible = false;
                CBox3.Visible = false;
                cmbroomtype.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                sqlcon.Close();
                FillRoomcmb();
                ReservationFill();
                populateCheckBoxes();
            }

        }

        void populateCheckBoxes()
        {
            try
            {
                sqlcon.Open();
                string sql = "SELECT RoomTB.Room_No,RoomTB.Room_Type FROM RoomTB LEFT JOIN" +
                    " ReservationTB ON RoomTB.Room_No = ReservationTB.Room_No EXCEPT SELECT " +
                    "ReservationTB.Room_No,RoomTB.Room_Type FROM RoomTB LEFT JOIN ReservationTB " +
                    "ON RoomTB.Room_No = ReservationTB.Room_No";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();

                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["Room_Type"].ToString())
                    {
                        case "Luxary":
                            CBox1.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;

                        case "Family":
                            CBox2.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;

                        case "Normal":
                            CBox3.Items.Add(dr["Room_No"].ToString() + " Room");
                            break;
                    }
                }
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

        void ReservationFill()
        {

            string sql = "select *from ReservationTB";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("RNO", typeof(string));
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                cmbRno.ValueMember = "RNO";
                cmbRno.DataSource = dt;
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
                //sqlcon.Open();
                //SqlCommand cmd = new SqlCommand("select Room_No from RoomTB where Availability = 'free'", sqlcon);
                //SqlDataReader rd;
                //rd = cmd.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Columns.Add("Room_No", typeof(string));
                //dt.Load(rd);
                //cmbroomno.ValueMember = "Room_No";
                //cmbroomno.DataSource = dt;
                


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

        void Reservationfilter()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from ReservationTB where RNO='" + cmbRno.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ReservationGV.DataSource = ds.Tables[0];
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        void StateRoomAvailability()
        {
            try
            {
                sqlcon.Open();
                string newAvailability = "Booked";
                string Query = "update RoomDB set Availability='" + newAvailability + "' where Room_No='" + Convert.ToInt32(cmbroomtype.SelectedItem.ToString()) + ";";
                SqlCommand cmd = new SqlCommand(Query, sqlcon);
                cmd.ExecuteNonQuery();
                
                
                FillRoomcmb();




            }
            catch(Exception ex)
            {

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
                SqlCommand cd = new SqlCommand("select RNO from ReservationTB", sqlcon);
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
                        reservationno.Text = "R00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        reservationno.Text = "R0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        reservationno.Text = "R" + CTR;
                    }
                }

                else
                {
                    reservationno.Text = "G001";
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
        private void button1_Click(object sender, EventArgs e)
        {
            Room fromobj = new Room();
            fromobj.Show();
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Reservationfilter();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            bool valid = true;

            string rooms = "";

            foreach(string s in roomNos)
            {
                rooms = rooms + s + ",";
            }

            if (String.IsNullOrEmpty(reservationno.Text) || String.IsNullOrEmpty(nic.Text) || String.IsNullOrEmpty(cmbadults.Text) || String.IsNullOrEmpty(cmbchildren.Text) || String.IsNullOrEmpty("") || String.IsNullOrEmpty(cmbroomtype.Text) )
            {
                MessageBox.Show("Need to fill all the values", "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            try
            {
                sqlcon.Open();
                if (valid)
                {

                    DialogResult confirm = MessageBox.Show("Are You Sure to Add the Reservation", "Manage Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("insert into ReservationTB values ('" + 
                            reservationno.Text + "','" + nic.Text + "','" + cmbadults.SelectedItem.ToString() + "','" + 
                            cmbchildren.SelectedItem.ToString() + "' ,'" /*+ rooms + "','"*/ + 
                            cmbroomtype.SelectedItem.ToString() + "','" + Datecheckin.Value + "','" +Datecheckout.Value + "')", sqlcon);
                        cmd.Connection = sqlcon;
                        int NoRows = cmd.ExecuteNonQuery();


                        if (NoRows > 0)
                        {
                            MessageBox.Show("Reservation details Added Successfully !", "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Reservation!", "Add Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                StateRoomAvailability();
                populate();
                
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (String.IsNullOrEmpty(reservationno.Text) || String.IsNullOrEmpty(nic.Text) || String.IsNullOrEmpty(cmbadults.Text) || String.IsNullOrEmpty(cmbchildren.Text) || String.IsNullOrEmpty("") || String.IsNullOrEmpty(cmbroomtype.Text))
            {
                MessageBox.Show("Need to fill all the values", "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            if (valid)
            {

                DialogResult confirm = MessageBox.Show("Are You Sure to Update the Guest", "Manage Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        sqlcon.Open();
                        string q = "Update ReservationTB set  P_NIC='" + nic.Text + "',Adults='" +
                            cmbadults.SelectedItem.ToString() + "',Children='" + cmbchildren.SelectedItem.ToString() + "',Room_No='" +
                            "" + "',Room_Type ='" + cmbroomtype.SelectedItem.ToString() + "',Checkin= '" +
                            Convert.ToDateTime(Datecheckin.Text) + "',Checkout = '" + Convert.ToDateTime(Datecheckout.Text) + "' where RNO= '" +
                            nic.Text + "'";
                        SqlCommand cmd = new SqlCommand(q, sqlcon);
                        int numberOfRecords = cmd.ExecuteNonQuery();
                        if (numberOfRecords > 0)
                        {
                            MessageBox.Show("Updated Succesfully", "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                     }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data is Not updated" + ex, "Manage Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlcon.Close();
                        populate();
                    }
                }
            }
        }

        DateTime today;
        private void Reservation_Load(object sender, EventArgs e)
        {
            today = Datecheckin.Value;
            populate();
            ReservationFill();
           // FillRoomTypecmb();
            Datelbl.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void dtpcheckin_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(Datecheckin.Value, today);
            if (res<0)
                MessageBox.Show("Wrong date for reservation");

            checkIn = Datecheckin.Value.ToString();
        }

        private void dtpcheckout_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(Datecheckout.Value, Datecheckin.Value);
            if (res < 0)
                MessageBox.Show("Wrong Dateout, check once more");
            checkOut = Datecheckin.Value.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void ReservationGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReservationGV_Click(object sender, EventArgs e)
        {
            reservationno.Text = ReservationGV.SelectedRows[0].Cells[0].Value.ToString();
            nic.Text = ReservationGV.SelectedRows[0].Cells[1].Value.ToString();
            cmbadults.SelectedIndex = cmbadults.FindString(ReservationGV.SelectedRows[0].Cells[2].Value.ToString());
            cmbchildren.SelectedIndex = cmbchildren.FindString(ReservationGV.SelectedRows[0].Cells[3].Value.ToString());
           // cmbroomno.SelectedIndex = cmbroomno.FindString(ReservationGV.SelectedRows[0].Cells[4].Value.ToString());
            cmbroomtype.SelectedIndex = cmbroomtype.FindString(ReservationGV.SelectedRows[0].Cells[5].Value.ToString());

            //var dd = ReservationGV.SelectedRows[0].Cells[6].Value.ToString();

            DateTime dt = DateTime.Parse(ReservationGV.SelectedRows[0].Cells[6].Value.ToString());
            Datecheckin.Value = dt;


            DateTime dt2 = DateTime.Parse(ReservationGV.SelectedRows[0].Cells[7].Value.ToString());
            Datecheckout.Value = dt2;
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            AutoID();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            Main_Menu menu = new Main_Menu();
            menu.Show();
            this.Hide();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void cmbroomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbroomtype.SelectedItem.ToString() == "Luxary")
            {
                CBox1.Visible = true;
                CBox2.Visible = false;
                CBox3.Visible = false;
            }
            else if (cmbroomtype.SelectedItem.ToString() == "Family")
            {
                CBox1.Visible = false;
                CBox2.Visible = true;
                CBox3.Visible = false;
            }
            else if (cmbroomtype.SelectedItem.ToString() == "Normal")
            {
                CBox1.Visible = false;
                CBox2.Visible = false;
                CBox3.Visible = true;
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = CBox1.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
            {
                roomNos.Add(roomNumber);
                CalculateAmount(roomNumber, true);
            }
            else
            {
                roomNos.Remove(roomNumber);
                CalculateAmount(roomNumber, false);
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = CBox2.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
            {
                roomNos.Add(roomNumber);
                CalculateAmount(roomNumber, true);
            }
            else
            {
                roomNos.Remove(roomNumber);
                CalculateAmount(roomNumber, false);
            }
        }

        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string room = CBox3.Items[e.Index].ToString();
            string roomNumber = room.Substring(0, room.Length - 5);
            if (e.NewValue == CheckState.Checked)
            {
                roomNos.Add(roomNumber);
                CalculateAmount(roomNumber, true);
            }
            else
            {
                roomNos.Remove(roomNumber);
                CalculateAmount(roomNumber, false);
            }
        }

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

                if (isAdd)
                    TotalAmount += chagingValue;
                else if (TotalAmount >= chagingValue)
                    TotalAmount -= chagingValue;

                textBox1.Text = TotalAmount.ToString();

                total = TotalAmount.ToString();

                //calculateForeignTotal();

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

        //private void calculateForeignTotal()
        //{
        //    if (comboBox1.SelectedIndex != -1)
        //    {
        //        decimal usd = 0.005m; // 1 USD = 200 Rs
        //        decimal usdTotal = TotalAmount > 0 ? TotalAmount * usd : 0;

        //        if (comboBox1.SelectedItem.ToString() == "Doller")
        //            bunifuMaterialTextbox1.Text = usdTotal.ToString("0.##") + " $";
        //        else if (comboBox1.SelectedItem.ToString() == "RS")
        //            bunifuMaterialTextbox1.Text = TotalAmount.ToString("0.##") + " Rs";
        //    }
        //}

        private void checkedListBox1_MouseHover(object sender, EventArgs e)
        {
            GetToolTip1(-1);
        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = CBox1.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip1(index);
            }
        }

        void GetToolTip1(int indx)
        {
            Point pos = CBox1.PointToClient(MousePosition);

            if(indx > -1)
            {
                tIndex = indx;
            }
            else
            {
                tIndex = CBox1.IndexFromPoint(pos);
            }

            if (tIndex > -1)
            {
                string roomN = CBox1.Items[tIndex].ToString();
                string roomNo = roomN.Substring(0, roomN.Length - 5);
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

                toolTip1.ToolTipTitle = CBox1.Items[tIndex].ToString();

                toolTip1.SetToolTip(CBox1, facilities);

            }

        }

        private void checkedListBox2_MouseHover(object sender, EventArgs e)
        {
            GetToolTip2(-1);
        }

        private void checkedListBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int index = CBox2.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip2(index);
            }
        }
        void GetToolTip2(int indx)
        {
            Point pos = CBox2.PointToClient(MousePosition);

            if (indx > -1)
            {
                tIndex = indx;
            }
            else
            {
                tIndex = CBox2.IndexFromPoint(pos);
            }

            if (tIndex > -1)
            {
                string roomN = CBox2.Items[tIndex].ToString();
                string roomNo = roomN.Substring(0, roomN.Length - 5);

                //string roomNo = CBox2.Items[tIndex].ToString();
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

                toolTip1.ToolTipTitle = CBox2.Items[tIndex].ToString();

                toolTip1.SetToolTip(CBox2, facilities);
            }
        }
        private void checkedListBox3_MouseHover(object sender, EventArgs e)
        {
            GetToolTip3(-1);
        }

        private void checkedListBox3_MouseMove(object sender, MouseEventArgs e)
        {
            int index = CBox3.IndexFromPoint(e.Location);
            if (tIndex != index)
            {
                GetToolTip3(index);
            }
        }
        void GetToolTip3(int indx)
        {
            Point pos = CBox3.PointToClient(MousePosition);

            if (indx > -1)
            {
                tIndex = indx;
            }
            else
            {
                tIndex = CBox3.IndexFromPoint(pos);
            }

            if (tIndex > -1)
            {
                string roomN = CBox3.Items[tIndex].ToString();
                string roomNo = roomN.Substring(0, roomN.Length - 5);

                //string roomNo = CBox3.Items[tIndex].ToString();
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

                toolTip1.ToolTipTitle = CBox3.Items[tIndex].ToString();

                toolTip1.SetToolTip(CBox3, facilities);

            }

        }

        private void btnBillCal_Click(object sender, EventArgs e)
        {
            Bill menu = new Bill(resNo, passNo, roomNos, checkIn, checkOut,total);
            menu.Show();
            this.Hide();
        }

        private void nic_OnValueChanged(object sender, EventArgs e)
        {
            passNo = nic.Text;
        }

        private void reservationno_OnValueChanged(object sender, EventArgs e)
        {
            resNo = reservationno.Text;
        }
    }
}
