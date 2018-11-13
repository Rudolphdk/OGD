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
        dynamic RetrieveData(string query, bool expectMultiple, bool adult = false);
        AggregatedModel GetMovieById(string id);
        AggregatedModel GetMovieByTitle(string title, string page, bool adult = false);
        List<AutoCompleteModel> GetTitleAutoComplete(string title, bool adult = false);
    }
}