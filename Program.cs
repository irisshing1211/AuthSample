using AuthSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 設定 Kestrel 伺服器的 URL
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Loopback, 5000); // HTTP
    options.Listen(System.Net.IPAddress.Loopback, 5001, listenOptions =>
    {
        listenOptions.UseHttps(); // 使用開發憑證的 HTTPS
    });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureServices(builder.Services);
var app = builder.Build();
Configure(app, app.Environment);
app.Run();

void ConfigureServices(IServiceCollection services)
{
    
    var configuration = builder.Configuration;
    
    services.AddDbContext<ApplicationDbContext>(options =>
                                                    options.UseSqlServer(
                                                        configuration.GetConnectionString("DefaultConnection")));
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
                          builder =>
                          {
                              builder.AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader();
                          });
    });
    services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    services.AddControllers();
    //services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCors("AllowAll");
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    // 設定 SPA 路由
    //app.MapFallbackToFile("index.html"); // SPA 路由，處理所有未匹配的路由


}
