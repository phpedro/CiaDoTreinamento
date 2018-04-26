using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class StatusNegociacaoBLL
    {

		public bool insertStatusNegociacao(StatusNegociacao status, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return StatusNegociacaoDAL.insertStatusNegociacao(status, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o status de negociação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateStatusNegociacao(StatusNegociacao status, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return StatusNegociacaoDAL.updateStatusNegociacao(status, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o status de negociação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteStatusNegociacao(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return StatusNegociacaoDAL.deleteStatusNegociacao(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o status de negociação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<StatusNegociacao> getStatusNegociacao(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return StatusNegociacaoDAL.getStatusNegociacao(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os status de negociação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
