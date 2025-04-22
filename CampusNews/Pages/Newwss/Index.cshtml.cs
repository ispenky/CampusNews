using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusNews.Data;
using CampusNews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampusNews.Pages.Newwss
{
    public class IndexModel : PageModel
    {
        private readonly CampusNews.Data.CampusNewsContext _context;

        public IndexModel(CampusNews.Data.CampusNewsContext context)
        {
            _context = context;
        }

        public IList<Newws> Newws { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? NewsGenre { get; set; }

        public string? UserEmail { get; set; }

        public async Task OnGetAsync()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");

            // <snippet_search_linqQuery>
            IQueryable<string> genreQuery = from m in _context.Newws
                                            orderby m.Genre
                                            select m.Genre;
            // </snippet_search_linqQuery>

            var movies = from m in _context.Newws
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(NewsGenre))
            {
                movies = movies.Where(x => x.Genre == NewsGenre);
            }

            // <snippet_search_selectList>
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            // </snippet_search_selectList>
            Newws = await movies.ToListAsync();
        }

    }
}
