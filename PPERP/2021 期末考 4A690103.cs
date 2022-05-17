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
    public partial class _2021_期末考_4A690103 : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataAdapter daGroup = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string EvtCode;    //記錄目前顯示的客戶代碼
        public _2021_期末考_4A690103()
        {
            InitializeComponent();
        }

        private void _2021_期末考_4A690103_Load(object sender, EventArgs e)
        {
            //登入資料庫建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

           // setSQLCommand();
            getData();  //下載BPGroup資料
            setBinding();//畫面元件繫節設定
            EvtCode = txtEvtCode.Text.Trim();
        }
        //下載資料
        private void getData()
        {
            //Items
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select Top 1 * From Events Order By EvtCode";
            da.SelectCommand = selectCmd;
            da.Fill(ds, "Events");


            //OWHS
            SqlCommand selectCmdGroup = new SqlCommand();
            selectCmdGroup.Connection = dbConn;
            selectCmdGroup.CommandText = "Select * From EventType";
            daGroup.SelectCommand = selectCmdGroup;
            daGroup.Fill(ds, "EventType");
        }
        private void setBinding()
        {
            //1.先設定繫節來源(BindingSoirce)至BP Datatable
            bsBP.DataSource = ds;
            bsBP.DataMember = "Events";
            //2.設定繫節瀏覽列元件(BindingNavigator)的資料繫結
            bnBP.BindingSource = bsBP;
            //3設定各TextBox元件的資料繫結
            txtEvtCode.DataBindings.Add("Text", bsBP, "EvtCode");
            txtEvTitle.DataBindings.Add("Text", bsBP, "EvTitle");
            txtETID.DataBindings.Add("Text", bsBP, "ETID");
            txtEvtDate.DataBindings.Add("Text", bsBP, "EvtDate");
            txtBeginTime.DataBindings.Add("Text", bsBP, "BeginTime");
            txtEndTime.DataBindings.Add("Text", bsBP, "EndTime");
            txtEvtContent.DataBindings.Add("Text", bsBP, "EvtContent");


            cbxEventType.DataSource = ds.Tables["EventType"];
            cbxEventType.DisplayMember = "ETName";
            cbxEventType.ValueMember = "ETID";
            cbxEventType.DataBindings.Add("SelectedValue", bsBP, "ETID");


        }//畫面元件繫結設定

        private void _2021_期末考_4A690103_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["Events"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From Events Order By EvtCode";
            da.Fill(ds, "Events");
            EvtCode = txtEvtCode.Text.Trim();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["Events"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From Events Order By EvtCode DESC";
            da.Fill(ds, "Events");
            EvtCode = txtEvtCode.Text.Trim();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["Events"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From Events Where EvtCode < @EvtCode Order By EvtCode DESC";
            if (!da.SelectCommand.Parameters.Contains("@EvtCode"))
            {
                da.SelectCommand.Parameters.AddWithValue("@EvtCode", EvtCode);
            }
            else
            {
                da.SelectCommand.Parameters["@EvtCode"].Value = EvtCode;
            }
            da.Fill(ds, "Events");

            //檢查是否已達第一筆
            if (ds.Tables["Events"].Rows.Count == 0)
            {
                bindingNavigatorMoveFirstItem.PerformClick();
            }
            EvtCode = txtEvtCode.Text.Trim();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            ds.Tables["Events"].Clear();

            da.SelectCommand.CommandText = "Select Top 1 * From Events Where EvtCode > @EvtCode Order By EvtCode";
            if (!da.SelectCommand.Parameters.Contains("@EvtCode"))
            {
                da.SelectCommand.Parameters.AddWithValue("@EvtCode", EvtCode);
            }
            else
            {
                da.SelectCommand.Parameters["@EvtCode"].Value = EvtCode;
            }
            da.Fill(ds, "Events");

            //檢查是否已達最後一筆
            if (ds.Tables["Events"].Rows.Count == 0)
            {
                bindingNavigatorMoveLastItem.PerformClick();
            }

            EvtCode = txtEvtCode.Text.Trim();
        }

        private void cbxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEventType.SelectedValue != null)
            {
                txtETID.Text = cbxEventType.SelectedValue.ToString();
            }
        }
        //設定DataAdapter的SQL命令
        private void setSQLCommand()
        {
            //2.Insert命令元件
            SqlCommand insertCmd = new SqlCommand();
            insertCmd.Connection = dbConn;
            insertCmd.CommandText = "Insert Into Events Values (@EvtCode, @EvTitle, @ETID, @EvtDate, @BeginTime, @EndTime, @EvtContent)";
            insertCmd.Parameters.Add("@EvtCode", SqlDbType.Decimal, 20, "EvtCode");
            insertCmd.Parameters.Add("@EvTitle", SqlDbType.NVarChar, 100, "EvTitle");
            insertCmd.Parameters.Add("@ETID", SqlDbType.Char, 3, "ETID");
            insertCmd.Parameters.Add("@EvtDate", SqlDbType.Date, 20, "EvtDate");
            insertCmd.Parameters.Add("@BeginTime", SqlDbType.Time, 7, "BeginTime");
            insertCmd.Parameters.Add("@EndTime", SqlDbType.Time, 7, "EndTime");
            insertCmd.Parameters.Add("@EvtContent", SqlDbType.NVarChar, 255, "EvtContent");
            da.InsertCommand = insertCmd;

            //3.Update命令元件
            SqlCommand updateCmd = new SqlCommand();
            updateCmd.Connection = dbConn;
            updateCmd.CommandText = @"Update Events Set EvtCode = @EvtCode, 
                                                    EvTitle = @EvTitle, 
                                                    ETID = @ETID, 
                                                    EvtDate = @EvtDate, 
                                                    BeginTime = @BeginTime, 
                                                    EndTime = @EndTime, 
                                                    EvtContent = @EvtContent
                                      Where EvtCode = @EvtCode";
            updateCmd.Parameters.Add("@EvtCode", SqlDbType.Decimal, 20, "EvtCode");
            updateCmd.Parameters.Add("@EvTitle", SqlDbType.NVarChar, 100, "EvTitle");
            updateCmd.Parameters.Add("@ETID", SqlDbType.Char, 3, "ETID");
            updateCmd.Parameters.Add("@EvtDate", SqlDbType.Date, 20, "EvtDate");
            updateCmd.Parameters.Add("@BeginTime", SqlDbType.Time, 7, "BeginTime");
            updateCmd.Parameters.Add("@EndTime", SqlDbType.Time, 7, "EndTime");
            updateCmd.Parameters.Add("@EvtContent", SqlDbType.NVarChar, 255, "EvtContent");
            da.UpdateCommand = updateCmd;

            //4.Delete命令元件
            SqlCommand deleteCmd = new SqlCommand();
            deleteCmd.Connection = dbConn;
            deleteCmd.CommandText = "Delete From Events Where EvtCode = @EvtCode";
            deleteCmd.Parameters.Add("@EvtCode", SqlDbType.Decimal, 20, "EvtCode");
            da.DeleteCommand = deleteCmd;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bsBP.EndEdit(); //將存在暫存區的修改資料存入Dataset
                da.Update(ds.Tables["Events"]);
                toolStripStatusLabel_BP.Text = "修改儲存成功";
                //MessageBox.Show("修改儲存成功");

                EvtCode = txtEvtCode.Text.Trim();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel_BP.Text = "修改儲存失敗(" + ex.Message + ")";
                //MessageBox.Show("修改儲存失敗(" + ex.Message + ")");
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //清除現有資料
            ds.Tables["Events"].Rows.Clear();
            //新增一筆空白資料
            DataRow newRow = ds.Tables["Events"].NewRow();
            ds.Tables["Events"].Rows.Add(newRow);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除嗎?", "刪除確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ds.Tables["Events"].Rows[0].Delete();
                btnSave.PerformClick();

                bindingNavigatorMovePreviousItem.PerformClick();    //刪除後, 顯示上一筆
            }
        }
        //檢查畫面資料是否有修改, 若有則詢問是否存檔
        private void checkChanged()
        {
            bsBP.EndEdit(); //將使用者在畫面元件中輸入的修改值存回Dataset

            if (ds.HasChanges()) //檢查Dataset內資料是否有修改過
            {
                if (MessageBox.Show("客戶資料已修改, 要先存檔嗎?", "存檔確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnSave.PerformClick(); //啟動點按存檔按鈕
                }
            }
        }

        private void txtEvtCode_Validating(object sender, CancelEventArgs e)
        {
            //新增時才檢查
            if (ds.Tables["Events"].Rows[0].RowState == DataRowState.Added)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbConn;
                cmd.CommandText = "Select * From Events Where EvtCode = @EvtCode";
                cmd.Parameters.AddWithValue("@EvtCode", txtEvtCode.Text.Trim());
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    toolStripStatusLabel_BP.Text = "輸入的客戶代碼重複, 請修改!";
                    //MessageBox.Show("輸入的客戶代碼重複, 請修改!");
                    txtEvtCode.Focus();
                }
                dr.Close();
            }
        }
    }
}
