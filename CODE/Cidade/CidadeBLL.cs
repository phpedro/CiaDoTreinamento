using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class CidadeBLL
    {
		public bool insertCidade(Cidade cidade, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CidadeDAL.insertCidade(cidade, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateCidade(Cidade cidade, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CidadeDAL.updateCidade(cidade, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteCidade(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CidadeDAL.deleteCidade(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Cidade> getCidade(int? codigo, string descricao, int? meso, int? micro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CidadeDAL.getCidade(codigo, descricao, meso, micro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cidade> getCidadeByEstado(string Estado, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CidadeDAL.getCidadeByEstado(Estado, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}
	}
}
