using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    public class ProcessSupport
    {
        /// <summary>
        /// URLの遷移を試行します。
        /// そのURLが有効ではない場合,末尾の一文字を削除し、新たにチェックします。
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task ForceMovingUrlPage(string url)
        {
            while (await HttpStatusCheck.IsNotEnabledUrl(url))
            {
                if (URLParse.IsEndCharacterEqualSlash(url))
                {
                    MessageBox.Show(Message.NOT_VALID_URL);
                    return;
                }
                url = url.Substring(0, url.Length-1);   
            }
            GoToWebBrowser(url,Message.FAIL_GO_TO_USER_VIDEO);
        }


        /// <summary>
        /// Webブラウザに遷移します
        /// </summary>
        /// <param name="fileName"> 遷移先ののURL </param>
        /// <param name="badMessage"> 失敗した場合のメッセージ </param>
        /// <returns></returns>
        public static bool GoToWebBrowser(string fileName, string badMessage)
        {
            if (fileName == null || fileName.IsEmpty()) return false;
            var psi = new ProcessStartInfo()
            {
                FileName = fileName,
                UseShellExecute = true,
            };

            try
            {
                Process.Start(psi);
            }
            catch (Exception)
            {
                if (badMessage.NotNull())
                {
                    MessageBox.Show(badMessage);
                }
                return false;
            }
            return true;

        }
    }
}
