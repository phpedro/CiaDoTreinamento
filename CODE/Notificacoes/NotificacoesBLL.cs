using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class NotificacoesBLL
    {
		//INSERT
		public bool insertNotificacao(Notificacoes notificacao, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return NotificacoesDAL.insertNotificacao(notificacao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public bool updateNotificacao(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return NotificacoesDAL.updateNotificacao(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Notificacoes> getNotificacoes(int? codigoUsuario, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return NotificacoesDAL.getNotificacoes(codigoUsuario, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
