using HelpDesk.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register TicketService with HttpClient
builder.Services.AddHttpClient<TicketService>(client =>
{
    // Make sure port 5036 matches your actual API port from Step 1!
    client.BaseAddress = new Uri("http://localhost:5036/"); 
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}");

app.Run();