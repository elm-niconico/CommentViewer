using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.connectLogic;
using UltimateNiconicoCommentViewer.src.model.httpClient;
using UltimateNiconicoCommentViewer.src.model.urlCreate;

namespace UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl
{
    public class ConnectNicoNico : IConnectNicoNico

    {
        private  UrlCreate _urlCreate { get;  set; }

        private  LoginLogic _loginLogic { get; set;}

        private  ConnectionLogic _connectionLogic { get; set; }

        public string responseMessage { get; private set; }

        public string responseComment { get; private set; }


        
        public ConnectNicoNico(UrlCreate urlCreate, LoginLogic login, ConnectionLogic connection)
        {
            _urlCreate =  urlCreate;
            _loginLogic = login;
            _connectionLogic = connection;
        }
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static ConnectNicoNico()
        {
            
            
        }

        public async IAsyncEnumerable<string> ConnectLive()
        {
            string addr = XmlParse.parseXml(responseMessage, XmlKeys.ADDR);
            string port = XmlParse.parseXml(responseMessage, XmlKeys.PORT);
            string thread = XmlParse.parseXml(responseMessage, XmlKeys.THREAD);
            if(addr.NotEmpty() && port.NotEmpty() && thread.NotEmpty())
            {
                await foreach (var response in _connectionLogic.connectLive(thread, addr, port))
                {
                    yield return response;
                }
            }
           

        }


        /// <summary>
        /// 対象の配信情報を取得し、文字列として格納します
        /// </summary>
        /// <param name="liveId"> Lv** から始まるライブID </param>
        public async Task<string> SetLiveStatus(string liveId)
        {
            var response = await _connectionLogic.GetLiveStatus(liveId);
            this.responseMessage = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response);
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            return this.responseMessage;
        }

        /// <summary>
        /// ログインを試みます
        /// 成功した場合Trueを返します
        /// </summary>
        /// <param name="mail_tel"> メールアドレス </param>
        /// <param name="password"> アカウントのパスワード </param>
        /// <returns></returns>
        public async Task<bool> LoginNiconicoAccount(string mail_tel, string password)
        {
            int loginStatus = await _loginLogic.GetLoginStatusNumber(mail_tel, password);
            return loginStatus > 0;
        }


        public async Task<bool> getUserIcon(int userId)
        { 
            return await _connectionLogic.getUserIcon(userId);
        }


        public async Task<string> getUserName(int userId)
        {
            var response = await _connectionLogic.getUserName(userId);
            var xmlStr =  await response.Content.ReadAsStringAsync();
            return XmlParse.parseXml(xmlStr, "nickname");
        }
        

        Task<string> IConnectNicoNico.ConnectLive()
        {
            throw new NotImplementedException();
        }
    }

    
}
