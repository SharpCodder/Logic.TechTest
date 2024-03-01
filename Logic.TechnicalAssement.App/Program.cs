using Logic.TechnicalAssement.App.Data;
using Logic.TechnicalAssement.App.Interfaces.Data;
using Logic.TechnicalAssement.App.Interfaces.Services;
using Logic.TechnicalAssement.App.Interfaces.Validators;
using Logic.TechnicalAssement.App.Services;
using Logic.TechnicalAssement.App.Validators.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ILogger<LeaveService>, Logger<LeaveService>>();
builder.Services.AddSession();

builder.Services.AddTransient<ILeaveViewModelValidator, LeaveViewModelValidator>();
builder.Services.AddTransient<ILeaveRepository, LeaveRepository>();
builder.Services.AddTransient<ILeaveService, LeaveService>();


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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
