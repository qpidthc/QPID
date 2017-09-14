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

namespace THC_ActivityDeploy
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "開啟檔案";
            openFileDialog1.InitialDirectory = ".\\";
            openFileDialog1.Filter = "txt files (*.*)|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
            }
        }

        private void ReadFile(string file)
        {
            StreamReader smReader = new StreamReader(file, System.Text.Encoding.UTF8);
            string strLine;
            string eventKey = "";
            string rwdEventKey = "";
            strLine = smReader.ReadLine();

            if (strLine.StartsWith("activity:"))
            {
                strLine = strLine.Substring(9);
                dynamic activity = Newtonsoft.Json.JsonConvert.DeserializeObject(strLine);

                eventKey = activity[0].AE001.ToString();
                lblAE002.Text = activity[0].AE002.ToString();
                lblAE003.Text = activity[0].AE003.ToString();
                lblAE004.Text = activity[0].AE004.ToString();
                lblAE005.Text = activity[0].AE005.ToString();
                lblAE006.Text = activity[0].AE006.ToString();
                lblAE008.Text = activity[0].AE008.ToString();
                lblAE010.Text = activity[0].AE010.ToString();
            }

            strLine = smReader.ReadLine();

            if (strLine.StartsWith("reward:"))
            {
                strLine = strLine.Substring(7);
                dynamic rewards = Newtonsoft.Json.JsonConvert.DeserializeObject(strLine);

                rwdEventKey = rewards[0].AEP002.ToString();
                lblAEP003.Text = rewards[0].AEP003.ToString();
                lblAEP005.Text = rewards[0].AEP005.ToString();
                lblAEP006.Text = rewards[0].AEP006.ToString();
                lblAEP011.Text = rewards[0].AEP011.ToString();
                lblAEP012.Text = rewards[0].AEP012.ToString();
                lblAEP013.Text = rewards[0].AEP013.ToString();
            }
            
//AEP001 Int Key PK
//AEP002  int 事件key
//AEP003  char(1) 獎項型態    0 虛擬 1 實體
//AEP004  tinyint 獎項層級
//AEP005 nvarchar(20)	獎項名稱
//AEP006  int 獎項數量
//AEP007 nvarchar(100)	獎項說明備註
//AEP008  datetime 將項分配時間  Default null 7 / 4新增
//AEP009  int 獎項廠商    7 / 21新增
//AEP010  int 指定產碼配獎  EQCH001 key 7 / 29 新增
//AEP011  Varchar(20) 獎項圖檔檔名 Null 8 / 17 新增
//AEP012  Varchar(10) 有效時間 Null 8 / 18 新增
//AEP013  text 簡訊內容    Null 9 / 9新增


             strLine = smReader.ReadToEnd();

            dynamic records = Newtonsoft.Json.JsonConvert.DeserializeObject(strLine);


        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            ReadFile(txtFile.Text);
        }
    }
}
