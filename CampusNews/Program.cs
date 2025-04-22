using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CampusNews.Data;
using CampusNews.Models;
using CampusNews.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CampusNewsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusNewsContext") ?? throw new InvalidOperationException("Connection string 'CampusNewsContext' not found.")));

// 添加会话服务
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 设置会话的空闲超时时间
    options.Cookie.HttpOnly = true; // 确保会话 Cookie 只能通过 HTTP 访问
    options.Cookie.IsEssential = true; // 将会话 Cookie 标记为必需的
});

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // 使用标准方法处理静态资源
app.UseSession(); // 使用会话中间件

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages()
   .WithStaticAssets();
app.UseStaticFiles();

app.Run();