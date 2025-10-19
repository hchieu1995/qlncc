using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Castle.Logging.Log4Net;
using Abp.PlugIns;
using AbpNet8.Authentication.JwtBearer;
using AbpNet8.Common;
using AbpNet8.Configuration;
using AbpNet8.EntityFrameworkCore;
using AbpNet8.Identity;
using AbpNet8.Web.Resources;
using AbpNet8.Web.Views;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AbpNet8.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            _hostingEnvironment = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Wkhtmltopdf
            //services.AddWkhtmltopdf();
            //RotativaConfiguration.Setup();
            // same site cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });
            // MVC
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
            })
#if DEBUG
                .AddRazorRuntimeCompilation()
#endif
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new DateTimeConverter());
                    options.SerializerSettings.Converters.Add(new DateTimeNullableConverter());
                });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new TenantViewLocationExpander());
            });

            IdentityRegistrar.Register(services);

            ////Identity server
            //if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            //{
            //    IdentityServerRegistrar.Register(services, _appConfiguration, options =>
            //        options.UserInteraction = new UserInteractionOptions()
            //        {
            //            LoginUrl = "/Account/Login",
            //            LogoutUrl = "/Account/LogOut",
            //            ErrorUrl = "/Error"
            //        });
            //}

            AuthConfigurer.Configure(services, _appConfiguration);

            if (WebConsts.SwaggerUiEnabled)
            {
                ConfigureSwagger(services);
                ////Swagger - Enable this line and the related lines in Configure method to enable swagger UI
                //services.AddSwaggerGen(options =>
                //{
                //    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "BnnSoft API", Version = "v1" });
                //    options.DocInclusionPredicate((docName, description) => true);
                //    options.ParameterFilter<SwaggerEnumParameterFilter>();
                //    options.SchemaFilter<SwaggerEnumSchemaFilter>();
                //    options.OperationFilter<SwaggerOperationIdFilter>();
                //    options.OperationFilter<SwaggerOperationFilter>();
                //    options.CustomDefaultSchemaIdSelector();
                //});
            }
            //ConfigureSwagger(services);
            ////Recaptcha
            //services.AddRecaptcha(new RecaptchaOptions
            //{
            //    SiteKey = _appConfiguration["Recaptcha:SiteKey"],
            //    SecretKey = _appConfiguration["Recaptcha:SecretKey"]
            //});

            //if (WebConsts.HangfireDashboardEnabled)
            //{
            //    //Hangfire (Enable to use Hangfire instead of default job manager)
            //    services.AddHangfire(config =>
            //    {
            //        config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
            //    });
            //}

            services.AddScoped<IWebResourceManager, WebResourceManager>();
            services.AddSignalR();

            //if (WebConsts.GraphQL.Enabled)
            //{
            //    services.AddAndConfigureGraphQL();
            //}

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            //if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
            //{
            //    services.AddAbpZeroHealthCheck();

            //    var healthCheckUISection = _appConfiguration.GetSection("HealthChecks")?.GetSection("HealthChecksUI");

            //    if (bool.Parse(healthCheckUISection["HealthChecksUIEnabled"]))
            //    {
            //        services.Configure<HealthChecksUISettings>(settings =>
            //        {
            //            healthCheckUISection.Bind(settings, c => c.BindNonPublicProperties = true);
            //        });
            //        services.AddHealthChecksUI();
            //    }
            //}

            //services.AddMvc(options =>
            //{
            //    options.Filters.AddService(typeof(ResultFilter));
            //});

            //Configure Abp and Dependency Injection
            return services.AddAbp<AbpNet8WebMvcModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );

                options.PlugInSources.AddFolder(Path.Combine(_hostingEnvironment.WebRootPath, "Plugins"), SearchOption.AllDirectories);
            });
        }

        private void CheckSameSite(HttpContext context, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                if (UserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = SameSiteMode.Lax;
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Initializes ABP framework.
            app.UseAbp(options =>
            {
                options.UseAbpRequestLocalization = false; //used below: UseAbpRequestLocalization
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            if (bool.Parse(_appConfiguration["Authentication:JwtBearer:IsEnabled"]))
            {
                app.UseJwtTokenMiddleware();
            }

            //if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            //{
            //    app.UseJwtTokenMiddleware("IdentityBearer");
            //    app.UseIdentityServer();
            //}

            app.UseCookiePolicy();
            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (scope.ServiceProvider.GetService<DatabaseCheckHelper>().Exist(_appConfiguration["ConnectionStrings:Default"]))
                {
                    app.UseAbpRequestLocalization(options =>
                    {
                        options.RequestCultureProviders = new List<IRequestCultureProvider>
                            {
                                new QueryStringRequestCultureProvider(),
                                new CookieRequestCultureProvider(),
                                new AbpNet8CultureProvider()
                            };
                    });
                }
            }

            //if (WebConsts.HangfireDashboardEnabled)
            //{
            //    //Hangfire dashboard & server (Enable to use Hangfire instead of default job manager)
            //    app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //    {
            //        Authorization = new[] { new AbpHangfireAuthorizationFilter() }
            //    });
            //    app.UseHangfireServer();
            //}

            //if (bool.Parse(_appConfiguration["Payment:Stripe:IsActive"]))
            //{
            //    StripeConfiguration.ApiKey = _appConfiguration["Payment:Stripe:SecretKey"];
            //}

            //if (WebConsts.GraphQL.Enabled)
            //{
            //    app.UseGraphQL<MainSchema>();
            //    if (WebConsts.GraphQL.PlaygroundEnabled)
            //    {
            //        app.UseGraphQLPlayground(
            //            new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground
            //    }
            //}

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AbpCommonHub>("/signalr");
                //endpoints.MapHub<ChatHub>("/signalr-chat");

                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                //if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
                //{
                //    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions()
                //    {
                //        Predicate = _ => true,
                //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //    });
                //}
            });

            //if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
            //{
            //    if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksUI:HealthChecksUIEnabled"]))
            //    {
            //        app.UseHealthChecksUI();
            //    }
            //}

            if (WebConsts.SwaggerUiEnabled)
            {
                app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
                // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"bnn API v1");
                }); //URL: /swagger
                // Enable middleware to serve generated Swagger as a JSON endpoint
                //app.UseSwagger();
                ////Enable middleware to serve swagger - ui assets(HTML, JS, CSS etc.)
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint(_appConfiguration["App:SwaggerEndPoint"], "BnnSoft API V1");
                //    options.IndexStream = () => Assembly.GetExecutingAssembly()
                //        .GetManifestResourceStream("AbpNet8.Web.wwwroot.swagger.ui.index.html");
                //    options.InjectBaseUrl(_appConfiguration["App:WebSiteRootAddress"]);
                //}); //URL: /swagger
            }

        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "bnn API",
                    Description = "bnn"
                });
                options.DocInclusionPredicate((docName, description) => true);
            });
        }
    }
}
