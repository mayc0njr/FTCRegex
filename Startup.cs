﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using FTCRegex.Models;
using FTCRegex.Utils;

namespace FTCRegex
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            Console.WriteLine($"connection: {connection}");
            // connection = "Server=localhost;DataBase=ftcRegex;Uid=ftcmanager;Pwd=8301;";
            services.AddEntityFrameworkMySql()
            .AddOptions()
            .AddDbContext<TagContext>(opt => opt.UseMySql(connection));
            // .AddDbContext<UserContext>(opt => opt.UseMySql(connection));
            // .AddDbContext<CommentContext>(opt => opt.UseMySql(connection));
            services.AddMvc();
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            var dbContext = serviceProvider.GetService<TagContext>();
            // var dbContext2 = serviceProvider.GetService<UserContext>();
            // var dbContext3 = serviceProvider.GetService<CommentContext>();
            InitializeBD.Initialize(dbContext);
            // InitializeBD.Initialize(dbContext2);
            // InitializeBD.Initialize(dbContext3);
        }
    }
}
