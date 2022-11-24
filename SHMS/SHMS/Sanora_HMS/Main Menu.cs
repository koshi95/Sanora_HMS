using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sanora_HMS
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
            designCustomized();
        }

        private void designCustomized()
        {
            submenu1.Visible = false;
            submenu2.Visible = false;
            submenu3.Visible = false;
        }

        private void HideSub_Menu()
        {
            if (submenu1.Visible == true)
                submenu1.Visible = false;
            if (submenu2.Visible == true)
                submenu2.Visible = false;
            if (submenu3.Visible == true)
                submenu3.Visible = false;

        }

        private void ShowSub_Menu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSub_Menu();
                subMenu.Visible = true;

            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            log fromobj = new log();
            fromobj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reservation fromobj = new Reservation();
            fromobj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            log fromobj = new log();
            fromobj.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Room fromobj = new Room();
            fromobj.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            log fromobj = new log();
            fromobj.Show();
            this.Hide();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ManageGuest fromobj = new ManageGuest();
            fromobj.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            log fromobj = new log();
            fromobj.Show();
            this.Hide();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            //toolTip1.SetToolTip(minimize, "minimize");
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(close, "close");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reservation fromobj = new Reservation();
            fromobj.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Room fromobj = new Room();
            fromobj.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ManageGuest fromobj = new ManageGuest();
            fromobj.Show();
            this.Hide();
        }
    }
}
