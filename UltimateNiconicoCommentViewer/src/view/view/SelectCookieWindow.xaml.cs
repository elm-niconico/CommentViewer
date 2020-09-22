using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UltimateNiconicoCommentViewer.src.view.logic;
using UltimateNiconicoCommentViewer.src.view.model.dialog;

namespace UltimateNiconicoCommentViewer.src.view.dialog
{
    /// <summary>
    /// SelectCookieWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectCookieWindow : Window
    {

        private SelectCookieWindowLogic _logic;

        public SelectCookieWindow(SelectCookieWindowLogic logic)
        {
            InitializeComponent();
            _logic = logic;

        }

        /// <summary>
        /// Event 【[参照]ボタンをクリック】
        /// -> クッキーファイルのパス
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void Show_OpenFileDialog_Click(object send, RoutedEventArgs e)
        {
            var path = _logic.SelectCookieFile();
            cookiePath.Text = path;
        }

        /// <summary>
        /// Event 【[完了ボタンをクリック]】
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void CompletedCookieSelect_Click(object send, RoutedEventArgs e)
        {
            _logic.IsLoginNiconicoFromCookie(cookiePath.Text);
        }
    }
}
