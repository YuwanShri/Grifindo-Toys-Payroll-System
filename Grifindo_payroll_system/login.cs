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

namespace Grifindo_payroll_system
{
    public partial class Login : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VILJ0BO\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;
        string status;

        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "yuwan" && textBox2.Text == "yuwan123")
            {
                main form3 = new main();
                form3.Show();
                this.Hide();
            }
            else if (textBox3.Text == "user" && textBox2.Text == "user123")
            {
                Salary form4 = new Salary("user");
                form4.Show();
                this.Hide();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
