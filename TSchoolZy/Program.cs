using Application.Interface;
using Application.Mapping;
using Application.Service;
using Application.validation;
using Core.Interface;
using DataAccessLayer.GenericRepository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositey;
using Repositey.Configuration;
using Repositey.Data;
using Repositey.GenericRepository;
using System;
using System.Text.Json;

namespace TSchoolZy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            int x = "hello";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorApp",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7153") // <-- رابط Blazor Server
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            // Database Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                      .UseSqlServer(builder.Configuration.GetConnectionString("constr")));

            // =============================================
            // Identity Configuration
            // =============================================
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";  // مسار تسجيل الدخول
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);  // مدة بقاء الكوكي (لـ Remember Me)
                options.SlidingExpiration = true; // تجديد الصلاحية عند النشاط
            });


            // =============================================
            // Application Services
            // =============================================
            // Generic Services
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddHttpClient();

            // Specific Services
            builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddScoped<IContactFormService, ContactFormService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IDataSeeder, DataSeederService>();

            // Configuration
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("EmailSettings"));

            // =============================================
            // Web Framework Services
            // =============================================
            // MVC & API
            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClientDtoValidator>());

            // Blazor
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSignalR();

            // Authentication/Authorization
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            
            // AutoMapper
            builder.Services.AddAutoMapper(typeof(TeamMemberProfile).Assembly);

            // =============================================
            // Build Application
            // =============================================
            var app = builder.Build();

            // =============================================
            // Middleware Pipeline Configuration
            // =============================================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors("AllowBlazorApp");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Authentication/Authorization must come after Routing
            app.UseAuthentication();
            app.UseAuthorization();

            // Anti-Forgery Middleware
            app.Use(async (context, next) =>
            {
                if (string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = context.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(context);
                    context.Response.Cookies.Append(
                        "X-CSRF-TOKEN-COOKIE", // نفس الاسم اللي اخترناه
                        tokens.RequestToken!,
                        new CookieOptions { HttpOnly = false }
                    );
                }
                await next();
            });
            // =============================================
            // Endpoint Configuration
            // =============================================
            app.MapControllers();
            app.MapBlazorHub();
         

            // =============================================
            // Data Seeding
            // =============================================
            using (var scope = app.Services.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                await dataSeeder.SeedAsync();
            }

            // =============================================
            // Run Application
            // =============================================
            app.Run();
        }
    }
}