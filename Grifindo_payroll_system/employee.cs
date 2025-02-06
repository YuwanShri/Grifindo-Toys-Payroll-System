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
    public partial class employee : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VILJ0BO\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;
        string status ;


        public employee()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void view()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VILJ0BO\SQLEXPRESS;Initial Catalog=Grifindo;Integrated Security=True;");
            try
            {
                con.Open();
                string query = "SELECT * FROM tbl_emplyee_detail";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                grd_emp.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void employee_Load(object sender, EventArgs e)
        {
            txtName.Enabled = false;
            txtSal.Enabled = false;
            txtAllo.Enabled = false;
            button1.Enabled = false;

            view();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = "ADD";
            con.Open();
            string query = "SELECT * FROM tbl_emplyee_detail WHERE emp_id = '" + txtId.Text + "'";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("This ID Is Already Taken");
            }
            else
            {
                txtName.Enabled = true;
                txtSal.Enabled = true;
                txtAllo.Enabled = true;
                button1.Enabled = true;
                btn_update.Enabled = false;
                btn_insert.Enabled = false;
                btn_del.Enabled = false;
                
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (status == "ADD")
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO tbl_emplyee_detail (emp_id,emp_name,month_sal,allow) VALUES ('" + int.Parse(txtId.Text) + "','" + txtName.Text + "','" + float.Parse(txtSal.Text) + "','" + float.Parse(txtAllo.Text) + "')";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    txtName.Enabled = false;
                    txtSal.Enabled = false;
                    txtAllo.Enabled = false;
                    button1.Enabled = false;
                    btn_update.Enabled = true;
                    btn_insert.Enabled = true;
                    btn_del.Enabled = true;


                    txtId.Text = "";
                    txtName.Text = "";
                    txtSal.Text = "";
                    txtAllo.Text = "";


                    con.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed");
                }

                view();
            }
            else if (status == "Edit")
            {
                try
                {
                    con.Open();
                    string query = "UPDATE tbl_emplyee_detail SET emp_name = '" + txtName.Text + "', month_sal = '" + float.Parse(txtSal.Text) + "' , allow = '" + float.Parse(txtAllo.Text) + "' WHERE emp_id = '" + txtId.Text + "' ";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Updated");
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Failed");
                }

                view();
                
            }
           
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            status = "Edit";
            con.Open();
            string query = "SELECT * FROM tbl_emplyee_detail WHERE emp_id = '" + txtId.Text + "'";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();


            if (dr.HasRows)
            {
                txtId.Text = dr[0].ToString();
                txtName.Text = dr[1].ToString();
                txtSal.Text = dr[2].ToString();
                txtAllo.Text = dr[3].ToString();

                txtName.Enabled = true;
                txtSal.Enabled = true;
                txtAllo.Enabled = true;
                btn_update.Enabled = true;
                btn_insert.Enabled = false;
                btn_del.Enabled = false;
                button1.Enabled = true;

                txtId.Text = "";
                txtName.Text = "";
                txtSal.Text = "";
                txtAllo.Text = "";
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT * FROM tbl_emplyee_detail WHERE emp_id = '" + txtId.Text + "'  ";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();


            if (dr.HasRows)
            {
                txtId.Text = dr[0].ToString();
                txtName.Text = dr[1].ToString();
                txtSal.Text = dr[2].ToString();
                txtAllo.Text = dr[3].ToString();

               
            }
            con.Close();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string del = "DELETE FROM  tbl_emplyee_detail WHERE emp_id = '"+txtId.Text+"' ";
                cmd = new SqlCommand(del,con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted Successfully");
                view();
                txtId.Text = "";
                txtName.Text = "";
                txtSal.Text = "";
                txtAllo.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Deleted Failed");
            }
        }

        private void grd_emp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                main form3 = new main();
                form3.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
