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
                    Message = "��ʹ�ü��ִ�ѧѧ������(@jlu.edu.cn)���е�¼";
                    return Page();
                }

                // �� Email �� User ���в����û�
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    // ��û���򴴽��û�
                    //user = new User { Email = Email };
                    //_context.User.Add(user);
                    Message = "����ע�ᣬ�ٵ�¼";
                    return Page();

                    //await _context.SaveChangesAsync();
                }

                // �����û� ID ���Ự
                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetInt32("UserId", user.Id);

                await _loginService.SendLoginEmail(Email);
                Message = "��¼�����ѷ��͵��������䣬����ա�";

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
                    // �����û���Ϣ���Ự����������¼�ɹ�����Ҫ�����û���Ϣ��
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
