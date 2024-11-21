using Domain;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LaborConnectionString") 
                            ?? throw new InvalidOperationException($"Connection string {nameof(LaborDbContext)} not found.");
builder.Services.AddDbContext<LaborDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("project-2")));

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "AkkrMintavetel",
        pattern: "{controller=AkkrMintavetel}/{action=Index}/{id?}");
});


app.UseAuthorization();

app.MapRazorPages();

//Seeder app minden indításakor lefut
//Ha seedelt adattáblák üresek, akkor feltölti adatokkal
DbInitializer.Seed(app);

app.Run();