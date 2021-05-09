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
                    await context.Response.WriteAsync("<h1>Neo4J Cluster Assignment</h1><p>First run: <a href=\"/setup\">/setup<a> (a bit slow)</p><p>Then try: <a href=\"/greet\">/greet<a></p>"+
                        "<p>Query1: <a href=\"/test\">/test<a></p>"+
                        "<p>Query2: <a href=\"/human\">/human<a></p>"+
                        "<p>Query3: <a href=\"/payment\">/payment<a></p>"+
                        "<p>Query4: <a href=\"/product\">/product<a></p>"+
                        "<p>Query5: <a href=\"/release\">/release<a></p>"+
                        "<p>Query6: <a href=\"/description\">/description<a></p>"+
                        "<p>Query7: <a href=\"/uat\">/uat<a></p>"+
                        "<p>Query8: <a href=\"/env\">/env<a></p>"+
                        "<p>Query9: <a href=\"/sftdev\">/sftdev<a></p>");
                });
                endpoints.MapGet("/setup", async context =>
                {
                    string great = Setup.RunSetup();
                    await context.Response.WriteAsync(great);
                });
                endpoints.MapGet("/greet", async context =>
                {
                    await context.Response.WriteAsync(HelloWorldExample.PrintGreeting("hello, world").Aggregate((a, b) => $"{a}\n{b}"));
                });
                endpoints.MapGet("/test", async context =>
                {
                    await context.Response.WriteAsync(Test.Run());
                });
                endpoints.MapGet("/human", async context =>
                {
                    await context.Response.WriteAsync(Human.Run());
                });
                endpoints.MapGet("/payment", async context =>
                {
                    await context.Response.WriteAsync(Payment.Run());
                });
                endpoints.MapGet("/product", async context =>
                {
                    await context.Response.WriteAsync(Payment.Run());
                });
                endpoints.MapGet("/release", async context =>
                {
                    await context.Response.WriteAsync(Release.Run());
                });
                endpoints.MapGet("/description", async context =>
                {
                    await context.Response.WriteAsync(Description.Run());
                });
                endpoints.MapGet("/uat", async context =>
                {
                    await context.Response.WriteAsync(UAT.Run());
                });
                endpoints.MapGet("/env", async context =>
                {
                    await context.Response.WriteAsync(Env.Run());
                });
                endpoints.MapGet("/sftdev", async context =>
                {
                    await context.Response.WriteAsync(SftDev.Run());
                });
            });
        }
    }
}
