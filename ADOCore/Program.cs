//the Kestrel web server,
//the application middleware pipeline,
//and all the other bits,
//and connects them, so that your application is ready to serve your requests.

using DAL;
using DAL.Repositories;
/// <summary>
///  1.  The web host builder is basically a factory to create a web host. 
///  2.  It is the thing that constructs the web host but also configures all the necessay bits 
///      the web host needs to determine how to run the web application.
///  3.  It gets created when your application starts up, and then it will construct all the necessary pieces, 
///  4.   like
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ITeacher, TeacherRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
