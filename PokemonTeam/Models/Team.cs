namespace PokemonTeam.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Pokemon> Roster { get; set; } = new List<Pokemon>();
    }
}
