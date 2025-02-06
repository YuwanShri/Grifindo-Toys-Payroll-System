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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Grifindo_payroll_system
{
    public partial class Setting : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VILJ0BO\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;

        public string CycleDateRange { get; set; }
        public string GovtTaxRate { get; set; }

        public Setting()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void salary_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                /*CycleDateRange = textBox1.Text;
                GovtTaxRate = textBox3.Text;
                Salary salaryForm = new Salary();
                salaryForm.Show();
                this.Hide();*/
                main form3 = new main();
                form3.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VILJ0BO\\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;Encrypt=False;");
            con.Open();

            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM salcy_and_govtx", con);
            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                SqlCommand updateCmd = new SqlCommand("UPDATE salcy_and_govtx SET sal_cyc_dt_range = @sal_cyc_dt_range, govt_tx_rate = @govt_tx_rate", con);
                updateCmd.Parameters.AddWithValue("@sal_cyc_dt_range", float.Parse(textBox1.Text));
                updateCmd.Parameters.AddWithValue("@govt_tx_rate", float.Parse(textBox3.Text));
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO salcy_and_govtx (sal_cyc_dt_range, govt_tx_rate) VALUES (@sal_cyc_dt_range, @govt_tx_rate)", con);
                insertCmd.Parameters.AddWithValue("@sal_cyc_dt_range", float.Parse(textBox1.Text));
                insertCmd.Parameters.AddWithValue("@govt_tx_rate", float.Parse(textBox3.Text));
                insertCmd.ExecuteNonQuery();
            }

            con.Close();
            MessageBox.Show("Successfully Inserted/Updated");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
