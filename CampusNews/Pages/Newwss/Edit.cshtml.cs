using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusNews.Data;
using CampusNews.Models;

namespace CampusNews.Pages.Newwss
{
    public class EditModel : PageModel
    {
        private readonly CampusNews.Data.CampusNewsContext _context;

        public EditModel(CampusNews.Data.CampusNewsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newws Newws { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newws =  await _context.Newws.FirstOrDefaultAsync(m => m.Id == id);
            if (newws == null)
            {
                return NotFound();
            }
            Newws = newws;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Newws).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //当一个客户端删除新闻并且另一个客户端对新闻发布更改时，前面的代码会检测并发异常。
                if (!NewwsExists(Newws.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewwsExists(int id)
        {
            return _context.Newws.Any(e => e.Id == id);
        }
    }
}
