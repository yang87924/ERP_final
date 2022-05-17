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
    public partial class frmU3_3_SQLCommand : Form
    {
        public frmU3_3_SQLCommand()
        {
            InitializeComponent();
        }

        //執行SQL命令
        private void btnExecuteSQL_Click(object sender, EventArgs e)
        {
            //檢查使用者是否有輸入SQL命令
            if(txtSQL.Text.Trim().Length == 0)
            {
                MessageBox.Show("尚未輸入SQL命令!");
                return;
            }

            try
            {
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行輸入的SQL命令
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand(); 
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = txtSQL.Text;
                //2.3 執行
                cmd.ExecuteNonQuery();  //Insert, Delete, Update命令適用

                MessageBox.Show("成功執行SQL命令!");

                //3.關閉資料庫連線
                dbConn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Server登入失敗! (" + ex.Message + ")");
            }

        }

        //新增
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //檢查使用者是否完整輸入資料
            if (txtGID.Text.Trim().Length == 0 || txtGName.Text.Trim().Length == 0)
            {
                MessageBox.Show("資料輸入不完整!");
                return;
            }

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Insert命令
                errString = "客戶群組新增";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Insert Into BPGroup Values (@gid, @gname)";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());
                cmd.Parameters.AddWithValue("@gname", txtGName.Text.Trim());
                //2.3 執行
                cmd.ExecuteNonQuery();  //Insert, Delete, Update命令適用

                MessageBox.Show("成功執行Insert命令!");

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("(" + ex.Message + ")", errString + "失敗!");
            }
        }

        //修改
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //檢查使用者是否完整輸入資料
            if (txtGID.Text.Trim().Length == 0 || txtGName.Text.Trim().Length == 0)
            {
                MessageBox.Show("資料輸入不完整!");
                return;
            }

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Update命令
                errString = "客戶群組修改";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Update BPGroup Set BPGroupName = @gname Where BPGroupID = @gid";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());
                cmd.Parameters.AddWithValue("@gname", txtGName.Text.Trim());
                //2.3 執行
                //Insert, Delete, Update命令適用
                if(cmd.ExecuteNonQuery() >=1)
                {
                    MessageBox.Show("成功執行Update命令!");
                } else
                {
                    MessageBox.Show("無此代碼的群組!");
                }

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }
        }

        //刪除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //檢查使用者是否完整輸入資料
            if (txtGID.Text.Trim().Length == 0)
            {
                MessageBox.Show("資料輸入不完整!");
                return;
            }

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組刪除";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Delete From BPGroup Where BPGroupID = @gid";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());
                //2.3 執行
                //Insert, Delete, Update命令適用
                if(cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("無此代碼的群組!");
                }
                else
                {
                    MessageBox.Show("成功執行Delete命令!");
                }

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }
        }

        //查詢
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //檢查使用者是否完整輸入資料
            if (txtGID.Text.Trim().Length == 0)
            {
                MessageBox.Show("資料輸入不完整!");
                return;
            }

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組查詢";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select * From BPGroup Where BPGroupID = @gid";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());
                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    txtGName.Text = dr.GetString(1);
                    dr.Close();
                } else
                {
                    MessageBox.Show("沒有指定群組代碼資料");
                    txtGName.Text = "";
                    txtGID.SelectAll();
                    txtGID.Focus();
                }

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }
        }

        //第一筆
        private void btnFirst_Click(object sender, EventArgs e)
        {

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組查詢";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select Top 1 * From BPGroup Order By BPGroupID";
                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtGID.Text = dr.GetString(0);
                    txtGName.Text = dr.GetString(1);
                }
                else
                {
                }
                dr.Close();

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }

        }

        //最後一筆
        private void btnLast_Click(object sender, EventArgs e)
        {
            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組查詢";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select Top 1 * From BPGroup Order By BPGroupID DESC";
                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtGID.Text = dr.GetString(0);
                    txtGName.Text = dr.GetString(1);
                }
                else
                {
                }
                dr.Close();

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }
        }

        //下一筆
        private void btnNext_Click(object sender, EventArgs e)
        {
            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組查詢";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select Top 1 * From BPGroup Where BPGroupID > @gid Order By BPGroupID";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());

                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtGID.Text = dr.GetString(0);
                    txtGName.Text = dr.GetString(1);
                }
                else
                {
                }
                dr.Close();

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }

        }

        //上一筆
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Delete命令
                errString = "客戶群組查詢";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select Top 1 * From BPGroup Where BPGroupID < @gid Order By BPGroupID DESC";
                cmd.Parameters.AddWithValue("@gid", txtGID.Text.Trim());

                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtGID.Text = dr.GetString(0);
                    txtGName.Text = dr.GetString(1);
                }
                else
                {
                }
                dr.Close();

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }
        }

        private void frmU3_3_SQLCommand_Load(object sender, EventArgs e)
        {
            //畫面開啟後, 顯示第一筆(模擬點按第一筆按鈕)
            btnFirst.PerformClick();

        }
    }
}
