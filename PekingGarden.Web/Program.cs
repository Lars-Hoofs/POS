using PekingGarden.DAL;
using PekingGarden.Logic.Interfaces;
using PekingGarden.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
string ConnectionString = builder.Configuration.GetConnectionString("PekingGarden") ?? throw new InvalidOperationException("Connection string 'PekingGarden' ontbreekt. Zet hem met dotnet user-secrets");

builder.Services.AddScoped<ITableRepository>(_ => new TableRepository(ConnectionString));
builder.Services.AddScoped<TableService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
