using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusNews.Data;
using CampusNews.Models;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CampusNews.Pages.Users
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
        public User User { get; set; } = default!;
        public string Message { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (!IsJluEmail(User.Email))
                {
                    Message = "请使用吉林大学学生邮箱(@jlu.edu.cn)进行登录";
                    return Page();
                }
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }

        private bool IsJluEmail(string email)
        {
            // Regular expression to validate Jilin University email format
            string pattern = @"^[a-zA-Z0-9._%+-]+@jlu\.edu\.cn$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
