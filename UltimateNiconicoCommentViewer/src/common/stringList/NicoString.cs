using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateNiconicoCommentViewer.src.common
{
    /// <summary>
    /// 放送に接続する際にしようする値を格納します
    /// </summary>
    public class NicoString
    {

        public const string NICO_URL = @"http://www.nicovideo.jp";

        public const string USER_SESSION = "user_session";

        public const string SLASH = "/";

        public const string NICO_NAME = ".nicovideo.jp";

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public const string USER_AGENT = "User-Agent";

        

        /// <summary>
        /// 自分への連絡先(SNSとかないので仮でA)
        /// </summary>
        public const string USER_INFOMATION = "A";

        /// <summary>
        /// コンテンツタイプ(使用していない)
        /// </summary>
        public const string CONTENT_TYPE = "application/x-www-form-urlencoded";

        /// <summary>
        /// メールアドレスのKey 
        /// </summary>
        public const string MAIL_TEL = "mail_tel";

        /// <summary>
        /// パスワードのKey
        /// </summary>
        public const string PASSWORD = "password";

        /// <summary>
        /// ログインしているかどうかのフラグ
        /// 0ならログインできていません
        /// 1なら一般ユーザーとしてログイン中
        /// 3ならプレミアムユーザーとしてログイン中
        /// </summary>
        public const string LOGIN_FLAG_KEY = "x-niconico-authflag";

        /// <summary>
        /// コメントサーバから接続が終了した際のメッセージです
        /// </summary>
        public const string FINISH_CONNECT_SERVER = "接続が終了しました";


        public const string DOESNOT_EXISTS_MYLIST = "マイリストが存在しません";

        public const string CAN_NOT_FOUNT_MYLIST = "マイリストを取得できません";
    }


}
