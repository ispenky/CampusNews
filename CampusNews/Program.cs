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

// ��ӻỰ����
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ���ûỰ�Ŀ��г�ʱʱ��
    options.Cookie.HttpOnly = true; // ȷ���Ự Cookie ֻ��ͨ�� HTTP ����
    options.Cookie.IsEssential = true; // ���Ự Cookie ���Ϊ�����
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
app.UseStaticFiles(); // ʹ�ñ�׼��������̬��Դ
app.UseSession(); // ʹ�ûỰ�м��

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages()
   .WithStaticAssets();
app.UseStaticFiles();

app.Run();