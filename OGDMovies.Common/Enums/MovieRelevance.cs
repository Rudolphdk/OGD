using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OGDMovies.Common.Enums
{
    public enum MovieRelevance : byte
    {
        [Display(Name = "Most Popular")]
        Popular = 1,
        [Display(Name = "Trending")]
        Trending = 2,
        [Display(Name = "Top Rated")]
        TopRated = 3,
        [Display(Name = "Upcomming")]
        Upcomming = 5,
        [Display(Name = "Search")]
        Search = 6
    }

    public static class CustomEnumHelper
    {
        public static string DisplayNameFor(this Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayName = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayName != null)
            {
                return displayName.Name;
            }

            return item.ToString();
        }
    }
}
