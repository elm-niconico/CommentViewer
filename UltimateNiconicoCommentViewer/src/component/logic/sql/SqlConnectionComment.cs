
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace SuperNiconicoCommentViewer.src.component.logic.sql
{
    public class SqlConnectionComment : IDisposable
    {

        private SQLiteConnection _con;


        public SqlConnectionComment()
        {
            if (DoesNotExistsUserDb())
            {
                CreateUserInfoDataBase();
            }
            else
            {
                _con = CreateConnection();
                _con.Open();
            }
            
           
        }

        /// <summary>
        /// データベースからユーザーIDに紐づけられているコテハンを探します。
        /// ない場合はユーザーIDを返却します。
        /// </summary>
        /// <param name="userId"> コメント元のユーザーID</param>
        /// <returns></returns>
        public string FindHandleOrElse(string userId, string userName,string communityNum)
        {
            string handle = string.Empty;
            using(var com = new SQLiteCommand(_con))
            {
                com.CommandText = SQLString.SELECT_HANDLE_WHERE_USERID(userId,communityNum);

                using(var reader = com.ExecuteReader())
                {
                    if (reader.HasRows == false) return userName;
                    while (reader.Read())
                    {
                        
                        handle = (string)reader["handle"];
                    }
                }
            }
            return handle;
        }


        public void EntryHandle(string userId, string handle, string communityNum)
        {
            if (FindUserId(userId))
            {
                UpdateHandle(userId, handle, communityNum);
            }
            else
            {
                InsertHandleIntoUserTable(userId, handle, communityNum);
            }
        }

        //TODO メソッド名変更
        private void InsertHandleIntoUserTable(string userId, string handle, string communityNum)
        {
            SqlCommandTemplate(SQLString.INSERT_HANDLE_USER(userId, handle, communityNum));
        }


        private void UpdateHandle(string userId, string handle, string communityNum)
        {
            SqlCommandTemplate(SQLString.UPDATE_HANDLE(handle, userId, communityNum));
        }


        private bool FindUserId(string userId)
        {
            return SqlCommandTemplate(SQLString.SELECT_USERID(userId));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        private bool SqlCommandTemplate(string command)
        {
            var result = false;
            using (var com = new SQLiteCommand(_con))
            {
                com.CommandText = command;
                var reader = com.ExecuteReader();
                result = reader.HasRows;
            }
            return result;
           
        }


       

        /// <summary>
        /// データベースが存在するかチェックする
        /// </summary>
        /// <returns></returns>
        private bool DoesNotExistsUserDb()
        {
            var path = UserSetting.Default.userDbPath;
            if (IsNotEqualDbName(path)) return true;
           
            return !File.Exists(path);   
        }

        /// <summary>
        /// 指定されたファイルがデータベースの名前と一致しない
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsNotEqualDbName(string path)
        {
            return Regex.Match(path, "userInfo.db$").Success == false;
        }

        /// <summary>
        /// データベースとテーブル作成
        /// </summary>
        public void CreateUserTable()
        {
            var absolutePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),SQLString.DB_NAME);
            var builder = new SQLiteConnectionStringBuilder()
            {
               
                DataSource = absolutePath,
            };
            _con = new SQLiteConnection(builder.ToString());
            _con.Open();
            using (var command = new SQLiteCommand(_con))
            {
                command.CommandText = SQLString.CREATE_USER_TABLE;
                command.ExecuteNonQuery();
                UserSetting.Default.userDbPath = absolutePath;
                UserSetting.Default.Save();


            }
           

            

        }


        /// <summary>
        /// UserInfoDBに繋がった
        /// コネクションを返します
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection CreateConnection()
        {
            var dbPath = UserSetting.Default.userDbPath ;

            var builder = new SQLiteConnectionStringBuilder()
            {
                DataSource = dbPath,
            };

            return new SQLiteConnection(builder.ToString());

        }

        /// <summary>
        /// データベースの存在チェックを行います。
        /// ない場合は新しく作ります
        /// </summary>
        private void CreateUserInfoDataBase()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DirectoryPaths.DB_SUBFOLDER);
            CreateDirectoryIfShould(path);
            CreateUserTable();
        }

        /// <summary>
        /// ディレクトリが存在しない場合、つくります
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirectoryIfShould(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }


        /// <summary>
        /// データベース廃棄処理
        /// </summary>
        public void Dispose()
        {
            _con.Dispose();
        }
    }
}