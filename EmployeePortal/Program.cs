using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Admin;
using Application.Interfaces.IService;
using Application.Interfaces.IService.Admin;
using Application.Services;
using Application.Services.Admin;
using EmployeePortal.Components;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System;
using System.Collections.Generic;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/";
        options.Cookie.Name = "EmployeePortalAuth";

   
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddScoped<IDashboardSummaryRepository, DashboardSummaryRepository>();
builder.Services.AddScoped<IDashboardSummaryService,DashboardSummaryService>(); 
builder.Services.AddScoped<IAttendanceChartRepository,AttendanceChartRepository>();
builder.Services.AddScoped<IAttendanceChartService,AttendanceChartService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    //return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
    var handler = new HttpClientHandler
    {
        UseCookies = true
    };

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(nav.BaseUri)
    };
});
//builder.Services.AddScoped(sp =>
//{
//    var navigationManager = sp.GetRequiredService<NavigationManager>();
//    return new HttpClient
//    {
//        BaseAddress = new Uri(navigationManager.https://localhost:7076/)
//    };
//});
var app = builder.Build();







// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapPost("/api/login", async (
    LoginDto loginDto,
    IAuthService authService, HttpContext httpContext) =>
{
    var user = await authService.Login(loginDto);
    if (user == null)
        return Results.Unauthorized();
    var claims = new List<Claim>
        {

          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
    var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal);
    return Results.Ok();
}
);
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
