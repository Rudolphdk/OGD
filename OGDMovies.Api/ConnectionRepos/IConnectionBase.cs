using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IConnectionBase<out T,out TR>
    {
        string Key { get; }
        string Url { get; }
        dynamic RetrieveData(string query, bool expectMultiple);
        T GetMovieById(string id);
        TR GetMovieByTitle(string title);
    }
}