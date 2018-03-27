using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Window = System.Windows.Window;
using System.Data;

namespace youtubeana
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public class youtuber {
            public string name;
            public string titlw;
            public string like;
            public string dislike;
            public string viewamount;
            public string commentamount;

        }

        public int count = 0;
        public string table ="";
        public string name = "";
        public List<string> titlelist = new List<string>();
        public List<string> publishedAt = new List<string>();
        public List<string> videolist = new List<string>();
        public List<string> viewCount = new List<string>();
        public List<string> likeCount = new List<string>();
        public List<string> dislikeCount = new List<string>();
        public List<string> commentCount = new List<string>();
        public List<string> categoryId = new List<string>();
        public MainWindow()
        {

            InitializeComponent();
        }

        

        public void GetDataDetail(string url)
        {
            
            string content = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            WebResponse wr = req.GetResponse();
            using (var reader = new StreamReader(wr.GetResponseStream()))
            {
                while ((content = reader.ReadLine()) != null)
                {
                    if (content.Contains("viewCount"))
                    {
                        content = content.Replace(@" ""viewCount"": """, "");
                        content = content.Replace(@""",", "");
                        viewCount.Add(content);
                    }
                    if (content.Contains("likeCount") && !content.Contains("dislikeCount"))
                    {
                        content = content.Replace(@" ""likeCount"": """, "");
                        content = content.Replace(@""",", "");
                        likeCount.Add(content);
                    }
                    if (content.Contains("dislikeCount"))
                    {
                        content = content.Replace(@" ""dislikeCount"": """, "");
                        content = content.Replace(@""",", "");
                        dislikeCount.Add(content);
                    }
                    if (content.Contains("categoryId"))
                    {
                        content = content.Replace(@" ""categoryId"": """, "");
                        content = content.Replace(@""",", "");
                        categoryId.Add(content);
                    }
                    
                    if (content.Contains("commentCount"))
                    {
                        content = content.Replace(@" ""commentCount"": """, "");
                        content = content.Replace(@"""", "");
                        commentCount.Add(content);
                    }
                };
            }
        }

        public string GetData(string url) {
            //&pageToken=CJYBEAA
            string pagetoken = "";
            string content = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            WebResponse wr = req.GetResponse();
            using (var reader = new StreamReader(wr.GetResponseStream()))
            {
                while ((content = reader.ReadLine()) != null) {
                    if (content.Contains("title"))
                    {
                        content = content.Replace(@" ""title"": """, "");
                        content = content.Replace(@""",", "");
                        titlelist.Add(content);
                    }
                    if (content.Contains("videoId"))
                    {
                        content = content.Replace(@" ""videoId"": """, "");
                        content = content.Replace(@"""", "");
                        videolist.Add(content);
                    }
                    if (content.Contains("publishedAt"))
                    {
                        content = content.Replace(@" ""publishedAt"": """, "");
                        content = content.Replace(@""",", "");
                        publishedAt.Add(content);
                    }
                    if (content.Contains("channelTitle"))
                    {
                        content = content.Replace(@" ""channelTitle"": """, "");
                        content = content.Replace(@""",", "");
                        name = content;
                    }
                    if (content.Contains("nextPageToken"))
                    {
                        pagetoken = content.Replace(@" ""nextPageToken"": ""","");
                        pagetoken = pagetoken.Replace(@""",","");
                    }
                };
            }
            return pagetoken;
        }

        public void IOWrite()
        {
           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv檔唷唷唷|*.csv";
            saveFileDialog.FileName = name.Trim();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, table, Encoding.Unicode);
            }
        }

        public void Settable()
        {
            trans tra = new trans();
            table = "影片標題\t上傳時間\t影片種類\t觀看人數\t喜歡數\t不喜歡數\t留言數\r\n";
            for (int i = 0; i < videolist.Count; i++)
            {

                table += titlelist[i] + "\t";
                table += publishedAt[i] + "\t";
                table += tra.TranCategory(categoryId[i].Trim()) + "\t";
                table += viewCount[i] + "\t";
                table += likeCount[i] + "\t";
                table += dislikeCount[i] + "\t";
                table += commentCount[i] + "\r\n";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string report;
            string url = string.Format("https://www.googleapis.com/youtube/v3/search?key=AIzaSyAWMQS2U8m0WIKMH1KbwxIj7aKUt9OINpU&type=video&part=snippet&channelId={0}&maxResults=50&order=date", txt.Text);
            while ((report = GetData(url)) != "")
            {
                url = string.Format("https://www.googleapis.com/youtube/v3/search?key=AIzaSyAWMQS2U8m0WIKMH1KbwxIj7aKUt9OINpU&type=video&part=snippet&channelId={0}&maxResults=50&order=date", txt.Text);
                url += "&pageToken=" + report;
            }
            count = videolist.Count;
            int i = 1;
            foreach (string video in videolist)
            {
                url = string.Format(
                    "https://www.googleapis.com/youtube/v3/videos?id={0}&key=AIzaSyAWMQS2U8m0WIKMH1KbwxIj7aKUt9OINpU&part=snippet,contentDetails,statistics,status",
                    video);
                GetDataDetail(url);
                complete.Content = "完成度:" + i + "/" + count;
                i++;
            }
            Settable();
            IOWrite();
            complete.Content = "完成";
            
            
        }
    }
}
