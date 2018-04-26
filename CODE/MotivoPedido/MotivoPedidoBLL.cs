using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class MotivoPedidoBLL
    {

		public List<MotivoPedido> getMotivosPedido(out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return MotivoPedidoDAL.getMotivosPedido(out mensagemErro);
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
