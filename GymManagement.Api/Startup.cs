using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GymManagement.Api.Context;
using GymManagement.Api.Config;
using GymManagement.DataModel;

namespace GymManagement.Api
{
    public class Startup
    {
        private ApiSettings ApiSettings { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new ApiSettings();
            Configuration.GetSection("ApiSettings").Bind(settings);

            var security = new SecuritySettings();
            Configuration.GetSection("SecuritySettings").Bind(security);

            if(settings.CORS != null)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.WithOrigins(settings.CORS)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });
            }

            // Add functionality to inject Ioptions<T>
            services.AddOptions();

            // Add config object so it can be injected
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.Configure<SecuritySettings>(Configuration.GetSection("SecuritySettings"));

            // Add JWT Support
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        // Add to settings so it changes with environments
                        ValidIssuer = security.Issuer,
                        ValidAudience = security.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.SecretKey))
                    };
                });

            services.AddDbContext<GymManagementDataContext>(options => options.UseSqlServer(settings.ConnectionStrings["gym_management"]));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<ApiSettings> settings)
        {
            ApiSettings = settings.Value;

            if (env.EnvironmentName != "Prod")
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
        }
    }
}
