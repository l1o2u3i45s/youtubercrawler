using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace youtubeana
{
    public class MainViewModel : ViewModelBase
    {
        private string channelId;
        public string ChannelId
        {
            get { return channelId; }
            set { Set(() => ChannelId, ref channelId, value); }
        }
        private List<YoutubeVideoDetailData> youtubeVideoAPIDataCollection;
        public List<YoutubeVideoDetailData> YoutubeVideoAPIDataCollection
        {
            get { return youtubeVideoAPIDataCollection; }
            set { Set(() => YoutubeVideoAPIDataCollection, ref youtubeVideoAPIDataCollection, value); }
        }

        public ICommand DoneCommand {
            get { return new RelayCommand(Act); }
        }
         
        public void SaveData()
        { 
            string table = "影片標題\t上傳時間\t影片種類\t觀看人數\t喜歡數\t不喜歡數\t留言數\r\n";
            foreach (var video in YoutubeVideoAPIDataCollection) {

                table += video.VideoDetailData[0].Snippet.Title + "\t";
                table += video.VideoDetailData[0].Snippet.PublishedAt + "\t";
                table += trans.TranCategory(video.VideoDetailData[0].Snippet.CategoryId) + "\t";
                table += video.VideoDetailData[0].StatisticsData.ViewCount + "\t";
                table += video.VideoDetailData[0].StatisticsData.LikeCount + "\t";
                table += video.VideoDetailData[0].StatisticsData.DislikeCount + "\t";
                table += video.VideoDetailData[0].StatisticsData.CommentCount + "\r\n";
            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "csv檔唷唷唷|*.csv";
            saveFileDialog.FileName = YoutubeVideoAPIDataCollection[0].VideoDetailData[0].Snippet.ChannelTitle;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, table, Encoding.Unicode);
            }
        }

        public void Act()
        {
            YoutubeVideoAPIDataCollection = YoutubeVideoAPI.GetAllVideoDetail(ChannelId); 
            SaveData(); 
        }
    }
}
