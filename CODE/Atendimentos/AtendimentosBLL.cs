using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class AtendimentosBLL
    {

		public bool insertAtendimento(Atendimentos atendimento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AtendimentosDAL.insertAtendimento(atendimento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateAtendimento(Atendimentos atendimento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AtendimentosDAL.updateAtendimento(atendimento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAtendimento(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AtendimentosDAL.deleteAtendimento(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Atendimentos> getAtendimentos(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AtendimentosDAL.getAtendimentos(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os atendimentos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Atendimentos> getAtendimentosPedido(int? codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AtendimentosDAL.getAtendimentosPedido(codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os atendimentos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
