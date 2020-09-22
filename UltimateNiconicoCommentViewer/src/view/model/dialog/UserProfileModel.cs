using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UltimateNiconicoCommentViewer.src.common;
using UltimateNiconicoCommentViewer.src.common.stringList;
using UltimateNiconicoCommentViewer.src.common.util;
using UltimateNiconicoCommentViewer.src.model.httpClient;

namespace UltimateNiconicoCommentViewer.src.viewModel.dialog
{
    public class UserProfileModel
    {
        public BitmapImage userIcon { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
        public string userId { get; set; }
        public string mylistId { get; set; }
        public string mylistName { get; set; }
        public bool isNamaId { get; set; }

        public string mylistVideoTitle { get; set; }
        public BitmapImage mylistSamune { get; set; }


        private UserProfileModel() { }

        public class UserProfileBuilder
        {
            public async Task<UserProfileModel> Build(object[] selectItem)
            {
                return await new UserProfileModel().SetModelInfo(selectItem);
            }
        }

        public async Task<UserProfileModel> SetModelInfo(object[] selectItem)
        {
            var id = 0;
            userIcon = (BitmapImage)selectItem[0];
            comment = selectItem[2] as string;
            isNamaId = int.TryParse(selectItem[3] as string, out id);

            if (isNamaId)
            {
                userId = selectItem[3] as string;
                userName = selectItem[1] as string;
                var mylistInfo = await GetMylistId(userId);
                mylistId = mylistInfo[0];
                SetMylistName_Samune_VideoTitle(mylistInfo[1]);
            }
            else
            {
                userId = $"{selectItem[3]}(184)";
                userName = "184";
                mylistName = NicoString.CAN_NOT_FOUNT_MYLIST;
            }
            return this;

        }

        private async static Task<string[]> GetMylistId(string userId)
        {
            var client = HttpClientHolder.GetStaticClient();
            var response = await client.GetAsync(ApiURL.GET_USER_MYLIST(userId));
            string[] mylistInfo = XmlParse.ParseMylistXml(await response.Content.ReadAsStringAsync());
            return mylistInfo;
        }

        private void SetMylistName_Samune_VideoTitle(string name)
        {
            if (name.NotEmpty())
            {
                mylistName = name;
                mylistVideoTitle = "";
            }
            else
            {
                mylistName = NicoString.DOESNOT_EXISTS_MYLIST;
            }
        }



    }
  
}
