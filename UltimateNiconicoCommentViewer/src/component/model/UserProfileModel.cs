using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.httpClient;
using static UltimateNiconicoCommentViewer.src.model.connectLogic.HttpMylistInfo;
using static UltimateNiconicoCommentViewer.src.model.Http.video.HttpUserVideoInfo;

namespace UltimateNiconicoCommentViewer.src.viewModel.dialog
{
    public class UserProfileModel
    {
        //ユーザーアイコン
        public BitmapImage userIcon { get; set; }
        //ユーザーネーム
        public string userName { get; set; }
        //コメント
        public string comment { get; set; }
        //ユーザーID
        public string userId { get; set; }
        //ユーザーが生IDでコメントしているか
        public bool isNamaId { get; set; }

        //マイリストID
        public string mylistId { get; set; }
        //マイリストのタイトル
        public string mylistName { get; set; }
        //マイリストの動画のタイトル
        public string mylistVideoUrl { get; set; }
        //マイリストの動画の名前
        public BitmapImage mylistSamune { get; set; }

        //最新の投稿動画のURL
        public string videoURL { get; set; }
        //最新の投稿動画のタイトル
        public string videoTitle { get; set; }
        //最新の投稿動画のサムネ
        public BitmapImage videoSamune { get; set; }



        private UserProfileModel() { }

        public class UserProfileBuilder
        {
            public async Task<UserProfileModel> Build(object[] selectItem)
            {
                return await new UserProfileModel().SetModelInfo(selectItem);
            }
        }

        public async Task<UserProfileModel> SetModelInfo(object[] selectItem)
        {
            var id = 0;
            userIcon = (BitmapImage)selectItem[0];
            comment = selectItem[2] as string;
            isNamaId = int.TryParse(selectItem[3] as string, out id);

            if (isNamaId)
            {
                userId = selectItem[3] as string;
                userName = selectItem[1] as string;
                var mylistInfo = await GetMylistId(userId);
                mylistId = mylistInfo[0];
                await SetUserVideoInfo();
                await SetMylistName_Samune_VideoTitle(mylistInfo[1]);

            }
            else
            {
                userId = $"{selectItem[3]}(184)";
                userName = "184";
                mylistName = NicoString.CAN_NOT_FOUNT_MYLIST;
            }
            return this;

        }

        private async static Task<string[]> GetMylistId(string userId)
        {
            using (var client = HttpClientBuilder.NewHttpClient())
            {
                var response = await client.GetAsync(ApiURL.GET_USER_MYLIST(userId));
                string[] mylistInfo = XmlParse.ParseMylistXml(await response.Content.ReadAsStringAsync());
                return mylistInfo;
            }
        }

        /// <summary>
        /// ユーザーの投稿動画の情報をセットします。
        /// </summary>
        /// <returns></returns>
        public async Task SetUserVideoInfo()
        {
            using (var client = HttpClientBuilder.NewHttpClient())
            {
                var httpUserVideo = await new HttpUserVideoInfoBuilder().Build(userId, client);
                videoSamune = httpUserVideo.GetVideoSamune();
                videoTitle = httpUserVideo.GetVideoTitle();
                videoURL = httpUserVideo.GetVideoURL();
            }
        }

        /// <summary>
        /// マイリスト欄の情報をセットします
        /// </summary>
        /// <param name="name"> マイリストのタイトル </param>
        /// <returns></returns>
        private async Task SetMylistName_Samune_VideoTitle(string mylistName)
        {

            if (mylistName.NotEmpty())
            {
                using (HttpClient client = HttpClientBuilder.NewHttpClient())
                {
                    this.mylistName = mylistName;
                    var httpMylist = await new HttpMylistInfoBuilder().Build(mylistId, client);
                    mylistSamune = httpMylist.GetMylistVideoSamune();
                    mylistVideoUrl = httpMylist.GetMylistVideoUri();

                }
            }
            else
            {
                this.mylistName = NicoString.DOESNOT_EXISTS_MYLIST;
            }
        }





    }

}
