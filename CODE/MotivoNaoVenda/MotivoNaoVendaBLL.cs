using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class MotivoNaoVendaBLL
    {
		public bool insertMotivoNaoVenda(MotivoNaoVenda motivo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return MotivoNaoVendaDAL.insertMotivoNaoVenda(motivo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o motivo. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateMotivoNaoVenda(MotivoNaoVenda motivo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return MotivoNaoVendaDAL.updateMotivoNaoVenda(motivo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o motivo. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteMotivoNaoVenda(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return MotivoNaoVendaDAL.deleteMotivoNaoVenda(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível excluir o motivo. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<MotivoNaoVenda> getMotivosNaoVenda(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return MotivoNaoVendaDAL.getMotivosNaoVenda(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o motivo. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
