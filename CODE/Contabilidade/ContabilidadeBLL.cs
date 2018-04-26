using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ContabilidadeBLL
    {

		public bool insertContabilidade(Contabilidade contabilidade, List<TelefoneContabilidade.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContabilidadeDAL.insertContabilidade(contabilidade, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateContabilidade(Contabilidade contabilidade, List<TelefoneContabilidade.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContabilidadeDAL.updateContabilidade(contabilidade, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteContabilidade(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContabilidadeDAL.deleteContabilidade(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Contabilidade> getContabilidades(int? codigo, string razao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContabilidadeDAL.getContabilidades(codigo, razao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
