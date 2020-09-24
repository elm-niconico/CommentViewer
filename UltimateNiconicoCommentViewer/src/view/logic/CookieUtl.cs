using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

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
            }
            catch (Win32Exception)
            {
                Debug.WriteLine("クッキーの取得に失敗しました");
            }

            return cookie;
        }




    }
}
