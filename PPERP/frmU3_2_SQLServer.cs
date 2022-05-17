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

namespace PPERP
{
    public partial class frmU3_2_SQLServer : Form
    {
        SqlConnection dbConn;

        public frmU3_2_SQLServer()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //連線登入SQL Server
            try
            {
                //SqlConnection dbConn;

                dbConn = new SqlConnection("data source=127.0.0.1; user id=sa; password=abc123; initial catalog=PPERP");
                dbConn.Open();

                MessageBox.Show("成功登入SQL Server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Server登入失敗! (" + ex.Message + ")" );
            }


        }

        private void frmU3_2_SQLServer_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if(dbConn != null)
                {
                    dbConn.Close();
                    MessageBox.Show("成功登出SQL Server!");
                } else
                {
                    MessageBox.Show("尚未登入SQL Server!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Server登出失敗! (" + ex.Message + ")");
            }
        }
    }
}
