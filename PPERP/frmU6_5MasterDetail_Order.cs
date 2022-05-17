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
    public partial class frmU6_5MasterDetail_Order : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter daMain = new SqlDataAdapter();     //訂單主檔使用
        SqlDataAdapter daList = new SqlDataAdapter();       //訂單明細使用
        SqlDataAdapter daStatus = new SqlDataAdapter();     //訂單狀態使用
        DataSet ds = new DataSet();
        int orderID;    //記錄目前顯示的訂單號碼

        public frmU6_5MasterDetail_Order()
        {
            InitializeComponent();
        }

        //開啟畫面時
        private void frmU6_5MasterDetail_Order_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            getData();              //下載資料
            setBinding();           //畫面元件繫結設定
            setSQLCommand_Main();      //設定SQL命令
            setSQLCommand_List();

            setDatagridviewDisplay();   //設定Datagridview顯示格式
        }

        //離開畫面時
        private void frmU6_5MasterDetail_Order_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //下載資料
        private void getData()
        {
            //訂單主檔
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select Top 1 * From OrderMain Order By OrderID DESC";
            daMain.SelectCommand = selectCmd;
            daMain.Fill(ds, "OrderMain");

            orderID = (int) ds.Tables["OrderMain"].Rows[0]["OrderID"];

            //訂單明細
            SqlCommand selectCmdList = new SqlCommand();
            selectCmdList.Connection = dbConn;
            selectCmdList.CommandText = "Select * From OrderList Where OrderID = @OrderID Order By LineID ";
            selectCmdList.Parameters.AddWithValue("@OrderID", orderID);
            daList.SelectCommand = selectCmdList;
            daList.Fill(ds, "OrderList");

            //狀態
            SqlCommand selectCmdGroup = new SqlCommand();
            selectCmdGroup.Connection = dbConn;
            selectCmdGroup.CommandText = "Select * From OrderStatus";
            daStatus.SelectCommand = selectCmdGroup;
            daStatus.Fill(ds, "OrderStatus");
        }

        //畫面元件繫結設定
        private void setBinding()
        {
            //訂單主檔
            //1.BindingSource(bsOrderMain)繫結到ds.Tables["OrderMain"]
            bsOrderMain.DataSource = ds;
            bsOrderMain.DataMember = "OrderMain";
            //2.資料瀏覽列(bnOrder)繫結到BindingSource(bsOrderMain)
            bnOrder.BindingSource = bsOrderMain;
            //3.訂單的各欄位值TextBox繫結到BindingSource(bsOrderMain)
            txtOrderID.DataBindings.Add("Text", bsOrderMain, "OrderID");
            txtCardCode.DataBindings.Add("Text", bsOrderMain, "CardCode");
            txtCardName.DataBindings.Add("Text", bsOrderMain, "CardName");
            txtStatus.DataBindings.Add("Text", bsOrderMain, "Status");
            dtpDocDate.DataBindings.Add("Text", bsOrderMain, "DocDate");
            dtpDueDate.DataBindings.Add("Text", bsOrderMain, "DueDate");
            txtDocTotal.DataBindings.Add("Text", bsOrderMain, "DocTotal");

            //訂單明細
            bsOrderDetail.DataSource = ds;
            bsOrderDetail.DataMember = "OrderList";
            dgvOrderList.DataSource = bsOrderDetail;

            //Status狀態下拉選單繫結
            cbxStatus.DataSource = ds.Tables["OrderStatus"];
            cbxStatus.DisplayMember = "StatusDrpt";
            cbxStatus.ValueMember = "StatusID";
            cbxStatus.DataBindings.Add("SelectedValue", bsOrderMain, "Status");
        }


        //設定DataAdapter的SQL命令
        //訂單主檔
        private void setSQLCommand_Main()
        {
            //2.Insert命令元件
            SqlCommand insertCmd = new SqlCommand();
            insertCmd.Connection = dbConn;
            insertCmd.CommandText = @"Insert Into OrderMain (OrderID, Status, DocDate, DueDate, CardCode, CardName, DocTotal) 
                                      Values (@OrderID, @Status, @DocDate, @DueDate, @CardCode, @CardName, @DocTotal)";
            insertCmd.Parameters.Add("@OrderID", SqlDbType.Int, 20, "OrderID");
            insertCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 1, "Status");
            insertCmd.Parameters.Add("@DocDate", SqlDbType.Date, 0, "DocDate");
            insertCmd.Parameters.Add("@DueDate", SqlDbType.Date, 0, "DueDate");
            insertCmd.Parameters.Add("@CardCode", SqlDbType.NVarChar, 20, "CardCode");
            insertCmd.Parameters.Add("@CardName", SqlDbType.NVarChar, 100, "CardName");
            insertCmd.Parameters.Add("@DocTotal", SqlDbType.Money, 0, "DocTotal");
            daMain.InsertCommand = insertCmd;

            //3.Update命令元件
            SqlCommand updateCmd = new SqlCommand();
            updateCmd.Connection = dbConn;
            updateCmd.CommandText = @"Update OrderMain Set 
                                                    Status = @Status, 
                                                    DocDate = @DocDate, 
                                                    DueDate = @DueDate, 
                                                    CardCode = @CardCode, 
                                                    CardName = @CardName, 
                                                    DocTotal = @DocTotal 
                                      Where OrderID = @OrderID";
            updateCmd.Parameters.Add("@OrderID", SqlDbType.Int, 20, "OrderID");
            updateCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 1, "Status");
            updateCmd.Parameters.Add("@DocDate", SqlDbType.Date, 0, "DocDate");
            updateCmd.Parameters.Add("@DueDate", SqlDbType.Date, 0, "DueDate");
            updateCmd.Parameters.Add("@CardCode", SqlDbType.NVarChar, 20, "CardCode");
            updateCmd.Parameters.Add("@CardName", SqlDbType.NVarChar, 100, "CardName");
            updateCmd.Parameters.Add("@DocTotal", SqlDbType.Money, 0, "DocTotal");
            daMain.UpdateCommand = updateCmd;

            //4.Delete命令元件
            SqlCommand deleteCmd = new SqlCommand();
            deleteCmd.Connection = dbConn;
            deleteCmd.CommandText = "Delete From OrderMain Where OrderID = @OrderID";
            deleteCmd.Parameters.Add("@OrderID", SqlDbType.Int, 0, "OrderID");
            daMain.DeleteCommand = deleteCmd;
        }

        //訂單明細
        private void setSQLCommand_List()
        {
            //2.Insert命令元件
            SqlCommand insertCmd = new SqlCommand();
            insertCmd.Connection = dbConn;
            insertCmd.CommandText = @"Insert Into OrderList (OrderID, LineID, Status, ItemCode, ItemName, Quantity, Price, Discount, DeliveryQty, LineTotal)
                                      Values (@OrderID, @LineID, @Status, @ItemCode, @ItemName, @Quantity, @Price, @Discount, @DeliveryQty, @LineTotal)";
            insertCmd.Parameters.Add("@OrderID", SqlDbType.Int, 0, "OrderID");
            insertCmd.Parameters.Add("@LineID", SqlDbType.Int, 0, "LineID");
            insertCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 1, "Status");
            insertCmd.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 20, "ItemCode");
            insertCmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 100, "ItemName");
            insertCmd.Parameters.Add("@Quantity", SqlDbType.Decimal, 0, "Quantity");
            insertCmd.Parameters.Add("@Price", SqlDbType.Money, 0, "Price");
            insertCmd.Parameters.Add("@Discount", SqlDbType.SmallInt, 0, "Discount");
            insertCmd.Parameters.Add("@DeliveryQty", SqlDbType.Decimal, 0, "DeliveryQty");
            insertCmd.Parameters.Add("@LineTotal", SqlDbType.Money, 0, "LineTotal");
            daList.InsertCommand = insertCmd;

            //3.Update命令元件
            SqlCommand updateCmd = new SqlCommand();
            updateCmd.Connection = dbConn;
            updateCmd.CommandText = @"Update OrderList Set 
                                                    Status = @Status, 
                                                    ItemCode = @ItemCode, 
                                                    ItemName = @ItemName, 
                                                    Quantity = @Quantity, 
                                                    Price = @Price, 
                                                    Discount = @Discount,
                                                    DeliveryQty = @DeliveryQty,
                                                    LineTotal = @LineTotal
                                      Where OrderID = @OrderID AND LineID = @LineID";
            updateCmd.Parameters.Add("@OrderID", SqlDbType.Int, 0, "OrderID");
            updateCmd.Parameters.Add("@LineID", SqlDbType.Int, 0, "LineID");
            updateCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 1, "Status");
            updateCmd.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 20, "ItemCode");
            updateCmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 100, "ItemName");
            updateCmd.Parameters.Add("@Quantity", SqlDbType.Decimal, 0, "Quantity");
            updateCmd.Parameters.Add("@Price", SqlDbType.Money, 0, "Price");
            updateCmd.Parameters.Add("@Discount", SqlDbType.SmallInt, 0, "Discount");
            updateCmd.Parameters.Add("@DeliveryQty", SqlDbType.Decimal, 0, "DeliveryQty");
            updateCmd.Parameters.Add("@LineTotal", SqlDbType.Money, 0, "LineTotal");
            daList.UpdateCommand = updateCmd;

            //4.Delete命令元件
            SqlCommand deleteCmd = new SqlCommand();
            deleteCmd.Connection = dbConn;
            deleteCmd.CommandText = "Delete From OrderList Where OrderID = @OrderID AND LineID = @LineID";
            deleteCmd.Parameters.Add("@OrderID", SqlDbType.Int, 0, "OrderID");
            deleteCmd.Parameters.Add("@LineID", SqlDbType.Int, 0, "LineID");
            daList.DeleteCommand = deleteCmd;
        }


        //狀態選單改變選項
        private void cbxStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbxStatus.SelectedValue != null)
            {
                txtStatus.Text = cbxStatus.SelectedValue.ToString();
            }
        }

        //設定Datagridview顯示格式
        private void setDatagridviewDisplay()
        {
            dgvOrderList.Columns["OrderID"].HeaderText = "訂單號";
            dgvOrderList.Columns["LineID"].HeaderText = "列號";
            dgvOrderList.Columns["Status"].HeaderText = "狀態";
            dgvOrderList.Columns["ItemCode"].HeaderText = "項目代碼";
            dgvOrderList.Columns["ItemName"].HeaderText = "項目名稱";
            dgvOrderList.Columns["Quantity"].HeaderText = "數量";
            dgvOrderList.Columns["Price"].HeaderText = "價格";
            dgvOrderList.Columns["Discount"].HeaderText = "折扣";
            dgvOrderList.Columns["DeliveryQty"].HeaderText = "已交貨量";
            dgvOrderList.Columns["LineTotal"].HeaderText = "小計";
        }

        //第一筆按鈕
        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //1.清除Dataset現有資料
            ds.Tables["OrderMain"].Clear();
            ds.Tables["OrderList"].Clear();

            //2.下載第一筆訂單的主檔與明細資料
            daMain.SelectCommand.CommandText = "Select Top 1 * From OrderMain Order By OrderID";
            daMain.Fill(ds, "OrderMain");

            //3.記錄第一筆訂單單號
            orderID = (int)ds.Tables["OrderMain"].Rows[0]["OrderID"];

            //4.依據第一筆訂單單號下載訂單明細資料
            daList.SelectCommand.CommandText = "Select * From OrderList Where OrderID = @OrderID Order By LineID";
            if (!daList.SelectCommand.Parameters.Contains("@OrderID"))
            {
                daList.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
            }
            else
            {
                daList.SelectCommand.Parameters["@OrderID"].Value = orderID;
            }
            daList.Fill(ds, "OrderList");

        }

        //最後一筆
        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //1.清除Dataset現有資料
            ds.Tables["OrderMain"].Clear();
            ds.Tables["OrderList"].Clear();

            //2.下載第一筆訂單的主檔與明細資料
            daMain.SelectCommand.CommandText = "Select Top 1 * From OrderMain Order By OrderID DESC";
            daMain.Fill(ds, "OrderMain");

            orderID = (int)ds.Tables["OrderMain"].Rows[0]["OrderID"];

            daList.SelectCommand.CommandText = "Select * From OrderList Where OrderID = @OrderID Order By LineID";
            if (!daList.SelectCommand.Parameters.Contains("@OrderID"))
            {
                daList.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
            }
            else
            {
                daList.SelectCommand.Parameters["@OrderID"].Value = orderID;
            }
            daList.Fill(ds, "OrderList");
        }

        //上一筆
        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //1.清除Dataset現有資料
            ds.Tables["OrderMain"].Clear();
            ds.Tables["OrderList"].Clear();

            //2.下載上一筆訂單的主檔與明細資料
            daMain.SelectCommand.CommandText = "SELECT TOP 1 * FROM OrderMain Where OrderID < @OrderID Order By OrderID DESC";
            if (!daMain.SelectCommand.Parameters.Contains("@OrderID"))
            {
                daMain.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
            }
            else
            {
                daMain.SelectCommand.Parameters["@OrderID"].Value = orderID;
            }
            daMain.Fill(ds, "OrderMain");

            //檢查是否已經第一筆
            if (ds.Tables["OrderMain"].Rows.Count == 0)
            {
                bindingNavigatorMoveFirstItem.PerformClick();   //回到第一筆
            } else
            {
                orderID = (int)ds.Tables["OrderMain"].Rows[0]["OrderID"];

                daList.SelectCommand.CommandText = "Select * From OrderList Where OrderID = @OrderID Order By LineID";
                if (!daList.SelectCommand.Parameters.Contains("@OrderID"))
                {
                    daList.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
                }
                else
                {
                    daList.SelectCommand.Parameters["@OrderID"].Value = orderID;
                }
                daList.Fill(ds, "OrderList");
            }
        }

        //下一筆
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //1.清除Dataset現有資料
            ds.Tables["OrderMain"].Clear();
            ds.Tables["OrderList"].Clear();

            //2.下載下一筆訂單的主檔與明細資料
            daMain.SelectCommand.CommandText = "SELECT TOP 1 * FROM OrderMain Where OrderID > @OrderID Order By OrderID";
            if (!daMain.SelectCommand.Parameters.Contains("@OrderID"))
            {
                daMain.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
            }
            else
            {
                daMain.SelectCommand.Parameters["@OrderID"].Value = orderID;
            }
            daMain.Fill(ds, "OrderMain");

            //檢查是否已經最後一筆
            if (ds.Tables["OrderMain"].Rows.Count == 0)
            {
                bindingNavigatorMoveLastItem.PerformClick();   //回到最後一筆
                lblStatus.Text = "已經最後一筆";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                orderID = (int)ds.Tables["OrderMain"].Rows[0]["OrderID"];

                daList.SelectCommand.CommandText = "Select * From OrderList Where OrderID = @OrderID Order By LineID";
                if (!daList.SelectCommand.Parameters.Contains("@OrderID"))
                {
                    daList.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
                }
                else
                {
                    daList.SelectCommand.Parameters["@OrderID"].Value = orderID;
                }
                daList.Fill(ds, "OrderList");
            }
        }

        //存檔按鈕
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //將存在暫存區的修改資料存入Dataset
                bsOrderMain.EndEdit(); 
                bsOrderDetail.EndEdit();

                daMain.Update(ds.Tables["OrderMain"]);
                daList.Update(ds.Tables["OrderList"]);

                //MessageBox.Show("修改儲存成功");
                lblStatus.Text = "存檔成功";
                lblStatus.ForeColor = Color.Blue;

                //存檔時, 如果ds.Tables["OrderMain"]有資料(新增或修改存檔), 記錄dataset中訂單號欄位值為目前單號; 如果沒有資料(刪除), 保留現有目前單號
                orderID = (ds.Tables["OrderMain"].Rows.Count > 0 ? (int)ds.Tables["OrderMain"].Rows[0]["OrderID"] : orderID); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改儲存失敗(" + ex.Message + ")");
            }
        }

        //檢查畫面資料是否有修改, 若有則詢問是否存檔
        private void checkChanged()
        {
            //將存在暫存區的修改資料存入Dataset
            bsOrderMain.EndEdit();
            bsOrderDetail.EndEdit();

            if (ds.HasChanges()) //檢查Dataset內資料是否有修改過
            {
                if (MessageBox.Show("訂單資料已修改, 要先存檔嗎?", "存檔確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnSave.PerformClick(); //啟動點按存檔按鈕
                }
            }
        }

        //新增訂單按鈕
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            checkChanged(); //檢查畫面資料是否有修改, 若有則詢問是否存檔

            //清除現有資料
            ds.Tables["OrderMain"].Rows.Clear();
            ds.Tables["OrderList"].Rows.Clear();
            //新增一筆空白資料
            DataRow newRow = ds.Tables["OrderMain"].NewRow();
            ds.Tables["OrderMain"].Rows.Add(newRow);

            //設定新訂單的預設值
            ds.Tables["OrderMain"].Rows[0]["OrderID"] = nextOrderID();
            ds.Tables["OrderMain"].Rows[0]["Status"] = "O";
        }

        //取得新訂單單號
        private int nextOrderID()
        {
            int nextID = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConn;
            cmd.CommandText = "Select Top 1 OrderID From OrderMain Order By OrderID DESC";
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                dr.Read();
                nextID = dr.GetInt32(0) + 1;
            } else
            {
                nextID = 1;
            }
            dr.Close();

            return nextID;
        }

        //刪除訂單按鈕
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除嗎?", "刪除確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //主檔: 1筆
                ds.Tables["OrderMain"].Rows[0].Delete();

                //明細: 多筆
                //for(int i=0; i < ds.Tables["OrderList"].Rows.Count; i++)
                //{
                //    ds.Tables["OrderList"].Rows[i].Delete();
                //}

                foreach(DataRow row in ds.Tables["OrderList"].Rows)
                {
                    row.Delete();
                }

                btnSave.PerformClick();

                bindingNavigatorMovePreviousItem.PerformClick();    //刪除後, 顯示上一筆
            }
        }

        private void dgvOrderList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            //dgvOrderList.CurrentRow.Cells[0] = Convert.ToInt32(txtOrderID.Text);
        }

        //選取客戶按鈕
        private void btnSelectBP_Click(object sender, EventArgs e)
        {
            frmSelectBP frmBP = new frmSelectBP();
            if(frmBP.ShowDialog() == DialogResult.OK)
            {
                txtCardCode.Text = frmBP.rtnCardCode;
                txtCardName.Text = frmBP.rtnCardName;
            }
        }
    }
}
