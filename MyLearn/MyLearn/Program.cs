using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyLearn.Core.Service.Course;
using MyLearn.Core.Service.ForumService;
using MyLearn.Core.Service.OrderService;
using MyLearn.Core.Service.PermissionService;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Context;
using TopLearn.Core.Convertors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MyLearnContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionStrings:MyLearnConnection"]);

});
#region IOT
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<ICourseService,CourseService>();
builder.Services.AddTransient<IOrderService,OrderService>();
builder.Services.AddTransient<IForumService,ForumService>();
builder.Services.AddTransient<FileExtensionContentTypeProvider>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/LogOut";
        options.ExpireTimeSpan = TimeSpan.FromDays(10);
    });
#endregion
#region MyRegion

#endregion
var app = builder.Build();



// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.ToString().ToLower().StartsWith("/CourseOnline"))
    {
        var callingurl = context.Request.Headers["Referer"].ToString();
        if (callingurl != "" && (callingurl.StartsWith("http://localhost:5213") || callingurl.StartsWith("https://localhost:5213")))
        {
            await next.Invoke();
        }
        else
        {
            context.Response.Redirect("/Login");
        }
    }
    else
    {
        await next.Invoke();
    }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
