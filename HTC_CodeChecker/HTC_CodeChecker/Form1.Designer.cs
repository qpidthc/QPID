namespace HTC_CodeChecker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lsOrignal = new System.Windows.Forms.ListBox();
            this.lblOrignal = new System.Windows.Forms.Label();
            this.btnStrat = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lsSingle = new System.Windows.Forms.ListBox();
            this.lsDuplicate = new System.Windows.Forms.ListBox();
            this.lblSingle = new System.Windows.Forms.Label();
            this.lblDuplicate = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.txtFile);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(987, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PIN_檔案";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(27, 41);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(822, 31);
            this.txtFile.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(867, 35);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(87, 43);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "選取";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lsOrignal
            // 
            this.lsOrignal.FormattingEnabled = true;
            this.lsOrignal.ItemHeight = 23;
            this.lsOrignal.Location = new System.Drawing.Point(13, 168);
            this.lsOrignal.Name = "lsOrignal";
            this.lsOrignal.Size = new System.Drawing.Size(519, 579);
            this.lsOrignal.TabIndex = 1;
            // 
            // lblOrignal
            // 
            this.lblOrignal.AutoSize = true;
            this.lblOrignal.ForeColor = System.Drawing.Color.Blue;
            this.lblOrignal.Location = new System.Drawing.Point(12, 142);
            this.lblOrignal.Name = "lblOrignal";
            this.lblOrignal.Size = new System.Drawing.Size(124, 24);
            this.lblOrignal.TabIndex = 2;
            this.lblOrignal.Text = "原始檔案內容";
            // 
            // btnStrat
            // 
            this.btnStrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStrat.Location = new System.Drawing.Point(770, 133);
            this.btnStrat.Name = "btnStrat";
            this.btnStrat.Size = new System.Drawing.Size(108, 43);
            this.btnStrat.TabIndex = 3;
            this.btnStrat.Text = "開始分析";
            this.btnStrat.UseVisualStyleBackColor = true;
            this.btnStrat.Click += new System.EventHandler(this.btnStrat_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(892, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 43);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "儲存檔案";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lsSingle
            // 
            this.lsSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lsSingle.FormattingEnabled = true;
            this.lsSingle.ItemHeight = 23;
            this.lsSingle.Location = new System.Drawing.Point(539, 211);
            this.lsSingle.Name = "lsSingle";
            this.lsSingle.Size = new System.Drawing.Size(461, 326);
            this.lsSingle.TabIndex = 5;
            // 
            // lsDuplicate
            // 
            this.lsDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lsDuplicate.FormattingEnabled = true;
            this.lsDuplicate.ItemHeight = 23;
            this.lsDuplicate.Location = new System.Drawing.Point(539, 578);
            this.lsDuplicate.Name = "lsDuplicate";
            this.lsDuplicate.Size = new System.Drawing.Size(461, 165);
            this.lsDuplicate.TabIndex = 6;
            // 
            // lblSingle
            // 
            this.lblSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSingle.AutoSize = true;
            this.lblSingle.ForeColor = System.Drawing.Color.Blue;
            this.lblSingle.Location = new System.Drawing.Point(538, 184);
            this.lblSingle.Name = "lblSingle";
            this.lblSingle.Size = new System.Drawing.Size(361, 24);
            this.lblSingle.TabIndex = 7;
            this.lblSingle.Text = "獨立資料內容 (儲存檔案 pin_checked.txt)";
            // 
            // lblDuplicate
            // 
            this.lblDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuplicate.AutoSize = true;
            this.lblDuplicate.ForeColor = System.Drawing.Color.Blue;
            this.lblDuplicate.Location = new System.Drawing.Point(538, 548);
            this.lblDuplicate.Name = "lblDuplicate";
            this.lblDuplicate.Size = new System.Drawing.Size(365, 24);
            this.lblDuplicate.TabIndex = 8;
            this.lblDuplicate.Text = "重複資料內容(儲存檔案 pin_duplicate.txt)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(27, 79);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(822, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 766);
            this.Controls.Add(this.lblDuplicate);
            this.Controls.Add(this.lblSingle);
            this.Controls.Add(this.lsDuplicate);
            this.Controls.Add(this.lsSingle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStrat);
            this.Controls.Add(this.lblOrignal);
            this.Controls.Add(this.lsOrignal);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THC Code Checker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.ListBox lsOrignal;
        private System.Windows.Forms.Label lblOrignal;
        private System.Windows.Forms.Button btnStrat;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lsSingle;
        private System.Windows.Forms.ListBox lsDuplicate;
        private System.Windows.Forms.Label lblSingle;
        private System.Windows.Forms.Label lblDuplicate;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

