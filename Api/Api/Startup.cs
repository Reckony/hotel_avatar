﻿using Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<HotelAvatarContext>(options =>
                options.UseSqlServer(getConnectionString()));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public string getConnectionString()
        {
            HotelAvatarSecret secret;
            using (StreamReader r = new StreamReader("secrets.json"))
            {
                string json = r.ReadToEnd();
                secret = JsonConvert.DeserializeObject<HotelAvatarSecret>(json);
            }
            return secret.SecretConnectionString;
        }

        class HotelAvatarSecret
        {
            public string SecretConnectionString { get; set; }
        }
    }
}
