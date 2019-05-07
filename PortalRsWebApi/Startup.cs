using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PortalRSApi.Common;
using Microsoft.AspNetCore.Http;
using PortalRSApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using PortalRSApi.Data.Interfaces;
using PortalRSApi.Data.Services;
using System.Globalization;
using PortalRSApi.Helpers;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PortalRSApi
{
    public partial class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public SymmetricSecurityKey signingKey;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);


            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    d => d.UseRowNumberForPaging()
                 ));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-Br");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("de-DE") };
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Portal RS Api", Version = "v1" });

                //c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "PortalRSApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();
            app.UseRequestLocalization();
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new RequestResult
                        {
                            Sucesso = false,
                            Mensagem = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        var config = Configuration.GetSection("PortalRhConfig").Get<MyConfiguration>();

                        var toEmail = config.EmailTo;

                        var sender = new EmailSender(config);

                        var msg = error.Error.ToString();

                        if (error.Error.InnerException != null)
                        {
                            msg += "<br> InnerException <br><br>" + error.Error.ToString();
                        }

                        await sender.SendEmailAsync(toEmail, "Erro no PortalRs", msg);

                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new RequestResult
                        {
                            Sucesso = false,
                            Mensagem = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });

            app.UseCors("SiteCorsPolicy");

            app.UseStaticFiles();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal Rs");
            });
        }
    }
}