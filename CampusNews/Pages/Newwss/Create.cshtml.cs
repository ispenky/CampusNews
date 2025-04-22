using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusNews.Data;
using CampusNews.Models;

namespace CampusNews.Pages.Newwss
{
    public class CreateModel : PageModel
    {
        private readonly CampusNews.Data.CampusNewsContext _context;

        public CreateModel(CampusNews.Data.CampusNewsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Newws Newws { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Newws.Add(Newws);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
