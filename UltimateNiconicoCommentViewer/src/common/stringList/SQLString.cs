using System.Reflection.Metadata;

namespace UltimateNiconicoCommentViewer.src.common.stringList
{
    class SQLString
    {
        //TODO データベースのディレクトリパス
        public const string DB_NAME = @"elmAppDb\userInfo.db";

        /// <summary>
        /// クッキーファイルからニコニコのUserSessionを探します
        /// </summary>
        public const string SELECT_USERSESSION_FROM_COOKIES = "SELECT name, encrypted_value FROM cookies WHERE host_key='.nicovideo.jp' AND name='user_session';";

        public static string INSERT_HANDLE_USER(string userId, string handle,string communityNum) => $"INSERT INTO user(userId,handle,community_num) VALUES('{userId}', '{handle}','{communityNum}')";

        public static string SELECT_USERID(string userId) => $"SELECT userId FROM user WHERE userId='{userId}'";

        public static string UPDATE_HANDLE(string handle, string userId,string communityNum) => $"UPDATE user SET handle='{handle}' WHERE userId='{userId}' AND community_num='{communityNum}'";

        public static string SELECT_HANDLE_WHERE_USERID(string userId,string communityNum) => $"SELECT handle FROM user WHERE userId='{userId}' AND community_num='{communityNum}'";

        public const string CREATE_USER_TABLE = "CREATE TABLE IF NOT EXISTS user(" +
                                                "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                "userId TEXT NOT NULL, " +
                                                "handle TEXT NOT NULL, " +
                                                "community_num TEXT NOT NULL)";

    }
}
