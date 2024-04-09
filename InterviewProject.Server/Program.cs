using InterviewProject.Data;
using InterviewProject.Data.Model;
using InterviewProject.InitialData.Classes;
using InterviewProject.InitialData.Interfaces;
using InterviewProject.Server.Cookie;
using InterviewProject.Services.Classes;
using InterviewProject.Services.Interfaces;
using InterviewProject.SqlServer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbSqlConnection")));

// // Angular's default header name for sending the XSRF token.
// builder.Services.AddAntiforgery(options =>
// {
//     options.HeaderName = "X-XSRF-TOKEN";
// });

builder.Services.AddControllersWithViews(config =>
{
    config.Filters.Add(new ResponseCacheAttribute { NoStore = true, Location = ResponseCacheLocation.None });
    // config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.EventsType = typeof(CustomCookieAuthenticationEvents);
    config.Cookie.Name = ".AspNetCore.Cookies";
    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    config.SlidingExpiration = true;
    config.LoginPath = new PathString("/account/login/");
});
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = new[]
    {
        // Default
        "text/plain",
        "text/css",
        "application/javascript",
        "text/html",
        "application/xml",
        "text/xml",
        "application/json",
        "text/json",
        // Custom
        "image/svg+xml"
    };
});

builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders()
.AddUserStore<Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, Role, DataContext, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
.AddRoleStore<Microsoft.AspNetCore.Identity.EntityFrameworkCore.RoleStore<Role, DataContext, string, UserRole, IdentityRoleClaim<string>>>()
.AddRoleManager<RoleManager<Role>>()
.AddUserManager<UserManager<User>>()
.AddSignInManager<SignInManager<User>>();

builder.Services.AddScoped<IDataContext, DataContext>();

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<IDatabaseInitializerService, DatabaseInitializerService>();

var app = builder.Build();

app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseResponseCompression();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.UseResponseCaching();

// Seed the db
using (var serviceScope = app.Services.CreateScope())
{
    var dbInitializer = serviceScope.ServiceProvider.GetRequiredService<IDatabaseInitializerService>();
    dbInitializer.EnsureInitialData();
}

app.Run();
