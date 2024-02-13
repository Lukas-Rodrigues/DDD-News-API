using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestutura.Configuracoes;
using Infraestutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestutura.Repositorio
{
	public class RepositorioNoticia : RepositorioGenericos<Noticia>, INoticia
	{
		private readonly DbContextOptions<Context> _OptionsBuider;
		public RepositorioNoticia()
		{
			_OptionsBuider = new DbContextOptions<Context>();	
		}
		public async Task<List<Noticia>> ListarNoticias(Expression<Func<Noticia, bool>> exNoticia)
		{
			using (var banco = new Context(_OptionsBuider))
			{
				return await banco.Noticia.Where(exNoticia).AsNoTracking().ToListAsync();	
			}
		}
	}
}
