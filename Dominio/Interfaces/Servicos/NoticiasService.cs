using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Servicos
{
	public class NoticiasService : INoticiasService
	{
		private readonly INoticia _INoticia;
		public NoticiasService(INoticia iNoticia)
		{
			_INoticia = iNoticia;
		}
		
		public async Task AdicionaNoticia(Noticia noticia)
		{
			var validarTitulo = noticia.ValidarPropriedadeString(noticia.titulo, "titulo");
			var validarInformacoes = noticia.ValidarPropriedadeString(noticia.informacao, "Informacao");

			if(validarTitulo && validarInformacoes)
			{
				noticia.DataAlteracao = DateTime.Now;
				noticia.DataCadastro = DateTime.Now;
				noticia.Ativo = true;
				await _INoticia.Adicionar(noticia);
			}


		}

		public async Task AtualizaNoticia(Noticia noticia)
		{
			var validarTitulo = noticia.ValidarPropriedadeString(noticia.titulo, "titulo");
			var validarInformacoes = noticia.ValidarPropriedadeString(noticia.informacao, "Informacao");

			if (validarTitulo && validarInformacoes)
			{
				noticia.DataAlteracao = DateTime.Now;
				noticia.DataCadastro = DateTime.Now;
				noticia.Ativo = true;
				await _INoticia.Atualizar(noticia);
			}
		}

		public async Task<List<Noticia>> ListarNoticiasAtivas()
		{
			return await _INoticia.ListarNoticias(n => n.Ativo);
		}

		public Task ListNoticia(Noticia noticia)
		{
			throw new NotImplementedException();
		}
	}
}
