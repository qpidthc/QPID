using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THC_Library.Language
{
    public enum Lan_Zone
    {
        TW = 0,
        EN = 1,
        CN = 2
    }

    public class LanguageBase
    {
        public static Lan_Zone CURRENT_LANGUAGE = Lan_Zone.TW;
        public static Dictionary<string, string[]> BaseSet = new Dictionary<string, string[]>();
        protected int iIndex = 0;

        public LanguageBase()
        {
            if (BaseSet.Count == 0)
            {
                BaseSet.Add("app_title", new string[] { "宏全國際行銷管理平台", "THC Global Selling Platform ", "宏全国际行销管理平台" });
                BaseSet.Add("sys_message", new string[] { "系統訊息", "System Message", "系统讯息" });
                BaseSet.Add("logout", new string[] { "登 出", "Logout", "登 出" });
                BaseSet.Add("lanuage", new string[] { "語 系", "Language", "语 系" });
                BaseSet.Add("TW", new string[] { "繁體中文", "Traditional Chinese", "繁体中文" });
                BaseSet.Add("EN", new string[] { "英文 ", "English", "英文" });
                BaseSet.Add("CN", new string[] { "簡體中文", "Simplified Chinese", "简体中文" });
                BaseSet.Add("search", new string[] { "搜尋", "Search", "搜寻" });
                BaseSet.Add("clear", new string[] { "清空", "Clear", "清空" });
                BaseSet.Add("add", new string[] { "新增", "AddNew", "新增" });
                BaseSet.Add("export", new string[] { "匯出", "Export", "汇出" });
                BaseSet.Add("editor", new string[] { "編輯", "Edit", "编辑" });
                BaseSet.Add("select", new string[] { "選取", "Select", "选取" });
                BaseSet.Add("editor_panel", new string[] { "資料編輯", "Edit Panel", "资料编辑" });
                BaseSet.Add("comfirm", new string[] { "確 認", "Confirm", "确 认" });
                BaseSet.Add("back", new string[] { "返 回", "Back", "返 回" });
                BaseSet.Add("check_remove", new string[] { "確定刪除 ?", "Confirm to remove  ?", "确定删除" });
                BaseSet.Add("check_save", new string[] { "確定儲存資料嗎 ?", "Save Data ?", "确定储存资料吗 ?" });
                /*data table*/
                BaseSet.Add("no_data", new string[] { "目前無資料", "Nothing found", "目前无资料" });
                BaseSet.Add("current_page", new string[] { "目前頁數", "Current Page", "目前页数" });
                BaseSet.Add("next_page", new string[] { "下一頁", "Next Page", "下一页" });
                BaseSet.Add("prev_page", new string[] { "上一頁", "Previous Page", "上一页" });

                /**/
                BaseSet.Add("yes", new string[] { "是", "Yes", "是" });
                BaseSet.Add("no", new string[] { "否", "No", "否" });
                BaseSet.Add("ok", new string[] { "確認", "OK", "确认" });
            }
        }

        public Lan_Zone CurrentZone
        {
            set
            {
                if (value == Lan_Zone.TW)
                {
                    iIndex = 0;
                }
                else if (value == Lan_Zone.EN)
                {
                    iIndex = 1;
                }
                else if (value == Lan_Zone.CN)
                {
                    iIndex = 2;
                }
            }
        }

        public string ApplicationTitle
        {
            get
            {
                return BaseSet["app_title"][iIndex];
            }
        }

        public string SystemMessage
        {
            get
            {
                return BaseSet["sys_message"][iIndex];
            }
        }

        public string Logout
        {
            get
            {
                return BaseSet["logout"][iIndex];
            }
        }

        public string Language
        {
            get
            {
                return BaseSet["lanuage"][iIndex];
            }
        }
        public string TW
        {
            get
            {
                return BaseSet["TW"][iIndex];
            }
        }
        public string EN
        {
            get
            {
                return BaseSet["EN"][iIndex];
            }
        }
        public string CN
        {
            get
            {
                return BaseSet["CN"][iIndex];
            }
        }

        public string Search
        {
            get
            {
                return BaseSet["search"][iIndex];
            }
        }

        public string Clear
        {
            get
            {
                return BaseSet["clear"][iIndex];
            }
        }

        public string AddDate
        {
            get
            {
                return BaseSet["add"][iIndex];
            }
        }

        public string ExportData
        {
            get
            {
                return BaseSet["export"][iIndex];
            }
        }

        public string Editor
        {
            get
            {
                return BaseSet["editor"][iIndex];
            }
        }

        public string Select
        {
            get
            {
                return BaseSet["select"][iIndex];
            }
        }

        public string EditorPanel
        {
            get
            {
                return BaseSet["editor_panel"][iIndex];
            }
        }

        public string Confirm
        {
            get
            {
                return BaseSet["comfirm"][iIndex];
            }
        }

        public string Back
        {
            get
            {
                return BaseSet["back"][iIndex];
            }
        }

        public string CheckRemove
        {
            get
            {
                return BaseSet["check_remove"][iIndex];
            }
        }

        public string CheckSave
        {
            get
            {
                return BaseSet["check_save"][iIndex];
            }
        }

        public string DataTable_NoData
        {
            get
            {
                return BaseSet["no_data"][iIndex];
            }
        }

        public string DataTable_CurrentPage
        {
            get
            {
                return BaseSet["current_page"][iIndex];
            }
        }

        public string DataTable_NextPage
        {
            get
            {
                return BaseSet["next_page"][iIndex];
            }
        }

        public string DataTable_PreviousPage
        {
            get
            {
                return BaseSet["prev_page"][iIndex];
            }
        }

        public string Yes
        {
            get
            {
                return BaseSet["yes"][iIndex];
            }
        }

        public string No
        {
            get
            {
                return BaseSet["no"][iIndex];
            }
        }

        public string OK
        {
            get
            {
                return BaseSet["ok"][iIndex];
            }
        }
    }
}
