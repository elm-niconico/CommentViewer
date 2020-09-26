using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;

namespace UltimateNiconicoCommentViewer.src.view.logic
{
    public enum UserProfileLogic
    {
        INSTANCE,
    }
    public static class Extends
    {

        /// <summary>
        /// ユーザーのプロフィールページに遷移します
        /// 184の場合は処理を中断します。
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="userId"></param>
        public static void GoToUserProfile(this UserProfileLogic me, string userId)
        {
            if (userId.IsNotNumber()) return;
            ProcessSupport.GoToWebBrowser(ApiURL.GO_TO_USER_PROFILE(int.Parse(userId)),
                                          Message.FAIL_GO_TO_USER_PROFILE);



        }


        /// <summary>
        /// 対象のマイリストのページに遷移します。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="mylistId"></param>
        /// <param name="userId"></param>
        public static void GoToMylist(this UserProfileLogic me, string userId, string mylistId)
        {
            var url = ApiURL.GO_TO_MYLIST_LINK(userId, mylistId);
            ProcessSupport.GoToWebBrowser(url, Message.FAIL_GO_TO_MYLIST);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GoToVideo_MouseDown(this UserProfileLogic me, string uri)
        {
            ProcessSupport.GoToWebBrowser(ApiURL.GO_TO_VIDEO_PAGE(uri), Message.FAIL_GO_TO_USER_VIDEO);

        }

        /// <summary>
        /// 最新の投稿動画のページに遷移します
        /// </summary>
        /// <param name="me"></param>
        /// <param name="uri"></param>
        public static void GoToLatestViedeo_MouseDown(this UserProfileLogic me, string uri)
        {
            ProcessSupport.GoToWebBrowser(uri, Message.FAIL_GO_TO_USER_VIDEO);
        }
    }
}
