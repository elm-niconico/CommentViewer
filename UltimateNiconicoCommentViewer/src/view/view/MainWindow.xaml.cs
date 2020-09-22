using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.connectLogic;
using UltimateNiconicoCommentViewer.src.model.getCommentLogic.impl;
using UltimateNiconicoCommentViewer.src.model.urlCreate;
using UltimateNiconicoCommentViewer.src.view.dialog;
using UltimateNiconicoCommentViewer.src.viewModel;
using Xceed.Wpf.Toolkit.Core.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UltimateNiconicoCommentViewer
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private ResponseNiconico _responseNiconico;

        private MainWindowLogic _windowLogic;

        public MainWindow()
        {
            _windowLogic = MainWindowLogic.INSTANCE;

            _windowLogic.ReadCookieValueFromSettings();

            _responseNiconico = new ResponseNiconico(new ConnectNicoNico(UrlCreate.GetInstance(),
                                                                     LoginLogic.GetInstance(),
                                                                     new ConnectionLogic()));
           
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
            //TODO 削除予定

            var liveId = liveIdText.Text;
            await Task.Run(async () =>
            {

                await foreach (var response in _responseNiconico.getResponseMessage(liveId))
                {
                    this.Dispatcher.Invoke(() =>
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

                        _windowLogic.ScrollCommentView(commentList);
                    });
                }
            });

          

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
        private async void Comment_Drop(object sender, DragEventArgs e)
        {
            var selectItem = (Object[])e.Data.GetData(typeof(Object[]));

            await _windowLogic.ShowUserProfile(selectItem,this);
         
            
        }
        
        /// <summary>
        /// 放送URLが入力された際のイベント
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        private void liveIdText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _windowLogic.LiveIdChanged(liveIdText,connectBtn);

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
