using System.Threading.Tasks;

namespace UltimateNiconicoCommentViewer.src.model.getCommentLogic
{
    interface IConnectNicoNico
    {


        /// <summary>
        /// 放送の情報を取得します
        /// </summary>
        /// <returns>XML形式で返す</returns>
        Task<string> SetLiveStatus(string liveId);

        /// <summary>
        /// コメントサーバに接続します
        /// </summary>
        Task<string> ConnectLive();

    }
}
