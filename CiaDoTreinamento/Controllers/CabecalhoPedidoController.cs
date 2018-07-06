using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using CiaDoTreinamento.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CiaDoTreinamento.Controllers
{
	public class CabecalhoPedidoController : Controller
	{
		#region Atributos e propriedades

		private readonly IHostingEnvironment _hostingEnvironment;

		#endregion

		#region Construtores

		public CabecalhoPedidoController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region Events

		[HttpGet]
		public IActionResult NovoPedido(int? codigoCliente, int? codigoPedido)
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			string mensagemErro;
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			ClienteBLL clienteBLL = new ClienteBLL();

			if (codigoPedido.HasValue && codigoPedido != 0)
			{
				CabecalhoPedido cabecalho = cabecalhoPedidoBLL.GetPedidoByCodigo((int)codigoPedido, out mensagemErro);
				cabecalho.Cliente = clienteBLL.GetClientes((int)cabecalho.Cliente.Codigo, out mensagemErro).First();

				return View(cabecalho);
			}
			else
			{
				CabecalhoPedido cabecalho = new CabecalhoPedido((int)codigoCliente, Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"].ToString()));

				if (!cabecalhoPedidoBLL.insertCabecalhoPedido(cabecalho, out mensagemErro))
				{
					TempData["mensagemErro"] = mensagemErro;

					return RedirectToAction("Index", "Home");
				}

				cabecalho.Cliente = clienteBLL.GetClientes((int)cabecalho.Cliente.Codigo, out mensagemErro).First();

				return View(cabecalho);
			}

		}

		[HttpGet]
		public IActionResult NovoPedido2(int? codigoCliente, int? codigoPedido)
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			string mensagemErro;
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			ClienteBLL clienteBLL = new ClienteBLL();

			if (codigoPedido.HasValue && codigoPedido != 0)
			{
				CabecalhoPedido cabecalho = cabecalhoPedidoBLL.GetPedidoByCodigo((int)codigoPedido, out mensagemErro);
				cabecalho.Cliente = clienteBLL.GetClientes((int)cabecalho.Cliente.Codigo, out mensagemErro).First();

				return View(cabecalho);
			}
			else
			{
				CabecalhoPedido cabecalho = new CabecalhoPedido((int)codigoCliente, Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"].ToString()));

				if (!cabecalhoPedidoBLL.insertCabecalhoPedido(cabecalho, out mensagemErro))
				{
					TempData["mensagemErro"] = mensagemErro;

					return RedirectToAction("Index", "Home");
				}

				cabecalho.Cliente = clienteBLL.GetClientes((int)cabecalho.Cliente.Codigo, out mensagemErro).First();

				return View(cabecalho);
			}

		}

		#endregion

		#region Services

		[HttpGet]
		public JsonResult BuscarAlunosItemPedido(int codigoPedido, int codigoProduto)
		{

			Produto produto = new Produto(codigoProduto);

			if (produto.TemCURSO)
			{
				ItensAlunosNr20BLL itensAlunosNr20BLL = new ItensAlunosNr20BLL();
				string mensagemErro;

				List<ItensAlunosNr20> listaItens = itensAlunosNr20BLL.buscarAlunos(codigoProduto, codigoPedido, out mensagemErro);

				if (listaItens != null)
				{
					return Json(new { sucesso = true, listaAlunos = listaItens });
				}
				else
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}
			else if (produto.TemPAE)
			{
				ItensAlunosPAEBLL BLL = new ItensAlunosPAEBLL();
				string mensagemErro;

				List<ItensAlunoPAE> listaItens = BLL.buscarAlunos(codigoProduto, codigoPedido, out mensagemErro);

				if (listaItens != null)
				{
					return Json(new { sucesso = true, listaAlunos = listaItens });
				}
				else
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}

			return Json(new { sucesso = true });

		}

		[HttpGet]
		public JsonResult BuscarProdutoPedido(int codigoPedido, int codigoProduto)
		{
			ItemPedidoBLL BLL = new ItemPedidoBLL();
			string mensagemErro;

			ItemPedido produto = BLL.getItemPedido(codigoProduto, codigoPedido, out mensagemErro).FirstOrDefault();

			if (produto != null)
			{
				return Json(new { sucesso = true, itemPedido = produto });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpGet]
		public JsonResult BuscarProdutosPedido(int codigoPedido)
		{
			CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
			string mensagemErro;

			string produtos = BLL.GetProdutosVendidosResumido(codigoPedido, out mensagemErro);

			if (!String.IsNullOrEmpty(produtos))
			{
				return Json(new { sucesso = true, listaProdutos = produtos });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult FecharPedido(int codigoPedido, int codigoVendedor, int? codigoHotel, int? codigoSalaTreinamento, bool enviarPorCorreio, int codigoConta)
		{
			string mensagemErro;
			CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
			CabecalhoPedido cabecalhoPedido = BLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

			//VALIDA SE TODOS OS ITENS FORAM CONFIRMADOS
			List<ItemPedido> listaItens = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);

			if (listaItens.Count() == 0)
			{
				return Json(new { sucesso = false, mensagemErro = "O pedido não possui itens!" });
			}

			if (listaItens.Where(x => x.Confirmado == false).Count() > 0)
			{
				return Json(new { sucesso = false, mensagemErro = "Existem itens que não foram confirmados no pedido!" });
			}

			cabecalhoPedido.FuncionarioVendedor = new Funcionario(codigoVendedor);
			if (codigoHotel != null)
			{
				cabecalhoPedido.ParceiroHotel = new Parceiro() { Codigo = codigoHotel };
			}
			if (codigoSalaTreinamento != null)
			{
				cabecalhoPedido.ParceiraSalaTreinamento = new Parceiro() { Codigo = codigoSalaTreinamento };
			}
			cabecalhoPedido.EnviarPorCorreio = enviarPorCorreio;
			cabecalhoPedido.ContaBancaria = new ContaBancaria() { Codigo = codigoConta };
			cabecalhoPedido.StatusNegociacao.CodigoStatus = 8;
			cabecalhoPedido.DataFechamento = DateTime.Now;

			if (!BLL.updateCabecalhoPedido(cabecalhoPedido, out mensagemErro))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult InserirAlunosPedido(int codigoPedido, int codigoProduto, List<int> listaAlunos)
		{
			string mensagemErro;

			Produto produto = new Produto(codigoProduto);

			if (produto.TemCURSO)
			{
				ItensAlunosNr20BLL itensAlunosNr20BLL = new ItensAlunosNr20BLL();
				itensAlunosNr20BLL.deleteItensAlunosNr20(codigoProduto, codigoPedido, out mensagemErro);

				foreach (int item in listaAlunos)
				{
					ItensAlunosNr20 itensAlunosNr20 = new ItensAlunosNr20();

					itensAlunosNr20.codigoPedido = codigoPedido;
					itensAlunosNr20.codigoProduto = codigoProduto;
					itensAlunosNr20.aluno = new Aluno()
					{
						Codigo = item
					};

					itensAlunosNr20BLL.insertItemAlunoNr20(itensAlunosNr20, out mensagemErro);
				}

			}
			else if (produto.TemPAE)
			{
				ItensAlunosPAEBLL itensAlunosPAEBLL = new ItensAlunosPAEBLL();
				itensAlunosPAEBLL.deleteItensAlunosPAE(codigoProduto, codigoPedido, out mensagemErro);
				var i = 1;

				foreach (int item in listaAlunos)
				{
					ItensAlunoPAE itensAlunosPAE = new ItensAlunoPAE();

					itensAlunosPAE.codigoPedido = codigoPedido;
					itensAlunosPAE.codigoProduto = codigoProduto;
					itensAlunosPAE.sequencia = i++;
					itensAlunosPAE.aluno = new Aluno()
					{
						Codigo = item
					};

					itensAlunosPAEBLL.insertItemAlunoPAE(itensAlunosPAE, out mensagemErro);
				}
			}

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult InserirItemResumido(int codigoPedido, int codigoProduto, int quantidade, decimal valorVenda, bool cobrarEncargos, int? codigoMotivo)
		{
			string mensagemErro;
			ProdutoBLL produtoBLL = new ProdutoBLL();
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();

			//Buscar o produto
			Produto produto = produtoBLL.GetProdutoById(codigoProduto, out mensagemErro);

			//CRIAR ITEM VENDIDO
			ItemPedido itemPedido = new ItemPedido();

			itemPedido.Produto.Codigo = codigoProduto;
			itemPedido.CodigoPedido = codigoPedido;
			itemPedido.Quantidade = quantidade;

			//ATRIBUIR DATAS
			itemPedido.DataInicioVigencia = DateTime.Now.AddDays(7);
			itemPedido.DataExpiracao = DateTime.Now.AddDays(7).AddMonths(produto.MesesVigencia);

			//CALCULAR OS ENCARGOS
			decimal valorEncargo = 0;
			if (cobrarEncargos)
			{
				valorEncargo = valorVenda * (produto.PercentualIIS / 100);
			}
			itemPedido.ValorEncargos = quantidade * valorEncargo;

			//CALCULAR VALOR DE DESCONTO
			itemPedido.ValorDesconto = (produto.ValorPorPessoa - valorVenda > 0 ? produto.ValorPorPessoa - valorVenda : 0);

			//CALCULAR VALORES TOTAIS
			itemPedido.valorFinal = valorVenda;
			itemPedido.Subtotal = (valorVenda * quantidade) + itemPedido.ValorEncargos;

			//DADOS GERAIS
			itemPedido.Confirmado = false;
			itemPedido.CodigoMotivoPedido = (codigoMotivo.HasValue && codigoMotivo > 0 ? (int)codigoMotivo : 1);
			itemPedido.ValorDesconto = 0;

			//VERIFICAR SE O PRODUTO JÁ FOI VENDIDO
			List<ItemPedido> retorno = itemPedidoBLL.getItemPedido(codigoProduto, codigoPedido, out mensagemErro);

			if (retorno == null || retorno.Count == 0)
			{
				//INSERIR O ITEM
				if (itemPedidoBLL.insertItemPedido(itemPedido, out mensagemErro))
				{
					return Json(new { sucesso = true, item = itemPedido });
				}
				else
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}

			}
			else
			{
				//ATUALIZA O ITEM
				if (itemPedidoBLL.updateItemPedido(itemPedido, out mensagemErro))
				{
					return Json(new { sucesso = true, item = itemPedido });
				}
				else
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}
		}

		[HttpPost]
		public JsonResult RemoverItemVendido(int codigoPedido, int codigoProduto)
		{
			string mensagemErro;
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();

			if (itemPedidoBLL.deleteItemPedido(codigoProduto, codigoPedido, out mensagemErro))
			{
				return Json(new { sucesso = true });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult UpdateCobrarBoletos(int codigoPedido, int codigoCondicao, bool cobrarBoletos)
		{
			CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
			CondicaoPagamentoBLL condicaoBLL = new CondicaoPagamentoBLL();
			string mensagemErro;

			CabecalhoPedido cabecalho = BLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				cabecalho.CobrarBoletos = cobrarBoletos;
				cabecalho.CondicaoPagamento.Codigo = codigoCondicao;

				if (cobrarBoletos)
				{
					//Atualiza valores para boletos
					CondicaoPagamento condicao = condicaoBLL.getCondicoes(cabecalho.CondicaoPagamento.Codigo, "", out mensagemErro).FirstOrDefault();

					int nParcelas = condicao.Descricao.Split('/').Length;

					cabecalho.ValorBoletos = Convert.ToDecimal(nParcelas * 3.00);
				}
				else
				{
					cabecalho.ValorBoletos = 0;
				}

				if (BLL.updateCabecalhoPedido(cabecalho, out mensagemErro))
				{
					return Json(new { sucesso = true, valorBoletos = cabecalho.ValorBoletos, codigoCondicao = cabecalho.CondicaoPagamento.Codigo });
				}
			}

			return Json(new { sucesso = false, mensagemErro = mensagemErro });
		}

		[HttpPost]
		public JsonResult UpdateConfirmarItem(int codigoPedido, int codigoProduto)
		{
			ItemPedidoBLL BLL = new ItemPedidoBLL();
			string mensagemErro;

			ItemPedido itemPedido = BLL.getItemPedido(codigoProduto, codigoPedido, out mensagemErro).FirstOrDefault();

			if (itemPedido != null)
			{
				itemPedido.Confirmado = true;

				if (!BLL.updateItemPedido(itemPedido, out mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult ValidaPermissaoUsuario(string usuario, string senha)
		{
			string mensagemErro;

			Funcionario funcionario = FuncionarioBLL.getFuncionario(usuario, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				if (funcionario.Perfil.Codigo != 1 && funcionario.Senha == Uteis.GeraHashMD5(senha))
				{
					return Json(new { sucesso = true, autorizado = true });
				}
				else
				{
					return Json(new { sucesso = true, autorizado = false });
				}
			}

			return Json(new { sucesso = false, mensagemErro = mensagemErro });
		}

		[HttpPost]
		public JsonResult UpdateCobrarEncargos(int codigoPedido, bool cobrarEncargos)
		{
			CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
			string mensagemErro;

			if (BLL.updateEncargosItensPedidos(codigoPedido, cobrarEncargos, out mensagemErro))
			{

				CabecalhoPedido cabecalho = BLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

				return Json(new { sucesso = true, ValorTotal = cabecalho.ValorTotal.ToString().Replace(".", ",") });
			}

			return Json(new { sucesso = false, mensagemErro = mensagemErro });
		}

		[HttpPost]
		public ActionResult UpdatePartialViewItensPedido(int codigoPedido)
		{
			return PartialView("CabecalhoPedido/PartialConfirmarPedido", codigoPedido);
		}

		[HttpPost]
		public JsonResult UpdateDescontoCabecalhoPedidoPercentual(int codigoPedido, decimal percentualDesconto, bool cobrarEncargos)
		{
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			string mensagemErro;
			List<ItemPedido> itens = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				foreach (ItemPedido item in itens)
				{
					decimal valorDescontoItem = Decimal.Multiply(item.Produto.ValorPorPessoa, (percentualDesconto / 100));

					item.ValorDesconto = valorDescontoItem * item.Quantidade;
					item.valorFinal = item.Produto.ValorPorPessoa - valorDescontoItem;
					item.Subtotal = item.Quantidade * item.valorFinal;

					itemPedidoBLL.updateItemPedido(item, out mensagemErro);

				}

				this.UpdateCobrarEncargos(codigoPedido, cobrarEncargos);

				CabecalhoPedido cabecalho = cabecalhoPedidoBLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

				return Json(new { sucesso = true, valorTotal = cabecalho.ValorTotal, percentualDesconto = cabecalho.PercentualDesconto, valorDesconto = cabecalho.ValorDesconto });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult UpdateDescontoCabecalhoPedidoValor(int codigoPedido, decimal valorDesconto, bool cobrarEncargos)
		{
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			string mensagemErro;
			List<ItemPedido> itens = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{

				//CALCULAR O PERCENTUAL DE DESCONTO APLICADO
				decimal valorTotalPedido = itens.Sum(x => x.Produto.ValorPorPessoa * x.Quantidade);

				decimal percentualDesconto = (valorDesconto / valorTotalPedido) * 100;

				foreach (ItemPedido item in itens)
				{

					decimal valorDescontoItem = Decimal.Multiply(item.Produto.ValorPorPessoa, (percentualDesconto / 100));

					item.ValorDesconto = valorDescontoItem * item.Quantidade;
					item.valorFinal = item.Produto.ValorPorPessoa - valorDescontoItem;
					item.Subtotal = item.Quantidade * item.valorFinal;

					itemPedidoBLL.updateItemPedido(item, out mensagemErro);

				}

				this.UpdateCobrarEncargos(codigoPedido, cobrarEncargos);

				CabecalhoPedido cabecalho = cabecalhoPedidoBLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

				return Json(new { sucesso = true, valorTotal = cabecalho.ValorTotal, percentualDesconto = cabecalho.PercentualDesconto, valorDesconto = cabecalho.ValorDesconto });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpGet]
		public FileResult DownloadImagem(int id)
		{
			var nomeImagem = id + ".png";

			string webRootPath = _hostingEnvironment.WebRootPath;

			if (!String.IsNullOrEmpty(nomeImagem) && System.IO.File.Exists(webRootPath + "/ImagensProdutos/" + nomeImagem))
			{
				return base.File(Path.Combine(webRootPath, "/ImagensProdutos/" + nomeImagem), "image/png");
			}

			return File(Path.Combine(webRootPath, "/images/sem_imagem.jpg"), "image/jpg");
		}

		#endregion

	}
}