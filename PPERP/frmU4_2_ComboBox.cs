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
    public partial class frmU4_2_ComboBox : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public frmU4_2_ComboBox()
        {
            InitializeComponent();
        }

        private void frmU4_2_ComboBox_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            getDate();  //下載BPGroup資料

            setComboBoxData();  //設定ComboBox下拉選單的選項資料
        }

        private void frmU4_2_ComboBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //下載BPGroup資料
        private void getDate()
        {
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select * From BPGroup";
            da.SelectCommand = selectCmd;

            da.Fill(ds, "BPGroup");
        }

        //設定ComboBox下拉選單的選項資料
        private void setComboBoxData()
        {
            cbxBPGroup.DataSource = ds.Tables["BPGroup"];
            cbxBPGroup.DisplayMember = "BPGroupName";
            cbxBPGroup.ValueMember = "BPGroupID";
        }

        private void cbxBPGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            txtBPGroupID.Text = cbxBPGroup.SelectedValue.ToString();
        }
    }
}
