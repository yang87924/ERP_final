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
    public partial class frmU5_2_Databinding : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();       //客戶資料使用
        SqlDataAdapter daGroup = new SqlDataAdapter();  //客戶群組使用
        DataSet ds = new DataSet();

        public frmU5_2_Databinding()
        {
            InitializeComponent();
        }

        //開啟畫面時
        private void frmU5_2_Databinding_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            getData();  //下載BP資料

            setBinding();   //畫面元件繫結設定

            setSQLCommand();
        }

        //下載資料
        private void getData()
        {
            //BP
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select * From BP";
            da.SelectCommand = selectCmd;
            da.Fill(ds, "BP");

            //BPGroup
            SqlCommand selectCmdGroup = new SqlCommand();
            selectCmdGroup.Connection = dbConn;
            selectCmdGroup.CommandText = "Select * From BPGroup";
            daGroup.SelectCommand = selectCmdGroup;
            daGroup.Fill(ds, "BPGroup");
        }

        //離開畫面時
        private void frmU5_2_Databinding_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //畫面元件繫結設定
        private void setBinding()
        {
            //1.BindingSource(bsBP)繫結到ds.Tables["BP"]
            bsBP.DataSource = ds;
            bsBP.DataMember = "BP";
            //2.資料瀏覽列(bnBP)繫結到BindingSource(bsBP)
            bnBP.BindingSource = bsBP;
            //3.客戶的各欄位值TextBox繫結到BindingSource(bsBP)
            txtCardCode.DataBindings.Add("Text", bsBP, "CardCode");
            txtCardName.DataBindings.Add("Text", bsBP, "CardName");
            txtPhone1.DataBindings.Add("Text", bsBP, "Phone1");
            txtPhone2.DataBindings.Add("Text", bsBP, "Phone2");
            txtFax.DataBindings.Add("Text", bsBP, "Fax");
            txtAddr.DataBindings.Add("Text", bsBP, "Addr");
            txtCreditLimit.DataBindings.Add("Text", bsBP, "CreditLimit");
            txtGroupID.DataBindings.Add("Text", bsBP, "GroupID");

            //BPGroup下拉選單繫結
            cbxBPGroup.DataSource = ds.Tables["BPGroup"];
            cbxBPGroup.DisplayMember = "BPGroupName";
            cbxBPGroup.ValueMember = "BPGroupID";
            cbxBPGroup.DataBindings.Add("SelectedValue", bsBP, "GroupID");
        }

        //客戶群組選單改變選項
        private void cbxBPGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbxBPGroup.SelectedValue != null) 
            {
                txtGroupID.Text = cbxBPGroup.SelectedValue.ToString().Trim();
            }
        }

        //設定DataAdapter的SQL命令
        private void setSQLCommand()
        {
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

        //存檔按鈕
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bsBP.EndEdit(); //將存在暫存區的修改資料存入Dataset
                da.Update(ds.Tables["BP"]);
                MessageBox.Show("修改儲存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改儲存失敗(" + ex.Message + ")");
            }
        }
    }
}
