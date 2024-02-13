using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Interfaces.Servicos;
using Entidades.Entidades;
using Infraestutura.Configuracoes;
using Infraestutura.Repositorio;
using Infraestutura.Repositorio.Genericos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Token;

namespace WebApi
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
			//configura quem pode acessar sua API
			services.AddCors();

			//Configura a conexao com o banco
			services.AddDbContext<Context>(options =>
			options.UseSqlServer(
			Configuration.GetConnectionString("DefaultConnection")));


			services.AddDefaultIdentity<AplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Context>();

			//INTERFACES E REPOSITORIOS
			services.AddSingleton(typeof(IGenericos<>), typeof(RepositorioGenericos<>));
			services.AddSingleton<INoticia, RepositorioNoticia>();
			services.AddSingleton<IUsuario, RepositorioUsuario>();

			//Servicos dominio
			services.AddSingleton<INoticiasService, NoticiasService>();

			//Interfaces aplicacao
			services.AddSingleton<IAplicacaoNoticia, AplicacaoNoticia>();
			services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();

			//validacao Token
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				   .AddJwtBearer(option =>
				   {
					   option.TokenValidationParameters = new TokenValidationParameters
					   {
						   ValidateIssuer = false,
						   ValidateAudience = false,
						   ValidateLifetime = true,
						   ValidateIssuerSigningKey = true,

						   ValidIssuer = "Teste.Securiry.Bearer",
						   ValidAudience = "Teste.Securiry.Bearer",
						   IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
					   };

					   option.Events = new JwtBearerEvents
					   {
						   OnAuthenticationFailed = context =>
						   {
							   Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
							   return Task.CompletedTask;
						   },
						   OnTokenValidated = context =>
						   {
							   Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
							   return Task.CompletedTask;
						   }
					   };
				   });


			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			//verifica os dominios com acessos. 
			var urlClient = "https://dominioDoCliente.com.br";
			app.UseCors(b => b.WithOrigins(urlClient));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
			}

			app.UseRouting();
			//token
			app.UseAuthentication();	
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
