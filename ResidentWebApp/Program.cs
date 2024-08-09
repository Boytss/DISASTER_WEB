using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using ResidentWebApp.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<INewsEventRepository, NewsEventRepository>();
builder.Services.AddScoped<IHazardMapRepository, HazardMapRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IDisasterRepository, DisasterRepository>();
builder.Services.AddScoped<ITutorialTextRepository, TutorialTextRepository>();
builder.Services.AddScoped<IEvacuationCenterRepository, EvacuationCenterRepository>();
builder.Services.AddControllersWithViews(); // Use AddControllersWithViews instead of AddControllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
         options.LogoutPath = "/account/login"; 
    });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins("hhttps://b7d9-175-176-53-44.ngrok-free.app") // Your web page's URL
        //builder.WithOrigins("http://192.168.1.10:5259") // Your web page's URL
        builder.AllowAnyOrigin() // Your web page's URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
     app.UseHttpsRedirection();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Use HTTPS redirection only in production
/*if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
} */
app.UseAuthentication(); 

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Image")),
    RequestPath = "/Image"
});
app.UseRouting();
app.UseCors();

app.UseAuthorization();
// Middleware to redirect unauthenticated users to the login page
app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated && context.Request.Path == "/")
    {
        context.Response.Redirect("/account/login");
    }
    else
    {
        await next();
    }
});


app.MapControllers();
app.MapDefaultControllerRoute(); // Use the default controller route to map controllers
// Serve index.html at the root URL
app.MapFallbackToFile("index.html");
app.Urls.Add("http://*:5259");
//app.Urls.Add("http://localhost:5259");

app.Run();