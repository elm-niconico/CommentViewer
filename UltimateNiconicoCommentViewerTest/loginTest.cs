using MaterialDesignThemes.Wpf;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.connectLogic;
using UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl;
using UltimateNiconicoCommentViewer.src.model.urlCreate;

namespace UltimateNiconicoCommentViewerTest
{
    public class LoginTest
    {
        private ConnectNicoNico logic = new ConnectNicoNico(UrlCreate.GetInstance(),
                                                                     LoginLogic.GetInstance(),
                                                                     ConnectionLogic.GetInstance());

        private ConnectionLogic connection = ConnectionLogic.getInstance();
       
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ログインチェック_成功()
        {
            bool result = await logic.LoginNiconicoAccount("elmgameinfo@gmail.com", "enjoygame1");          
            Assert.AreEqual(true, result);
        }

  
        [Test]
        public async Task ログインチェック_失敗()
        {
            bool result = await logic.LoginNiconicoAccount("dummy@gmail.com", "hogefuga");
            Assert.AreEqual(false, result);
        }

        [TestCase(24,"addr")]
        [TestCase(4,"port")]
        [TestCase(10,"thread")]
        public async Task 放送ステータスの取得_成功(int expected, string key)
        {
            var response = await logic.SetLiveStatus("lv328021115");
            
            var result = XmlParse.parseXml(response,key);

            TestContext.WriteLine(result);

        }

        [TestCase("https://live2.nicovideo.jp/watch/lv328148760")]
        [TestCase("lv328148760")]
        [TestCase("")]
        [TestCase("https://live2.nicovideo.jp/watch/")]
        public void 放送URLから放送IDに変換(string url)
        {
            var res = URLParse.tryParseUrl(url);
            TestContext.WriteLine(res);
        }


        [Test]
        public async Task ユーザーアイコン取得()
        {
            
            var premium = new Uri(ApiURL.USER_ICON_URL(161843));
            var response = new Uri(ApiURL.USER_ICON_URL(97491199));
            HttpClient client = new HttpClient();
            var res  = await client.GetAsync(response);
            TestContext.WriteLine(res.IsSuccessStatusCode);
            BitmapImage preSource = new BitmapImage(premium);
            BitmapImage imageSource = new BitmapImage(response);
            TestContext.WriteLine(preSource);
            TestContext.WriteLine(imageSource);
        }



        [Test]
        public async Task 放送chat(int expected, string key)
        {
            var response = await logic.SetLiveStatus("lv328021115");

            var result = XmlParse.parseXml(response, key);

            TestContext.WriteLine(result);
        }









    }


}