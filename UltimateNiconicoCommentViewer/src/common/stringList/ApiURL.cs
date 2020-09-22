using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateNiconicoCommentViewer.src.common.stringList
{
    public class ApiURL
    {
        /// <summary>
        /// ログインする際に使用するURL
        /// </summary>
        public const string LOGIN_URL       = "https://account.nicovideo.jp/api/v1/login";

        /// <summary>
        /// 配信情報を取得するときに使用するURL
        /// </summary>
        public const string LIVE_STATUS_URL = "http://live.nicovideo.jp/api/getplayerstatus";

        public static string USER_ICON_URL(int userId) => $@"http://usericon.nimg.jp/usericon/{userId/10000}/{userId}.jpg";

        public static  string CONNECT_SEVER_URL(string threadId, int logNum)
      => $"<thread thread=\"{threadId}\" res_from=\"{logNum}\" version=\"20061206\" scores=\"1\" />\0";

        /// <summary>
        /// ユーザーIDを指定して
        /// ユーザーネームを取得します。
        /// </summary>
        /// <param name="userId"> ユーザーID </param>
        /// <returns>　ユーザーネーム　</returns>
        public static string GET_USER_NAME(int userId) => @$"https://seiga.nicovideo.jp/api/user/info?id={userId}";

        /// <summary>
        /// ユーザーのプロフィールページに移動します
        /// </summary>
        /// <param name="userId"></param>s
        /// <returns></returns>
        public static string GO_TO_USER_PROFILE(int userId) => $@"https://www.nicovideo.jp/user/{userId}";

        /// <summary>
        /// ユーザーのマイリストを取得します
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GET_USER_MYLIST(int userId) => $@"http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup.get?detail=0&user_id=${userId}";


        /// <summary>
        /// 対象のマイリストページに移動します
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mylistId"></param>
        /// <returns></returns>
        public static string GO_TO_MYLIST_LINK(int userId, int mylistId) => $@"https://www.nicovideo.jp/user/{userId}/mylist/{mylistId}";
    }

}
