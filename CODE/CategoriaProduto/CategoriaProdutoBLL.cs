using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class CategoriaProdutoBLL
    {
		public List<CategoriaProduto> getCategorias(out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CategoriaProdutoDAL.getCategorias(out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as categorias. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
