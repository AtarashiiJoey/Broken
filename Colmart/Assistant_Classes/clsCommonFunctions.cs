using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Security.Cryptography;
using Encoder = System.Text.Encoder;



namespace ColmartCMS.Assistant_Classes
{
    /// <summary>
    /// Common usage methods
    /// </summary>
    public class clsCommonFunctions
    {
        #region Password Methods

        /// <summary>
        /// Password generation function
        /// </summary>
        /// <param name="iPasswordLength"></param>
        /// <returns>a string of letters and numbers length of iPasswordLength</returns>
        public static string strCreateRandomPassword(int iPasswordLength)
        {
            string strAllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            Random rNum = new Random();
            string strNewPassWord = "";
            for (int i = 0; i < iPasswordLength; i++)
            {
                strNewPassWord += strAllowedChars[rNum.Next(strAllowedChars.Length)];
            }

            return strNewPassWord;
        }

        public static string GetMd5Sum(string str)
        {
            Encoder enc = Encoding.Unicode.GetEncoder();

            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            return sb.ToString();
        }

        #endregion
    }

}
