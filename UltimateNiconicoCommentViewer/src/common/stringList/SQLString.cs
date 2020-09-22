using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateNiconicoCommentViewer.src.common.stringList
{
    class SQLString
    {
        /// <summary>
        /// クッキーファイルからニコニコのUserSessionを探します
        /// </summary>
        public const string SELECT_USERSESSION_FROM_COOKIES = "SELECT name, encrypted_value FROM cookies WHERE host_key='.nicovideo.jp' AND name='user_session';";
    }
}
