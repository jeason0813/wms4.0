using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class SecurityEncrypt
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string ESCEncrypt(string pToEncrypt, string sKey)
        {
            sKey = sKey == "" ? "20160403" : sKey;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
            provider.Key = Encoding.ASCII.GetBytes(sKey);
            provider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            builder.ToString();
            return builder.ToString();
        }

       /// <summary>
        /// 解密
       /// </summary>
       /// <param name="pToDecrypt"></param>
       /// <param name="sKey"></param>
       /// <returns></returns>
        public static string ESCDecrypt(string pToDecrypt, string sKey)
        {
            sKey = sKey == "" ? "20160403" : sKey;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < (pToDecrypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(sKey);
            provider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            return Encoding.Default.GetString(stream.ToArray());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(password);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length-1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
       
    }
}
