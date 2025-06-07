using CommandCenter.Configurations;
using CommandCenter.Repository;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDB
builder.Services.Configure<DatabaseConfig>(
    builder.Configuration.GetSection("DatabaseConfig"));

// Injeta `DatabaseConfig` como `IDatabaseConfig`
builder.Services.AddSingleton<IDatabaseConfig>(sp =>
    sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

// Repositórios
builder.Services.AddSingleton<ICrisesRepository, CrisesRepository>();
builder.Services.AddSingleton<IInformativosRepository, InformativosRepository>(); // ✅ NOVO!

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
