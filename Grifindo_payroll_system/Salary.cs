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
using System.Xml.Linq;
namespace Grifindo_payroll_system
{
    public partial class Salary : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VILJ0BO\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;

        public Salary()
        {
            InitializeComponent();
            Setting settingForm = Application.OpenForms.OfType<Setting>().FirstOrDefault();
            if (settingForm != null)
            {
                txt_sal_c_range.Text = settingForm.CycleDateRange;
                textBox2.Text = settingForm.GovtTaxRate;
            }
        }

        private string userRole;

        public Salary(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT * FROM tbl_emplyee_detail WHERE emp_id = '" + txtID.Text + "'  ";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();


            if (dr.HasRows)
            {
                txtID.Text = dr[0].ToString();
                textBox4.Text = dr[2].ToString();
                txtAllow.Text = dr[3].ToString();


            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float tot_sal = float.Parse(txt_tot_sal.Text);
            float sal_cycle_range = float.Parse(txt_sal_c_range.Text);
            float no_of_absent_day = float.Parse(txt_ab_date.Text);

            float no_pay_value = (tot_sal / sal_cycle_range) * no_of_absent_day;


            txt_no_pay_value.Text = no_pay_value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float tot_sal = float.Parse(txt_tot_sal.Text);
            float allow = float.Parse(txtAllow.Text);
            float ot_r = float.Parse(txt_r_hour.Text);
            float hour_worked = float.Parse(txt_Worked_h.Text);

            float b_p_v = tot_sal + allow + (ot_r * hour_worked);

            b_pay_v.Text = b_p_v.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            float govt_tax_rate = float.Parse(textBox2.Text);
            float b_p_v = float.Parse(b_pay_v.Text);
            float no_pay_value = float.Parse(txt_no_pay_value.Text);

            float gross_pay = b_p_v - (no_pay_value + b_p_v * govt_tax_rate);

            textBox3.Text = gross_pay.ToString();
        }

        private void frm_attendance_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VILJ0BO\\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;Encrypt=False;");
            SqlCommand cmd = new SqlCommand("Select sal_cyc_dt_range from salcy_and_govtx", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            txt_sal_c_range.DataSource = table1;
            txt_sal_c_range.DisplayMember = "sal_cyc_dt_range";
            txt_sal_c_range.ValueMember = "sal_cyc_dt_range";

            SqlCommand cmd2 = new SqlCommand("Select govt_tx_rate from salcy_and_govtx", con);
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd2;
            DataTable table2 = new DataTable();
            da2.Fill(table2);
            textBox2.DataSource = table2;
            textBox2.DisplayMember = "govt_tx_rate";
            textBox2.ValueMember = "govt_tx_rate";

            txt_tot_sal.Enabled = false;
            textBox2.Enabled = false;
            txt_sal_c_range.Enabled = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (userRole == "user")
            {
                Login form2 = new Login();
                form2.Show();
                this.Hide();
            }
            else
            {
                main form3 = new main();
                form3.Show();
                this.Hide();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            float allow = float.Parse(txtAllow.Text);
            float monthly_sal = float.Parse(textBox4.Text);

            float tota_salary = allow + monthly_sal;

            txt_tot_sal.Text = tota_salary.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            float tot_sal = float.Parse(txt_tot_sal.Text);
            float allow = float.Parse(txtAllow.Text);
            float ot_r = float.Parse(txt_r_hour.Text);
            float hour_worked = float.Parse(txt_Worked_h.Text);

            float b_p_v = tot_sal + allow + (ot_r * hour_worked);

            b_pay_v.Text = b_p_v.ToString();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            float b_p_v = float.Parse(b_pay_v.Text);
            float no_pay_value = float.Parse(txt_no_pay_value.Text);
            float govt_tax = float.Parse(textBox2.Text);

            float gross_pay = b_p_v - (no_pay_value + b_p_v * govt_tax);

            textBox3.Text = gross_pay.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VILJ0BO\\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;Encrypt=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into salary_component values (@sal_id, @emp_id, @tot_sal, @work_h, @per_h_r, @absent_d, @n_p_v, @b_p_v, @g_p_v)", con);
            cmd.Parameters.AddWithValue("@sal_id", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@emp_id", int.Parse(txtID.Text));
            cmd.Parameters.AddWithValue("@tot_sal", float.Parse(txt_tot_sal.Text));
            cmd.Parameters.AddWithValue("@work_h", float.Parse(txt_Worked_h.Text));
            cmd.Parameters.AddWithValue("@per_h_r", float.Parse(txt_r_hour.Text));
            cmd.Parameters.AddWithValue("@absent_d", float.Parse(txt_ab_date.Text));
            cmd.Parameters.AddWithValue("@n_p_v", float.Parse(txt_no_pay_value.Text));
            cmd.Parameters.AddWithValue("@b_p_v", float.Parse(b_pay_v.Text));
            cmd.Parameters.AddWithValue("@g_p_v", float.Parse(textBox3.Text));
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully Inserted");
        }

        private void txt_no_pay_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VILJ0BO\\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;Encrypt=False;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update salary_component set emp_id=@emp_id, tot_sal=@tot_sal, work_h=@work_h, per_h_r=@per_h_r, absent_d=@absent_d, n_p_v=@n_p_v, b_p_v=@b_p_v, g_p_v=@g_p_v where sal_id=@sal_id", con);
            cmd.Parameters.AddWithValue("@sal_id", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@emp_id", int.Parse(txtID.Text));
            cmd.Parameters.AddWithValue("@tot_sal", float.Parse(txt_tot_sal.Text));
            cmd.Parameters.AddWithValue("@work_h", float.Parse(txt_Worked_h.Text));
            cmd.Parameters.AddWithValue("@per_h_r", float.Parse(txt_r_hour.Text));
            cmd.Parameters.AddWithValue("@absent_d", float.Parse(txt_ab_date.Text));
            cmd.Parameters.AddWithValue("@n_p_v", float.Parse(txt_no_pay_value.Text));
            cmd.Parameters.AddWithValue("@b_p_v", float.Parse(b_pay_v.Text));
            cmd.Parameters.AddWithValue("@g_p_v", float.Parse(textBox3.Text));
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully Updated");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VILJ0BO\\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;Encrypt=False;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM salary_component WHERE sal_id = @sal_id", con);
            cmd.Parameters.AddWithValue("@sal_id", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully Deleted");
        }
    }
}
