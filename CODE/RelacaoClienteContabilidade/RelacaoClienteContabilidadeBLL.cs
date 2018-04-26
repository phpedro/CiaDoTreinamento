using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class RelacaoClienteContabilidadeBLL
    {

		public bool insertRelacaoClienteContabilidade(RelacaoClienteContabilidade relacao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteContabilidadeDAL.insertRelacaoClienteContabilidade(relacao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteRelacaoClienteContabilidade(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteContabilidadeDAL.deleteRelacaoClienteContabilidade(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<RelacaoClienteContabilidade> getContabilidadesByCliente(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteContabilidadeDAL.getContabilidadesByCliente(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}
	}
}
