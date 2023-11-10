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

namespace DisconnectedDemo
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;database=northwind;integrated security=true");
            SqlCommand cmd = new SqlCommand("[dbo].[ValidateUser]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_username", txtusername.Text.Trim());
            cmd.Parameters.AddWithValue("@p_pwd", txtpwd.Text.Trim());
            cmd.Parameters.Add("@p_userStatus", SqlDbType.VarChar, 7);
            cmd.Parameters["@p_userStatus"].Direction = ParameterDirection.Output;

            cn.Open();
            cmd.ExecuteNonQuery();
            string status = cmd.Parameters["@p_userStatus"].Value.ToString();
            if (status=="Valid")
            {
                MessageBox.Show("Welcome");
                Form1 f=new Form1();
                f.Show();
            }
            else
            {
                MessageBox.Show("Verify Username/password");
            }
            cn.Close();
            cn.Dispose();


        }
    }
}
