using InterviewProject.Data;
using InterviewProject.Data.Model;
using InterviewProject.InitialData.Classes;
using InterviewProject.InitialData.Interfaces;
using InterviewProject.Services.Classes;
using InterviewProject.Services.Interfaces;
using InterviewProject.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbSqlConnection")));

builder.Services.AddControllers();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders()
.AddUserStore<Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, Role, DataContext, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
.AddRoleStore<Microsoft.AspNetCore.Identity.EntityFrameworkCore.RoleStore<Role, DataContext, string, UserRole, IdentityRoleClaim<string>>>()
.AddRoleManager<RoleManager<Role>>()
.AddUserManager<UserManager<User>>();

builder.Services.AddScoped<IDataContext, DataContext>();

builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddScoped<IDatabaseInitializerService, DatabaseInitializerService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

// Seed the db
using (var serviceScope = app.Services.CreateScope())
{
    var dbInitializer = serviceScope.ServiceProvider.GetRequiredService<IDatabaseInitializerService>();
    dbInitializer.EnsureInitialData();
}

app.Run();
