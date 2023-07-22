using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Project.Detroit.Models;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Project.Detroit.Services;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

//Create a connection string and connect to projectDetroitServer
var ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=projectDetroitServer2;Trusted_Connection=True;";
builder.Services.AddDbContext<AppDBContext>(item => item.UseSqlServer(ConnectionString));
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<SharedService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();