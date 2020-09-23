using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    class ProcessSupport
    {

        /// <summary>
        /// Webブラウザに遷移します
        /// </summary>
        /// <param name="fileName"> 遷移先ののURL </param>
        /// <param name="badMessage"> 失敗した場合のメッセージ </param>
        /// <returns></returns>
        public static bool GoToWebBrowser(string fileName,string badMessage)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = fileName,
                UseShellExecute = true,
            };

            try
            {
                Process.Start(psi);
            }catch(Exception)
            {
                MessageBox.Show(badMessage);
                return false;
            }
            return true;

        }
    }
}
