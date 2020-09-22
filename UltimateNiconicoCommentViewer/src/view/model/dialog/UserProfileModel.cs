using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace UltimateNiconicoCommentViewer.src.viewModel.dialog
{
    public class UserProfileModel
    {
        public BitmapImage userIcon { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
        public string userId { get; set; }
        public string[][] mylistId { get; set; }

        public bool is184 { get; set; }
    }
}
