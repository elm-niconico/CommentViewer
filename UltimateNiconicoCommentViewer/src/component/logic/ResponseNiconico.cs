using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl;


namespace UltimateNiconicoCommentViewer.src.viewModel
{



    public class ResponseNiconico
    {
        private ConnectNicoNico _connect;


        public ResponseNiconico(ConnectNicoNico connect)
        {
            _connect = connect;
        }


        public async IAsyncEnumerable<object[]> GetResponseMessage(string liveId)
        {
            await _connect.SetLiveStatus(liveId);
            await foreach (var response in _connect.ConnectLive())
            {
                var comment = XmlParse.parseCommentXml(response, "chat");
                var userId = XmlParse.parseUserIdXml(response);
                var userName = userId;
                int id;
                bool isNot184 = int.TryParse(userId, out id);
                var userIcon = new Uri(DirectoryPaths.UserDefaultIconPath, UriKind.Relative);
                if (isNot184)
                {
                    userName = await _connect.ExstractUserName(id);
                    if (await _connect.ExstractUserIcon(id))
                    {
                        userIcon = new Uri(ApiURL.USER_ICON_URL(id));
                    }

                }


                if (comment.NotEmpty() && userId.NotEmpty())
                    yield return new object[] { userIcon, userName, comment, userId };

            }


        }



        /// <summary>
        /// ログインを試みます
        /// True  -> 成功
        /// False -> 失敗
        /// </summary>
        /// <param name="userAddr"> ユーザーのメールアドレス </param>
        /// <param name="password"> ニコニコのパスワード </param>
        /// <returns></returns>
        public async Task<bool> LoginNicoNicoAccount(string userAddr, string password)
        {
            var response = await _connect.LoginNiconicoAccount(userAddr, password);
            return response;
        }










    }
}
