using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_CommerceWithIdentity.Areas.Identity.Data;
using E_CommerceWithIdentity.Services.Abstract;
using E_CommerceWithIdentity.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("E_CommerceWithIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'E_CommerceWithIdentityContextConnection' not found.");
builder.Services.AddDbContext<E_CommerceWithIdentityContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser , IdentityRole>()
    .AddEntityFrameworkStores<E_CommerceWithIdentityContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI(); // AddDefaultUI() bu noktada eklenmeli
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
