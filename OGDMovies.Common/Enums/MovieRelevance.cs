using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OGDMovies.Common.Enums
{
    public enum MovieRelevance : byte
    {
        [Display(Name = "Most Popular")]
        Popular = 1,
        [Display(Name = "Latest Releases")]
        Latest = 2,
        [Display(Name = "Top Rated")]
        TopRated = 3
    }
}
