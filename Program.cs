using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();




//Getting Connection string
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
//Getting Assembly Name
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();



void ConfigureServices(IServiceCollection services)
{
    //Added for session state
    services.AddDistributedMemoryCache();
    services.AddHttpContextAccessor();
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
    });
}


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// using medium.Models;
// using Microsoft.AspNetCore.Mvc;
// using System.Diagnostics;
// using Microsoft.Data.SqlClient;

// string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediumDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

// string login = "lz";

// SqlConnection con = new SqlConnection(connString);
// con.Open();
// SqlParameter p1 = new SqlParameter("l", login);
// string query = "select login,writerName from users " +
// $"where login = @l";
// SqlCommand cmd = new SqlCommand(query, con);
// cmd.Parameters.AddWithValue("@l", login ?? (object)DBNull.Value);
// SqlDataReader dr = cmd.ExecuteReader();
// if (dr.HasRows)
// {
//     while (dr.Read())
//     {
//         Console.WriteLine(dr[0].ToString()));

//     }
// }
// cmd.Parameters.Clear();
// con.Close();
