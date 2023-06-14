using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Extensions;

namespace UKParliament.CodeTest.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            //DateOnly input to work with json - api output
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

            //create dbcontext for Inmemory database
            builder.Services.AddDbContext<PersonManagerContext>(op => op.UseInMemoryDatabase("PersonManager"));

            builder.Services.AddScoped<IPersonService, PersonService>();

            builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(new AutoMapper.MapperConfiguration(_ =>
            {
                _.CreateMap<Data.Person, ViewModels.PersonViewModel>();
            }));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //var logger = app.Services.GetRequiredService<ILogger>();
            //app.ConfigureExceptionHandler(logger);

            using (var scope = app.Services.CreateScope())
            {
                //Get the instance of PersonManagerContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PersonManagerContext>();

                //Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //custom global exception handler
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "text/html";
                        var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                        if (null != exceptionObject)
                        {
                            var errorMessage = $"{exceptionObject.Error.Message}";
                            await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                        }
                    });
                }
            );

            //redirect to another page when global exception 
            //app.UseExceptionHandler("/Error");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}