using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class RelacaoClienteLicenciamentoAmbientalBLL
    {

		public bool insertRelacaoClienteLicenciamento(RelacaoClienteLicenciamentoAmbiental relacao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteLicenciamentoAmbientalDAL.insertRelacaoClienteLicenciamento(relacao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteRelacaoClienteLicenciamento(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteLicenciamentoAmbientalDAL.deleteRelacaoClienteLicenciamento(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<RelacaoClienteLicenciamentoAmbiental> getLicenciamentosByCliente(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteLicenciamentoAmbientalDAL.getLicenciamentosByCliente(codigoCliente, out mensagemErro);
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
