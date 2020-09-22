using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.httpClient;
using UltimateNiconicoCommentViewer.src.viewModel.dialog;

namespace UltimateNiconicoCommentViewer.src.view.logic
{
    public enum UserProfileLogic
    {
        INSTANCE,
        
        
    }
    public static class Extends {
    
        /// <summary>
        /// ユーザーのプロフィールページに遷移します
        /// 184の場合は処理を中断します。
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="userId"></param>
        public static void GoToUserProfile(this UserProfileLogic logic, string userId)
        {
            if (userId.IsNotNumber()) return;
            var psi = new ProcessStartInfo()
            {
                FileName = ApiURL.GO_TO_USER_PROFILE(int.Parse(userId)),
                UseShellExecute = true,
            };
            try
            {
                Process.Start(psi);
            }catch(Win32Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
           
        }

     


       
       

    }
}
