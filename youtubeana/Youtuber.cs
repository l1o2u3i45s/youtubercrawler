using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtubeana
{

    public static class YoutubeVideoAPI
    {
        private static string ytRequestMasterUrl = "https://www.googleapis.com/youtube/v3/search?key=AIzaSyAWMQS2U8m0WIKMH1KbwxIj7aKUt9OINpU&type=video&part=snippet&channelId={0}&maxResults=50&order=date";

        private static string ytRequestDetailUrl = "https://www.googleapis.com/youtube/v3/videos?id={0}&key=AIzaSyAWMQS2U8m0WIKMH1KbwxIj7aKUt9OINpU&part=snippet,contentDetails,statistics,status";

        public static List<YoutubeVideoAPIData.VideoMasterData> GetAllVideos(string channelID) {
            string url = ytRequestMasterUrl;
            YoutubeVideoAPIData youtubeVideoAPISearchList = JsonConvert.DeserializeObject<YoutubeVideoAPIData>(HttpMethod.GetMethod(string.Format(url,channelID)));
            List<YoutubeVideoAPIData.VideoMasterData> videoContainer = youtubeVideoAPISearchList.VideoMasterDataCollection;

            while (true) { 
                youtubeVideoAPISearchList = JsonConvert.DeserializeObject<YoutubeVideoAPIData>(HttpMethod.GetMethod(string.Format(url, channelID) + $"&pageToken={youtubeVideoAPISearchList.NextPageToken}"));
                videoContainer.AddRange(youtubeVideoAPISearchList.VideoMasterDataCollection);
                if (youtubeVideoAPISearchList.NextPageToken == null)
                    break;
            } 
            return videoContainer;
        }

        public static List<YoutubeVideoDetailData> GetAllVideoDetail(string channelID) {
         
            List<Task> tasks = new List<Task>();
            List<YoutubeVideoDetailData> ytDetailData = new List<YoutubeVideoDetailData>();
            var videoList = YoutubeVideoAPI.GetAllVideos(channelID);

            foreach (var video in videoList)
            {
                tasks.Add(Task.Run(() => { ytDetailData.Add(JsonConvert.DeserializeObject<YoutubeVideoDetailData>(HttpMethod.GetMethod(string.Format(ytRequestDetailUrl, video.Id.VideoId)))); }));
            }
            Task.WhenAll(tasks).Wait();
            
            return ytDetailData;
        }

    }

    public class YoutubeVideoAPIData : ViewModelBase
    {
         
            [JsonProperty("nextPageToken")]
            public string NextPageToken { get; set; }
        
            [JsonProperty("pageInfo")]
            public PageInfo PageInfoData { get; set; }

            [JsonProperty("items")]
            public List<VideoMasterData> VideoMasterDataCollection { get; set; }
        

        public  class VideoMasterData
        {
            [JsonProperty("id")]
            public Id Id { get; set; }

            [JsonProperty("snippet")]
            public Snippet Snippet { get; set; }
        }

        public  class Id
        { 

            [JsonProperty("videoId")]
            public string VideoId { get; set; }
        }

        public  class Snippet
        {
            [JsonProperty("publishedAt")]
            public DateTimeOffset PublishedAt { get; set; }

            [JsonProperty("channelId")]
            public string ChannelId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("thumbnails")]
            public Thumbnails Thumbnails { get; set; }

            [JsonProperty("channelTitle")]
            public string ChannelTitle { get; set; }

            [JsonProperty("liveBroadcastContent")]
            public string LiveBroadcastContent { get; set; }
        }

        public  class Thumbnails
        {
            [JsonProperty("default")]
            public Default Default { get; set; }

            [JsonProperty("medium")]
            public Default Medium { get; set; }

            [JsonProperty("high")]
            public Default High { get; set; }
        }

        public  class Default
        {
            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("width")]
            public long Width { get; set; }

            [JsonProperty("height")]
            public long Height { get; set; }
        }

        public  class PageInfo
        {
            [JsonProperty("totalResults")]
            public long TotalResults { get; set; }

            [JsonProperty("resultsPerPage")]
            public long ResultsPerPage { get; set; }
        }
         
    }

    public class YoutubeVideoDetailData
    {
       
        [JsonProperty("items")]
        public List<VideoDetail> VideoDetailData { get; set; }
        
        public partial class VideoDetail
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("snippet")]
            public Snippet Snippet { get; set; }

            [JsonProperty("contentDetails")]
            public ContentDetails ContentDetails { get; set; }
            [JsonProperty("statistics")]
            public Statistics StatisticsData { get; set; }

        }

        public partial class ContentDetails
        {
            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("dimension")]
            public string Dimension { get; set; }

            [JsonProperty("definition")]
            public string Definition { get; set; }

            [JsonProperty("caption")]
            public bool Caption { get; set; }

            [JsonProperty("licensedContent")]
            public bool LicensedContent { get; set; }

            [JsonProperty("projection")]
            public string Projection { get; set; }
        }

        public partial class Snippet
        {
            [JsonProperty("publishedAt")]
            public DateTimeOffset PublishedAt { get; set; }

            [JsonProperty("channelId")]
            public string ChannelId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("thumbnails")]
            public Thumbnails Thumbnails { get; set; }

            [JsonProperty("channelTitle")]
            public string ChannelTitle { get; set; }

            [JsonProperty("tags")]
            public string[] Tags { get; set; }

            [JsonProperty("categoryId")] 
            public string CategoryId { get; set; }

            [JsonProperty("liveBroadcastContent")]
            public string LiveBroadcastContent { get; set; }

            [JsonProperty("localized")]
            public Localized Localized { get; set; }

            [JsonProperty("defaultAudioLanguage")]
            public string DefaultAudioLanguage { get; set; }
        }

        public partial class Localized
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

        public partial class Thumbnails
        {
            [JsonProperty("default")]
            public Default Default { get; set; }

            [JsonProperty("medium")]
            public Default Medium { get; set; }

            [JsonProperty("high")]
            public Default High { get; set; }

            [JsonProperty("standard")]
            public Default Standard { get; set; }

            [JsonProperty("maxres")]
            public Default Maxres { get; set; }
        }

        public partial class Default
        {
            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("width")]
            public long Width { get; set; }

            [JsonProperty("height")]
            public long Height { get; set; }
        }
        public partial class Statistics
        {
            [JsonProperty("viewCount")]
            public long ViewCount { get; set; }

            [JsonProperty("likeCount")]
            public long LikeCount { get; set; }

            [JsonProperty("dislikeCount")]
            public long DislikeCount { get; set; }

            [JsonProperty("favoriteCount")]
            public long FavoriteCount { get; set; }

            [JsonProperty("commentCount")]
            public long CommentCount { get; set; }
        }
    }
}
