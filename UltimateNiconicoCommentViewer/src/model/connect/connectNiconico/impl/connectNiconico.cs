using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.connectLogic;

namespace UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl
{
    public class ConnectNicoNico : IConnectNicoNico
    {

        private LoginLogic _loginLogic { get; set; }

        private ConnectionLogic _connectionLogic { get; set; }

        public string responseMessage { get; private set; }

        public string responseComment { get; private set; }

        public ConnectNicoNico(LoginLogic login, ConnectionLogic connection)
        {
            _loginLogic = login;
            _connectionLogic = connection;
        }

        public async IAsyncEnumerable<string> ConnectLive()
        {
            string addr = XmlParse.parseXml(responseMessage, XmlKeys.ADDR);
            string port = XmlParse.parseXml(responseMessage, XmlKeys.PORT);
            string thread = XmlParse.parseXml(responseMessage, XmlKeys.THREAD);
            if (addr.NotEmpty() && port.NotEmpty() && thread.NotEmpty())
            {
                IAsyncEnumerable<string> stream =  _connectionLogic.ConnectLive(thread, addr, port);
                var first = await stream.FirstAsync();
              
                await foreach (var response in stream.Skip(1))
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
            
            return XmlParse.parseXml(responseMessage, XmlKeys.COMMUNITY_NUM); ;
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


        public async Task<bool> ExstractUserIcon(int userId)
        {
            return await _connectionLogic.GetUserIcon(userId);
        }


        public async Task<string> ExstractUserName(int userId)
        {
            var response = await _connectionLogic.GetUserName(userId);
            var xmlStr = await response.Content.ReadAsStringAsync();
            return XmlParse.parseXml(xmlStr, NicoString.USER_NICKNAME);
        }


        Task<string> IConnectNicoNico.ConnectLive()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _connectionLogic.Dispose();
        }
    }


}
