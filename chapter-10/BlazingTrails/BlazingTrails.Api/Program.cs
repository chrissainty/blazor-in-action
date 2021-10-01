using BlazingTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlazingTrailsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext")));
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("BlazingTrails.Shared")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/Images")
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();