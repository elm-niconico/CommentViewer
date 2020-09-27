using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace SuperNiconicoCommentViewer.src.component.model
{
    public class MainWindowModel :System.ComponentModel.INotifyPropertyChanged 
    {
        public const string CONNECT_TEXT = "接続";
        public const string DISCONNECT_TEXT = "切断";


        public StackPanel communityInfo;

        public string communityNum;


        private string connectBtnText = "接続";
        
        public string BtnBind
        {
            get
            {
                return this.connectBtnText;
            }
            set
            {
                this.connectBtnText = value;
                OnChanged(nameof(BtnBind));
                return;
            }
        }

        public string Bind
        {
            get
            {
                return this.communityNum; 
            }
            set
            {
                this.communityNum = value;
                this.OnChanged(nameof(Bind));
                return;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        
        public void OnChanged(string info)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info)); 
        }
    }
}
