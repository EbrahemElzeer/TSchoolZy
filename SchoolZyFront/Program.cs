using Application.Interface;
using Application.Service;
using Application.validation;
using Blazored.Toast;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using SchoolZyFront.Components;

using System.Net;

namespace SchoolZyFront
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
          

            builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7258/")
    });

           

            builder.Services.AddServerSideBlazor();
           
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddFluentValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            builder.Services.AddBlazoredToast();

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Use(next => context =>
            {
                var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
                var tokens = antiforgery.GetAndStoreTokens(context);
                return next(context);
            });
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            //app.MapFallbackToPage("/");
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

           
            app.Run();
        }
    }
}
