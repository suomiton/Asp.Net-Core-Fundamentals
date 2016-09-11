using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace HelloWorld
{
    public class Startup
    {
        public MyOptions Options { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            //Debug
            Console.WriteLine("Greeting:" + configuration["Greeting"]);
            Console.WriteLine("FontSize:" + configuration["FontSize"]);

            Options = new MyOptions();
            configuration.Bind(Options);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/images"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images"))
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                RequestPath = new PathString("/images"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images"))
            });

            app.UseStatusCodePages();
            app.MapWhen(context => context.Request.Path == "/missingpage", builder => { });

            app.Map("/error", error => ErrorPage(error));
            app.UseTimer();
            app.Map("/base", applic => HomePage(applic, Options));
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
            services.AddSingleton<MyOptions>(Options);
            services.AddMvc();
        }

        public static void HomePage(IApplicationBuilder app, MyOptions options)
        {
            app.Run(async context =>
            {
                if (context.Request.Query.ContainsKey("throw"))
                {
                    throw new Exception("Exception triggered!");
                }
                var builder = new StringBuilder();
                builder.AppendLine("<html><body>Hello World!");
                builder.AppendLine("<ul>");
                builder.AppendLine("<li><a href=\"/?throw=true\">Throw Exception </ a ></ li > ");
                builder.AppendLine("<li><a href=\"/missingpage\">Missing Page </ a ></ li > ");
                builder.AppendLine("<li><a href=\"/images\">Images</a></li>");
                builder.AppendLine("</ul>");
                builder.AppendLine( $"<html><body><font size='{options.FontSize}'>{options.Greeting}</font size>");
                builder.AppendLine("</body></html>");
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(builder.ToString());
            });
        }

        public static void ErrorPage(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var builder = new StringBuilder();
                builder.AppendLine("<html><body>");
                builder.AppendLine("An error occurred");
                builder.AppendLine("</body></html>");
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(builder.ToString());
            });
        }
    }
}