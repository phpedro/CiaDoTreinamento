using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class RedePostoBLL
    {

		public bool insertRedePosto(RedePosto rede, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RedePostoDAL.insertRedePosto(rede, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a rede. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateRedePosto(RedePosto rede, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RedePostoDAL.updateRedePosto(rede, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a rede. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteRedePosto(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RedePostoDAL.deleteRedePosto(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a rede. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<RedePosto> getRedes(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RedePostoDAL.getRedes(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a rede. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
