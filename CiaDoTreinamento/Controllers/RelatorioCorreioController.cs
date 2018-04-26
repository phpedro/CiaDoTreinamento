using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using CiaDoTreinamento.Models;

namespace CiaDoTreinamento.Controllers
{
    public class RelatorioCorreioController : Controller
    {
        public IActionResult List()
        {
            return View(new List<RegistroCorreio>());
        }

		public IActionResult Edit(int? codigoRegistro)
		{
			if (codigoRegistro != null && codigoRegistro > 0)
			{
				string mensagemErro;
				RegistroCorreioBLL BLL = new RegistroCorreioBLL();
				RegistroCorreioViewModel viewModel = new RegistroCorreioViewModel();

				viewModel.registroCorreio = BLL.getRegistroCorreioByCodigo((int)codigoRegistro, out mensagemErro);
				viewModel.listaEmails = BLL.getEmailsRegistroCorreio((int)codigoRegistro, out mensagemErro);

				return View(viewModel);
			}
			
			return View();
		}

		public IActionResult Consultar(string txtCnpjFiltro, string txtRazaoSocialFiltro, int? txtCodigoClienteFiltro, string txtCpfFiltro, string txtNomeClienteFiltro, 
										int? txtCodigoPedidoFiltro, DateTime? dtpDataInicialFiltro, DateTime? dtpDataFinalFiltro)
		{
			RegistroCorreioBLL BLL = new RegistroCorreioBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<RegistroCorreio> listaRegistros = BLL.getRegistrosCorreio(txtCnpjFiltro, txtRazaoSocialFiltro, txtCodigoClienteFiltro, txtCpfFiltro, txtNomeClienteFiltro,
																			txtCodigoPedidoFiltro, dtpDataInicialFiltro, dtpDataFinalFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaRegistros);
		}

		public IActionResult Delete(int? codigoRegistro)
		{
			RegistroCorreioBLL BLL = new RegistroCorreioBLL();
			string mensagemErro;

			if (codigoRegistro.HasValue)
			{
				if (BLL.deleteRegistroCorreio((int)codigoRegistro, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Registro removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(RegistroCorreio registroCorreio, int[] codigoEmail)
		{
			RegistroCorreioBLL BLL = new RegistroCorreioBLL();
			string mensagemErro;
			bool retorno = false;

			if (registroCorreio.Codigo != null && registroCorreio.Codigo > 0)
			{
				retorno = BLL.updateRegistroCorreio(registroCorreio, codigoEmail, out mensagemErro);
			}
			else
			{
				retorno = BLL.insertRegistroCorreio(registroCorreio, codigoEmail, out mensagemErro);
			}

			if (retorno)
			{
				if (!EnviarEmail(registroCorreio, out mensagemErro))
				{
					TempData["mensagemErro"] = mensagemErro;
				}

				return RedirectToAction("List");
			}
			else
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("Edit", new RegistroCorreioViewModel() { registroCorreio = registroCorreio });
			}
		}

		public bool EnviarEmail(RegistroCorreio registro, out string mensagemErro)
		{
			RegistroCorreioBLL BLL = new RegistroCorreioBLL();
			mensagemErro = "";

			string Destinatario = "";

			//Multiplos emails
			Destinatario = BLL.getDescricaoEmailsRegitroCorreio((int)registro.Codigo, out mensagemErro);

			string Assunto = "Cia do Treinamento - Informativo";
			string Mensagem = "";

			string caminhoImagemCab = "http://cpro37549.publiccloud.com.br/TesteCiaTreinamento/Images/cabEmail2.png";
			string caminhoImagemRod = "http://cpro37549.publiccloud.com.br/TesteCiaTreinamento/Images/rodEmail.png";

			Mensagem = @"<html><body>";

			Mensagem += "<div align='center'>" +
							"<img src = '" + caminhoImagemCab + "' border= '" + 0 + "' />" +
						 "</div>";

			Mensagem += "<div align='left'>" +
							"<p>Olá Cliente,</p>" +
							"<p>Razão Social: " + registro.cliente.RazaoSocial + " <br /> " +
							"CNPJ: " + registro.cliente.CNPJ + ".</p>" +
							"<p>O(s) produto(s) abaixo, referente ao pedido número " + registro.CodigoPedido + " estão a caminho. A entrega será feita em breve.<br /><br />" +
							"Produtos inclusos no pedido: <br /><br />" +
							registro.Descricao.Replace("#-#", "<br />") +
							"<br /></p> " +
						"</div>";

			if (registro.CodigoPostagem != null && registro.CodigoPostagem.Length > 0)
			{

				Mensagem += "<div align='left'>" +
								"<p>Acompanhe o envio de seus documentos pelos " +
									"<a href='http://www2.correios.com.br/sistemas/rastreamento/' target='_blank' rel='noreferrer'>Correios</a>" +
									" utilizando o seguinte código de rastreamento: " + registro.CodigoPostagem + "." +
								"</p>" +
							"</div>" +
							"<br />" +
							"<br />";

			}

			Mensagem += "<div align='left'>" +
							"Atenciosamente,<br />" +
							"<br />" +
							"<img src = '" + caminhoImagemRod + "' border= '" + 0 + "' /><br />" +
							"(34) 3253-0533<br />" +
							"<a href='http://www.ciadotreinamento.com.br' target='_blank' rel='noreferrer'>www.ciadotreinamento.com.br</a>" +
						 "</div>";

			Mensagem += @"</body></html>";

			mensagemErro = Uteis.SendMail("Cia Correio", Destinatario, Assunto, Mensagem);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return false;
			}

			return true;
		}

	}
}