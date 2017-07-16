using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHTCBackEnd.Language
{   
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
                BaseSet.Add("editor", new string[] { "編輯", "Edit", "编辑" });
                BaseSet.Add("editor_panel", new string[] { "資料編輯", "Edit Panel", "资料编辑" });
                BaseSet.Add("comfirm", new string[] { "確 認", "Confirm", "确 认" });
                BaseSet.Add("back", new string[] { "返 回", "Back", "返 回" });
                BaseSet.Add("check_remove", new string[] { "確定刪除 ?", "Confirm to remove  ?", "确定删除" });
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

        public string Editor
        {
            get
            {
                return BaseSet["editor"][iIndex];
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

    public class Vender_Lan : LanguageBase
    {
        public static Dictionary<string, string[]> VenderSet = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (VenderSet.Count == 0)
            {
                VenderSet.Add("title", new string[] { "廠商資料管理", "Vender Info", "厂商资料管理" });
                VenderSet.Add("accno", new string[] { "廠商帳號", "Account", "厂商帐号" });
                VenderSet.Add("name", new string[] { "廠商簡稱", "Name", "厂商简称" });
                VenderSet.Add("full_name", new string[] { "廠商全名", "FullName", "厂商全名" });
                VenderSet.Add("bno", new string[] { "統一編號", "Bussiness No", "统一编号" });
                VenderSet.Add("addr", new string[] { "廠商地址", "Address", "厂商地址" });
                VenderSet.Add("person1", new string[] { "聯絡人一", "Contactor 1", "联络人一" });
                VenderSet.Add("tel", new string[] { "聯絡電話", "TEL.", "联络电话" });
                VenderSet.Add("mail", new string[] { "聯絡信箱", "EMAIL", "联络信箱" });
                VenderSet.Add("person2", new string[] { "聯絡人二", "Contactor 2", "联络人二" });
                VenderSet.Add("err_accno", new string[] { "廠商帳號必要", "Account required", "厂商帐号必要" });
                VenderSet.Add("err_name", new string[] { "廠商簡稱必要", "Account required", "厂商简称必要" });
                //VenderSet.Add("mail2", new string[] { "EMAIL", "Bussiness No", "统一编号" });
            }
        }

        public Vender_Lan()
        {
            InitLanguage();
        }

        public string Title
        {
            get
            {
                return VenderSet["title"][iIndex];
            }
        }

        public string AccountNo
        {
            get
            {
                return VenderSet["accno"][iIndex];
            }
        }

        public string Name
        {
            get
            {
                return VenderSet["name"][iIndex];
            }
        }

        public string FullName
        {
            get
            {
                return VenderSet["full_name"][iIndex];
            }
        }

        public string BNo
        {
            get
            {
                return VenderSet["bno"][iIndex];
            }
        }

        public string Address
        {
            get
            {
                return VenderSet["addr"][iIndex];
            }
        }

        public string Person_1
        {
            get
            {
                return VenderSet["person1"][iIndex];
            }
        }

        public string Tel
        {
            get
            {
                return VenderSet["tel"][iIndex];
            }
        }

        public string Mail
        {
            get
            {
                return VenderSet["mail"][iIndex];
            }
        }

        public string Person_2
        {
            get
            {
                return VenderSet["person2"][iIndex];
            }
        }

        public string Error_Accno
        {
            get
            {
                return VenderSet["err_accno"][iIndex];
            }
        }

        public string Error_Name
        {
            get
            {
                return VenderSet["err_name"][iIndex];
            }
        }
    }

    public class Activity_Event_Lan : LanguageBase
    {
        public static Dictionary<string, string[]> EventSet = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (EventSet.Count == 0)
            {
                EventSet.Add("title", new string[] { "活動管理", "Event Management", "活动管理" });
                EventSet.Add("event_no", new string[] { "專案編號", "Project No", "专案编号" });
                EventSet.Add("event_name", new string[] { "專案名稱", "Project Name", "专案名称" });
                EventSet.Add("vender", new string[] { "活動廠商", "Vender", "活动厂商" });
                EventSet.Add("lunch", new string[] { "是否啟用", "Lunch", "是否启用" });
                EventSet.Add("start_time", new string[] { "開始時間", "Start Time", "开始时间" });
                EventSet.Add("end_time", new string[] { "結束時間", "Stop Time", "结束时间" });
                EventSet.Add("code_id", new string[] { "發碼代號", "Code Id", "发码代号" });
                EventSet.Add("code_nums", new string[] { "發碼數量", "Code Numbers", "发码数量" });
                EventSet.Add("qr_bit", new string[] { "QR CODE檢查位元", "QR CODE Check Bit", "QR CODE检查位元" });
                EventSet.Add("serial_bit", new string[] { "序號 檢查位元", "Serial Number Check Bit", "序号 检查位元" });
                EventSet.Add("com_bit", new string[] { "複合 檢查位元", "Combine Bit", "複合 检查位元" });
                EventSet.Add("production", new string[] { "商品系列", "Produection", "商品系列" });
                EventSet.Add("web_url", new string[] { "活動網址", "Activity Web Url", "活动网址" });
                EventSet.Add("event_type", new string[] { "活動類型", "Activity Type", "活动类型" });
                EventSet.Add("memo", new string[] { "活動備註", "Activity Memo", "活动备注" });
                EventSet.Add("used", new string[] { "已用序號", "Code Used", "已使用序号" });

                /*error*/
                EventSet.Add("err_event_no", new string[] { "專案編號必要", "Project No required", "专案编号必要" });
                EventSet.Add("err_event_name", new string[] { "專案名稱必要", "Project Name required", "专案名称必要" });
                EventSet.Add("err_vender", new string[] { "活動廠商必要", "Vender required", "活动厂商称必要" });
                EventSet.Add("err_start_time", new string[] { "開始時間必要", "Vender required", "开始时间必要" });

                EventSet.Add("err_code_id", new string[] { "發碼代號必要", "Code Id required", "发码代号必要" });
                EventSet.Add("err_code_nums", new string[] { "發碼數量必要", "Code Numbers required", "发码数量必要" });
                EventSet.Add("err_qr_bit", new string[] { "QR COD E檢查位元必要", "QR CODE Check Bit required", "QR CODE 检查位元必要" });
                EventSet.Add("err_serial_bit", new string[] { "序號 檢查位元必要", "Serial Number Check Bit required", "序号 检查位元数量必要" });
                EventSet.Add("err_com_bit", new string[] { "複合 檢查位元必要", "Serial Number Check Bit required", "複合 检查位元数量必要" });
                EventSet.Add("err_production", new string[] { "商品系列必要", "Produection required", "商品系列必要" });
                EventSet.Add("err_web_url", new string[] { "活動網址必要", "Activity Web Url required", "活动网址必要" });
                EventSet.Add("err_event_type", new string[] { "活動類型必要", "Activity Typ required", "活动类型必要" });

                /**/
                EventSet.Add("event_type1", new string[] { "立即中獎", "Award right now", "立即中奖" });
                EventSet.Add("event_type2", new string[] { "掃描參加遊戲在立即中獎", "Award after gaming", "扫描参加游戏在立即中奖" });
                EventSet.Add("event_type3", new string[] { "非立即中獎", "Not award right now", "非立即中奖" });
            }
        }

        public Activity_Event_Lan()
        {
            InitLanguage();
        }

        public string Title
        {
            get
            {
                return EventSet["title"][iIndex];
            }
        }
        public string Event_No
        {
            get
            {
                return EventSet["event_no"][iIndex];
            }
        }
        public string Event_Name
        {
            get
            {
                return EventSet["event_name"][iIndex];
            }
        }
        public string Vender
        {
            get
            {
                return EventSet["vender"][iIndex];
            }
        }
        public string Lunch
        {
            get
            {
                return EventSet["lunch"][iIndex];
            }
        }
        public string Start_Time
        {
            get
            {
                return EventSet["start_time"][iIndex];
            }
        }
        public string End_Time
        {
            get
            {
                return EventSet["end_time"][iIndex];
            }
        }
        public string Code_Id
        {
            get
            {
                return EventSet["code_id"][iIndex];
            }
        }
        public string Code_Nums
        {
            get
            {
                return EventSet["code_nums"][iIndex];
            }
        }
        public string QR_Bit
        {
            get
            {
                return EventSet["qr_bit"][iIndex];
            }
        }
        public string Serial_Bit
        {
            get
            {
                return EventSet["serial_bit"][iIndex];
            }
        }
        public string Com_Bit
        {
            get
            {
                return EventSet["com_bit"][iIndex];
            }
        }
        public string Production
        {
            get
            {
                return EventSet["production"][iIndex];
            }
        }
        public string Web_Url
        {
            get
            {
                return EventSet["web_url"][iIndex];
            }
        }
        public string Event_Type
        {
            get
            {
                return EventSet["event_type"][iIndex];
            }
        }
        public string Memo
        {
            get
            {
                return EventSet["memo"][iIndex];
            }
        }
        public string Uesd
        {
            get
            {
                return EventSet["used"][iIndex];
            }
        }

        public string Error_Event_No
        {
            get
            {
                return EventSet["err_event_no"][iIndex];
            }
        }
        public string Error_Event_Name
        {
            get
            {
                return EventSet["err_event_name"][iIndex];
            }
        }
        public string Error_Vender
        {
            get
            {
                return EventSet["err_vender"][iIndex];
            }
        }
        public string Error_Start_Time
        {
            get
            {
                return EventSet["err_start_time"][iIndex];
            }
        }
        public string Error_Code_Id
        {
            get
            {
                return EventSet["err_code_id"][iIndex];
            }
        }
        public string Error_Code_Nums
        {
            get
            {
                return EventSet["err_code_nums"][iIndex];
            }
        }        
        public string Error_QR_Bit
        {
            get
            {
                return EventSet["err_qr_bit"][iIndex];
            }
        }        
        public string Error_Serial_Bit
        {
            get
            {
                return EventSet["err_serial_bit"][iIndex];
            }
        }                
        public string Error_Com_Bit
        {
            get
            {
                return EventSet["err_com_bit"][iIndex];
            }
        }                                      
        public string Error_Production
        {
            get
            {
                return EventSet["err_production"][iIndex];
            }
        }                                             
        public string Error_WebUrl
        {
            get
            {
                return EventSet["err_web_url"][iIndex];
            }
        }                                                    
        public string Error_Event_Type
        {
            get
            {
                return EventSet["err_event_type"][iIndex];
            }
        }     

        public string Event_Type1
        {
            get
            {
                return EventSet["event_type1"][iIndex];
            }
        }

        public string Event_Type2
        {
            get
            {
                return EventSet["event_type2"][iIndex];
            }
        }

        public string Event_Type3
        {
            get
            {
                return EventSet["event_type3"][iIndex];
            }
        }
    }

    public class Event_Code_Gen : LanguageBase
    {
        public static Dictionary<string, string[]> EventCodeGen = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (EventCodeGen.Count == 0)
            {
                EventCodeGen.Add("title", new string[] { "活動發碼管理", "Event Code Generating Management", "活动发码管理" });
                EventCodeGen.Add("event_no", new string[] { "專案編號", "Project No", "专案编号" });
                EventCodeGen.Add("event_name", new string[] { "專案名稱", "Project Name", "专案名称" });
                EventCodeGen.Add("vender", new string[] { "活動廠商", "Vender", "活动厂商" });
                EventCodeGen.Add("gen_datetime", new string[] { "產生日期時間", "PreCode Datetime", "产生日期时间" });
                EventCodeGen.Add("ptype", new string[] { "產品類別", "Production Class", "产品类别" });
                EventCodeGen.Add("po_number", new string[] { "工單序號", "PO Number", "工单序号" });
                EventCodeGen.Add("serial_no", new string[] { "產品料號", "Serial Number", "产品料号" });
                EventCodeGen.Add("gen_nums", new string[] { "產生數量", "Code Numbers", "产生数量" });
                EventCodeGen.Add("employee", new string[] { "產碼員工", "Employee", "产码员工" });
                EventCodeGen.Add("finish_datetime", new string[] { "完成日期時間", "Completed Datetime", "完成日期时间" });
                              
                /*error*/
                EventCodeGen.Add("err_event_no", new string[] { "專案編號必要", "Project No required", "专案编号必要" }); 
                EventCodeGen.Add("err_vender", new string[] { "活動廠商必要", "Vender required", "活动厂商称必要" });
                EventCodeGen.Add("err_gen_datetime", new string[] { "產生日期時間必要", "PreCode Datetime required", "产生日期时间必要" });
                EventCodeGen.Add("err_ptype", new string[] { "產品類別必要", "Production Class", "产品类别必要" });
                EventCodeGen.Add("err_po_number", new string[] { "工單序號必要", "PO Number required", "工单序号必要" });
                EventCodeGen.Add("err_serial_no", new string[] { "產品料號必要", "Serial Number required", "产品料号必要" });
                EventCodeGen.Add("err_gen_nums", new string[] { "產生數量必要", "Code Numbers required", "产生数量必要" });
                EventCodeGen.Add("err_employee", new string[] { "產碼員工必要", "Employee required", "产码员工必要" });                               

                ///**/
                //EventCodeGen.Add("event_type1", new string[] { "立即中獎", "Award right now", "立即中奖" });
                //EventCodeGen.Add("event_type2", new string[] { "掃描參加遊戲在立即中獎", "Award after gaming", "扫描参加游戏在立即中奖" });
                //EventCodeGen.Add("event_type3", new string[] { "非立即中獎", "Not award right now", "非立即中奖" });
            }
        }

        public Event_Code_Gen()
        {
            InitLanguage();
        }

        public string Title
        {
            get
            {
                return EventCodeGen["title"][iIndex];
            }
        }
        public string Event_No
        {
            get
            {
                return EventCodeGen["event_no"][iIndex];
            }
        }
        public string Event_Name
        {
            get
            {
                return EventCodeGen["event_name"][iIndex];
            }
        }
        public string Vender
        {
            get
            {
                return EventCodeGen["vender"][iIndex];
            }
        }
        public string GenDateTime
        {
            get
            {
                return EventCodeGen["gen_datetime"][iIndex];
            }
        }
        public string ProductType
        {
            get
            {
                return EventCodeGen["ptype"][iIndex];
            }
        }
        public string PO_Number
        {
            get
            {
                return EventCodeGen["po_number"][iIndex];
            }
        }
        public string Serial_Number
        {
            get
            {
                return EventCodeGen["serial_no"][iIndex];
            }
        }
        public string GenNumber
        {
            get
            {
                return EventCodeGen["gen_nums"][iIndex];
            }
        }
        public string Employee
        {
            get
            {
                return EventCodeGen["employee"][iIndex];
            }
        }
        public string FinishDateTime
        {
            get
            {
                return EventCodeGen["finish_datetime"][iIndex];
            }
        }

        public string Error_Event_No
        {
            get
            {
                return EventCodeGen["err_event_no"][iIndex];
            }
        }
        public string Error_Vender
        {
            get
            {
                return EventCodeGen["err_vender"][iIndex];
            }
        }
        public string Error_GenDateTime
        {
            get
            {
                return EventCodeGen["err_gen_datetime"][iIndex];
            }
        }
        public string Error_ProductType
        {
            get
            {
                return EventCodeGen["err_ptype"][iIndex];
            }
        }
        public string Error_PO_Number
        {
            get
            {
                return EventCodeGen["err_po_number"][iIndex];
            }
        }
        public string Error_Serial_Number
        {
            get
            {
                return EventCodeGen["err_serial_no"][iIndex];
            }
        }
        public string Error_GenNumber
        {
            get
            {
                return EventCodeGen["err_gen_nums"][iIndex];
            }
        }
        public string Error_Employee
        {
            get
            {
                return EventCodeGen["err_employee"][iIndex];
            }
        }
    }

    public enum Lan_Zone
    {
        TW = 0,
        EN = 1,
        CN = 2
    }
}