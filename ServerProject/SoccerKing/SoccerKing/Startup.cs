using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SoccerKing.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog.Extensions.Logging;

namespace SoccerKing
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
			services.AddDbContext<soccerkingContext>(opt => opt.UseMySql("Server=localhost;User Id=root;Password=haitao;Database=soccerking"));
			
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions( 
				options =>
				{
					if (options == null)
					{
						throw new ArgumentNullException(nameof(options));
					}

					options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				}) ;
			//配置跨域处理，允许所有来源：
			services.AddCors(options =>
			{
				options.AddPolicy("corsAll", builder =>
				{
					builder.WithOrigins("http://localhost:7457").AllowAnyMethod().AllowAnyHeader();
				});
			}
			);

			services.Configure<WxConfig>(Configuration.GetSection("WxConfig"));

			services.AddTransient<WxConfigurtaionServices>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Jwt";
				options.DefaultChallengeScheme = "Jwt";
			}).AddJwtBearer("Jwt", options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					//ValidAudience = "the audience you want to validate",
					ValidateIssuer = false,
					//ValidIssuer = "the isser you want to validate",

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BellaMyDaughterBabaLoveYou")),

					ValidateLifetime = true, //validate the expiration and not before values in the token

					ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
				};
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
			app.UseAuthentication();
			app.UseCors("corsAll");//必须位于UserMvc之前 
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseHttpsRedirection();			
			app.UseMvc();

			/*使用NLog*/
			loggerFactory.AddNLog();
			env.ConfigureNLog("NLog.config");
		}
	}
}
