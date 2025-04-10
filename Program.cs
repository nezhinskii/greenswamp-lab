using greenswamp.Models;
using greenswamp.Services;

var builder = WebApplication.CreateBuilder(args);

string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "requests.log");
builder.Services.AddSingleton<RequestLogging>(new RequestLogging(next => Task.CompletedTask, logFilePath));


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddSingleton<IEmailService, EmailService>(); 
builder.Services.AddSingleton<IContactFormService, ContactFormService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<RequestLogging>(logFilePath);

app.UseStatusCodePagesWithReExecute("/404");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
