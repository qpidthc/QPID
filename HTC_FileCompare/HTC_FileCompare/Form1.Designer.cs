namespace HTC_FileCompare
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAInfo = new System.Windows.Forms.Label();
            this.btnA = new System.Windows.Forms.Button();
            this.txtA = new System.Windows.Forms.TextBox();
            this.pBarA = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBInfo = new System.Windows.Forms.Label();
            this.btnB = new System.Windows.Forms.Button();
            this.txtB = new System.Windows.Forms.TextBox();
            this.pBarB = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIgnore = new System.Windows.Forms.CheckBox();
            this.lblFinally = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCmp = new System.Windows.Forms.Button();
            this.pBarCmp = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblAInfo);
            this.groupBox1.Controls.Add(this.btnA);
            this.groupBox1.Controls.Add(this.txtA);
            this.groupBox1.Controls.Add(this.pBarA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "選擇檔案";
            // 
            // lblAInfo
            // 
            this.lblAInfo.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.lblAInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblAInfo.Location = new System.Drawing.Point(394, 15);
            this.lblAInfo.Name = "lblAInfo";
            this.lblAInfo.Size = new System.Drawing.Size(171, 20);
            this.lblAInfo.TabIndex = 4;
            this.lblAInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnA
            // 
            this.btnA.ForeColor = System.Drawing.Color.Blue;
            this.btnA.Location = new System.Drawing.Point(571, 38);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(55, 30);
            this.btnA.TabIndex = 3;
            this.btnA.Text = "...";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(74, 38);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(491, 34);
            this.txtA.TabIndex = 2;
            // 
            // pBarA
            // 
            this.pBarA.Location = new System.Drawing.Point(32, 80);
            this.pBarA.Name = "pBarA";
            this.pBarA.Size = new System.Drawing.Size(594, 23);
            this.pBarA.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "A 檔";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblBInfo);
            this.groupBox2.Controls.Add(this.btnB);
            this.groupBox2.Controls.Add(this.txtB);
            this.groupBox2.Controls.Add(this.pBarB);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 121);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "選擇檔案";
            // 
            // lblBInfo
            // 
            this.lblBInfo.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.lblBInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblBInfo.Location = new System.Drawing.Point(394, 14);
            this.lblBInfo.Name = "lblBInfo";
            this.lblBInfo.Size = new System.Drawing.Size(171, 20);
            this.lblBInfo.TabIndex = 6;
            this.lblBInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnB
            // 
            this.btnB.ForeColor = System.Drawing.Color.Blue;
            this.btnB.Location = new System.Drawing.Point(571, 37);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(55, 30);
            this.btnB.TabIndex = 5;
            this.btnB.Text = "...";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(74, 37);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(491, 34);
            this.txtB.TabIndex = 4;
            // 
            // pBarB
            // 
            this.pBarB.Location = new System.Drawing.Point(32, 78);
            this.pBarB.Name = "pBarB";
            this.pBarB.Size = new System.Drawing.Size(594, 23);
            this.pBarB.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "B 檔";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chkIgnore);
            this.groupBox3.Controls.Add(this.lblFinally);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnCmp);
            this.groupBox3.Controls.Add(this.pBarCmp);
            this.groupBox3.Location = new System.Drawing.Point(12, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(648, 168);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "比對";
            // 
            // chkIgnore
            // 
            this.chkIgnore.AutoSize = true;
            this.chkIgnore.Location = new System.Drawing.Point(33, 75);
            this.chkIgnore.Name = "chkIgnore";
            this.chkIgnore.Size = new System.Drawing.Size(134, 29);
            this.chkIgnore.TabIndex = 5;
            this.chkIgnore.Text = "忽略大小寫";
            this.chkIgnore.UseVisualStyleBackColor = true;
            // 
            // lblFinally
            // 
            this.lblFinally.ForeColor = System.Drawing.Color.Blue;
            this.lblFinally.Location = new System.Drawing.Point(288, 36);
            this.lblFinally.Name = "lblFinally";
            this.lblFinally.Size = new System.Drawing.Size(338, 25);
            this.lblFinally.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(153, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCmp
            // 
            this.btnCmp.ForeColor = System.Drawing.Color.Blue;
            this.btnCmp.Location = new System.Drawing.Point(32, 28);
            this.btnCmp.Name = "btnCmp";
            this.btnCmp.Size = new System.Drawing.Size(115, 40);
            this.btnCmp.TabIndex = 2;
            this.btnCmp.Text = "比對";
            this.btnCmp.UseVisualStyleBackColor = true;
            this.btnCmp.Click += new System.EventHandler(this.btnCmp_Click);
            // 
            // pBarCmp
            // 
            this.pBarCmp.Location = new System.Drawing.Point(32, 118);
            this.pBarCmp.Name = "pBarCmp";
            this.pBarCmp.Size = new System.Drawing.Size(594, 23);
            this.pBarCmp.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 444);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTC File Compare";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar pBarA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar pBarB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCmp;
        private System.Windows.Forms.ProgressBar pBarCmp;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Label lblAInfo;
        private System.Windows.Forms.Label lblBInfo;
        private System.Windows.Forms.Label lblFinally;
        private System.Windows.Forms.CheckBox chkIgnore;
        private System.Windows.Forms.Timer timer1;
    }
}

