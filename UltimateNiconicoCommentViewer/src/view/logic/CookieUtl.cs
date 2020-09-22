using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Windows;
using UltimateNiconicoCommentViewer.src.common;

namespace UltimateNiconicoCommentViewer.src.viewModel
{
    public class CookieUtl
    {



        /// <summary>
        ///  Cookieを取得します
        /// </summary>
        /// <param name="url"> Cookieファイルの参照URI </param>
        /// <returns> </returns>
        public string getCokkie(string url)
        {
            
            var cookiePath = new Uri(url);
            var cookie = string.Empty;
            try
            {
                 cookie = Application.GetCookie(cookiePath);
            }catch(Win32Exception ex)
            {
                Debug.WriteLine("クッキーの取得に失敗しました");
            }

            return cookie;
        }


       

    }
}
