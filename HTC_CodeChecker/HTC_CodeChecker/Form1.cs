using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace HTC_CodeChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files|*.txt";
            openDialog.Title = "選擇 PIN 檔案";
            
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = openDialog.FileName;                
                ReadPinFile(txtFile.Text);
                lblOrignal.Text = string.Format("原始檔案內容 {0}筆", lsOrignal.Items.Count);
            }
        }

        private void ReadPinFile(string file)
        {
            if(!File.Exists(file))
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            lsOrignal.Items.Clear();
            int iCount = 0;
            StreamReader streamR = new StreamReader(file, System.Text.Encoding.Default);
            string strLine;            
            while ((strLine = streamR.ReadLine()) != null)
            {
                lsOrignal.Items.Add(strLine);
                iCount++;
            }
            streamR.Close();
            Cursor = Cursors.Default;
        }

        private void btnStrat_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            StartAnalysis();
            Cursor = Cursors.Default;
        }

        private void StartAnalysis()
        {
            lsSingle.Items.Clear();
            lsDuplicate.Items.Clear();
            Cursor = Cursors.WaitCursor;

            DataTable codeTable = new System.Data.DataTable();
            //codeTable.CaseSensitive = true;
            codeTable.Columns.Add("code");
            DataRow newRow;          
            //System.Collections.ArrayList singleList = new System.Collections.ArrayList();
            //System.Collections.ArrayList duplicateList = new System.Collections.ArrayList();
            System.Collections.ArrayList removeStone = new System.Collections.ArrayList();

            DataRow[] selRows;
            foreach (object code in lsOrignal.Items)
            {
                newRow = codeTable.NewRow();
                newRow[0] = code.ToString();
                codeTable.Rows.Add(newRow);
            }

            string strCode;
            progressBar1.Value = 0;
            progressBar1.Maximum = codeTable.Rows.Count;
            int iCount = 0;
            bool bFind = false;

            foreach (DataRow row in codeTable.Rows)
            {
                strCode = row[0].ToString();
                selRows = codeTable.Select("code='" + strCode + "'");
                if (selRows.Length > 1)
                {
                    //duplicate key                   
                    for (int i = 1; i < selRows.Length; i++)
                    {
                        if(removeStone.IndexOf(selRows[i]) < 0)
                        {
                            removeStone.Add(selRows[i]);
                        }                        
                    }
                }
                iCount++;
                progressBar1.Value = iCount;
                Application.DoEvents();
            }

            foreach (DataRow row in removeStone)
            {
                lsDuplicate.Items.Add(row[0].ToString());
            }

            foreach (DataRow row in removeStone)
            {
                codeTable.Rows.Remove(row);
            }

            foreach (DataRow row in codeTable.Rows)
            {
                lsSingle.Items.Add(row[0].ToString());
            }

            

            //lsSingle.DataSource = codeTable;
            //lsDuplicate.DataSource = removeStone;
            codeTable.Dispose();

            lblSingle.Text = string.Format("獨立資料內容  {0}筆 (儲存檔案 pin_checked.txt)",
                    lsSingle.Items.Count);
            lblDuplicate.Text = string.Format("重複資料內容  {0}筆 (儲存檔案 pin_duplicate.txt)",
                    lsDuplicate.Items.Count);
            Cursor = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "儲存檔案";

            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strPath = folderDialog.SelectedPath;
                SaveFile(strPath);
            }
        }

        private void SaveFile(string path)
        {
            try
            {
                string strFile1 = path + "\\pin_checked.txt";
                StreamWriter streamW = new StreamWriter(strFile1);
                foreach (object line in lsSingle.Items)
                {
                    streamW.WriteLine(line);
                }
                streamW.Close();

                string strFile2 = path + "\\pin_duplicate.txt";
                streamW = new StreamWriter(strFile2);
                foreach (object line in lsDuplicate.Items)
                {
                    streamW.WriteLine(line);
                }
                streamW.Close();

                MessageBox.Show("儲 存 完 成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
