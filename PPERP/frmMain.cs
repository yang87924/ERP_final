using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPERP
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void 連線登入SQLServer資料庫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU3_2_SQLServer newForm = new frmU3_2_SQLServer();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void sqlCommand元件資料庫存取架構ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU3_3_SQLCommand newForm = new frmU3_3_SQLCommand();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void dataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU4_3_DataGridView newForm = new frmU4_3_DataGridView();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void 下拉選單ComboBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU4_2_ComboBox newForm = new frmU4_2_ComboBox();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void bP逐筆瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU4_4_Dataset newForm = new frmU4_4_Dataset();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void 資料繫結瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU5_2_Databinding newForm = new frmU5_2_Databinding();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmU5_5_DatabindingOneRecord newForm = new frmU5_5_DatabindingOneRecord();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void 主檔明細訂單ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU6_5MasterDetail_Order newForm = new frmU6_5MasterDetail_Order();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void 清單式逐筆報表客戶清單ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU7_2_BPList newForm = new frmU7_2_BPList();
            newForm.MdiParent = this;
            newForm.Show();

        }

        private void 群組式報表訂單明細ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmU7_3_OrderList newForm = new frmU7_3_OrderList();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void 期末考ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _2021_期末考_4A690103 newForm = new _2021_期末考_4A690103();
            newForm.MdiParent = this;
            newForm.Show();
        }
    }
}
