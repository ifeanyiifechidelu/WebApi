using AutoMapper;
using FirstApiSQ011.ConfigExtensions;
using FirstApiSQ011.Data;
using FirstApiSQ011.Models;
using FirstApiSQ011.Security;
using FirstApiSQ011.Services;
using FirstApiSQ011.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApiSQ011
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
            //IOC => Inversion of Control
            services.AddControllers();

            //services.AddDbContext<APIContext>();

            services.AddDbContext<APIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<APIContext>();

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<APIContext>();

            services.AddScoped<IJWTSecurity, JWTSecurity>();

            services.AddTransient<IUserService, UserService>();
            services.AddAutoMapper();

            ConfigSettings.ConfigureSwagger(services);
           
            ConfigSettings.ConfigureAuthentication(services, Configuration);
            
            //Adding global authorization
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Decadev", policy => policy.RequireRole("Decadev"));
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Cross-Origin Resource Sharing (CORS)
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "SQ011 Fast API"));
        }
    }
}
