using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Notificacoes
{
	public class Notifica
	{
		[NotMapped]
		public string NomePropriedade { get; set; }
		[NotMapped]
		public string Mensagem { get; set; }
		[NotMapped]
		public List<Notifica> Notificacoes { get; set; }



		public Notifica()
		{
			Notificacoes= new List<Notifica>();	
		}
		public bool ValidarPropriedadeString(string valor, string NomeDaPropriedade)
		{
			if (string.IsNullOrEmpty(valor) || string.IsNullOrWhiteSpace(NomeDaPropriedade))
			{
				Notificacoes.Add(new Notifica
				{
					Mensagem = "Campo Obrigatório",
					NomePropriedade= NomeDaPropriedade,
				});
				return false;
			}
			return true;
		}
		public bool ValidacaoDeDecimal(decimal valor, string NomeDaPropriedade)
		{
			if (valor <1 || string.IsNullOrWhiteSpace(NomeDaPropriedade))
			{
				Notificacoes.Add(new Notifica
				{
					Mensagem = "valor deve ser maior que 0",
					NomePropriedade = NomeDaPropriedade,
				});
				return false;
			}
			return true;
		}


	}
}
