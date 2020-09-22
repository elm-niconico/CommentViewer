using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.viewModel;

namespace UltimateNiconicoCommentViewer.src.view.logic
{
    public enum SelectCookieWindowLogic
    {
        INSTANCE,

    }public static class SelectExtends
    {


        /// <summary>
        /// クッキーファイルのパスを参照するダイアログを生成します
        /// </summary>
        /// <param name="me"></param>
        /// <returns> パス </returns>
        public static string SelectCookieFile(this SelectCookieWindowLogic me)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return ofd.ShowDialog()==true? ofd.FileName : "";

        }

        /// <summary>
        /// クッキーファイルからNicoNicoにログインしているかを確認します。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="path"> クッキーファイルのディレクトリパス </param>
        /// <returns></returns>
        public static void IsLoginNiconicoFromCookie(this SelectCookieWindowLogic me, string path)
        {
            if (path == null || path.IsEmpty())
            {
                MessageBox.Show(Message.FILE_EMPTY);
                return;
            }
            var con = new SqlConnectionCookie();
            var result = con.ReadCookie(path);
            var message =  result? Message.LOGIN_OK : Message.LOGIN_FALSE;
            MessageBox.Show(message);
          
            if (result) SaveSettingsCookiePath(path);
        }

        /// <summary>
        /// クッキーパスを設定ファイルに保存
        /// </summary>
        /// <param name="cookiePath"> クッキーパス </param>
        private static void SaveSettingsCookiePath(string cookiePath)
        {
            UserSetting.Default.userCookiePath = cookiePath;
            UserSetting.Default.Save();
        }
    }

}
