using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PokemonTeam.Data;
using PokemonTeam.Models;

namespace PokemonTeam.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly PokemonTeam.Data.PokemonTeamContext _context;

        public IndexModel(PokemonTeam.Data.PokemonTeamContext context)
        {
            _context = context;
        }

        public IList<Team> Team { get;set; }

        public async Task OnGetAsync()
        {
            Team = await _context.Teams.ToListAsync();
        }
    }
}
