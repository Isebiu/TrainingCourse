using Microsoft.EntityFrameworkCore;
using Training.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.DataAccess.Repository;
using Training.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Training.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//.AddRazorRuntimeCompilation();
//builder.Services.AddControllers();

//var connstr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); 

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IUnitOfWork, UnitOfwork>(); //inregistram Repo-urile in DI container 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

//Adugam session in project => trebuie sa adaugam 2 servicii
builder.Services.AddDistributedMemoryCache();//pt stocarea in ram de date volatile
builder.Services.AddSession(options => //configuram sesiunea cu optiuni (timeout, cookie...)
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//adaugare logare cu facebook
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "3741905526074367";
    options.AppSecret = "03eaaade85db931e16370ce990b53e6f";
});


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

string key = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
StripeConfiguration.ApiKey = key;

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllers(); //it give us the ability to use controllers 

app.Run();
