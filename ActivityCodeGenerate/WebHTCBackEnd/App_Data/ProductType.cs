using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace WebHTCBackEnd.classes
{
    public class ProductType
    {
        private static Dictionary<short, NameValueCollection> proTypes = new Dictionary<short, NameValueCollection>();
        
        public static void RefreshProductType()
        {
            proTypes.Clear();
            DB.DataBaseControl dbCtl = new DB.DataBaseControl();
            string strSQL = "select * from product_type order by PT001,PT003";

            try
            {
                dbCtl.Open();
                DataTable proTable = dbCtl.GetDataTable(strSQL, null);

                DataTable keyTable = proTable.DefaultView.ToTable(true, "PT001");
                DataRow[] proRows;
                foreach (DataRow keyRow in keyTable.Rows)
                {
                    proRows = proTable.Select("PT001=" + keyRow[0].ToString());
                    NameValueCollection proPair = new NameValueCollection(proRows.Length);
                    for (short i = 0; i < proRows.Length; i++)
                    {
                        proPair.Add(proRows[i]["PT004"].ToString(), proRows[i]["PT002"].ToString());                       
                    }                    
                    proTypes.Add(short.Parse(keyRow[0].ToString()), proPair);
                }
            }
            catch (Exception ex)
            {
                //error.Number = 900001;
                //error.ErrorMessage = ex.ToString();
            }
            finally
            {
                
                dbCtl.Close();
            }
        }

        public static NameValueCollection GetProductType(Language.Lan_Zone lan)
        {
            short index = (short)lan;
            return proTypes[index];
        }
    }


    public class RewardType
    {
        private static Dictionary<short, NameValueCollection> rwdTypes = new Dictionary<short, NameValueCollection>();

        public static void RefreshRewardType()
        {
            rwdTypes.Clear();
            DB.DataBaseControl dbCtl = new DB.DataBaseControl();
            string strSQL = "select * from reward_type order by RT001,RT003";

            try
            {
                dbCtl.Open();
                DataTable rwdTable = dbCtl.GetDataTable(strSQL, null);

                DataTable keyTable = rwdTable.DefaultView.ToTable(true, "RT001");
                DataRow[] proRows;
                foreach (DataRow keyRow in keyTable.Rows)
                {
                    proRows = rwdTable.Select("RT001=" + keyRow[0].ToString());
                    NameValueCollection proPair = new NameValueCollection(proRows.Length);
                    for (short i = 0; i < proRows.Length; i++)
                    {
                        proPair.Add(proRows[i]["RT004"].ToString(), proRows[i]["RT002"].ToString());
                    }
                    rwdTypes.Add(short.Parse(keyRow[0].ToString()), proPair);
                }
            }
            catch (Exception ex)
            {
                //error.Number = 900001;
                //error.ErrorMessage = ex.ToString();
            }
            finally
            {

                dbCtl.Close();
            }
        }

        public static NameValueCollection GetRewardType(Language.Lan_Zone lan)
        {
            short index = (short)lan;
            return rwdTypes[index];
        }
    }


    public class RewardVender
    {
        private static Dictionary<short, NameValueCollection> rwdVender = new Dictionary<short, NameValueCollection>();

        public static void RefreshRewardVender()
        {
            rwdVender.Clear();
            DB.DataBaseControl dbCtl = new DB.DataBaseControl();
            string strSQL = "select * from reward_vender order by RDWV001,RDWV003";

            try
            {
                dbCtl.Open();
                DataTable rwdTable = dbCtl.GetDataTable(strSQL, null);

                DataTable keyTable = rwdTable.DefaultView.ToTable(true, "RDWV001");
                DataRow[] proRows;
                foreach (DataRow keyRow in keyTable.Rows)
                {
                    proRows = rwdTable.Select("RDWV001=" + keyRow[0].ToString());
                    NameValueCollection proPair = new NameValueCollection(proRows.Length);
                    for (short i = 0; i < proRows.Length; i++)
                    {
                        proPair.Add(proRows[i]["RDWV004"].ToString(), proRows[i]["RDWV002"].ToString());
                    }
                    rwdVender.Add(short.Parse(keyRow[0].ToString()), proPair);
                }
            }
            catch (Exception ex)
            {
                //error.Number = 900001;
                //error.ErrorMessage = ex.ToString();
            }
            finally
            {

                dbCtl.Close();
            }
        }

        public static NameValueCollection GetRewardVender(Language.Lan_Zone lan)
        {
            short index = (short)lan;
            return rwdVender[index];
        }
    }
}
