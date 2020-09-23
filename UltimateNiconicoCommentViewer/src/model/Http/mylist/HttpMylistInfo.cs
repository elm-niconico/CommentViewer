using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.Http;

namespace UltimateNiconicoCommentViewer.src.model.connectLogic
{
    class HttpMylistInfo
    {
        private HttpClient _client;

        private string _responseMessage;

        private HttpMylistInfo(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// マイリスト欄のサムネイル画像から動画ページに遷移するためのUriを取得します。
        /// </summary>
        /// <returns></returns>
        public string GetMylistVideoUri()
        {
            var url = JsonParse.ParseFromJson(_responseMessage, NicoString.MYLIST_ITEM_VIDEOID);
  
            return url;
        }


        /// <summary>
        /// マイリストのサムネイル画像を取得します。
        /// </summary>
        /// <returns></returns>
        public BitmapImage GetMylistVideoSamune()
        {
            var samuneUrl = JsonParse.ParseFromJson(_responseMessage, NicoString.MYLIST_ITEM_SAMUNE);
            BitmapImage bitMapImage = null;
            try
            {
               bitMapImage =  new BitmapImage(new Uri(samuneUrl));
            }catch(Exception ex)
            {


            }
            return bitMapImage;
            
        }

      

        /// <summary>
        /// Mylistの詳細情報をセットします
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<HttpMylistInfo> SetMylistDetail(string param)
        {
            var response =  await _client.GetAsync(ApiURL.GET_DETAIL_MYLIST_DATA(param));
            this._responseMessage = await response.Content.ReadAsStringAsync();
            return this;
        }

        public class HttpMylistInfoBuilder
        {
            /// <summary>
            /// HttpMylistInfoのインスタンスを返します
            /// </summary>
            /// <param name="mylistId"></param>
            /// <param name="client"></param>
            /// <returns></returns>
            public async Task<HttpMylistInfo> Build(string mylistId, HttpClient client)
            {
                var instance = new HttpMylistInfo(client);
                return await instance.SetMylistDetail(mylistId);
            }
        }
    }
}
