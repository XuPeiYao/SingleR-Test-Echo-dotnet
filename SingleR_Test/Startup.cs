using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using SingleR_Test.Hubs;
using Swashbuckle.AspNetCore.Swagger;

namespace SingleR_Test {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors();

            services.AddMvc();

            services.AddSignalR();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new Info {
                        Title = "SingleR_Test API",
                        Version = "v1"
                    }
                 );
                foreach (var file in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml")) {
                    c.IncludeXmlComments(file);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => {
                await next();
                if ((context.Response.StatusCode == 404) &&
                    !System.IO.Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/")) {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseCors(c => {
                c.AllowAnyOrigin();
                c.AllowCredentials();
                c.AllowAnyMethod();
                c.AllowAnyHeader();
            });


            app.UseStaticFiles();

            app.UseMvc();

            app.UseSignalR(routes => {
                routes.MapHub<EchoHub>("/echo");
            });

            app.UseSwagger(c => {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
                    swaggerDoc.BasePath = "/signalr";
                });
                c.RouteTemplate = "swagger/api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("api-docs/v1/swagger.json", "Test API");
            });

        }
    }
}
