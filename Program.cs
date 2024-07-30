using System.Reflection;
using System.Text;
using AuthSample;
using AuthSample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 設定 Kestrel 伺服器的 URL
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Loopback, 5000); // HTTP

    options.Listen(System.Net.IPAddress.Loopback,
                   5001,
                   listenOptions =>
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
await GetPermission();
app.Run();

void ConfigureServices(IServiceCollection services)
{
    var configuration = builder.Configuration;

    #region db

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                                            options.UseSqlServer(
                                                                builder.Configuration.GetConnectionString(
                                                                    "DefaultConnection")));

// Add Identity services
    builder.Services.AddIdentity<ApplicationUser, Role>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

    #endregion

    #region cors

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
                          builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
    });

    #endregion

    #region jwt auth

    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

    services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

    // services.AddAuthorization(options =>
    // {
    //     options.AddPolicy("PermissionPolicy", policy =>
    //                           policy.Requirements.Add(new PermissionRequirement("SomePermission")));
    // });

    //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

    #endregion

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
    app.UseMiddleware<PermissionMiddleware>();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    // 設定 SPA 路由
    //app.MapFallbackToFile("index.html"); // SPA 路由，處理所有未匹配的路由
}

async Task GetPermission()
{
    using (var scope = app.Services.CreateScope())
    {
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var assembly = Assembly.GetExecutingAssembly();

        var controllers = assembly.GetTypes()
                                  .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                                  .SelectMany(t => t.GetMethods(
                                                  BindingFlags.Instance |
                                                  BindingFlags.Public |
                                                  BindingFlags.DeclaredOnly))
                                  .Where(m => m.GetCustomAttribute<ApiPermissionAttribute>() != null);

        foreach (var method in controllers)
        {
            var attr = method.GetCustomAttribute<ApiPermissionAttribute>();
            var code = $"{attr.Module}_{attr.Func}_{attr.Action}";

            var permission = new Permission
            {
                Code = code, Module = attr.Module, Func = attr.Func, Action = attr.Action
            };

            if (!ctx.Permissions.Any(p => p.Code == code)) { ctx.Permissions.Add(permission); }
        }

        await ctx.SaveChangesAsync();

        // Map Module="Setting" permissions to Admin role
        //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var role = await ctx.Roles.FirstOrDefaultAsync(a => a.Name == "Admin");

        if (role != null)
        {
            var permissions = ctx.Permissions.ToList(); //.Where(p => p.Module == "Setting").ToList();

            // Assuming you have a way to associate permissions with roles, such as a RolePermission table.
            foreach (var permission in permissions)
            {
                if (role.Permissions == null || role.Permissions.All(a => a.Rd != permission.Rd))
                    role.Permissions?.Add(permission);
            }

            await ctx.SaveChangesAsync();
        }

        var hmpy = await ctx.Roles.FirstOrDefaultAsync(a => a.Name == "Hmpy");

        if (hmpy != null)
        {
            var permissions = ctx.Permissions.Where(p => p.Module == "Basic").ToList();

            // Assuming you have a way to associate permissions with roles, such as a RolePermission table.
            foreach (var permission in permissions)
            {
                if (hmpy.Permissions == null || hmpy.Permissions.All(a => a.Rd != permission.Rd))
                    hmpy.Permissions?.Add(permission);
            }

            await ctx.SaveChangesAsync();
        }
    }
}
