using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OGDMovies.Common.Enums
{
    public enum DatabaseRepo : byte
    {
        [Display(Name = "The Movie Database (TMDB)")]
        Tmdb = 1,
        [Display(Name = "OMDb")]
        Omdb = 2
    }
}
