using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LoCoMPro.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LoCoMProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoCoMProContext") ?? throw new InvalidOperationException("Connection string 'LoCoMProContext' not found.")));

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Home/Index", "");  // Set default Index route to home
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();