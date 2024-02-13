using Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
	public class AplicationUser: IdentityUser
	{
		[Column("USR_IDADE")]
		public int Idade { get; set; }

		[Column("USR_Celular")]
		public string Celular { get; set; }
		public ETipoUsuario Tipo { get; set; }

	}
}
