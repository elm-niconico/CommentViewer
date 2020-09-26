using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.model.connectLogic;
using UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl;
using UltimateNiconicoCommentViewer.src.model.httpClient;
using UltimateNiconicoCommentViewer.src.viewModel;

namespace UltimateNiconicoCommentViewer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowLogic _windowLogic;

        public MainWindow()
        {
            _windowLogic = new MainWindowLogic(new ResponseNiconico(
                                               new ConnectNicoNico(
                                               new LoginLogic(
                                                   HttpClientBuilder.NewHttpClient()),
                                               new ConnectionLogic(
                                                   HttpClientBuilder.NewHttpClient()))));

            InitializeComponent();


        }

        /// <summary>
        /// Event 【「接続開始」ボタンをクリック】
        /// -> {コメントサーバに接続を試みます }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Connect_NicoNico_Click(object sender, RoutedEventArgs e)
        {
            commentList.Items.Clear();

            var liveId = liveIdText.Text;
            await _windowLogic.ConnectLiveServer(liveId, commentList, this);
        }


        /// <summary>
        /// コメントビューアをスクロールするかフラグを設定します
        /// スクロールバーが一番下なら自動スクロールON
        ///                           違うならOFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> スクロールバーの位置 </param>
        private void CommentList_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            _windowLogic.CommentList_ScrollChanged(e);

        }

        /// <summary>
        /// Event 【コメントをドラッグ】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comment_MouseMove(object sender, MouseEventArgs e)
        {
            _windowLogic.CommentDragAndDrop(sender, e, commentList);

        }


        /// <summary>
        /// Event 【コメントをユーザープロフィール欄にドロップ】
        /// -> UserProfileWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Drop_CommentOnUserProfileMark(object sender, DragEventArgs e)
        {
            var selectItem = (Object[])e.Data.GetData(typeof(Object[]));

            await _windowLogic.ShowUserProfile(selectItem, this);


        }

        /// <summary>
        /// 放送URLが入力された際のイベント
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        private void liveIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _windowLogic.LiveIdChanged(liveIdText, connectBtn);

        }

        /// <summary>
        /// Event 【ファビコンがコメントリストにドロップ】
        /// -> { 正常なURLの場合,接続開始 }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentList_Drop(object sender, DragEventArgs e)
        {
            liveIdText.Text = e.Data.GetData(DataFormats.Text) as string;

            if (connectBtn.IsEnabled)
            {
                Connect_NicoNico_Click(null, null);
            }
        }

        /// <summary>
        /// Event 【URL付きのコメントがURLマーク(仮)にドロップされた時】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Drop_CommentOnUrlMark(object sender, DragEventArgs e)
        {
            string comment = ((Object[])e.Data.GetData(typeof(Object[])))[2] as string;
            await _windowLogic.MovingUrlPage(comment);
        }



        /// <summary>
        /// Event 【クッキーファイルを選択」をクリック】
        /// -> SelectCookieWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateSelectCookieWindow_Click(object sender, RoutedEventArgs e)
        {
            _windowLogic.NavigateSelectCookieWindow();
        }
    }
}
