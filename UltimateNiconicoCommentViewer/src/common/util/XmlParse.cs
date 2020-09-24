using System.Text.RegularExpressions;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    public class XmlParse
    {

        /// <summary>
        /// マイリストID
        /// マイリスト名を取得します
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string[] ParseMylistXml(string response)
        {
            var id = parseXml(response, "id");
            var name = parseXml(response, "name");
            return new string[] { id, name };

        }

        /// <summary>
        /// 正規表現をもとにXMLの検索するKeyの値を返します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public static string parseXml(string response, string key)
        {
            return Regex.Match(response, @$"(?<={key}>)[^<]*(?=</{key})").ToString();
        }

        /// <summary>
        /// 投稿動画のサムネイル画像を取得します。
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseXmlToImageURL(string response)
        {
            return Regex.Match(response, "(?<=src=\")[^\"]+").ToString();
        }


        public static MatchCollection ParseXmlMatches(string response, string key)
        {
            return Regex.Matches(response, @$"(?<={key}>)[^<]*(?=</{key})");
        }


        /// <summary>
        /// 正規表現をもとにXMLの検索するKeyの値を返します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public static string parseCommentXml(string response, string key)
        {
            return Regex.Match(response, @$"(?<={key}.+>).*(?=</{key})").ToString();
        }

        /// <summary>
        /// 正規表現をもとにXMLの検索するKeyの値を返します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public static string parseUserIdXml(string response)
        {
            return Regex.Match(response, $"(?<=user_id=\")[^\"]+(?=\")").ToString();
        }

        /// <summary>
        /// 投稿動画のURLを取得します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseUserVideoUrl(string response, string key)
        {
            var match = Regex.Match(response, "(?<=<media : thumbnail url=\")[^\"]+").ToString();
            return Regex.Unescape(match);
        }



    }
}
