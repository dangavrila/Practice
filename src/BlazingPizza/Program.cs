using BlazingPizza.Data;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<PizzaService>();
builder.Services.AddScoped<OrderState>();
builder.Services.AddHttpClient();
builder.Services.AddDbContextFactory<PizzaStoreContext>(
    options => options.UseSqlite("Data Source=pizza.db"));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");


app.Run();