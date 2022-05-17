
namespace PPERP
{
    partial class frmU4_2_ComboBox
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
            this.cbxBPGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBPGroupID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxBPGroup
            // 
            this.cbxBPGroup.FormattingEnabled = true;
            this.cbxBPGroup.Location = new System.Drawing.Point(227, 183);
            this.cbxBPGroup.Name = "cbxBPGroup";
            this.cbxBPGroup.Size = new System.Drawing.Size(181, 24);
            this.cbxBPGroup.TabIndex = 0;
            this.cbxBPGroup.SelectedValueChanged += new System.EventHandler(this.cbxBPGroup_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "客戶群組";
            // 
            // txtBPGroupID
            // 
            this.txtBPGroupID.Location = new System.Drawing.Point(227, 238);
            this.txtBPGroupID.Name = "txtBPGroupID";
            this.txtBPGroupID.Size = new System.Drawing.Size(181, 27);
            this.txtBPGroupID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "選取群組的代碼";
            // 
            // frmU4_2_ComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 508);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPGroupID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxBPGroup);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmU4_2_ComboBox";
            this.Text = "單元 4 C#.Net與SQL Server程式設計 - 2資料表作為ComboBox下拉選單元件內容";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmU4_2_ComboBox_FormClosed);
            this.Load += new System.EventHandler(this.frmU4_2_ComboBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxBPGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBPGroupID;
        private System.Windows.Forms.Label label2;
    }
}