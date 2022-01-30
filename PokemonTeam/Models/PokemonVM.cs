using System.ComponentModel.DataAnnotations;

namespace PokemonTeam.Models
{
    public class PokemonVM
    {
        [Key]
        public string Name { get; set; }

        public bool Selected { get; set; } = false;

        public PokemonVM(Pokemon p) { Name = p.Name; }

        public PokemonVM(string name) { Name = name; }
    }
}