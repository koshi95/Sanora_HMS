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
    public partial class PrintBill : Form
    {

        SqlConnection sqlcon;
        public PrintBill()
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
        void Poplate()
        {
            try
            {
                sqlcon.Open();
                string sql = "select * from ReservationTB";
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                BillGV.DataSource = ds.Tables[0];
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the category " + ex);

            }

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Reservation Bill", new Font("Century", 25, FontStyle.Bold), Brushes.Blue, new Point(230));
            e.Graphics.DrawString("Reservation No:" + BillGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 20, FontStyle.Regular),
                Brushes.Black, new Point(80, 100));
            e.Graphics.DrawString("Passport/NIC:" + BillGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 20, FontStyle.Regular),
                Brushes.Black, new Point(80, 133));
            e.Graphics.DrawString("Total" + BillGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 20, FontStyle.Regular),
                Brushes.Black, new Point(80, 166));
            e.Graphics.DrawString("Check in :" + BillGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 20, FontStyle.Regular),
                Brushes.Black, new Point(80, 199));
            e.Graphics.DrawString("Check out:" + BillGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 20, FontStyle.Regular),
                Brushes.Black, new Point(80, 232));
        }

        private void BillGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void PrintBill_Load(object sender, EventArgs e)
        {
            Poplate();
        }

        private void btnBillCal_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlDataAdapter das1 = new SqlDataAdapter("select * from ReservationTB where RNO= '" + txtprint.Text + "'", sqlcon);
            DataTable dt1 = new DataTable();
            das1.Fill(dt1);
            BillGV.DataSource = dt1;
            sqlcon.Close();
        }
    }
}
