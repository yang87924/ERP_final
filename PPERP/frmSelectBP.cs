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
    public partial class frmSelectBP : Form
    {
        //回傳值變數宣告
        public string rtnCardCode { set; get; }
        public string rtnCardName { set; get; }

        //
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public frmSelectBP()
        {
            InitializeComponent();
        }

        private void frmSelectBP_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            //2.Select命令元件
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select CardCode, CardName From BP Order By CardCode";
            da.SelectCommand = selectCmd;

            da.Fill(ds, "BP");
            dgvBPList.DataSource = ds.Tables["BP"];

            dgvBPList.AutoResizeColumns();

            dgvBPList.Columns["CardCode"].HeaderText = "客戶代碼";
            dgvBPList.Columns["CardName"].HeaderText = "客戶名稱";
        }

        //選取按鈕
        private void btnSelect_Click(object sender, EventArgs e)
        {
            rtnCardCode = (string) dgvBPList.CurrentRow.Cells[0].Value;
            rtnCardName = (string)dgvBPList.CurrentRow.Cells[1].Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //Double Click客戶
        private void dgvBPList_DoubleClick(object sender, EventArgs e)
        {
            btnSelect.PerformClick();
        }

        //取消按鈕
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
