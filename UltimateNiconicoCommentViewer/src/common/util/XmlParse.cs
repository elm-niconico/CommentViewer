using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var id   = parseXml(response, "id");
            var name = parseXml(response, "name");
            return new string[] { id, name };
            
        }

        /// <summary>
        /// 正規表現をもとにXMLの検索するKeyの値を返します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public static string parseXml(string response,string key)
        {
            return Regex.Match(response, @$"(?<={key}>).*(?=</{key})").ToString();
        }


        /// <summary>
        /// 正規表現をもとにXMLの検索するKeyの値を返します
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lastKey"></param>
        /// <returns></returns>
        public static string parseCommentXml(string response, string key)
        {
            return Regex.Match(response, @$"(?<={key}.+>)[^</{key}]*(?=</{key})").ToString();
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
    }
}
