using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        public string server
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }

        public string pass
        {
            get;
            set;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            server = tbServer.Text;
            username = tbLogin.Text;
            pass = tbPass.Text;
            
            this.Close();

        }
    }
}
