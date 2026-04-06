using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;
using ISYS366_VascoBerardo_Assignment3.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ISYS366_VascoBerardo_Assignment3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ISYS366_VascoBerardo_Assignment3Context") ?? throw new InvalidOperationException("Connection string 'ISYS366_VascoBerardo_Assignment3Context' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ISYS366_VascoBerardo_Assignment3Context>();

builder.Services.AddValidation();

builder.Services.AddAuthorization(options =>
{
    // in our authorization options we add a policy
    // that requires the user to have the admin role
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// add this section to configure options for our razor pages
builder.Services.AddRazorPages(options =>
{
    // secure anything in the Pages/Items folder 
    // by assigning it the admin policy
    // which we created above 
    // saying it requires a user to have the admin role
    options.Conventions.AuthorizeFolder("/Movies", "AdminPolicy");
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    await AdminHelper.SeedAdminAsync(scope.ServiceProvider);
}

app.Run();
