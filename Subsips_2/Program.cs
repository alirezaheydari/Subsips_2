using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Subsips_2;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CafeConfig;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.ExclusiveCustomer;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.SendNotification;
using Subsips_2.BusinessLogic.SubwayStation;
using Subsips_2.BusinessLogic.UserCustomer;
using Subsips_2.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Subsips_2ContextConnection") ?? throw new InvalidOperationException("Connection string 'Subsips_2ContextConnection' not found.");

builder.Services.AddDbContext<Subsips_2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<Subsips_2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ISubwayStationRepository, SubwayStationRepository>();
builder.Services.AddTransient<ICoffeeCupRepository, CoffeeCupRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ISendSmsNotification, SendSmsNotification>();
builder.Services.AddTransient<IVerificationCodeRepository, VerificationCodeRepository>();
builder.Services.AddTransient<IUserCustomerRepository, UserCustomerRepository>();
builder.Services.AddTransient<ICafeStationRepository, CafeStationRepository>();
builder.Services.AddTransient<ICustomerPhoneRegisterAuthenticationRepository, CustomerPhoneRegisterAuthenticationRepository>();
builder.Services.AddTransient<ICafeStationAspNetUserRepository, CafeStationAspNetUserRepository>();
builder.Services.AddTransient<ITimeSheetShiftCafeRepository, TimeSheetShiftCafeRepository>();
builder.Services.AddTransient<IExclusiveCafeCustomerRepository, ExclusiveCafeCustomerRepository>();
builder.Services.AddTransient<ICafeStationConfigRepository, CafeStationConfigRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        await SeedData.Initialize(services, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

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
