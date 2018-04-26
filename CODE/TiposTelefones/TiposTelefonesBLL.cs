using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TiposTelefonesBLL
    {

		public List<TiposTelefones> getTiposTelefones(out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TiposTelefonesDAL.getTiposTelefones(out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os tipos de telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
