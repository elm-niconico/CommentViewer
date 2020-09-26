using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.view.dialog;
using UltimateNiconicoCommentViewer.src.view.logic;
using static UltimateNiconicoCommentViewer.src.viewModel.dialog.UserProfileModel;

namespace UltimateNiconicoCommentViewer.src.viewModel
{
    public class MainWindowLogic
    {
        private static bool autoScrollFlag = true;

        private static UserProfile _userProfileWindow;

        private ResponseNiconico _responseNiconico;

        public MainWindowLogic(ResponseNiconico responseNiconico)
        {
            this._responseNiconico = responseNiconico;
        }

        /// <summary>
        /// クッキーファイル選択ダイアログを表示します。
        /// </summary>
        /// <param name="me"></param>
        public void NavigateSelectCookieWindow()
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
        public async Task ShowUserProfile(Object[] selectItem, Window window)
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

        public async Task ConnectLiveServer(string liveId, ListView commentList, Window mainWindow)
        {
            await Task.Run(async () =>
            {

                await foreach (var response in _responseNiconico.GetResponseMessage(liveId))
                {
                    mainWindow.Dispatcher.Invoke(() =>
                    {
                        //ユーザーアイコン
                        var imageSource = new BitmapImage(response[0] as Uri);
                        //ユーザー名
                        var userName = response[1] as string;
                        //コメント
                        var comment = response[2] as string;

                        var hiddenUserId = response[3] as string;
                        object[] items = { imageSource, userName, comment, hiddenUserId };
                        commentList.Items.Add(items);

                        ScrollCommentView(commentList);
                    });
                }
            });

        }

        /// <summary>
        /// 放送URL欄にテキストが入力された時にURLの形式かチェックします。
        /// URLの形式なら接続開始ボタンを有効化します。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="liveIdText"></param>
        /// <param name="connectBtn"></param>
        public  void LiveIdChanged( TextBox liveIdText, Button connectBtn)
        {
            //放送URL or liveId
            var liveId = URLParse.ParseUrlOrDefault(liveIdText.Text);

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

        /// <summary>
        /// コメントがURLマークにドロップされた時にそのコメントが有効かチェックします
        /// 有効ならプロセスサポートによってURLの遷移を試行します。
        /// </summary>
        public  async Task MovingUrlPage( string comment)
        {
            var url = URLParse.TryUrlparseFromComment(comment);
            if (url.NotNull())
            {
                await ProcessSupport.ForceMovingUrlPage(url);
            }
            else
            {
                MessageBox.Show(Message.HAS_NOT_URL);
            }
        }

        /// <summary>
        /// コメントがドラッグされたばあいそのデータをわたし、ドラッグアンドドロップに移ります。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="commentList"></param>
        public void CommentDragAndDrop(object sender, MouseEventArgs e, ListView commentList)
        {
            if (sender.NotNull() && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop((Card)sender, commentList.SelectedItem, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// コメントリストにファビコンがドロップされたされた時に対象のURLに遷移します。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="liveIdText"></param>
        public  void CommentListDrop(TextBox liveIdText)
        {
            liveIdText.Text = URLParse.ParseUrlOrDefault(liveIdText.Text);
        }

        /// <summary>
        /// オートスクロールのフラグがONの時に自動スクロールを行います。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="commentList"></param>
        private void ScrollCommentView(ListView commentList)
        {
            if (autoScrollFlag)
            {
                commentList.ScrollIntoView(commentList.Items[commentList.Items.Count-1]);
            }
        }

        /// <summary>
        /// コメントリストが自動スクロールするかチェックします
        /// </summary>
        /// <param name="me"></param>
        /// <param name="e"></param>
        public void CommentList_ScrollChanged(ScrollChangedEventArgs e)
        {
            autoScrollFlag = (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight-2);
        }


    }

}
