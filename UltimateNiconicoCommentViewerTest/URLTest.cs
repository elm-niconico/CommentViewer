using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.util;

namespace UltimateNiconicoCommentViewerTest
{
    class URLTest
    {
        [TestCase("https://live2.nicovideo.jp/watch/lv328248919")]
        [TestCase("lv328248919")]
        [TestCase("sm212332aaada")]
        [TestCase("https://www.youtube.com/watch?v=yTaw2nChbqA")]
        [TestCase("https://atcoder.jp/?lang=ja")]
        public void URLを取得成功(string url)
        {
            var result = URLParse.TryUrlparseFromComment(url);

            TestContext.WriteLine(result);
            Assert.IsTrue(result.NotNull());
        }

      
       

        //[TestCase("https://atcoder.jp/?lang=jadajouoaudojgnlhioahf")]
        //public async Task URL遷移_成功(string url)
        //{
        //    await ProcessSupport.ForceMovingUrlPage(url);
          
        //}

        //[TestCase("https://at.jp/")]
        //public async Task URL遷移_失敗(string url)
        //{
        //    await ProcessSupport.ForceMovingUrlPage(url);

        //}
    }
}
