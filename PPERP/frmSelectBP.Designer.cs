
namespace PPERP
{
    partial class frmSelectBP
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvBPList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBPList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(87, 564);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 43);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "選  取";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 564);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 43);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取  消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvBPList
            // 
            this.dgvBPList.AllowUserToAddRows = false;
            this.dgvBPList.AllowUserToDeleteRows = false;
            this.dgvBPList.AllowUserToOrderColumns = true;
            this.dgvBPList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBPList.Location = new System.Drawing.Point(48, 34);
            this.dgvBPList.MultiSelect = false;
            this.dgvBPList.Name = "dgvBPList";
            this.dgvBPList.ReadOnly = true;
            this.dgvBPList.RowTemplate.Height = 24;
            this.dgvBPList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBPList.Size = new System.Drawing.Size(333, 510);
            this.dgvBPList.TabIndex = 2;
            this.dgvBPList.DoubleClick += new System.EventHandler(this.dgvBPList_DoubleClick);
            // 
            // frmSelectBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 652);
            this.Controls.Add(this.dgvBPList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSelectBP";
            this.Text = "選取客戶";
            this.Load += new System.EventHandler(this.frmSelectBP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBPList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvBPList;
    }
}