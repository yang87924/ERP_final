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
    public partial class frmU4_4_Dataset : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public frmU4_4_Dataset()
        {
            InitializeComponent();
        }

        private void frmU4_4_Dataset_Load(object sender, EventArgs e)
        {
            //1.登入SQL Server, 建立連線
            clsDB newDB = new clsDB();
            dbConn = newDB.loginDB();

            getData();  //下載BP資料

            //顯示第一筆
            showData(1);
        }

        //顯示指定的資料
        private void showData(int rowNo)
        {
            //if (rowNo < 1 || rowNo > ds.Tables["BP"].Rows.Count)
            //{
            //    MessageBox.Show("超出資料數範圍", "輸入錯誤");
            //}
            //else
            //{
                txtCardCode.Text = ds.Tables["BP"].Rows[rowNo - 1]["CardCode"].ToString();
                txtCardName.Text = ds.Tables["BP"].Rows[rowNo - 1]["CardName"].ToString();
                txtPhone1.Text = ds.Tables["BP"].Rows[rowNo - 1]["Phone1"].ToString();
                txtPhone2.Text = ds.Tables["BP"].Rows[rowNo - 1]["Phone2"].ToString();
                txtFax.Text = ds.Tables["BP"].Rows[rowNo - 1]["Fax"].ToString();
                txtAddr.Text = ds.Tables["BP"].Rows[rowNo - 1]["Addr"].ToString();
                txtCreditLimit.Text = ds.Tables["BP"].Rows[rowNo - 1]["CreditLimit"].ToString();
                txtGroupID.Text = ds.Tables["BP"].Rows[rowNo - 1]["GroupID"].ToString();

                txtRowNo.Text = rowNo.ToString();
            //}
        }

        private void frmU4_4_Dataset_FormClosed(object sender, FormClosedEventArgs e)
        {
            //關閉資料庫連線
            dbConn.Close();
        }

        //下載BP資料
        private void getData()
        {
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = dbConn;
            selectCmd.CommandText = "Select * From BP";
            da.SelectCommand = selectCmd;

            da.Fill(ds, "BP");

            lblRowCount.Text = "/ " + ds.Tables["BP"].Rows.Count.ToString();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            showData(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            showData(ds.Tables["BP"].Rows.Count);
        }

        //查詢指定第幾筆的資料
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int rowNo = Convert.ToInt32(txtRowNo.Text);
                if (rowNo < 1 || rowNo > ds.Tables["BP"].Rows.Count)
                {
                    MessageBox.Show("超出資料數範圍", "輸入錯誤");
                }
                else
                {
                    showData(rowNo);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("輸入值錯誤!", "輸入錯誤");
            }
        }

        //下一筆按鈕
        private void btnNext_Click(object sender, EventArgs e)
        {
            int rowNo = Convert.ToInt32(txtRowNo.Text);
            if (rowNo == ds.Tables["BP"].Rows.Count)
            {
                toolStripStatusLabel_BP.Text = "已是最後一筆";
                //MessageBox.Show("已是最後一筆");
            } else
            {
                showData(rowNo+1);
            }
        }

        //上一筆按鈕
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int rowNo = Convert.ToInt32(txtRowNo.Text);
            if (rowNo == 1)
            {
                toolStripStatusLabel_BP.Text = "已是第一筆";
                //MessageBox.Show("已是第一筆");
            }
            else
            {
                showData(rowNo - 1);
            }
        }

        private void frmU4_4_Dataset_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel_BP.Text = "";
        }
    }
}
