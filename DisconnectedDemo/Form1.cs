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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
                SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
                DataSet ds = new DataSet();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key

                da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

                DataRow found = ds.Tables["TechSkills"].Rows.Find(Convert.ToInt32(txtskillid.Text));
                if (found != null)
                {
                    found["SkillName"] = txtname.Text.Trim();
                    found["SkillType"] = txttype.Text.Trim();
                    //Reconnect to db server--submitting the changes
                    SqlCommandBuilder bldr = new SqlCommandBuilder(da);//insert
                    da.Update(ds.Tables["TechSkills"]);
                    Form1_Load(sender, e);
                    MessageBox.Show("Updated Successfully...");

                }
                else
                {

                    throw new SkillNotFoundException("Pls check skillid.... Enter a valid Skill ID");
                }
            }
            catch (SkillNotFoundException ex)
            {

                MessageBox.Show(ex.Message);
            }
            

            

        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            try
            {

            
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key

            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            DataRow found=ds.Tables["TechSkills"].Rows.Find(Convert.ToInt32(txtskillid.Text));

            if (found != null) {
                txtname.Text = found["SkillName"].ToString();
                txttype.Text = found["SkillType"].ToString();

            }
                else
                {
                    txtname.Text = "";
                    txttype.Text = "";
                    throw new SkillNotFoundException("no record found with this id");
                    

                }
            }
            catch (SkillNotFoundException ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
                SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
                DataSet ds = new DataSet();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key

                da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

                DataRow found = ds.Tables["TechSkills"].Rows.Find(Convert.ToInt32(txtskillid.Text));

                if (found != null)
                {
                    txtname.Text = found["SkillName"].ToString();
                    txttype.Text = found["SkillType"].ToString();
                   DialogResult dialogresult= MessageBox.Show("Found R u sure to delete?","Confirmation from User",MessageBoxButtons.OKCancel);
                    if (DialogResult.OK==dialogresult)
                        {
                        found.Delete();
                        SqlCommandBuilder bldr = new SqlCommandBuilder(da);
                        da.Update(ds.Tables["TechSkills"]);
                        MessageBox.Show("Deleted Successfully....");
                        txtskillid.Text = "";
                        txtname.Text = "";
                        txttype.Text = "";

                        Form1_Load(sender, e);

                    }
                    else
                    {
                        MessageBox.Show("Ok... I am not deleting");
                    }


                }
                else
                {
                    txtname.Text = "";
                    txttype.Text = "";
                    throw new SkillNotFoundException("no record found with this id");


                }
            }
            catch (SkillNotFoundException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
