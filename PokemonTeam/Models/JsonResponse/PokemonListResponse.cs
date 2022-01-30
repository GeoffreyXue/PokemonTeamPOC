using Newtonsoft.Json;

namespace PokemonTeam.Models.JsonResponse
{
    public class PokemonListResponse
    {
        [JsonProperty(PropertyName = "next")]
        public string NextURL { get; set; }
        [JsonProperty(PropertyName = "previous")]
        public string PreviousURL { get; set; }
        [JsonProperty(PropertyName = "results")]
        public IEnumerable<Pokemon> Pokemon { get; set; }
    }
}
