using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
    

        public static void GoToUserProfile(this UserProfileLogic logic, string userId)
        {
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
               var e =  new PrintDialogException();
               //TODO 例外処理
            }
           
        }

     

        public  static async Task<UserProfileModel> NewUserProfileModel(this UserProfileLogic logic, object[] selectItem)
        {

            int userId = 0;
            var model = new UserProfileModel()
            {
                userIcon = (BitmapImage)selectItem[0],
                userName = selectItem[1] as string,
                comment = selectItem[2] as string,
                is184 = int.TryParse(selectItem[3] as string, out userId),

            };
            if (model.is184)
            {
                model.userId = selectItem[3] as string ;

                model.mylistId = await GetMylistId(userId);
            }
            else
            {
                model.userId = $"{selectItem[3]}(184)";
            }   
            return model;
        }

        private async static Task<string[][]> GetMylistId(int userId)
        {
            var client = HttpClientHolder.GetStaticClient();
            var response = await client.GetAsync(ApiURL.GET_USER_MYLIST(userId));
            string[][] mylistInfo = XmlParse.ParseMylistXml(await response.Content.ReadAsStringAsync()); 
            return mylistInfo;
        }

    }
}
