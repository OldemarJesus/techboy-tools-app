using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        var defaultDuration = Environment.GetEnvironmentVariable("DEFAULT_COOKIE_HOUR_DURATION");
        if(defaultDuration == null)
            return;

        x.ExpireTimeSpan = TimeSpan.FromHours(int.Parse(defaultDuration));
        x.SlidingExpiration = true;
        x.AccessDeniedPath = "/Forbidden/";
        x.LoginPath = "/Auth/Login";
        x.LogoutPath = "/Auth/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// cookie options
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
