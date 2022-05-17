
namespace PPERP
{
    partial class frmU7_2_BPList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crvBPList = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crBPList1 = new PPERP.crBPList();
            this.SuspendLayout();
            // 
            // crvBPList
            // 
            this.crvBPList.ActiveViewIndex = 0;
            this.crvBPList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvBPList.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvBPList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvBPList.Location = new System.Drawing.Point(0, 0);
            this.crvBPList.Name = "crvBPList";
            this.crvBPList.ReportSource = this.crBPList1;
            this.crvBPList.Size = new System.Drawing.Size(1249, 796);
            this.crvBPList.TabIndex = 0;
            // 
            // frmU7_2_BPList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 796);
            this.Controls.Add(this.crvBPList);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmU7_2_BPList";
            this.Text = "清單式逐筆報表-客戶清單報表";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvBPList;
        private crBPList crBPList1;
    }
}