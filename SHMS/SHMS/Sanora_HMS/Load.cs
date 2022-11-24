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
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            progressBar1.Increment(1);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                log fromobj = new log();
                fromobj.Show();
                this.Hide();
            }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            log fromobj = new log();
            fromobj.Show();
            this.Hide();
        }
    }
}
