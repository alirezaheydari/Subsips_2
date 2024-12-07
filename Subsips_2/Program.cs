using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.SubwayStation;
using Subsips_2.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Subsips_2ContextConnection") ?? throw new InvalidOperationException("Connection string 'Subsips_2ContextConnection' not found.");

builder.Services.AddDbContext<Subsips_2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Subsips_2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ISubwayStationRepository, SubwayStationRepository>();
builder.Services.AddTransient<ICoffeeCupRepository, CoffeeCupRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Subsips}/{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();



app.Run();
