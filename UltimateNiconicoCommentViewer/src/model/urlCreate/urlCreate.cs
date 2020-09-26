using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common;

namespace UltimateNiconicoCommentViewer.src.model.urlCreate
{
    /// <summary>
    /// Http通信に使用する文字列またはURLを生成します
    /// </summary>
    public class UrlCreate
    {

        private UrlCreate() { }


        /// <summary>
        /// クエリパラメータを生成します
        /// </summary>
        /// <param name="baseUrl"> 基本となるURL </param>
        /// <param name="param"> 渡すパラメータ </param>
        /// <returns> クエリパラメータ </returns>
        public async Task<string> createQueryParam(string baseUrl, Dictionary<string, string> param)
        {
            return $"{baseUrl}?${await new FormUrlEncodedContent(param).ReadAsStringAsync()}";//URL修正
        }

        public StringContent createPostBody(Dictionary<string, string> param)
        {
            var jsonParam = JsonConvert.SerializeObject(param);
            return new StringContent(jsonParam, Encoding.UTF8, NicoString.CONTENT_TYPE);
        }


    }
}
