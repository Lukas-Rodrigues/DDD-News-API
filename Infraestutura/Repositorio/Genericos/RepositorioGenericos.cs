using Dominio.Interfaces.Genericos;
using Infraestutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestutura.Repositorio.Genericos
{
	public class RepositorioGenericos<T> : IGenericos<T>, IDisposable where T : class
	{
		private bool disposedValue;
		private readonly DbContextOptions<Context> _OptionsBuider;
		public RepositorioGenericos()
		{
			_OptionsBuider  = new DbContextOptions<Context>();	
		}
		public async Task Adicionar(T objeto)
		{
			using (var data = new Context(_OptionsBuider))
			{
				await data.Set<T>().AddAsync(objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task Atualizar(T objeto)
		{
			using (var data = new Context(_OptionsBuider))
			{
				 data.Set<T>().Update(objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task<T> BuscarPorId(int id)
		{
			using (var data = new Context(_OptionsBuider))
			{
				
				return  await data.Set<T>().FindAsync(id);
			}
		}

		public async Task Excluir(T objeto)
		{
			using (var data = new Context(_OptionsBuider))
			{
				data.Set<T>().Remove(objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task<List<T>> Listar()
		{
			using (var data = new Context(_OptionsBuider))
			{
				//AsNotrcking evita que puxe toda as configurações do entity
				return await data.Set<T>().AsNoTracking().ToListAsync();
			}
		}




		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~RepositorioGenericos()
		// {
		//     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
	
}
