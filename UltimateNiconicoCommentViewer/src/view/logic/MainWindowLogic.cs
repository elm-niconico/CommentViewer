using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.httpClient;
using UltimateNiconicoCommentViewer.src.view.dialog;
using UltimateNiconicoCommentViewer.src.view.logic;
using static UltimateNiconicoCommentViewer.src.viewModel.dialog.UserProfileModel;

namespace UltimateNiconicoCommentViewer.src.viewModel
{
    public enum MainWindowLogic
    {
        INSTANCE,

    }
    public static class Extends { 

        private static bool autoScrollFlag = true;

        private static UserProfile _userProfileWindow;

  

       public static void NavigateSelectCookieWindow(this MainWindowLogic me)
        {
            var cookieWindow = new SelectCookieWindow(SelectCookieWindowLogic.INSTANCE);
            cookieWindow.ShowDialog();
        }

        /// <summary>
        /// 対象ユーザーの詳細プロフィールが記載されたWindowを生成します 
        /// </summary>
        /// <param name="selectItem"> 選択ユーザーの アイコン画像,ユーザー名,コメント本文,ユーザーID</param>
        /// <param name="window">　親Window　</param>
        /// <returns></returns>
        public static async Task ShowUserProfile(this MainWindowLogic me,Object[] selectItem, Window window)
        {
            if (selectItem.NotNull())
            {
                if (_userProfileWindow.NotNull() && _userProfileWindow.IsLoaded)
                {
                    _userProfileWindow.Close();
                }

                var model = await new UserProfileBuilder().Build(selectItem);

                _userProfileWindow = new UserProfile(model)
                {
                    Owner = window,
                };
                _userProfileWindow.Show();

            }

        }

        public static void LinkToURL(this MainWindowLogic me, string comment)
        {
            var liveIdUrl = URLParse.tryParseUrl(comment);
            if (liveIdUrl.NotEmpty())
            {
                try
                {
                    Process.Start(liveIdUrl);
                }catch(Win32Exception ex)
                {
                    //TODO 例外処理
                }
            }
            else
            {

            }
        }

        public static void LiveIdChanged(this MainWindowLogic me, TextBox liveIdText, Button connectBtn)
        {
            //放送URL or liveId
            var liveId = URLParse.tryParseUrl(liveIdText.Text);

            if (liveId.NotEmpty())
            {
                connectBtn.IsEnabled = true;
                liveIdText.Text = liveId;
            }
            else
            {
                connectBtn.IsEnabled = false;
            }
        }

        public static void CommentDragAndDrop(this MainWindowLogic me, object sender, MouseEventArgs e, ListView commentList)
        {
            if (sender.NotNull() && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop((Card)sender, commentList.SelectedItem, DragDropEffects.Copy);
            }
        }


        public static void CommentListDrop(this MainWindowLogic me, TextBox liveIdText)
        {
            liveIdText.Text = URLParse.tryParseUrl(liveIdText.Text);
           
        }

        public static void ScrollCommentView(this MainWindowLogic me, ListView commentList)
        {
            if (autoScrollFlag)
            {
                commentList.ScrollIntoView(commentList.Items[commentList.Items.Count - 1]);
            }
        }


        public static void CommentList_ScrollChanged(this MainWindowLogic me, ScrollChangedEventArgs e)
        {
            autoScrollFlag = (e.VerticalOffset + e.ViewportHeight == e.ExtentHeight);
        }


        public static void ReadCookieValueFromSettings(this MainWindowLogic me)
        {
            var path = UserSetting.Default.cookieValue;
            if(path.NotNull() && path.NotEmpty())
            {
                var con = new SqlConnectionCookie();
                if (con.ReadCookie(UserSetting.Default.userCookiePath))
                {
                    
                    var uri = new Uri(NicoString.NICO_URL);
                    var cookie = new Cookie(NicoString.USER_SESSION,path,NicoString.SLASH,NicoString.NICO_NAME);
                    var handler = HttpClientHolder.GetHandler();
                    handler.CookieContainer.Add(uri, cookie);
                    
                    HttpClientHolder.client = HttpClientHolder.NewHttpClient(handler);
                  
                }
                else
                {
                    //TODO Cookieファイルからログインできていなかったばあいの処理
                }
            }
        }
    }
}
