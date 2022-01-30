using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PokemonTeam.Models
{
    public class Pokemon
    {
        [Key]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public Pokemon(string name) { Name = name; }
    }
}
