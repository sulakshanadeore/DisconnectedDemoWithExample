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
namespace DisconnectedDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds,"TechSkills");//After execution of this line, database is disconnected

            DataTable dt = new DataTable();
            dt=ds.Tables["TechSkills"];
            dataGridView1.DataSource = dt;
            }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

           DataRow myrow= ds.Tables["TechSkills"].NewRow();

            myrow["SkillName"] = txtname.Text.Trim();
            myrow["SkillType"] = txttype.Text.Trim();

            ds.Tables["TechSkills"].Rows.Add(myrow);

            //Reconnect to db server--submitting the changes
            SqlCommandBuilder bldr = new SqlCommandBuilder(da);//insert
            da.Update(ds.Tables["TechSkills"]);
            MessageBox.Show("Added Successfully...");

















        }

        private void txtskillid_MouseMove(object sender, MouseEventArgs e)
        {
  
        }

        private void txtskillid_MouseHover(object sender, EventArgs e)
        {
            txtskillid.ReadOnly=false;
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
