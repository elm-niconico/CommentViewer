using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.model.httpClient;

namespace UltimateNiconicoCommentViewer.src.model.connectLogic
{
    public class LoginLogic
    {

        private static  HttpClient _client;

        /// <summary>
        /// インスタンス生成禁止
        /// </summary>
        public LoginLogic(HttpClient client) {
            _client = client;
        }

        /// <summary>
        /// レスポンスデータからログイン状態を表したナンバーを取得します
        /// </summary>
        /// <param name="mailTel"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<int> GetLoginStatusNumber(string mailTel, string password)
        {
            var response = await DoLogin(mailTel, password);
            int loginFlag = 0;
            System.Diagnostics.Debug.WriteLine(response);

            //TODO XMLPARSEを使う処理に変える予定
            foreach (var loginStatus in response.Headers.GetValues(NicoString.LOGIN_FLAG_KEY))
            {
                loginFlag = int.Parse(loginStatus);
                break;
            }
            return loginFlag;
        }

        /// <summary>
        /// ログインを試みます
        /// </summary>
        /// <param name="mailTel"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> DoLogin(string mailTel, string password)
        {
            //Post通信の際にセットするパラメータ
            var param = new Dictionary<string, string>() { { NicoString.MAIL_TEL, mailTel },
                                                           { NicoString.PASSWORD, password } };
            var content = new FormUrlEncodedContent(param);
            var response = await _client.PostAsync(ApiURL.LOGIN_URL, content);
            return response;
        }

    }
}
