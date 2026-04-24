var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map Razor Pages
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();