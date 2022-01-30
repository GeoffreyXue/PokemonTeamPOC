using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokemonTeam.Data;
using PokemonTeam.Models;
using PokemonTeam.Models.JsonResponse;

namespace PokemonTeam.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly PokemonTeamContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(PokemonTeamContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Team Team { get; set; }
        public IEnumerable<PokemonVM> Pokemon { get; set; } = new List<PokemonVM>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Teams.FirstOrDefaultAsync(m => m.ID == id);

            if (Team == null)
            {
                return NotFound();
            }

            await FetchPokemonList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, string[] selectedPokemon)
        {
            var teamToUpdate = await _context.Teams.FindAsync(id);

            //foreach (string p in selectedPokemon)
            //{
            //    teamToUpdate.Roster.Add(new Pokemon(p));
            //}

            ICollection<Pokemon> pokemon = new List<Pokemon>();

            foreach (string p in selectedPokemon)
            {
                pokemon.Add(new Pokemon(p));
            }

            if (teamToUpdate == null)
            {
                return NotFound();
            }

            teamToUpdate.Roster = pokemon;

            if (await TryUpdateModelAsync<Team>(
                teamToUpdate,
                "team",
                t => t.Name, t => t.Roster))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private async Task FetchPokemonList()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://pokeapi.co/api/v2/pokemon");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response != null)
                {
                    PokemonListResponse? responseWrapper = JsonConvert.DeserializeObject
                        <PokemonListResponse>(response);

                    if (responseWrapper != null)
                    {
                        IEnumerable<Pokemon> list = responseWrapper.Pokemon ?? new List<Pokemon>();
                        ICollection<PokemonVM> pokemonVM = new List<PokemonVM>();
                        foreach (Pokemon p in list)
                        {
                            PokemonVM pVM = new PokemonVM(char.ToUpper(p.Name[0]) + p.Name[1..].ToLower());
                            if (Team.Roster.Any(pokemon => pokemon.Name == p.Name))
                            {
                                pVM.Selected = true;
                            }
                            pokemonVM.Add(pVM);
                        }

                        Pokemon = pokemonVM;
                    }
                }
            }
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
