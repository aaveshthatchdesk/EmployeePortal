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
using Microsoft.EntityFrameworkCore;

using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IDashboardSummaryRepository, DashboardSummaryRepository>();
builder.Services.AddScoped<IDashboardSummaryService,DashboardSummaryService>(); 
builder.Services.AddScoped<IAttendanceChartRepository,AttendanceChartRepository>();
builder.Services.AddScoped<IAttendanceChartService,AttendanceChartService>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
