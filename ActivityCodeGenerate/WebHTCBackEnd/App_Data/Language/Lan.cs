using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using THC_Library.Language;

namespace WebHTCBackEnd.Language
{       
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
                EventSet.Add("qr_length", new string[] { "QR碼長度", "QR Code Length", "QR码长度" });
                EventSet.Add("event_page", new string[] { "活動網頁", "Html Page", "活动网页" });

                /*error*/
                EventSet.Add("err_event_no", new string[] { "專案編號必要", "Project No required", "专案编号必要" });
                EventSet.Add("err_event_name", new string[] { "專案名稱必要", "Project Name required", "专案名称必要" });
                EventSet.Add("err_vender", new string[] { "活動廠商必要", "Vender required", "活动厂商称必要" });
                EventSet.Add("err_start_time", new string[] { "開始時間必要", "Begining Time required", "开始时间必要" });
                EventSet.Add("err_end_time", new string[] { "結束時間必要", "Ending Time required", "结束时间必要" });

                EventSet.Add("err_code_id", new string[] { "發碼代號必要", "Code Id required", "发码代号必要" });
                EventSet.Add("err_code_nums", new string[] { "發碼數量必要", "Code Numbers required", "发码数量必要" });
                EventSet.Add("err_qr_bit", new string[] { "QR COD E檢查位元必要，且要藉於1到64之間", "QR CODE Check Bit required,and must between 1 and 64", "QR CODE 检查位元必要，且要藉于1到64之间" });
                EventSet.Add("err_serial_bit", new string[] { "序號 檢查位元必要，且要藉於1到64之間", "Serial Number Check Bit required", "序号 检查位元数量必要，且要藉于1到64之间" });
                EventSet.Add("err_com_bit", new string[] { "複合 檢查位元必要", "Serial Number Check Bit required", "複合 检查位元数量必要" });
                EventSet.Add("err_production", new string[] { "商品系列必要", "Produection required", "商品系列必要" });
                EventSet.Add("err_web_url", new string[] { "活動網址必要", "Activity Web Url required", "活动网址必要" });
                EventSet.Add("err_event_type", new string[] { "活動類型必要", "Activity Typ required", "活动类型必要" });
                EventSet.Add("err_qr_length", new string[] { "QR碼長度必要", "QR Code Length required", "QR码长度必要" });

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
        public string RQLength
        {
            get {
                return EventSet["qr_length"][iIndex];
            }
        }
        public string Event_Page
        {
            get
            {
                return EventSet["event_page"][iIndex];
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
        public string Error_End_Time
        {
            get
            {
                return EventSet["err_end_time"][iIndex];
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
        public string Error_RQLength
        {
            get
            {
                return EventSet["err_qr_length"][iIndex];
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
                EventCodeGen.Add("memo", new string[] { "** 查詢活動專案資料後，再行新增修改 **", "** Before editing data，Please search your event **", "** 查询活动专案资料后，再行新增修改 **" });
                EventCodeGen.Add("event_no", new string[] { "專案編號", "Project No", "专案编号" });
                EventCodeGen.Add("event_name", new string[] { "專案名稱", "Project Name", "专案名称" });
                EventCodeGen.Add("vender_no", new string[] { "廠商代號", "Vender No", "厂商代号" });
                EventCodeGen.Add("vender", new string[] { "活動廠商", "Vender", "活动厂商" });
                EventCodeGen.Add("gen_datetime", new string[] { "產生日期時間", "PreCode Datetime", "产生日期时间" });
                EventCodeGen.Add("ptype", new string[] { "產品類別", "Production Class", "产品类别" });
                EventCodeGen.Add("po_number", new string[] { "工單序號", "PO Number", "工单序号" });
                EventCodeGen.Add("serial_no", new string[] { "產品品項", "Product Item", "产品品项" });
                EventCodeGen.Add("gen_nums", new string[] { "產生數量", "Code Numbers", "产生数量" });
                EventCodeGen.Add("employee", new string[] { "產碼員工", "Employee", "产码员工" });
                EventCodeGen.Add("finish_datetime", new string[] { "完成日期時間", "Completed Datetime", "完成日期时间" });
                EventCodeGen.Add("event_title", new string[] { "活動專案", "Event Project", "活动专案" });
                EventCodeGen.Add("code_state", new string[] { "產碼狀態", "GenCode State", "产码状态" });
                EventCodeGen.Add("code_run", new string[] { "產碼中", "GenCode Running", "产码中" });
                EventCodeGen.Add("down_load", new string[] { "下載碼檔案", "Download Code File", "下载码档案" });
                EventCodeGen.Add("event_list", new string[] { "事件清單 ", "Event List ", "事件清单 " });
                               
        
                /*error*/
                EventCodeGen.Add("err_event_no", new string[] { "專案編號必要", "Project No required", "专案编号必要" }); 
                EventCodeGen.Add("err_vender", new string[] { "活動廠商必要", "Vender required", "活动厂商称必要" });
                EventCodeGen.Add("err_gen_datetime", new string[] { "產生日期時間必要", "PreCode Datetime required", "产生日期时间必要" });
                EventCodeGen.Add("err_ptype", new string[] { "產品類別必要", "Production Class", "产品类别必要" });
                EventCodeGen.Add("err_po_number", new string[] { "工單序號必要", "PO Number required", "工单序号必要" });
                EventCodeGen.Add("err_serial_no", new string[] { "產品料號必要", "Serial Number required", "产品料号必要" });
                EventCodeGen.Add("err_gen_nums", new string[] { "產生數量必要", "Code Numbers required", "产生数量必要" });
                EventCodeGen.Add("err_employee", new string[] { "產碼員工必要", "Employee required", "产码员工必要" });
                EventCodeGen.Add("err_gen_code", new string[] { "產碼發生錯誤", "Generate Code Error", "产码发生错误" });   
                ///**/
                //EventCodeGen.Add("ptype1", new string[] { "汽水", "soda", "汽水" });
                //EventCodeGen.Add("ptype2", new string[] { "茶飲", "tea", "茶饮" });
                //EventCodeGen.Add("ptype3", new string[] { "水", "water", "水" });
                EventCodeGen.Add("msg_gen_code", new string[] { "確定開始產生序號資料嗎 ?", "Start Generate Code ?", "确定开始产生序号资料吗 ?" }); 
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
        public string Memo
        {
            get
            {
                return EventCodeGen["memo"][iIndex];
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
        public string Vender_No
        {
            get
            {
                return EventCodeGen["vender_no"][iIndex];
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
        public string EventTitle
        {
            get
            {
                return EventCodeGen["event_title"][iIndex];
            }
        }
        public string CodeState
        {
            get
            {
                return EventCodeGen["code_state"][iIndex];
            }
        }
        public string CodeRun
        {
            get
            {
                return EventCodeGen["code_run"][iIndex];
            }
        }
        public string Download
        {
            get
            {
                return EventCodeGen["down_load"][iIndex];
            }
        }
        public string EventList
        {
            get
            {
                return EventCodeGen["event_list"][iIndex];
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
        public string Error_GenCode
        {
            get
            {
                return EventCodeGen["err_gen_code"][iIndex];
            }
        }

        public string MSG_GenCode
        {
            get
            {
                return EventCodeGen["msg_gen_code"][iIndex];
            }
        }
        //水／汽水／茶／果汁／乳製品／咖啡／運動飲料／機能性飲料
        //public string PType1
        //{
        //    get
        //    {
        //        return EventCodeGen["ptype1"][iIndex];
        //    }
        //}

        //public string PType2
        //{
        //    get
        //    {
        //        return EventCodeGen["ptype2"][iIndex];
        //    }
        //}

        //public string PType3
        //{
        //    get
        //    {
        //        return EventCodeGen["ptype3"][iIndex];
        //    }
        //}
    }

    public class Event_Reward : LanguageBase
    {
        public static Dictionary<string, string[]> EventReward = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (EventReward.Count == 0)
            {
                EventReward.Add("title", new string[] { "活動獎項管理", "Event Prize Management", "活动奖项管理" });                
                EventReward.Add("event_no", new string[] { "專案編號", "Project No", "专案编号" });
                EventReward.Add("event_name", new string[] { "專案名稱", "Project Name", "专案名称" });
                EventReward.Add("vender_no", new string[] { "廠商代號", "Vender No", "厂商代号" });
                EventReward.Add("vender", new string[] { "活動廠商", "Vender", "活动厂商" });

                EventReward.Add("rwd_type", new string[] { "獎項型態", "Prize Type", "奖项型态" });
                EventReward.Add("rwd_level", new string[] { "獎項層級", "Prize Level", "奖项层级" });
                EventReward.Add("rwd_name", new string[] { "獎項名稱", "Prize Item", "奖项名称" });
                EventReward.Add("rwd_number", new string[] { "獎項數量", "Prize Number", "奖项数量" });
                EventReward.Add("rwd_sms", new string[] { "獎項簡訊 (%s%表示電子序號符號)", "Prize SMS (%s% represent electric no)", "奖项简讯 (%s%表示电子序号符号)" });
                EventReward.Add("rwd_memo", new string[] { "獎項說明 (實體獎項說明)", "Prize Memo (For Phyical Reward)", "奖项说明 (实体奖项说明)" });
                EventReward.Add("event_list", new string[] { "事件清單 ", "Event List ", "事件清单 " });
                EventReward.Add("event_title", new string[] { "活動專案", "Event Project", "活动专案" });
               
                EventReward.Add("rwd_allot", new string[] { "獎項分配", "Event Project", "活动专案" });
                EventReward.Add("rwd_vender", new string[] { "獎項提供商", "Event Project", "活动专案" });
                EventReward.Add("rwd_upload", new string[] { "確認上傳", "Event Project", "活动专案" });
                EventReward.Add("rwd_select_file", new string[] { "選擇或拖拉檔案", "Event Project", "活动专案" });
                EventReward.Add("rwd_upload_file", new string[] { "上傳獎項檔案", "Event Project", "活动专案" });
                EventReward.Add("rwd_check_allot", new string[] { "確認分配獎項", "Event Project", "活动专案" });

                EventReward.Add("rwd_completed", new string[] { "配獎日期", "Event Project", "活动专案" });
                EventReward.Add("rwd_state", new string[] { "配獎狀態", "State", "配奖状态" });
                EventReward.Add("assign_allot", new string[] { "指定配獎", "Assign Allot", "指定配奖" });
                EventReward.Add("qr_records", new string[] { "發碼紀錄", "QR Records", "发码纪录" });
                EventReward.Add("rwd_image", new string[] { "獎項圖示", "Prize Image", "奖项图示" });
                EventReward.Add("rwd_effective_time", new string[] { "有效時間", "Effective Time", "有效时间" });
                EventReward.Add("rwd_win_desc", new string[] { "兌獎網址", "Winning Desc", "兑奖网址" });
                

                EventReward.Add("qr_gendate", new string[] { "產生日期時間 ", "Code Generate Date ", "产生日期时间 " });
                EventReward.Add("qr_qty", new string[] { "產生數量 ", "Quantity ", "产生数量 " });
                EventReward.Add("qr_employee", new string[] { "產碼員工 ", "Employee ", "产码员工 " });
                EventReward.Add("qr_completed_date", new string[] { "完成日期時間 ", "Completed Date ", "完成日期时间 " });

                /*error*/
                EventReward.Add("err_rwd_type", new string[] { "獎項型態必要", "Prize Type required", "奖项型态必要" });
                EventReward.Add("err_rwd_level", new string[] { "獎項層級必要", "Prize Level required", "奖项层级必要" });
                EventReward.Add("err_rwd_name", new string[] { "獎項名稱必要", "Prize Item required", "奖项名称必要" });
                EventReward.Add("err_rwd_number", new string[] { "獎項數量必要", "Prize Number required", "奖项数量必要" });
                EventReward.Add("err_rwd_vender", new string[] { "獎項廠商必要", "Prize Vender required", "奖项厂商必要" });
                                
                EventReward.Add("msg_rwd_upload_ok", new string[] { "上傳檔案完成，可執行配獎動作", "Upload file completed,continue to allot.", "上传档案完成，可执行配奖动作" }); 
            }
        }

        public Event_Reward()
        {
            InitLanguage();
        }

        public string Title
        {
            get
            {
                return EventReward["title"][iIndex];
            }
        }      
        public string Event_No
        {
            get
            {
                return EventReward["event_no"][iIndex];
            }
        }
        public string Event_Name
        {
            get
            {
                return EventReward["event_name"][iIndex];
            }
        }
        public string Vender_No
        {
            get
            {
                return EventReward["vender_no"][iIndex];
            }
        }
        public string Vender
        {
            get
            {
                return EventReward["vender"][iIndex];
            }
        }
        public string RwdType
        {
            get
            {
                return EventReward["rwd_type"][iIndex];
            }
        }
        public string RwdLevel
        {
            get
            {
                return EventReward["rwd_level"][iIndex];
            }
        }
        public string RwdName
        {
            get
            {
                return EventReward["rwd_name"][iIndex];
            }
        }
        public string RwdNumber
        {
            get
            {
                return EventReward["rwd_number"][iIndex];
            }
        }
        public string RwdSMS
        {
            get
            {
                return EventReward["rwd_sms"][iIndex];
            }
        }
        public string RwdMemo
        {
            get
            {
                return EventReward["rwd_memo"][iIndex];
            }
        }
        public string EventList
        {
            get
            {
                return EventReward["event_list"][iIndex];
            }
        }
        public string EventTitle
        {
            get
            {
                return EventReward["event_title"][iIndex];
            }
        }
        public string RwdAllot
        {
            get
            {
                return EventReward["rwd_allot"][iIndex];
            }
        }
        public string RwdVender
        {
            get
            {
                return EventReward["rwd_vender"][iIndex];
            }
        }
        public string RwdUpload
        {
            get
            {
                return EventReward["rwd_upload"][iIndex];
            }
        }
        public string RwdSelectFile
        {
            get
            {
                return EventReward["rwd_select_file"][iIndex];
            }
        }
        public string RwdUploadFile
        {
            get
            {
                return EventReward["rwd_upload_file"][iIndex];
            }
        }
        public string RwdCompleted
        {
            get
            {
                return EventReward["rwd_completed"][iIndex];
            }
        }
        public string RwdCheckAllot 
        {
            get
            {
                return EventReward["rwd_check_allot"][iIndex];
            }
        }
        public string RwdState
        {
            get
            {
                return EventReward["rwd_state"][iIndex];
            }
        }
        public string QRRecords
        {
            get
            {
                return EventReward["qr_records"][iIndex];
            }
        }
        public string AssignAllot
        {
            get
            {
                return EventReward["assign_allot"][iIndex];
            }
        }
        public string RwdImage
        {
            get
            {
                return EventReward["rwd_image"][iIndex];
            }
        }
        public string RwdWinDesc
        {
            get
            {
                return EventReward["rwd_win_desc"][iIndex];
            }
        }


        public string RwdEffectiveTime
        {
            get
            {
                return EventReward["rwd_effective_time"][iIndex];
            }
        }

        public string QR_GenDate
        {
            get
            {
                return EventReward["qr_gendate"][iIndex];
            }
        }
        public string QR_Qty
        {
            get
            {
                return EventReward["qr_qty"][iIndex];
            }
        }
        public string QR_Employee
        {
            get
            {
                return EventReward["qr_employee"][iIndex];
            }
        }
        public string QR_CompletedDate
        {
            get
            {
                return EventReward["qr_completed_date"][iIndex];
            }
        }

        public string Error_RwdType
        {
            get
            {
                return EventReward["err_rwd_type"][iIndex];
            }
        }
        public string Error_RwdLevel
        {
            get
            {
                return EventReward["err_rwd_level"][iIndex];
            }
        }
        public string Error_RwdName
        {
            get
            {
                return EventReward["err_rwd_name"][iIndex];
            }
        }
        public string Error_RwdNumber
        {
            get
            {
                return EventReward["err_rwd_number"][iIndex];
            }
        }
        public string Error_RwdVender
        {
            get
            {
                return EventReward["err_rwd_vender"][iIndex];
            }
        }

        public string MsgUploadOK
        {
            get
            {
                return EventReward["msg_rwd_upload_ok"][iIndex];
            }
        }
    }

    public class Event_Export : LanguageBase
    {
        public static Dictionary<string, string[]> EventExport = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (EventExport.Count == 0)
            {
                EventExport.Add("title", new string[] { "活動資料匯出", "Event Export", "活动资料汇出" });                
                EventExport.Add("event_no", new string[] { "專案編號", "Project No", "专案编号" });
                EventExport.Add("event_name", new string[] { "專案名稱", "Project Name", "专案名称" });
                EventExport.Add("event_list", new string[] { "事件清單 ", "Event List ", "事件清单 " });
                EventExport.Add("employee", new string[] { "下載員工", "Employee", "下载员工" });               
                EventExport.Add("download_datetime", new string[] { "下載日期時間", "Download Datetime", "下载日期时间" });
                EventExport.Add("down_load", new string[] { "下載碼檔案", "Download Code File", "下载码档案" });
                EventExport.Add("chk_down_load", new string[] { "確定下載碼檔案", "Make Sure Download File", "确定下载码档案" });
                EventExport.Add("download_state", new string[] { "下載狀態", "State", "下载状态" });
                EventExport.Add("log", new string[] { "查詢LOG", "Search Log", "查询LOG" });

                EventExport.Add("serial_no", new string[] { "序號", "State", "序号" });
                EventExport.Add("qr", new string[] { "QRCODE", "QRCODE", "QRCODE" });
                EventExport.Add("sbit", new string[] { "序號補碼", "SBIT", "序号补码" });
                EventExport.Add("qrbit", new string[] { "QR補碼", "QRBIT", "QR补码" });
                EventExport.Add("rwd_vender", new string[] { "宜睿碼", "Vender Code", "宜睿码" });
                EventExport.Add("rwd_level", new string[] { "獎項層級", "Level", "奖项层级" });
                EventExport.Add("rwd_name", new string[] { "獎項名稱", "name", "奖项名称" });
                EventExport.Add("rwd_qty", new string[] { "獎項總數", "Number", "奖项总数" });
                EventExport.Add("rwd_url", new string[] { "活動網址", "URL", "活动网址" });

                ///*error*/
                EventExport.Add("err_employee", new string[] { "下載員工必要", "Employee required", "下载员工必要" });              
                
                //EventReward.Add("msg_rwd_upload_ok", new string[] { "上傳檔案完成，可執行配獎動作", "Upload file completed,continue to allot.", "上传档案完成，可执行配奖动作" }); 
            }
        }

        public Event_Export()
        {
            InitLanguage();
        }

        public string Title
        {
            get
            {
                return EventExport["title"][iIndex];
            }
        }
        public string Event_No
        {
            get
            {
                return EventExport["event_no"][iIndex];
            }
        }
        public string Event_Name
        {
            get
            {
                return EventExport["event_name"][iIndex];
            }
        }
        public string EventList
        {
            get
            {
                return EventExport["event_list"][iIndex];
            }
        }
        public string Employee
        {
            get
            {
                return EventExport["employee"][iIndex];
            }
        }
        public string SerialNo
        {
            get
            {
                return EventExport["serial_no"][iIndex];
            }
        }
        public string QR
        {
            get
            {
                return EventExport["qr"][iIndex];
            }
        }
        public string SBit
        {
            get
            {
                return EventExport["sbit"][iIndex];
            }
        }
        public string QRBit
        {
            get
            {
                return EventExport["qrbit"][iIndex];
            }
        }
        public string RWD_Vender
        {
            get
            {
                return EventExport["rwd_vender"][iIndex];
            }
        }
        public string RWD_Level
        {
            get
            {
                return EventExport["rwd_level"][iIndex];
            }
        }
        public string RWD_Name
        {
            get
            {
                return EventExport["rwd_name"][iIndex];
            }
        }
        public string RWD_Qty
        {
            get
            {
                return EventExport["rwd_qty"][iIndex];
            }
        }
        public string RWD_URL
        {
            get
            {
                return EventExport["rwd_url"][iIndex];
            }
        }
        public string SearchLog
        {
            get
            {
                return EventExport["log"][iIndex];
            }
        }

        public string DownloadDatetime
        {
            get
            {
                return EventExport["download_datetime"][iIndex];
            }
        } 
        public string Download
        {
            get
            {
                return EventExport["down_load"][iIndex];
            }
        }
        public string CheckDownload
        {
            get
            {
                return EventExport["chk_down_load"][iIndex];
            }
        }
        public string DownloadState
        {
            get
            {
                return EventExport["download_state"][iIndex];
            }
        }
       
        public string Error_Employee
        {
            get
            {
                return EventExport["err_employee"][iIndex];
            }
        }
    }

    public class User_Lan : LanguageBase
    {
        public static Dictionary<string, string[]> VenderSet = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (VenderSet.Count == 0)
            {
                VenderSet.Add("title", new string[] { "使用者資料管理", "Uaer Management Info", "使用者資料管理" });
                VenderSet.Add("account", new string[] { "使用者帳號", "Account", "使用者帳號" });
                VenderSet.Add("name", new string[] { "使用者名稱", "Name", "使用者名稱" });
                VenderSet.Add("password", new string[] { "使用者密碼", "Password", "使用者密碼" });
                VenderSet.Add("login_time", new string[] { "登入時間", "LoginTime", "登入時間" });
                VenderSet.Add("new_user", new string[] { "新增使用者帳號", "Add User", "新增使用者帳號" });
            }
        }

        public User_Lan()
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

        public string Account
        {
            get
            {
                return VenderSet["account"][iIndex];
            }
        }

        public string Name
        {
            get
            {
                return VenderSet["name"][iIndex];
            }
        }

        public string Password
        {
            get
            {
                return VenderSet["password"][iIndex];
            }
        }

        public string LoginTime
        {
            get
            {
                return VenderSet["login_time"][iIndex];
            }
        }

        public string NewUser
        {
            get
            {
                return VenderSet["new_user"][iIndex];
            }
        }

    }

    public class ChangeCode_Lan : LanguageBase
    {
        public static Dictionary<string, string[]> VenderSet = new Dictionary<string, string[]>();

        public static void InitLanguage()
        {
            if (VenderSet.Count == 0)
            {
                VenderSet.Add("title", new string[] { "變更密碼", "Vender Info", "厂商资料管理" });
                VenderSet.Add("old_pass", new string[] { "輸入舊密碼", "Account", "厂商帐号" });
                VenderSet.Add("new_pass", new string[] { "輸入新密碼", "FullName", "厂商全名" });
                VenderSet.Add("confirm_pass", new string[] { "確認新密碼", "FullName", "厂商全名" });
               
            }
        }

        public ChangeCode_Lan()
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

        public string Old_Pass
        {
            get
            {
                return VenderSet["old_pass"][iIndex];
            }
        }

        public string New_Pass
        {
            get
            {
                return VenderSet["new_pass"][iIndex];
            }
        }

        public string Confirm_Pass
        {
            get
            {
                return VenderSet["confirm_pass"][iIndex];
            }
        }

    }
}