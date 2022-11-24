using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Sanora_HMS
{
    public partial class VerifyPassword : Form
    {
        public static string to;
        private string randomCode;
        public VerifyPassword()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string from, pass, messageBody;
            Random rand = new Random();
            var randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = (bunifuMaterialTextbox3.Text).ToString();
            from = "csemailsndr@gmail.com";
            pass = "tsntest123";
            messageBody = "your reset code is" + randomCode;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            //Message.Subject = "password reseting code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
                MessageBox.Show("code sent successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (randomCode == (bunifuMaterialTextbox4.Text).ToString())
            {
                to = bunifuMaterialTextbox3.Text;
                Reset_Password obj = new Reset_Password();
                obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("wrong code");
            }
        }
    }
}
