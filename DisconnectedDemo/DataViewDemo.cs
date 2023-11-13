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
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            //all rows ---current rows
            DataView dv_defaultView = ds.Tables["TechSkills"].DefaultView;
            dataGridView1.DataSource = dv_defaultView;
            dv_defaultView.RowStateFilter = DataViewRowState.CurrentRows;
            DataRow drowNew = ds.Tables["TechSkills"].NewRow();
            drowNew["SkillName"] = txtname.Text.Trim();
            drowNew["SkillType"] = txttype.Text.Trim();
            ds.Tables["TechSkills"].Rows.Add(drowNew);

            DataView newlyAddedRowsToDataView = new DataView(ds.Tables["TechSkills"], "", "", DataViewRowState.Added);

            dataGridView2.DataSource = newlyAddedRowsToDataView;

            //SqlCommandBuilder builder = new SqlCommandBuilder(da);
            //da.Update(ds.Tables["TechSkills"]);




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
            //Find--search--->filter
            dv_defaultView.RowStateFilter = DataViewRowState.CurrentRows;
            dv_defaultView.Sort = "SkillId";
            DataRowView[] data = dv_defaultView.FindRows(2);
            txtname.Text = data[0].Row["SkillName"].ToString();
            txtskillid.Text = data[0].Row["SkillId"].ToString();
            txttype.Text = data[0].Row["SkillType"].ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            DataView dv_defaultView = ds.Tables["TechSkills"].DefaultView;
            dataGridView1.DataSource = dv_defaultView;
            //Find--search--->filter
            dv_defaultView.RowStateFilter = DataViewRowState.CurrentRows;
            dataGridView1.DataSource = dv_defaultView;


            DataRow found = ds.Tables["TechSkills"].Rows.Find(Convert.ToInt32(txtskillid.Text));
            if (found != null)
            {
                found["SkillName"] = txtname.Text.Trim();
                found["SkillType"] = txttype.Text.Trim();


                DataView newlyModifiedRowsToDataView = new DataView(ds.Tables["TechSkills"], "", "", DataViewRowState.ModifiedCurrent);

                dataGridView2.DataSource = newlyModifiedRowsToDataView;

            }



            //SqlCommandBuilder bldr = new SqlCommandBuilder(da);
            //da.Update(ds.Tables["TechSkills"]);


            //dv_defaultView.Sort = "SkillId";
            //DataRowView[] data = dv_defaultView.FindRows(2);
            //txtname.Text = data[0].Row["SkillName"].ToString();
            //txtskillid.Text = data[0].Row["SkillId"].ToString();
            //txttype.Text = data[0].Row["SkillType"].ToString();



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("server=Sulakshana\\sqlexpress;Integrated Security=true;database=Northwind");
            SqlDataAdapter da = new SqlDataAdapter("select * from TechSkills", cn);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//PrimaryKey/Foreign key
            da.Fill(ds, "TechSkills");//After execution of this line, database is disconnected

            DataView dv_defaultView = ds.Tables["TechSkills"].DefaultView;
            dataGridView1.DataSource = dv_defaultView;
            //Find--search--->filter
            dv_defaultView.RowStateFilter = DataViewRowState.CurrentRows;
            dataGridView1.DataSource = dv_defaultView;


            DataRow found = ds.Tables["TechSkills"].Rows.Find(Convert.ToInt32(txtskillid.Text));
            if (found != null)
            {
                found.Delete();


                DataView newlyDeletedRowsToDataView = new DataView(ds.Tables["TechSkills"], "", "", DataViewRowState.Deleted);

                dataGridView2.DataSource = newlyDeletedRowsToDataView;




            }
        }
    }
}
