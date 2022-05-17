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
    public partial class frmU4_3_DataGridView : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public frmU4_3_DataGridView()
        {
            InitializeComponent();
        }

        //表單畫面開啟
        private void frmU4_3_DataGridView_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            //2.設定DataAdapter的SQL命令
            setSQLCommand();
        }

        //表單畫面關閉
        private void frmU4_3_DataGridView_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //設定DataAdapter的SQL命令
        private void setSQLCommand()
        {
            //1.Select命令元件
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select BP.*, BPGroup.BPGroupName From BP Left Join BPGroup On BP.GroupID = BPGroup.BPGroupID";
            da.SelectCommand = selectCmd;

            //2.Insert命令元件
            SqlCommand insertCmd = new SqlCommand();
            insertCmd.Connection = dbConn;
            insertCmd.CommandText = "Insert Into BP Values (@CardCode, @CardName, @Phone1, @Phone2, @Fax, @Addr, @CreditLimit, @GroupID)";
            insertCmd.Parameters.Add("@CardCode", SqlDbType.NVarChar, 20, "CardCode");
            insertCmd.Parameters.Add("@CardName", SqlDbType.NVarChar, 100, "CardName");
            insertCmd.Parameters.Add("@Phone1", SqlDbType.NVarChar, 20, "Phone1");
            insertCmd.Parameters.Add("@Phone2", SqlDbType.NVarChar, 20, "Phone2");
            insertCmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 20, "Fax");
            insertCmd.Parameters.Add("@Addr", SqlDbType.NVarChar, 100, "Addr");
            insertCmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal, 0, "CreditLimit");
            insertCmd.Parameters.Add("@GroupID", SqlDbType.NChar, 2, "GroupID");
            da.InsertCommand = insertCmd;

            //3.Update命令元件
            SqlCommand updateCmd = new SqlCommand();
            updateCmd.Connection = dbConn;
            updateCmd.CommandText = @"Update BP Set CardName = @CardName, 
                                                    Phone1 = @Phone1, 
                                                    Phone2 = @Phone2, 
                                                    Fax = @Fax, 
                                                    Addr = @Addr, 
                                                    CreditLimit = @CreditLimit, 
                                                    GroupID = @GroupID 
                                      Where CardCode = @CardCode";
            updateCmd.Parameters.Add("@CardCode", SqlDbType.NVarChar, 20, "CardCode");
            updateCmd.Parameters.Add("@CardName", SqlDbType.NVarChar, 100, "CardName");
            updateCmd.Parameters.Add("@Phone1", SqlDbType.NVarChar, 20, "Phone1");
            updateCmd.Parameters.Add("@Phone2", SqlDbType.NVarChar, 20, "Phone2");
            updateCmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 20, "Fax");
            updateCmd.Parameters.Add("@Addr", SqlDbType.NVarChar, 100, "Addr");
            updateCmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal, 0, "CreditLimit");
            updateCmd.Parameters.Add("@GroupID", SqlDbType.NChar, 2, "GroupID");
            da.UpdateCommand = updateCmd;

            //4.Delete命令元件
            SqlCommand deleteCmd = new SqlCommand();
            deleteCmd.Connection = dbConn;
            deleteCmd.CommandText = "Delete From BP Where CardCode = @CardCode";
            deleteCmd.Parameters.Add("@CardCode", SqlDbType.NVarChar, 20, "CardCode");
            da.DeleteCommand = deleteCmd;
        }



        //重新載入按鈕
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //清除原有內容
            if(ds.Tables.Contains("BP"))
            {
                ds.Tables["BP"].Rows.Clear();
            }

            //2.執行Select命令下載資料後, 存入Dataset, 命令為BP
            da.Fill(ds, "BP");

            //3.在DataGridView元件顯示下載的BP資料
            dgvBP.DataSource = ds.Tables["BP"];

            //4.修改DataGridView顯示格式
            //(1)標題文字
            dgvBP.Columns["CardCode"].HeaderText = "代碼";
            dgvBP.Columns["CardName"].HeaderText = "名稱";
            dgvBP.Columns["Phone1"].HeaderText = "電話1";
            dgvBP.Columns["Phone2"].HeaderText = "電話2";
            dgvBP.Columns["Fax"].HeaderText = "傳真";
            dgvBP.Columns["Addr"].HeaderText = "地址";
            dgvBP.Columns["CreditLimit"].HeaderText = "信用額度";
            dgvBP.Columns["GroupID"].HeaderText = "群組代碼";
            dgvBP.Columns["BPGroupName"].HeaderText = "群組";
            //dgvBP.Columns[0].HeaderText = "代碼";
            //dgvBP.Columns[1].HeaderText = "名稱";
            //dgvBP.Columns[2].HeaderText = "電話1";
            //dgvBP.Columns[3].HeaderText = "電話2";
            //dgvBP.Columns[4].HeaderText = "傳真";
            //dgvBP.Columns[5].HeaderText = "地址";
            //dgvBP.Columns[6].HeaderText = "信用額度";
            //dgvBP.Columns[7].HeaderText = "群組代碼";

            //(2)欄位寬度依內容自動調整
            dgvBP.AutoResizeColumns();

            //(3)
            dgvBP.Columns["GroupID"].Visible = false;
        }

        //清除按鈕
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("BP"))
            {
                ds.Tables["BP"].Rows.Clear();
            }

        }

        //儲存變更按鈕
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update(ds.Tables["BP"]);
                MessageBox.Show("修改儲存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改儲存失敗(" + ex.Message + ")");
            }
        }

        private void frmU4_3_DataGridView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ds.HasChanges())
            {
                if(MessageBox.Show("修改資料未存檔, 確定要離開嗎?", "存檔確認", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
