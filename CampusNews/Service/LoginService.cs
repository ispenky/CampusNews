using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CampusNews.Data;
using CampusNews.Models;
using CampusNews.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace CampusNews.Service
{
    public class LoginService
    {
        private readonly CampusNewsContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(CampusNewsContext context, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendLoginEmail(string email)
        {
            if (email.EndsWith("@mails.jlu.edu.cn"))
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    user = new User { Email = email };
                    _context.User.Add(user);
                }

                string token = Guid.NewGuid().ToString();
                user.Token = token;
                user.TokenExpiration = DateTime.Now.AddMinutes(10);

                await _context.SaveChangesAsync();

                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                var loginLink = $"{baseUrl}/Login?token={token}";

                await _emailSender.SendEmailAsync(email, "登录链接", $"请点击以下链接登录：{loginLink}");
            }
        }

        public async Task<bool> ValidateToken(string token)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Token == token && u.TokenExpiration > DateTime.Now);
            return user != null;
        }
    }
}