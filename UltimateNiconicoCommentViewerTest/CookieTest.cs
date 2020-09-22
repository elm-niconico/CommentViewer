using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UltimateNiconicoCommentViewer.src.viewModel;

namespace UltimateNiconicoCommentViewerTest
{
    class CookieTest
    {
        private CookieUtl utl = new CookieUtl();

        //暗号化で使用する追加のバイト配列
        private byte[] entropy = new byte[] { 0x72, 0xa2, 0x12, 0x04 };

        [Test]
        public void クッキーファイルの取得_成功()
        { 
            using(SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\merut\AppData\Local\Google\Chrome\User Data\Default\Cookies"))
            {
                try
                {

                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT encrypted_value, value, name FROM cookies WHERE host_key='.nicovideo.jp' AND name='user_session';", conn);
                  
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        
             

                        while (reader.Read())
                        {
                            var s = (Byte[])reader.GetValue(0);
                            var n = reader.GetValue(1);
                            var nv = reader.GetValue(2);
                            ////復号化する
                            byte[] userData = System.Security.Cryptography.ProtectedData.Unprotect(
                                s, null,
                                System.Security.Cryptography.DataProtectionScope.CurrentUser);
                            TestContext.WriteLine(nv.ToString() + "11");
                            TestContext.WriteLine(Encoding.ASCII.GetString(userData));
                            TestContext.WriteLine(s);
                          
                        }
                        
                    }
                       
                    }catch(Exception e)
                {
                    TestContext.WriteLine(e.Message);
                }
            }
        }
    }
}