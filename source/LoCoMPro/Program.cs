using Microsoft.EntityFrameworkCore;
using LoCoMPro.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LoCoMProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoCoMProContext") ?? throw new InvalidOperationException("Connection string 'LoCoMProContext' not found.")));

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Home/Index", "");  // Set default Index route to home
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else  // This is for errors while migration data
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// This creates the data base if it is not created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<LoCoMProContext>();
    context.Database.EnsureCreated();
    // This feeds the DB if there is nothing
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