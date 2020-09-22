using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using UltimateNiconicoCommentViewer.src.common;

namespace UltimateNiconicoCommentViewer.src.model.httpClient
{
    class HttpClientHolder
    {
        public static HttpClient client { get; set; }

        /// <summary>
        /// HTTPCLIENTを返します
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetStaticClient() => client;

        static HttpClientHolder()
        {
            client = NewHttpClient(GetHandler());
        }


        /// <summary>
        /// インスタンス生成禁止
        /// </summary>
        private HttpClientHolder() { }

        /// <summary>
        /// HttpClientの初期化を行います
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static HttpClient NewHttpClient(HttpClientHandler handler)
        {
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add(NicoString.USER_AGENT,
                                             NicoString.CONTENT_TYPE);
            return client;
        }

        /// <summary>
        /// クライアントハンドラーを返します
        /// </summary>
        /// <returns></returns>
        public static HttpClientHandler GetHandler() => new HttpClientHandler()
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true,
        };
    }
}
