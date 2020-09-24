using System.Text.RegularExpressions;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    public class URLParse
    {
        //放送IDを取得する正規表現
        private static readonly Regex _regex = new Regex("lv[0-9]+");

        /// <summary>
        /// 放送URLからLIVEIDを取得します。
        /// </summary>
        /// <param name="url"> 放送URL </param>
        /// <returns>　放送ID </returns>
        public static string tryParseUrl(string url)
        {
            return _regex.Match(url).ToString();
        }
    }
}
