using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("<h1>Neo4J Cluster Assignment</h1><p>First run: <a href=\"/setup\">/setup<a> (a bit slow)</p><p>Then try: <a href=\"/greet\">/greet<a></p>");
                });
                endpoints.MapGet("/setup", async context =>
                {
                    string great = Setup.RunSetup();
                    await context.Response.WriteAsync(great);
                });
                endpoints.MapGet("/greet", async context =>
                {
                    await context.Response.WriteAsync(HelloWorldExample.PrintGreeting("hello, world"));
                });
            });
        }
    }
}
