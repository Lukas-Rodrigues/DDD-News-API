using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestutura.Configuracoes;
using Infraestutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infraestutura.Repositorio
{
	public class RepositorioUsuario : RepositorioGenericos<AplicationUser>, IUsuario
	{
		private readonly DbContextOptions<Context> _OptionsBuider;
		public RepositorioUsuario()
		{
			_OptionsBuider = new DbContextOptions<Context>();
		}


		public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
		{
			try
			{
				using (var data = new Context(_OptionsBuider))
				{
					await data.AplicationUser.AddAsync(
					   new AplicationUser
					   {
						   Email = email,
						   Celular = celular,
						   Idade = idade,
						   PasswordHash = senha,
						   Tipo = ETipoUsuario.Comum
					   }
					   );
					await data.SaveChangesAsync();	
				}

			}
			catch (Exception)
			{

				return false;
			}
			return true;
		}

		public async Task<bool> ExisteUsuario(string email, string senha)
		{
			try
			{
				using (var data = new Context(_OptionsBuider))
				{
					await data.AplicationUser.
						Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(senha))
						.AsNoTracking()
						.AnyAsync();
					   
					
				}

			}
			catch (Exception)
			{

				return false;
			}
			return true;
		}

		public async Task<string> RetornaIdUsario(string email)
		{
			try
			{
				using (var data = new Context(_OptionsBuider))
				{
					var usuario = await data.AplicationUser.
						Where(u => u.Email.Equals(email))
						.AsNoTracking()
						.FirstOrDefaultAsync();
					return usuario.Id;
				}

			}
			catch (Exception)
			{

				return string.Empty;
			}

		}
	}
}
