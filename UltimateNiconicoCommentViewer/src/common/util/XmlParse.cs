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
 
        public static string[][] ParseMylistXml(string response)
        {
            var ids = Regex.Matches(response,@$"(?<=id>).*(?=</id)");
            var names = Regex.Matches(response, @$"(?<=name>).*(?=</name)");
            string[][] result = new string[ids.Count][];
           
            for(int i=0; i<ids.Count; i++)
            {
                result[i] = new string[] { ids[i].ToString(), names[i].ToString() };
            }
        
            return result;
            
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
