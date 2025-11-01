using Newtonsoft.Json;

namespace MyMovieShelf.Domain.DTO
{
    public class ActorDTO
    {
        [JsonProperty("id")]
        public int TmdbId { get; set; }

        [JsonProperty("name")]
        public string? FullName { get; set; }

        [JsonProperty("profile_path")]
        public string? ProfilePath { get; set; }

        [JsonProperty("birthday")]
        public string? Birthday { get; set; }

        [JsonProperty("place_of_birth")]
        public string? PlaceOfBirth { get; set; }

        [JsonProperty("biography")]
        public string? Biography { get; set; }

    }

}
