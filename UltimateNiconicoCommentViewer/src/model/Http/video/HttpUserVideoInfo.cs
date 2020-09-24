using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;

namespace UltimateNiconicoCommentViewer.src.model.Http.video
{
    public class HttpUserVideoInfo
    {
        private HttpClient _client;

        private string _responseMessage;

        private HttpUserVideoInfo(HttpClient client)
        {
            _client = client;
        }

        public BitmapImage GetVideoSamune()
        {
            var url = XmlParse.ParseXmlToImageURL(_responseMessage);

            BitmapImage bitmapImage = null;
            try
            {
                bitmapImage = new BitmapImage(new Uri(url));
            }
            catch (Exception)
            {

            }

            return bitmapImage;
        }

        /// <summary>
        /// ユーザーの最新の投稿動画のURLを返します
        /// </summary>
        /// <returns></returns>
        public string GetVideoURL()
        {
            return GetValue(NicoString.USER_VIDEO_LINK);
        }

        /// <summary>
        /// ユーザーの投稿動画のタイトルを返します
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetVideoTitle()
        {
            var title = GetValue(NicoString.USER_VIDEO_TITLE);
            return (title.NotEmpty()) ? title : NicoString.DOES_NOT_EXISTS_VIDEO;
        }


        private string GetValue(string key)
        {
            var values = XmlParse.ParseXmlMatches(_responseMessage, key);

            return (values.Count <= 1) ? string.Empty : values[1].ToString();
        }

        /// <summary>
        /// ユーザーの投稿動画の一覧をセットします
        /// (XML)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<HttpUserVideoInfo> SetResponseMessage(string userId)
        {
            var response = await _client.GetAsync(ApiURL.GET_USER_VIDEO(userId));
            _responseMessage = await response.Content.ReadAsStringAsync();
            _client.Dispose();
            return this;
        }


        public class HttpUserVideoInfoBuilder
        {

            public async Task<HttpUserVideoInfo> Build(string userId, HttpClient client)
            {
                var instance = new HttpUserVideoInfo(client);

                return await instance.SetResponseMessage(userId);
            }
        }



    }
}
