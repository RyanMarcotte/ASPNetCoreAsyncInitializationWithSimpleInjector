﻿using System;
using System.Collections.Generic;
using System.Linq;
using AISIDemo.EntityFramework.Infrastructure;
using AsyncInitializationWithSimpleInjectorDemo.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class Startup
	{
		private readonly Container _container = new Container();
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = new ConfigurationBuilder()
				.AddConfiguration(configuration ?? throw new ArgumentNullException(nameof(configuration)))
				.AddUserSecrets<Startup>()
				.Build();
		}

		/// <summary>
		/// This method gets called by the runtime as part of <see cref="IWebHostBuilder.Build"/>.
		/// No application components get registered as part of this step...  only infrastructure components.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.AddDbContext<SchoolContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
			services.AddSimpleInjector(_container, options => options.AddAspNetCore().AddControllerActivation());
		}

		/// <summary>
		/// This method gets called by the runtime as part of <see cref="IWebHost.Start"/> / <see cref="IWebHost.StartAsync(System.Threading.CancellationToken)"/>.
		/// Application components are registered as part of this step.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSimpleInjectorWithApplicationComponents(_container);
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(c => c.MapControllers());
		}
	}
}
