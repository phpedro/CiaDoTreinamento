using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;
using DinkToPdf;
using System.Runtime.Loader;
using System.Reflection;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;

namespace CiaDoTreinamento.Controllers
{
    public class CabecalhoOrcamentoController : Controller
    {

		#region Atributos e propriedades

		private readonly IHostingEnvironment _hostingEnvironment;

		public string DiretorioArquivosGerados { get; set; }

		#endregion

		#region Construtores

		public CabecalhoOrcamentoController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region Eventos

		public IActionResult NovoOrcamento(int CodigoCliente)
        {
			ClienteBLL clienteBLL = new ClienteBLL();
			CabecalhoOrcamento cabecalhoOrcamento = new CabecalhoOrcamento();

			cabecalhoOrcamento.Cliente = new Cliente(CodigoCliente);
			cabecalhoOrcamento.FuncionarioVendedor = new Funcionario(Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]));
			cabecalhoOrcamento.CondicaoPagamento = new CondicaoPagamento() { Codigo = 1 };
			cabecalhoOrcamento.StatusOrcamento = new StatusOrcamento() { Codigo = 1 };

            return View(cabecalhoOrcamento);
        }

		public IActionResult Edit(int codigoOrcamento)
		{
			string mensagemErro;

			CabecalhoOrcamentoBLL cabecalhoOrcamentoBLL = new CabecalhoOrcamentoBLL();
			CabecalhoOrcamento cabecalhoOrcamento = cabecalhoOrcamentoBLL.GetCabecalhoOrcamento(codigoOrcamento, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return View("NovoOrcamento", cabecalhoOrcamento);
			}
		}

		[HttpPost]
		public JsonResult GravarOrcamento(int CodigoOrcamento, int CodigoCliente, int CodigoFuncionario, int CodigoCondicao, int CodigoStatus, int ValidadeOrcamento, string TelefoneContato, string itensJson)
		{
			CabecalhoOrcamentoBLL cabecalhoOrcamentoBLL = new CabecalhoOrcamentoBLL();
			ItemOrcamentoBLL itemOrcamentoBLL = new ItemOrcamentoBLL();
			CondicaoPagamentoBLL condicaoPagamentoBLL = new CondicaoPagamentoBLL();
			string mensagemErro;
			decimal valorTotalOrcamento = 0;
			int numeroParcelas = 0;

			try
			{
				CabecalhoOrcamento cabecalhoOrcamento = new CabecalhoOrcamento();
				cabecalhoOrcamento.Cliente = new Cliente(CodigoCliente);
				cabecalhoOrcamento.FuncionarioVendedor = new Funcionario(CodigoFuncionario);
				cabecalhoOrcamento.CondicaoPagamento = condicaoPagamentoBLL.getCondicoes(CodigoCondicao, null, out mensagemErro).First();
				cabecalhoOrcamento.StatusOrcamento = new StatusOrcamento() { Codigo = CodigoStatus };
				cabecalhoOrcamento.ValidadeOrcamento = ValidadeOrcamento;
				cabecalhoOrcamento.TelefoneContato = TelefoneContato;
				cabecalhoOrcamento.DataExpiracao = DateTime.Now.AddDays(ValidadeOrcamento);

				List<ItemOrcamento.ItemOrcamentoTela> itens = JsonConvert.DeserializeObject<List<ItemOrcamento.ItemOrcamentoTela>>(itensJson);

				//Calcula total dos itens
				foreach (ItemOrcamento.ItemOrcamentoTela item in itens)
				{
					valorTotalOrcamento += item.Subtotal;
				}

				numeroParcelas = cabecalhoOrcamento.CondicaoPagamento.Descricao.Split('/').Count();
				cabecalhoOrcamento.ValorOrcamento = valorTotalOrcamento + numeroParcelas * (decimal)3.00;

				if (CodigoOrcamento > 0)
				{
					//UPDATE ORÇAMENTO
					cabecalhoOrcamento.Codigo = CodigoOrcamento;

					if (!cabecalhoOrcamentoBLL.GetUpdateCabecalhoOrcamento(cabecalhoOrcamento, out mensagemErro))
					{
						return Json(new { sucesso = false, mensagemErro = mensagemErro });
					}
					else
					{
						if (!itemOrcamentoBLL.GetDeleteItemOrcamento((int)cabecalhoOrcamento.Codigo, out mensagemErro))
						{
							return Json(new { sucesso = false, mensagemErro = mensagemErro });
						}

						foreach (ItemOrcamento.ItemOrcamentoTela item in itens)
						{
							ItemOrcamento itemOrcamento = new ItemOrcamento();

							itemOrcamento.cabecalhoOrcamento = cabecalhoOrcamento;
							itemOrcamento.produto = new Produto(item.produto_Codigo);
							itemOrcamento.quantidade = item.Quantidade;
							itemOrcamento.percentualDesconto = item.PercentualDesconto;
							itemOrcamento.subtotal = item.Subtotal;
							itemOrcamento.acrescimo = item.Acrescimo;

							if (!itemOrcamentoBLL.GetInsertItemOrcamento(itemOrcamento, out mensagemErro))
							{
								return Json(new { sucesso = false, mensagemErro = mensagemErro });
							}
						}
					}

					return Json(new { sucesso = true, CodigoOrcamento = cabecalhoOrcamento.Codigo });
				}
				else
				{
					//INSERT ORÇAMENTO
					cabecalhoOrcamento.DataCriacao = DateTime.Now;
					if (!cabecalhoOrcamentoBLL.GetInsertCabecalhoOrcamento(cabecalhoOrcamento, out mensagemErro))
					{
						return Json(new { sucesso = false, mensagemErro = mensagemErro });
					}
					else
					{
						foreach (ItemOrcamento.ItemOrcamentoTela item in itens)
						{
							ItemOrcamento itemOrcamento = new ItemOrcamento();

							itemOrcamento.cabecalhoOrcamento = cabecalhoOrcamento;
							itemOrcamento.produto = new Produto(item.produto_Codigo);
							itemOrcamento.quantidade = item.Quantidade;
							itemOrcamento.percentualDesconto = item.PercentualDesconto;
							itemOrcamento.subtotal = item.Subtotal;
							itemOrcamento.acrescimo = item.Acrescimo;

							if (!itemOrcamentoBLL.GetInsertItemOrcamento(itemOrcamento, out mensagemErro))
							{
								return Json(new { sucesso = false, mensagemErro = mensagemErro });
							}
						}
					}

					return Json(new { sucesso = true, CodigoOrcamento = cabecalhoOrcamento.Codigo });

				}
			}
			catch (Exception ex)
			{
				return Json(new { sucesso = false, mensagemErro = ex.Message });
			}
		}

		[HttpGet]
		[DeleteFile]
		public FileResult GerarPDF(int codigoOrcamento = 1597, bool removerColunaDesconto = false)
		{
			try
			{
				CabecalhoOrcamentoBLL cabecalhoOrcamentoBLL = new CabecalhoOrcamentoBLL();
				string mensagemErro = "";

				CabecalhoOrcamento cab = cabecalhoOrcamentoBLL.GetCabecalhoOrcamento(codigoOrcamento, out mensagemErro);

				if (cab == null)
				{
					TempData["mensagemErro"] = mensagemErro;
					return null;
				}

				DiretorioArquivosGerados = Path.Combine(_hostingEnvironment.WebRootPath + "/Arquivos_Gerados", Guid.NewGuid().ToString());

				if (Directory.Exists(DiretorioArquivosGerados))
				{
					Directory.Delete(DiretorioArquivosGerados, true);
				}

				Directory.CreateDirectory(DiretorioArquivosGerados);

				string caminhoArquivoSaida = Path.Combine(DiretorioArquivosGerados, "orcamento.pdf");

				if (!cabecalhoOrcamentoBLL.GerarPdfOrcamento(codigoOrcamento, removerColunaDesconto, caminhoArquivoSaida, _hostingEnvironment.WebRootPath, out mensagemErro))
				{
					TempData["mensagemErro"] = mensagemErro;
					return null;
				}
				
				return PhysicalFile(caminhoArquivoSaida, "application/pdf", "Orcamento_" + cab.Codigo.ToString() + ".pdf");

			}
			catch (Exception e)
			{
				TempData["mensagemErro"] = e.Message;
				return null;
			}
		}

		#endregion

		#region Classes Aninhadas

		public class DeleteFileAttribute : ActionFilterAttribute
		{
			public override void OnResultExecuted(ResultExecutedContext filterContext)
			{
				if (((CabecalhoOrcamentoController)filterContext.Controller).DiretorioArquivosGerados != null)
				{
					Directory.Delete(((CabecalhoOrcamentoController)filterContext.Controller).DiretorioArquivosGerados, true);
				}
			}
		}

		#endregion
	}
}