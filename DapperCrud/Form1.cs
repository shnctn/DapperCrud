using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace DapperCrud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        private string sql = "";

        void CUD(DynamicParameters dynamicParameters=null)
        {

            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }

            con.Execute(sql,dynamicParameters,commandType:CommandType.Text);
            con.Close();
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text= "";
                }
            }
            dataGridView1.DataSource = con.Query<Test>("Select * from dbo.Test");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = con.Query<Test>("Select * from dbo.Test");


        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            DynamicParameters prm = new DynamicParameters();
           
            prm.Add("@p1",txtid.Text);
            prm.Add("@p2",txtad.Text);
            prm.Add("@p3",txtsoyad.Text);
            sql = "insert into Test values(@p1,@p2,@p3) ";
            CUD(prm);
        }

        private void btnGün_Click(object sender, EventArgs e)
        {
            DynamicParameters prm = new DynamicParameters();

            prm.Add("@p1", int.Parse(txtid.Text));
            prm.Add("@p2", txtad.Text);
            prm.Add("@p3", txtsoyad.Text);
            sql = "update dbo.Test set soyad=@p3, Ad=@p2 where Id =@p1";
            CUD(prm);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sql="delete from test where Id='"+int.Parse(txtid.Text)+"'";
            CUD();
        }
    }
}
