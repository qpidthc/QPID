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

namespace HTC_FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            completeHandler = Complete;
        }

        private System.Collections.Generic.List<string> listA = new List<string>();
        private System.Collections.Generic.List<string> listB = new List<string>();
        private System.Collections.Generic.List<string> listRemove = new List<string>();

        private int iOrignalCount = 0;
        private int iFinallyCount = 0;
        long lCount = 0;

        EventHandler completeHandler;

        private void btnA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files|*.txt|All files (*.*)|*.*";
            openDlg.Title = "選擇A檔";

            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listA.Clear();
                txtA.Text = openDlg.FileName;
                Cursor = Cursors.WaitCursor;
                try
                {
                    FileInfo fInfo = new FileInfo(openDlg.FileName);
                    long fLength = fInfo.Length > 0 ? fInfo.Length : 0;                    
                    StreamReader stReader = new StreamReader(openDlg.OpenFile());                    
                    string strLine;
                    long fReadLength = 0;                 
                    pBarA.Value = 0;
                    lblAInfo.Text = "";
                    while ((strLine = stReader.ReadLine()) != null)
                    {
                        listA.Add(strLine);                        
                        fReadLength += strLine.Length;
                        pBarA.Value = (int)( fReadLength * 100 / fLength);
                        Application.DoEvents();
                    }
                    pBarA.Value = 100;
                    iOrignalCount = listA.Count;
                    lblAInfo.Text = string.Format("共{0}筆資料", listA.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files|*.txt|All files (*.*)|*.*";
            openDlg.Title = "選擇B檔";

            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listB.Clear();
                txtB.Text = openDlg.FileName;
                Cursor = Cursors.WaitCursor;
                try
                {
                    FileInfo fInfo = new FileInfo(openDlg.FileName);
                    long fLength = fInfo.Length > 0 ? fInfo.Length : 0;
                    StreamReader stReader = new StreamReader(openDlg.OpenFile());
                    string strLine;
                    long fReadLength = 0;                   
                    pBarB.Value = 0;
                    lblBInfo.Text = "";
                    while ((strLine = stReader.ReadLine()) != null)
                    {
                        listB.Add(strLine);
                        fReadLength += strLine.Length;
                        pBarB.Value = (int)(fReadLength *100 / fLength);
                        Application.DoEvents();
                    }
                    pBarB.Value = 100;
                    lblBInfo.Text = string.Format("共{0}筆資料", listB.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Default;
            }
        }

        private void btnCmp_Click(object sender, EventArgs e)
        {
           
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnCmp.Enabled = false;
            btnSave.Enabled = false;

            Cursor = Cursors.WaitCursor;
            if (chkIgnore.Checked)
            {
                System.Threading.Thread thd = new System.Threading.Thread(ComapreIgnore);
                thd.IsBackground = true;
                thd.Start();
            }
            else
            {
                System.Threading.Thread thd = new System.Threading.Thread(Comapre);
                thd.IsBackground = true;
                thd.Start();
            }            
            Cursor = Cursors.Default;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Complete(object sender, EventArgs e)
        {
            MessageBox.Show("比 對 完 成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnA.Enabled = true;
            btnB.Enabled = true;
            btnCmp.Enabled = true;
            btnSave.Enabled = true;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Comapre()
        {
            try
            {
                listRemove.Clear();
                lCount = 0;
                pBarCmp.Value = 0;                
                string strLastA, strLastB;
                foreach (string itemA in listA)
                {
                    strLastA = itemA[itemA.Length - 1].ToString();
                    foreach (string itemB in listB)
                    {
                        strLastB = itemB[itemB.Length - 1].ToString();
                        if (strLastA != strLastB)
                        {
                            continue;
                        }

                        if (string.Compare(itemA, itemB, false) == 0)
                        {
                            listRemove.Add(itemA);
                            break;
                        }
                    }
                    lCount++;                    
                }

                foreach (string itemRemove in listRemove)
                {
                    listA.Remove(itemRemove);
                }
                iFinallyCount = listA.Count;
                //lblFinally.Text = string.Format("原始A檔 {0} 比對後 {1}",
                //                iOrignalCount, iFinallyCount);
                //MessageBox.Show("比 對 完 成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Invoke(completeHandler, this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComapreIgnore()
        {
            try
            {
                listRemove.Clear();
                lCount = 0;
                pBarCmp.Value = 0;                
                string strLastA, strLastB;
                foreach (string itemA in listA)
                {
                    strLastA = itemA[itemA.Length - 1].ToString();
                    foreach (string itemB in listB)
                    {
                        strLastB = itemB[itemB.Length - 1].ToString();
                        if (strLastA.ToLower() != strLastB.ToLower())
                        {
                            continue;
                        }                     

                        if (string.Compare(itemA, itemB, true) == 0)
                        {
                            listRemove.Add(itemA);
                            break;
                        }
                    }
                    lCount++;                    
                }

                foreach (string itemRemove in listRemove)
                {
                    listA.Remove(itemRemove);
                }
                iFinallyCount = listA.Count;
                //lblFinally.Text = string.Format("原始A檔 {0} 比對後 {1}",
                //                iOrignalCount, iFinallyCount);
                //MessageBox.Show("比 對 完 成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Invoke(completeHandler, this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Text Files|*.txt|All files (*.*)|*.*";
            saveDlg.Title = "儲存檔案";
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    StreamWriter stWrite = new StreamWriter(saveDlg.FileName, false, System.Text.Encoding.Default);
                    foreach (string itemA in listA)
                    {
                        stWrite.WriteLine(itemA);
                    }
                    stWrite.Close();

                    string strDir = Path.GetDirectoryName(saveDlg.FileName);
                    string strFileNoExtension = Path.GetFileNameWithoutExtension(saveDlg.FileName);
                    string strRemoveFile = strFileNoExtension + "_remov.txt";
                    StreamWriter stWrite2 = new StreamWriter(strDir + "\\" + strRemoveFile, false, System.Text.Encoding.Default);
                    foreach (string itemRemove in listRemove)
                    {
                        stWrite2.WriteLine(itemRemove);
                    }
                    stWrite2.Close();

                    MessageBox.Show("儲 存 完 成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Cursor = Cursors.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(listA.Count > 0)
            {
                lblFinally.Text = lCount.ToString();
                int iValue = (int)(lCount * 100 / listA.Count);
                if (iValue > 100)
                {
                    iValue = 100;
                }
                pBarCmp.Value = iValue;              
            }            
        }
    }
}
