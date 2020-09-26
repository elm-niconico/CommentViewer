using System;
using System.Net;
using System.Net.Http;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.viewModel;

namespace UltimateNiconicoCommentViewer.src.model.httpClient
{
    public class HttpClientBuilder
    {

        /// <summary>
        /// インスタンス生成禁止
        /// </summary>
        private HttpClientBuilder() { }

        /// <summary>
        /// HttpClientの初期化を行います
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static HttpClient NewHttpClient()
        {
            var client = new HttpClient(GetHandler());
            client.DefaultRequestHeaders.Add(NicoString.USER_AGENT,
                                             NicoString.CONTENT_TYPE);
            return client;
        }

        /// <summary>
        /// クライアントハンドラーを返します
        /// </summary>
        /// <returns></returns>
        private static HttpClientHandler GetInitHandler() => new HttpClientHandler()
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true,
        };

        private static HttpClientHandler GetHandler()
        {
            var value = UserSetting.Default.cookieValue;
            if (value == null || value.IsEmpty()) return GetInitHandler();
            var con = new SqlConnectionCookie();
            if (con.ReadCookie(UserSetting.Default.userCookiePath))
            {
                var uri = new Uri(NicoString.NICO_URL);
                var cookie = new Cookie(NicoString.USER_SESSION, value, NicoString.SLASH, NicoString.NICO_NAME);
                var handler = GetInitHandler();
                handler.CookieContainer.Add(uri, cookie);
                return handler;
            }
            else
            {
                return GetInitHandler();

            }
        }

    }
}
