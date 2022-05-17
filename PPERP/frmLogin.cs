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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); //結束系統
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //檢查使用者是否完整輸入資料
            //if (txtID.Text.Trim().Length == 0 ||
            //    txtPwd.Text.Trim().Length ==0)
            //{
            //    MessageBox.Show("帳號/密碼未輸入!");
            //    return;
            //}

            string errString = "";  //錯誤類別
            try
            {
                errString = "SQL Server登入";
                //1.登入SQL Server, 建立連線
                clsDB newDB = new clsDB();
                SqlConnection dbConn = newDB.loginDB();

                //2.執行SQL Select命令
                errString = "帳號/密碼檢查";
                //2.1 宣告變數
                SqlCommand cmd = new SqlCommand();
                //2.2 設定屬性
                cmd.Connection = dbConn;
                cmd.CommandText = "Select * From [User] Where usr_ID = @uid AND password = @pwd";
                cmd.Parameters.AddWithValue("@uid", txtID.Text.Trim());
                cmd.Parameters.AddWithValue("@pwd", txtPwd.Text.Trim());
                //2.3 執行
                //Select命令適用
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (true) //(dr.HasRows)
                {
                    //2.帳密正確
                    //MessageBox.Show("登入成功");
                    //2.1 隱藏登入畫面
                    this.Hide();

                    //2.2 開啟主畫面
                    frmMain myMain = new frmMain();
                    //myMain.Show();    //獨立表單
                    myMain.ShowDialog();    //對話表單

                    //2.3 使用者關閉主畫面後, 
                    //this.Show();          //回到登入畫面
                    Application.Exit();     //結束系統
                }
                else
                {
                    MessageBox.Show("登入失敗");
                }
                dr.Close();

                //3.關閉資料庫連線
                dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errString + "失敗!" + Environment.NewLine + "(" + ex.Message + ")");
            }


            ////1.檢查輸入的帳密是否正確
            //if (txtID.Text.Trim() == "1" &&
            //   txtPwd.Text.Trim() == "2")
            //{
            //    //2.帳密正確
            //    //2.1 隱藏登入畫面
            //    this.Hide();

            //    //2.2 開啟主畫面
            //    frmMain myMain = new frmMain();
            //    //myMain.Show();    //獨立表單
            //    myMain.ShowDialog();    //對話表單

            //    //2.3 使用者關閉主畫面後, 
            //    //this.Show();          //回到登入畫面
            //    Application.Exit();     //結束系統
            //}
            //else
            //{
            //    //3.帳密錯誤, 顯示錯誤訊息, 留在登入畫面
            //    MessageBox.Show("帳號/密碼錯誤, 無法登入");
            //}
        }

    }
}
