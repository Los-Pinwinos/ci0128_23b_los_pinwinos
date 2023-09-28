using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LoCoMPro.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LoCoMProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoCoMProContext") ?? throw new InvalidOperationException("Connection string 'LoCoMProContext' not found.")));

// Agrega servicios a los containers
builder.Services.AddRazorPages(options =>
{
    // Pone el Index default a Home
    options.Conventions.AddPageRoute("/Home/Index", "");
});

var app = builder.Build();

// Configura el pipeline de requests HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
// Maneja errores de migración de datos
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// Crea la base de datos si se encuentra nula
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<LoCoMProContext>();
    context.Database.EnsureCreated();
    // Alimenta la base de datos
    DBInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();