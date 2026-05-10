using Microsoft.EntityFrameworkCore;
using VideoGameManager.Data;
using VideoGameManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// GameService, Repository afegit
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<GameRepository>();
builder.Services.AddSingleton<GamesExporter>();
builder.Services.AddSingleton<GamesRanking>();

// DbContext service afegit per treballar amb EntityFramework
builder.Services.AddDbContext<GameStoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
