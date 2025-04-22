using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using CampusNews.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CampusNews.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CampusNewsContext _context;

        public IndexModel(ILogger<IndexModel> logger, CampusNewsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string UserEmail { get; set; }
        public int UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");
            UserId = HttpContext.Session.GetInt32("UserId") ?? 0; // 如果 Session 是 null，则默认 0
        }

        public IActionResult OnGetLogout()
        {
            // 清空会话
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}