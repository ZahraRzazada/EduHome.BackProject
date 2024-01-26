using System;
using EduHome.Data.Contexts;
using EduHome.Data.ServiceRegisterations;
using EduHome.Service.ServiceRegistrations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddDbContext<EduDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("default"))
);
builder.Services.DataAccessServiceRegister(builder.Configuration);
builder.Services.ServiceLaierServiceRegister();
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

app.UseAuthentication();
app.UseAuthorization();



    
app.UseEndpoints(endpoint =>
{

    endpoint.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Identity}/{action=Login}/{id?}"
    );

    endpoint.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
