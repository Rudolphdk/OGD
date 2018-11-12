using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace OGDMovies.Web.Cache
{
    /// <summary>
    /// Custom caching interface
    /// Used an approach from:
    /// https://exceptionnotfound.net/a-simple-caching-scheme-for-web-api-using-dependency-injection/ 
    /// </summary>
    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }

    public class InMemoryCache : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            //Check if key exists in cache and get the object
            T item = MemoryCache.Default.Get(cacheKey) as T;
            //If object is null then call the non cached method to retrieve the data
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(30)); //cache for 30min
            }
            return item;
        }
    }


}