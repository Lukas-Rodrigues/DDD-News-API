using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NoticiaController : ControllerBase
	{

		private readonly IAplicacaoNoticia _IAplicacaoNoticia;

		public NoticiaController(IAplicacaoNoticia IAplicacaoNoticia)
		{
			_IAplicacaoNoticia = IAplicacaoNoticia;
		}

		//Authorize só vai poder ser acessado se ele tiver o token
		[Authorize]
		[Produces("application/json")]
		[HttpPost("/api/ListarNoticias")]
		public async Task<List<Noticia>> ListarNoticias()
		{
			return await _IAplicacaoNoticia.ListarNoticiasAtivas();
		}

		[Authorize]
		[Produces("application/json")]
		[HttpPost("/api/AdicionaNoticia")]
		public async Task<List<Notifica>> AdicionaNoticia(NoticiaModel noticia)
		{
			var noticiaNova = new Noticia();
			noticiaNova.titulo = noticia.Titulo;
			noticiaNova.informacao = noticia.Informacao;
			noticiaNova.UserId = await RetornarIdUsuarioLogado();
			await _IAplicacaoNoticia.AdicionaNoticia(noticiaNova);

			return noticiaNova.Notificacoes;

		}


		[Authorize]
		[Produces("application/json")]
		[HttpPost("/api/AtualizaNoticia")]
		public async Task<List<Notifica>> AtualizaNoticia(NoticiaModel noticia)
		{
			var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
			noticiaNova.titulo = noticia.Titulo;
			noticiaNova.informacao = noticia.Informacao;
			noticiaNova.UserId = await RetornarIdUsuarioLogado();
			await _IAplicacaoNoticia.AtualizaNoticia(noticiaNova);

			return noticiaNova.Notificacoes;

		}


		[Authorize]
		[Produces("application/json")]
		[HttpPost("/api/ExcluirNoticia")]
		public async Task<List<Notifica>> ExcluirNoticia(NoticiaModel noticia)
		{
			var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

			await _IAplicacaoNoticia.Excluir(noticiaNova);

			return noticiaNova.Notificacoes;

		}

		[Authorize]
		[Produces("application/json")]
		[HttpPost("/api/BuscarPorId")]
		public async Task<Noticia> BuscarPorId(NoticiaModel noticia)
		{
			var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);

			return noticiaNova;

		}


		private async Task<string> RetornarIdUsuarioLogado()
		{
			if (User != null)
			{
				var idusuario = User.FindFirst("idUsuario");
				return idusuario.Value;
			}
			else
			{
				return string.Empty;
			}
		}



	}
}