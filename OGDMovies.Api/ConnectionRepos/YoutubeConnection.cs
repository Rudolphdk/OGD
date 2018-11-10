using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Auth.OAuth2;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IYoutubeConnection 
    {
        string Key { get; }
        IEnumerable<SearchResult> RetrieveData(string query, string page = "", int maxResults = 20);
        IEnumerable<Video> RetrieveMostPopular(string page = "", int maxResults = 20);
        ChannelSectionListResponse RetrieveImdbChannelList(string page);
    }
    public class YoutubeConnection: IYoutubeConnection
    {
        public string Key { get; private set; }

        public YoutubeConnection()
        {
            Key = System.Configuration.ConfigurationManager.AppSettings["YOUTUBE_API_KEY"];
        }

        public IEnumerable<SearchResult> RetrieveData(string query, string page, int maxResults)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Key,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = $"IMDB MOVIE {query}"; 
            searchListRequest.MaxResults = maxResults;
            //searchListRequest.PageToken = page;
            searchListRequest.Type = "youtube#video";

            var searchListResponse = searchListRequest.Execute();
            var searchResult = searchListResponse.Items.Where(s => s.Id.Kind == "youtube#video");
            return searchResult;
        }
        public IEnumerable<Video> RetrieveMostPopular(string page, int maxResults)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Key,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Videos.List("snippet, contentDetails, statistics");
            searchListRequest.MaxResults = maxResults;
            searchListRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;

            var searchListResponse = searchListRequest.Execute();
            return searchListResponse.Items;
        }

        public ChannelSectionListResponse RetrieveImdbChannelList(string page)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Key,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.ChannelSections.List("snippet,contentDetails");
            //searchListRequest.MaxResults = maxResults;
            searchListRequest.ChannelId = "UC_vz6SvmIkYs1_H3Wv2SKlg";//IMDB channel ID

            var searchListResponse = searchListRequest.Execute();
            return searchListResponse;
        }
    }
}