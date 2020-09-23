using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public static void GoToUserProfile(this UserProfileLogic me, string userId)
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


        /// <summary>
        /// 対象のマイリストのページに遷移します。
        /// </summary>
        /// <param name="me"></param>
        /// <param name="mylistId"></param>
        /// <param name="userId"></param>
        public static void GoToMylist(this UserProfileLogic me, string userId, string mylistId )
        {
            var url = ApiURL.GO_TO_MYLIST_LINK(userId, mylistId);
            var psi = new ProcessStartInfo()
            {
                FileName = url,
                UseShellExecute = true,
            };
            try
            {
                Process.Start(psi);
            }catch(Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GoToVideo_MouseDown(this UserProfileLogic me, string uri)
        {
            GetInfo(ApiURL.GO_TO_VIDEO_PAGE(uri));

        }

        /// <summary>
        /// 最新の投稿動画のページに遷移します
        /// </summary>
        /// <param name="me"></param>
        /// <param name="uri"></param>
        public static void GoToLatestViedeo_MouseDown(this UserProfileLogic me, string uri)
        {
            GetInfo(uri);
        }

        private static  void GetInfo(string uri)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = uri,
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
            }catch(Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     


       
       

    }
}
