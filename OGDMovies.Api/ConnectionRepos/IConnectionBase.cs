using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using OGDMovies.Common.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IConnectionBase
    {
        string Key { get; }
        string Url { get; }
        dynamic RetrieveData(string query, bool expectMultiple);
        AggregatedModel GetMovieById(string id);
        AggregatedModel GetMovieByTitle(string title, string page = "1");
        List<string> GetTitleAutoComplete(string title);
    }
}