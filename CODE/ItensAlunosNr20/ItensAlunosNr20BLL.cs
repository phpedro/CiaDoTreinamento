using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItensAlunosNr20BLL
    {
		public bool insertItemAlunoNr20(ItensAlunosNr20 item, out string mensagemErro)
		{
			try
			{
				return ItensAlunosNr20DAL.insertItemAlunoNr20(item, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				return false;
			}
		}

		public bool deleteItensAlunosNr20(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			try
			{
				return ItensAlunosNr20DAL.deleteItensAlunosNr20(codigoProduto, codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				return false;
			}
		}

		public List<ItensAlunosNr20> buscarAlunos(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItensAlunosNr20DAL.getAlunos(codigoProduto, codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os alunos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
