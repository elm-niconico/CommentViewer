using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UltimateNiconicoCommentViewer.src.common.util
{
    public class HttpStatusCheck
    {
        /// <summary>
        /// 有効なページかチェックします
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<bool> IsNotEnabledUrl(string url)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var reuslt = await client.GetAsync(url);
                    return !reuslt.IsSuccessStatusCode;
                }catch(Exception)
                {
                    return true;
                }
                
                
                
            }
        }
    }
}
