using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UltimateNiconicoCommentViewer.src.view.logic;
using UltimateNiconicoCommentViewer.src.viewModel.dialog;

namespace UltimateNiconicoCommentViewer.src.view.dialog
{
    /// <summary>
    /// UserProfile.xaml の相互作用ロジック
    /// </summary>
    public partial class UserProfile : Window
    {
        private static UserProfileModel _model;

        private static UserProfileLogic _logic;

        private static UserProfile _instance;

      

        public UserProfile(UserProfileModel model)
        {
            InitializeComponent();
            _logic = UserProfileLogic.INSTANCE;
            _model = model;
            this.DataContext = _model;

        }

        /// <summary>
        /// ユーザーページに移動します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            _logic.GoToUserProfile(_model.userId);
        }
    }
}
