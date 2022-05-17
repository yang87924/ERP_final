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
    public partial class frmU5_5_DatabindingOneRecord : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();       //客戶資料使用
        SqlDataAdapter daGroup = new SqlDataAdapter();  //客戶群組使用
        DataSet ds = new DataSet();
        string cardCode;    //記錄目前顯示的客戶代碼

        public frmU5_5_DatabindingOneRecord()
        {
            InitializeComponent();
        }

        //開啟畫面時
        private void frmU5_5_DatabindingOneRecord_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            getData();  //下載資料
            setBinding();   //畫面元件繫結設定
            setSQLCommand();

            cardCode = txtCardCode.Text.Trim();
        }

        //下載資料
        private void getData()
        {
            //BP
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select Top 1 * From BP Order By CardCode";
            da.SelectCommand = selectCmd;
            da.Fill(ds, "BP");


            //BPGroup
            SqlCommand selectCmdGroup = new SqlCommand();
            selectCmdGroup.Connection = dbConn;
            selectCmdGroup.CommandText = "Select * From BPGroup";
            daGroup.SelectCommand = selectCmdGroup;
            daGroup.Fill(ds, "BPGroup");
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

        //離開畫面時
        private void frmU5_5_DatabindingOneRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //第一筆
        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["BP"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From BP Order By CardCode";
            da.Fill(ds, "BP");
            cardCode = txtCardCode.Text.Trim();
        }
        //最後一筆
        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["BP"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From BP Order By CardCode DESC";
            da.Fill(ds, "BP");
            cardCode = txtCardCode.Text.Trim();
        }
        //上一筆
        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["BP"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From BP Where CardCode < @CardCode Order By CardCode DESC";
            if (!da.SelectCommand.Parameters.Contains("@CardCode"))
            {
                da.SelectCommand.Parameters.AddWithValue("@CardCode", cardCode);
            }
            else
            {
                da.SelectCommand.Parameters["@CardCode"].Value = cardCode;
            }
            da.Fill(ds, "BP");

            //檢查是否已達第一筆
            if(ds.Tables["BP"].Rows.Count == 0)
            {
                bindingNavigatorMoveFirstItem.PerformClick();
            }
            cardCode = txtCardCode.Text.Trim();
        }
        //下一筆
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["BP"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From BP Where CardCode > @CardCode Order By CardCode";
            if(!da.SelectCommand.Parameters.Contains("@CardCode"))
            {
                da.SelectCommand.Parameters.AddWithValue("@CardCode", cardCode);
            } else
            {
                da.SelectCommand.Parameters["@CardCode"].Value = cardCode;
            }
            da.Fill(ds, "BP");

            //檢查是否已達最後一筆
            if (ds.Tables["BP"].Rows.Count == 0)
            {
                bindingNavigatorMoveLastItem.PerformClick();
            }

            cardCode = txtCardCode.Text.Trim();
        }

        //群組選取項改變時連動群組代碼
        private void cbxBPGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbxBPGroup.SelectedValue != null)
            {
                txtGroupID.Text = cbxBPGroup.SelectedValue.ToString();
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

                cardCode = txtCardCode.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改儲存失敗(" + ex.Message + ")");
            }
        }

        //新增按鈕
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //清除現有資料
            ds.Tables["BP"].Rows.Clear();
            //新增一筆空白資料
            DataRow newRow = ds.Tables["BP"].NewRow();
            ds.Tables["BP"].Rows.Add(newRow);
        }

        //刪除按鈕
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("確定要刪除嗎?", "刪除確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ds.Tables["BP"].Rows[0].Delete();
                btnSave.PerformClick();

                bindingNavigatorMovePreviousItem.PerformClick();    //刪除後, 顯示上一筆
            }
        }

        //檢查畫面資料是否有修改, 若有則詢問是否存檔
        private void checkChanged()
        {
            bsBP.EndEdit(); //將使用者在畫面元件中輸入的修改值存回Dataset

            if(ds.HasChanges()) //檢查Dataset內資料是否有修改過
            {
                if(MessageBox.Show("客戶資料已修改, 要先存檔嗎?", "存檔確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnSave.PerformClick(); //啟動點按存檔按鈕
                }
            }
        }

        //新增時, 客戶代碼檢查是否重複
        private void txtCardCode_Validating(object sender, CancelEventArgs e)
        {
            //新增時才檢查
            if(ds.Tables["BP"].Rows[0].RowState == DataRowState.Added)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbConn;
                cmd.CommandText = "Select * From BP Where CardCode = @CardCode";
                cmd.Parameters.AddWithValue("@CardCode", txtCardCode.Text.Trim());
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("輸入的客戶代碼重複, 請修改!");
                    txtCardCode.Focus();
                }
                dr.Close();
            }
        }

        private void txtCreditLimit_Validating(object sender, CancelEventArgs e)
        {
            
        }
    }

}
