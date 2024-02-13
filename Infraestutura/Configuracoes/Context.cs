using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestutura.Configuracoes
{
	public class Context : IdentityDbContext<AplicationUser>
	{
		public Context(DbContextOptions<Context> options):base (options)
		{

		}

		public DbSet<Noticia> Noticia { get; set; }

		public DbSet<AplicationUser> AplicationUser { get; set; }


		//metodo consegue verificar se a string de conexao está configurada
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ObserStringDeConexao());
				base.OnConfiguring(optionsBuilder);	
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<AplicationUser>().ToTable("users").HasKey(t=> t.Id);	
			base.OnModelCreating(builder);	
		}

		public string ObserStringDeConexao(){
			string stringDeConexao = "Server=localhost\\sqlexpress;initial catalog=API_DDD_2023;Integrated Security=True;Encrypt=False";
			return stringDeConexao;
		}

	}
}
