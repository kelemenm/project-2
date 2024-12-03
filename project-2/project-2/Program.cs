using Domain;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LaborConnectionString") 
                            ?? throw new InvalidOperationException($"Connection string {nameof(LaborDbContext)} not found.");
builder.Services.AddDbContext<LaborDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("project-2")));

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Adjuk hozzá az ExcelFileReader-t is a DI konténerhez
builder.Services.AddScoped<ExcelFileReader>();

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

    endpoints.MapControllerRoute(
    name: "HUMVIfelelos",
    pattern: "{controller=HUMVIfelelos}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "HUMVImodul",
    pattern: "{controller=HUMVImodul}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "Mintavevo",
    pattern: "{controller=Mintavevo}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "MvHely",
    pattern: "{controller=MvHely}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "MvOka",
    pattern: "{controller=MvOka}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "MvTipus",
    pattern: "{controller=MvTipus}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "VizsgaloLabor",
    pattern: "{controller=VizsgaloLabor}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "Mertekegyseg",
    pattern: "{controller=Mertekegyseg}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "Parameter",
    pattern: "{controller=Parameter}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "Minta",
    pattern: "{controller=Minta}/{action=Index}/{id?}");


});


app.UseAuthorization();

app.MapRazorPages();

//Seeder app minden indításakor lefut
//Ha seedelt adattáblák üresek, akkor feltölti adatokkal
DbInitializer.Seed(app);

// Az Excel fájl beolvasása
// Scoped szolgáltatásként kérjük le az ExcelFileReader-t
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LaborDbContext>();

    if (!context.Minta.Any())
    { 
        var reader = scope.ServiceProvider.GetRequiredService<ExcelFileReader>(); // Scoped példány kérése
        FileInfo uploadedFile = reader.ExcelFileUploader();
        string sheetName = "HUMVI";
        int headerRow = 2;
        Dictionary<string, int> headerColumns = reader.HeaderCols(uploadedFile, sheetName, headerRow);
        List<List<string>> excelData = reader.ReadExcelSheet(uploadedFile, sheetName, headerColumns);
        reader.ProcessLines(excelData, headerColumns);
    }
}


app.Run();