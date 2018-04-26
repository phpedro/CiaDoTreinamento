using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class NotificacoesController : Controller
    {
		public JsonResult getNotificacoes()
		{
			NotificacoesBLL BLL = new NotificacoesBLL();
			string mensagemErro;
			int codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

			List<Notificacoes> listaNotificacoes = BLL.getNotificacoes(codigoUsuario, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, lista = listaNotificacoes });
			}
			else
			{
				return Json(new { sucesso = false, mensagem = mensagemErro });
			}

			
		}

		public JsonResult updateNotificacao(int codigoNotificacao)
		{
			NotificacoesBLL BLL = new NotificacoesBLL();
			string mensagemErro;

			bool retorno = BLL.updateNotificacao(codigoNotificacao, out mensagemErro);

			return Json(new { sucesso = retorno, mensagem = mensagemErro });
		}
    }
}