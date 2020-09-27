using NUnit.Framework;
using SuperNiconicoCommentViewer.src.component.logic.sql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace UltimateNiconicoCommentViewerTest
{
    class SqlTest
    {
        [Test]
        public void データベース接続()
        {
            SqlConnectionComment sql = new SqlConnectionComment();
            using (var con = sql.CreateConnection())
            {
                con.Open();
                TestContext.WriteLine(con.State);
                var result = sql.FindUserId(new SQLiteCommand(con), "97491199");

                Assert.IsTrue(result);
            }
               
        }


        [Test]
        public void TEST()
        {
            var startupPath = new Uri(@"C:\Users\elm\Desktop\MyApp\C#\CommentViewer\UltimateNiconicoCommentViewer\src\component\logic\sql\SqlConnectionComment.cs");
            var targetPath = new Uri(@"C:\Users\elm\Desktop\MyApp\C#\CommentViewer\UltimateNiconicoCommentViewer\userInfo.db"); ;

            //startupPathから見た、targetPathを相対パスで取得する
            TestContext.WriteLine(Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData));
        }
    }
}
