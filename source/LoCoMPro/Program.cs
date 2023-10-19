using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LoCoMProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoCoMProContext") ?? throw new InvalidOperationException("Connection string 'LoCoMProContext' not found.")));

// Agrega servicios a los containers
builder.Services.AddRazorPages(options =>
{
    // Pone el Index default a Home
    options.Conventions.AddPageRoute("/Home/Index", "");
});

// Establece una sesión con el timeout en minutos de configuración
// y cookies. Una sesión guarda la información de la cuenta ingresada.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("minutosTimeout"));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Establece autenticación mediante cookies y el timeout en minutos
// de configuración. Esto realiza la autentificación.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name = builder.Configuration.GetValue<string>("nombreCookie");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("minutosTimeout"));
    options.SlidingExpiration = true;
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

    // TODO(emilia): comentar:
    context.Database.EnsureCreated();
    // Alimenta la base de datos si no hay nada
    DBInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Opciones necesarias para las cuentas
// Establece que la app utilice sesiones
app.UseSession();
// Establece que la app utilice autenticación
app.UseAuthentication();
// Establece que la app utilice autorización (acciones condicionadas)
app.UseAuthorization();

app.MapRazorPages();

app.Run();