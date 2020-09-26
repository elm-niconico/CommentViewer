using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    public class URLParse
    {
        //放送IDを取得する正規表現
        private static readonly Regex _regexLv = new Regex("lv[0-9]+");

        private static readonly Regex _regexSm = new Regex("sm[0-9]+");

        private static readonly Regex _regexURL = new Regex("http(s)?://.+/[^ |　|$]*");

        private static readonly Regex _regexEndOfChar = new Regex("/$");



        /// <summary>
        /// 末尾が/ならtrueを返します
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEndCharacterEqualSlash(string str)
        {
            return _regexEndOfChar.IsMatch(str);
        }

        /// <summary>
        /// 放送URLからLIVEIDを取得します。
        /// </summary>
        /// <param name="url"> 放送URL </param>
        /// <returns>　放送ID </returns>
        public static string ParseUrlOrDefault(string url)
        {
            return _regexLv.Match(url).ToString();
        }

        /// <summary>
        /// コメントの中にURLがあった場合そのページに遷移する。
        /// lvかsmがあった場合絶対パスに変換する
        /// URLがなかった場合,エラーメッセージを出力してFalseを返す。
        /// 
        /// </summary>
        /// <param name="comment">  </param>
        /// <returns></returns>
        public static string TryUrlparseFromComment(string comment)
        {
            Match sm = _regexSm.Match(comment);
            Match lv = _regexLv.Match(comment);
            if (sm.Success)
            {
             
                return ApiURL.ABSOLUTE_VIDEO_PATH(sm.ToString());
            }
            else if (lv.Success)
            {
                return ApiURL.ABSOLUTE_LIVE_PATH(lv.ToString());
            }
            Match match = null;
            if ((match = _regexURL.Match(comment)).Success)
            {
                return match.ToString();
            }


            return null;
        }
    }
}
