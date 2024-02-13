using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
	[Table("TB_NOTICIA")]
	public class Noticia: Notifica
	{
		[Column ("NTC_ID")]
		public int Id { get; set; }

		[Column("NTC_titulo")]
		[MaxLength(255)]
		public string titulo { get; set; }

		[Column("NTC_INFORMACAO")]
		[MaxLength(255)]
		public string informacao { get; set; }

		[Column("NTC_ATIVO")]
		public bool Ativo { get; set; }

		[Column("NTC_DATA_CADASTRO")]
		public DateTime DataCadastro { get; set; }


		[Column("NTC_DATA_ALTERACAO")]
		public DateTime DataAlteracao{ get; set; }

		[ForeignKey("AplicationUser")]
		[Column(Order = 1)]
		public string UserId { get; set; }
		public virtual AplicationUser AplicationUser { get; set; }
	}
}
