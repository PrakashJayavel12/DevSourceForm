using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Encrypt_V5
{
    public partial class Encrypt
    {
        //[DllImport("aesmaster.dll", CallingConvention = CallingConvention.Cdecl)]
        //[DllImport("aesmaster.dll")]
        [System.Runtime.InteropServices.DllImport("aesmaster.dll")]
        public static extern void encrypt(string msg, StringBuilder output);
        public string Encrytion(string value)
        {
            string EncryptValue = "";
            StringBuilder SB = new StringBuilder(8192);
            try
            {
                encrypt(value, SB);
                EncryptValue = SB.ToString().Trim();//System.Runtime.InteropServices.Marshal.PtrToStringAnsi(SB);
            }
            catch (System.AccessViolationException ex)
            {

            }
            return EncryptValue;
        }
        //[DllImport("aesmaster.dll")]
        [System.Runtime.InteropServices.DllImport("aesmaster.dll", CharSet = CharSet.Ansi)]
        public static extern void decrypt(string b, StringBuilder doutput);
        public string decrypt(string val)
        {
            string decryptvalue = "";
            string output = val.ToString().Trim();
            StringBuilder SB2 = new StringBuilder(8192);
            try
            {
                decrypt(output, SB2);
                decryptvalue = SB2.ToString().Trim();//.Replace("api.pospatrol.com","apipos.kafd.sa"); ;//System.Runtime.InteropServices.Marshal.PtrToStringAnsi(intptr);
            }
            catch (System.AccessViolationException ex)
            {

            }
            return decryptvalue;
        }

        public string DecPassword45(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            try
            {
                string password = "MAKV2SPBNI99212";
                byte[] buffer = Convert.FromBase64String(data);

                using (Aes aes = Aes.Create())
                {
                    byte[] salt = new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 };
                    var key = new Rfc2898DeriveBytes(password, salt);
                    aes.Key = key.GetBytes(32);
                    aes.IV = key.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        return Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                // Optional: log error
                return string.Empty; // fallback
            }
        }

        public string DecPassword35(string data)
        {
            string Password;
            string fsEncPassword = string.Empty;
            //int nCount;


            for (int iCount = 1; iCount <= Convert.ToInt32(Strings.Len(data), null); iCount++)
            {
                Password = string.Empty;
                Password = Convert.ToString(Strings.Asc(Strings.Mid(data, iCount, 1)), null);
                Password = Convert.ToString((Conversion.Val(Password) + 15) - 120, null);
                Password = Convert.ToString(Strings.Chr(Convert.ToInt32(Password, null)), null);
                fsEncPassword = string.Concat(fsEncPassword, Password);
            }
            return fsEncPassword;
        }


    }
}
