using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DTO
{
    public class MovieSearchResultDTO
    {
        [JsonProperty("id")]
        public int TmdbId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("overview")]
        public string? Overview { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("release_date")]
        public string? ReleaseDateRaw { get; set; }

        [JsonIgnore]
        public DateTime? ReleaseDate
        {
            get => DateTime.TryParse(ReleaseDateRaw, out var dt) ? dt : null;
            set => ReleaseDateRaw = value?.ToString("yyyy-MM-dd");
        }
    }
}
