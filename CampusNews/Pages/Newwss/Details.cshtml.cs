using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusNews.Data;
using CampusNews.Models;

namespace CampusNews.Pages.Newwss
{
    public class DetailsModel : PageModel
    {
        private readonly CampusNews.Data.CampusNewsContext _context;

        public DetailsModel(CampusNews.Data.CampusNewsContext context)
        {
            _context = context;
        }

        public Newws Newws { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newws = await _context.Newws.FirstOrDefaultAsync(m => m.Id == id);

            if (newws is not null)
            {
                Newws = newws;

                return Page();
            }

            return NotFound();
        }
    }
}
