using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using System.IO;

namespace THC_Weather
{
    public partial class Form1 : Form
    {
        System.Collections.Generic.Dictionary<string, int> TAIWAN_AREA = new Dictionary<string, int>();
        System.Collections.Generic.Dictionary<int, string> WEATHER_CODE = new Dictionary<int, string>();
        bool bExcuted = false;
        public Form1()
        {
            InitializeComponent();

            TAIWAN_AREA.Add("基隆", 2306188);
            TAIWAN_AREA.Add("台北市", 2306179);
            TAIWAN_AREA.Add("新北市", 90717580);
            TAIWAN_AREA.Add("桃園市", 91982232);
            TAIWAN_AREA.Add("新竹", 2306185);
            TAIWAN_AREA.Add("苗栗", 2301128);
            TAIWAN_AREA.Add("台中", 2306181);
            TAIWAN_AREA.Add("彰化", 91290191);
            TAIWAN_AREA.Add("雲林", 28776037);
            TAIWAN_AREA.Add("嘉義", 2296315);
            TAIWAN_AREA.Add("台南", 2306182);
            TAIWAN_AREA.Add("高雄", 2306180);
            TAIWAN_AREA.Add("屏東", 91290319);
            TAIWAN_AREA.Add("宜蘭", 91290369);
            TAIWAN_AREA.Add("花蓮", 91290403);
            TAIWAN_AREA.Add("台東", 91290354);
            TAIWAN_AREA.Add("南投", 2306204);

            WEATHER_CODE.Add(0, "龍捲風");
            WEATHER_CODE.Add(1, "熱帶風暴");
            WEATHER_CODE.Add(2, "颶風");
            WEATHER_CODE.Add(3, "強雷陣雨");
            WEATHER_CODE.Add(4, "雷陣雨");
            WEATHER_CODE.Add(5, "混合雨雪");
            WEATHER_CODE.Add(6, "混合雨雪");
            WEATHER_CODE.Add(7, "混合雨雪");
            WEATHER_CODE.Add(8, "冰凍小雨");
            WEATHER_CODE.Add(9, "細雨");
            WEATHER_CODE.Add(10, "凍雨");
            WEATHER_CODE.Add(11, "陣雨");
            WEATHER_CODE.Add(12, "陣雨");
            WEATHER_CODE.Add(13, "飄雪");
            WEATHER_CODE.Add(14, "陣雪");
            WEATHER_CODE.Add(15, "吹雪");
            WEATHER_CODE.Add(16, "下雪");
            WEATHER_CODE.Add(17, "冰雹");
            WEATHER_CODE.Add(18, "雨雪");
            WEATHER_CODE.Add(19, "多塵");
            WEATHER_CODE.Add(20, "多霧");
            WEATHER_CODE.Add(21, "陰霾");
            WEATHER_CODE.Add(22, "多煙");
            WEATHER_CODE.Add(23, "狂風大作");
            WEATHER_CODE.Add(24, "有風");
            WEATHER_CODE.Add(25, "冷");
            WEATHER_CODE.Add(26, "多雲");
            WEATHER_CODE.Add(27, "晴間多雲（夜）");
            WEATHER_CODE.Add(28, "晴間多雲（日）");
            WEATHER_CODE.Add(29, "晴間多雲（夜）");
            WEATHER_CODE.Add(30, "晴間多雲（日）");
            WEATHER_CODE.Add(31, "清晰的（夜）");
            WEATHER_CODE.Add(32, "晴朗");
            WEATHER_CODE.Add(33, "晴朗（夜）");
            WEATHER_CODE.Add(34, "晴朗（日）");
            WEATHER_CODE.Add(35, "雨和冰雹");
            WEATHER_CODE.Add(36, "炎熱");
            WEATHER_CODE.Add(37, "雷陣雨");
            WEATHER_CODE.Add(38, "零星雷陣雨");
            WEATHER_CODE.Add(39, "零星雷陣雨");
            WEATHER_CODE.Add(40, "零星雷陣雨");
            WEATHER_CODE.Add(41, "大雪");
            WEATHER_CODE.Add(42, "零星陣雪");
            WEATHER_CODE.Add(43, "大雪");
            WEATHER_CODE.Add(44, "多雲");
            WEATHER_CODE.Add(45, "雷陣雨");
            WEATHER_CODE.Add(46, "陣雪");
            WEATHER_CODE.Add(47, "雷陣雨");
            WEATHER_CODE.Add(3200, "資料錯誤");

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datNow = DateTime.Now;

            Text = datNow.ToString("HH:mm:ss");
            if (DateTime.Now.Minute == 47 )
            {
                if (!bExcuted)
                {
                    bExcuted = true;
                    richTextBox1.Text = "";

                    var api = "https://query.yahooapis.com/v1/public/yql?q=";
                    var wc = new WebClient();
                    int waether_code;
                    foreach (KeyValuePair<string, int> item in TAIWAN_AREA)
                    {
                        string url = api +
                                      string.Format("select%20item.condition%20from%20weather.forecast%20where%20woeid%20%3D%20{0}%20and%20u%3D%22c%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys",
                                                  item.Value);

                        wc.Encoding = Encoding.UTF8;
                        string result = wc.DownloadString(url);

                        dynamic weather = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                        object temp = weather.query.results.channel.item.condition.temp;
                        object code = weather.query.results.channel.item.condition.code;

                        if (int.TryParse(code.ToString(), out waether_code))
                        {
                            richTextBox1.Text += WEATHER_CODE[waether_code];
                        }
                        richTextBox1.Text += item.Key + System.Environment.NewLine + result + System.Environment.NewLine;
                    }
                }                
            }
            else
            {
                bExcuted = false;
            }
        }
           
    }
}
