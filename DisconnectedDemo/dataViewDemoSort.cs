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
    public partial class dataViewDemoSort : Form
    {
        public dataViewDemoSort()
        {
            InitializeComponent();
        }

        private void dataViewDemoSort_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            //int rowsinTable=ds.Tables["TechSkills"].Rows.Count;

            //ds.Tables["TechSkills"].DefaultView.Sort = "SkillType";
            //DataView dv=ds.Tables["TechSkills"].DefaultView;
            //dataGridView1.DataSource = dv;


            DataView dv = ds.Tables["TechSkills"].DefaultView;
         //   dv.Sort = "SkillName ASC";
            dv.Sort = "SkillName DESC";
            dataGridView1.DataSource = dv;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            //1. datareader ----readonly,fwdonly stream of data
            //2. datatable can be sorted and filtered so that is beneficial
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from TechSkills", cn);
            cn.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            dt.Load(dr);

            
            for (int i = 0; i < dt.Rows.Count-1; i++)
            {
                TechSkills t = new TechSkills();
                t.SkillId = Convert.ToInt32(dt.Rows[i]["SkillId"]);
               t.SkillName = dt.Rows[i]["SkillName"].ToString();
                t.SkillType= dt.Rows[i]["SkillType"].ToString();

                listBox1.Items.Add(t.SkillId + " " +t.SkillName + " " +t.SkillType );
            }
            //combo

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                TechSkills t = new TechSkills();
                t.SkillId = Convert.ToInt32(dt.Rows[j]["SkillId"]);
                comboBox1.Items.Add(t.SkillId);

            }


            


            



            


            
            
                       
            
            
            
            cn.Close();
            cn.Dispose();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("You selected skill id=" + comboBox1.SelectedItem);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("You selected skill id=" + listBox1.SelectedItem);
        }
    }
}
