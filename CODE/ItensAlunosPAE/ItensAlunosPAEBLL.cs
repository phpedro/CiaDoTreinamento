using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItensAlunosPAEBLL
    {
		public bool insertItemAlunoPAE(ItensAlunoPAE item, out string mensagemErro)
		{
			try
			{
				return ItensAlunoPAEDAL.insertItemAlunoPAE(item, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				return false;
			}
		}

		public bool deleteItensAlunosPAE(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			try
			{
				return ItensAlunoPAEDAL.deleteItensAlunosPAE(codigoProduto, codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				return false;
			}
		}

		public List<ItensAlunoPAE> buscarAlunos(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItensAlunoPAEDAL.getAlunos(codigoProduto, codigoPedido, out mensagemErro);
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
