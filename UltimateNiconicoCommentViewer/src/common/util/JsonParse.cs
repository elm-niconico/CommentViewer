using System.Text.RegularExpressions;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    class JsonParse
    {

        /// <summary>
        /// Jsonから特定の値を抜き出します
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseFromJson(string json, string key)
        {
            var match = Regex.Match(json, $"(?<={key}\":\")[^(\\\")]+");
            return Regex.Unescape(match.ToString());
        }
    }
}
