using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaOdontologica.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgendaOdontologica.Models;

namespace AgendaOdontologica
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

            services.AddDbContext<AgendaOdontologicaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Conexao")));
            //configurando o Identity
            services.AddIdentity<HomeLogin,NivelAcesso>()
                .AddEntityFrameworkStores<AgendaOdontologicaDbContext>();

            // configurando senha e login para o Identity
            services.Configure<IdentityOptions>(Opcoes =>
            {
                Opcoes.Password.RequireDigit = false;
                Opcoes.Password.RequireLowercase = false;
                Opcoes.Password.RequireNonAlphanumeric = false;
                Opcoes.Password.RequireUppercase = false;
                Opcoes.Password.RequiredLength = 6;
                Opcoes.Password.RequiredUniqueChars = 1;

            });

            // injeção de Dependência
            services.AddScoped<UserManager<HomeLogin>, UserManager<HomeLogin>>();
            services.AddScoped<SignInManager<HomeLogin>, SignInManager<HomeLogin>>();
            services.AddScoped<RoleManager<NivelAcesso>, RoleManager<NivelAcesso>>();


            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = false;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
