using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace StartPoint.GameServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // configure session management
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddJsonOptions(opts =>
                 {
                     // force camel case to json
                     opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 });

            // register db context and specifying migration assembly.
            
            services.AddDbContext<ApiDbContext>(dbOptions => dbOptions.UseSqlServer(Configuration["DefaultConnection:ConnectionString"],
                                               sqlDBOptions => sqlDBOptions.MigrationsAssembly("DAL")));

            // register external login providers
            services.AddAuthentication()
                    .AddFacebook(options =>
                    {
                        options.AppId = Configuration["FacebookAppId"]; //"395758431172092";
                        options.AppSecret = Configuration["FacebookAppSecret"]; //"6ab756291700b92070596f5c8ce9fa50";
                        options.CallbackPath = PathString.FromUriComponent("/signin-facebook");
                        options.Scope.Add("email");
                        options.Fields.Add("name");
                        options.Fields.Add("email");
                        options.SaveTokens = true;
                    })
                    .AddGoogle(options => 
                    {
                        options.ClientId = Configuration["GoogleClientId"]; //"493351261361-98bjgbc7oqi7htebbovobv046967g44b.apps.googleusercontent.com";
                        options.ClientSecret = Configuration["GoogleClientSecret"]; //"pbHY0qCua55leGfn2b5yM0oH";
                        options.CallbackPath = PathString.FromUriComponent("/signin-google");
                        options.Scope.Add("email");
                        options.SaveTokens = true;
                    })
                    .AddTwitter(options => 
                    {
                        options.ConsumerKey = Configuration["TwitterConsumerKey"]; //"98bjgbc7oqi7htebbovobv046967g44b";
                        options.ConsumerSecret = Configuration["TwitterConsumerSecret"]; //"pbHY0qCua55leGfn2b5yM0oH";
                        options.CallbackPath = PathString.FromUriComponent("/signin-twitter");
                        options.SaveTokens = true;
                    });

            
            services.Configure<CookieAuthenticationOptions>(opt =>
            {
                opt.LoginPath = new PathString("/Identity/Account/Login");
            });

            // register repositeries
            services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IIconService, IconService>();
            services.AddTransient<IGameUserService, GameUserService>();
            services.AddTransient<IQuizCategoryService, QuizCategoryService>();
            services.AddTransient<IQuizOfferService, QuizOfferService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuizHistoryService, QuizHistoryService>();
            services.AddTransient<IQuizSummaryService, QuizSummaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
