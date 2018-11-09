using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IConnectionBase
    {
        string Key { get; }
        string Url { get; }
        int Page { get; set; }
        string RetrieveData();
    }
}