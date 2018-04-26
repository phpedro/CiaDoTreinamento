using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class StatusOrcamentoBLL
    {

		public List<StatusOrcamento> getStatusNegociacao(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return StatusOrcamentoDAL.getStatusOrcamento(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os status de orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
