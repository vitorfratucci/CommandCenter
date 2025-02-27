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

// Injeta o repositório no container de dependências
builder.Services.AddSingleton<ICrisesRepository, CrisesRepository>();

// Adicionando MVC ao projeto
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Definição da Rota Padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
