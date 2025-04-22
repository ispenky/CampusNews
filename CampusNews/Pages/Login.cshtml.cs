using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CampusNews.Service;
using System.Threading.Tasks;
using CampusNews.Data;
using CampusNews.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
namespace CampusNews.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LoginService _loginService;
        private readonly CampusNewsContext _context;
        public LoginModel(LoginService loginService, CampusNewsContext context)
        {
            _loginService = loginService;
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsValidToken { get; set; }

        private bool IsJluEmail(string email)
        {
            // Regular expression to validate Jilin University email format
            string pattern = @"^[a-zA-Z0-9._%+-]+@mails\.jlu\.edu\.cn$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!IsJluEmail(Email))
                {
                    Message = "请使用吉林大学学生邮箱(@jlu.edu.cn)进行登录";
                    return Page();
                }

                // 用 Email 在 User 表中查找用户
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    // 若没有则创建用户
                    //user = new User { Email = Email };
                    //_context.User.Add(user);
                    Message = "请先注册，再登录";
                    return Page();

                    //await _context.SaveChangesAsync();
                }

                // 保存用户 ID 到会话
                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetInt32("UserId", user.Id);

                await _loginService.SendLoginEmail(Email);
                Message = "登录链接已发送到您的邮箱，请查收。";

                //return RedirectToPage("/Index");
            }
            if (Token != null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }


        public async Task<IActionResult> OnGetAsync(string token)
        {
            Token = token;
            if (!string.IsNullOrEmpty(token))
            {
                IsValidToken = await _loginService.ValidateToken(token);
                if (IsValidToken)
                {
                    // 保存用户信息到会话（这里假设登录成功后需要保存用户信息）
                    var user = await _context.User.FirstOrDefaultAsync(u => u.Email == Email);
                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserEmail", Email);
                        HttpContext.Session.SetInt32("UserId", user.Id);
                    }
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
        //public async Task OnGetAsync(string token)
        //{
        //    Token = token;
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        IsValidToken = await _loginService.ValidateToken(token);
        //    }

        //}
    }
}
