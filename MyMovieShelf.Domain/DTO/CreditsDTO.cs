using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Domain.DTO
{
    public class CreditsDTO
    {
        [JsonProperty("cast")]
        public List<ActorDTO>? Cast { get; set; }
    }
}
