using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace DisconnectedDemo
{
    public partial class DataViewDemo : Form
    {
        public DataViewDemo()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void DataViewDemo_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            
            DataView dv_defaultView = ds.Tables["TechSkills"].DefaultView;
            dataGridView1.DataSource = dv_defaultView;
            DataRowView[] data=dv_defaultView.FindRows(2);
            txtname.Text = data[0].Row["SkillName"].ToString();
            txtskillid.Text = data[0].Row["SkillID"].ToString();
            txtskillid.Text = data[0].Row["SkillType"].ToString();

        }
    }
}
