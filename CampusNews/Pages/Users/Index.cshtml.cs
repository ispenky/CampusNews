using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusNews.Data;
using CampusNews.Models;

namespace CampusNews.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly CampusNews.Data.CampusNewsContext _context;

        public IndexModel(CampusNews.Data.CampusNewsContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;
        public string UserEmail { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
            UserEmail = HttpContext.Session.GetString("UserEmail");
            System.Diagnostics.Debug.WriteLine($"UserEmail11111111111: {UserEmail}");
        }
    }
}
