using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace THC_Library
{
    public class UserData
    {
        public static string EncrySaveData(string acc, string pwd)
        {
            string source = string.Format("{0}:{1}", acc, pwd);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("54FeiaT8");
            byte[] iv = Encoding.ASCII.GetBytes("jzDr5f8R");
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            des.Key = key;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

        public static bool DecrypSaveData(string source, out string acc, out string pwd)
        {
            acc = "";
            pwd = "";

            bool bResult = false;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("54FeiaT8");
            byte[] iv = Encoding.ASCII.GetBytes("jzDr5f8R");
            byte[] dataByteArray = Convert.FromBase64String(source);

            des.Key = key;
            des.IV = iv;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    string strSource = Encoding.UTF8.GetString(ms.ToArray());
                    string[] strSplit = strSource.Split(':');
                    if(strSplit.Length == 2)
                    {
                        acc = strSplit[0];
                        pwd = strSplit[1];
                        bResult = true;
                    }                   
                }
            }
            return bResult;
        }
    }
}
