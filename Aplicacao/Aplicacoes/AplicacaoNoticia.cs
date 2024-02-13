using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Interfaces.Servicos;
using Entidades.Entidades;
using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
	public class AplicacaoNoticia : IAplicacaoNoticia
	{
		INoticia _Noticia;
		INoticiasService _NoticiaService;

		public AplicacaoNoticia(INoticia noticia, INoticiasService noticiasService)
		{
			_Noticia = noticia;
			_NoticiaService = noticiasService;
		}

		public async Task AdicionaNoticia(Noticia noticia)
		{
			 await _NoticiaService.AdicionaNoticia(noticia);
		}

		public async Task Adicionar(Noticia objeto)
		{
			await _Noticia.Adicionar(objeto);	
		}

		public async Task AtualizaNoticia(Noticia noticia)
		{
			await _NoticiaService.AtualizaNoticia(noticia);
		}

		public async Task Atualizar(Noticia objeto)
		{
			await _Noticia.Atualizar(objeto);
		}

		public async Task<Noticia> BuscarPorId(int id)
		{
			return await _Noticia.BuscarPorId(id);	
		}

		public async Task Excluir(Noticia objeto)
		{
			await _Noticia.Excluir(objeto);
		}

		public async Task<List<Noticia>> Listar()
		{
			return await _Noticia.Listar();
		}

		public async Task<List<Noticia>> ListarNoticiasAtivas()
		{
			return await _NoticiaService.ListarNoticiasAtivas();
		}
	}
}
