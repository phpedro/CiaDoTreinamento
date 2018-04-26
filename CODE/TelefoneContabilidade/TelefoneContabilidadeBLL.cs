using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefoneContabilidadeBLL
    {

		public bool insertTelefoneContabilidade(TelefoneContabilidade telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.insertTelefoneContabilidade(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateTelefoneContabilidade(TelefoneContabilidade telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.updateTelefoneContabilidade(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteTelefoneContabilidade(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.deleteTelefoneContabilidade(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAllTelefoneContabilidade(int codigoContabilidade, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.deleteAllTelefoneContabilidade(codigoContabilidade, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<TelefoneContabilidade> getTelefonesContabilidade(int? codigoContabilidade, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.getTelefonesContabilidade(codigoContabilidade, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<TelefoneContabilidade.TelefoneTela> getTelefonesContabilidadeTela(int? codigoContabilidade, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneContabilidadeDAL.getTelefonesContabilidadeTela(codigoContabilidade, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
