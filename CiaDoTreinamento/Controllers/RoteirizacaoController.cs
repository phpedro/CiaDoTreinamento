using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;
using CiaDoTreinamento.Models;

namespace CiaDoTreinamento.Controllers
{
	public class RoteirizacaoController : Controller
	{
		#region Events

		public IActionResult List()
		{
			return View();
		}

		public IActionResult Consultar(int? ddlAgenteVendasFiltro, int? ddlInstrutorFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro,
										int? ddlMesoFiltro, int? ddlMicroFiltro, string txtaRazaoNomeClienteFiltro, int? txtaCodigoPedidoFiltro,
										int? ddlProdutosFiltro, DateTime? dtpDataInicioFechamentoPedido, DateTime? dtpDataFinalFechamentoPedido)
		{

			RoteirizacaoBLL BLL = new RoteirizacaoBLL();
			string mensagemErro;

			List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosRoteirizacao(ddlAgenteVendasFiltro, ddlInstrutorFiltro, txtaRazaoNomeClienteFiltro, ddlCidadesFiltro,
																				ddlEstadosFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, ddlMesoFiltro, 
																				ddlMicroFiltro, txtaCodigoPedidoFiltro, ddlProdutosFiltro, out mensagemErro);

			if (!string.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaPedidos);
		}

		public IActionResult GerarRotaAutomatica(string listaPedidos)
		{
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			string mensagemErro;
			List<CabecalhoPedido> cabecalhosPedidos = new List<CabecalhoPedido>();

			if (!String.IsNullOrEmpty(listaPedidos))
			{
				List<int> pedidos = JsonConvert.DeserializeObject<List<int>>(listaPedidos);

				foreach (int pedido in pedidos)
				{
					cabecalhosPedidos.Add(cabecalhoPedidoBLL.GetPedidoByCodigo(pedido, out mensagemErro));
				}
			}

			return View(cabecalhosPedidos);
		}

		public IActionResult RelatorioCorreio(int? ddlAgenteVendasFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro,
												string txtaRazaoNomeClienteFiltro, DateTime? dtpDataInicioFechamentoPedido, DateTime? dtpDataFinalFechamentoPedido)
		{

			RoteirizacaoBLL BLL = new RoteirizacaoBLL();
			string mensagemErro;

			List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosCorreio(ddlAgenteVendasFiltro, txtaRazaoNomeClienteFiltro, ddlCidadesFiltro,
																			ddlEstadosFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

			if (!string.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View(listaPedidos);
		}

		public IActionResult RelatorioPendenteRota(int? ddlAgenteVendasFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro,
												string txtaRazaoNomeClienteFiltro, DateTime? dtpDataInicioFechamentoPedido, DateTime? dtpDataFinalFechamentoPedido)
		{

			RoteirizacaoBLL BLL = new RoteirizacaoBLL();
			string mensagemErro;

			List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosPendenteRota(ddlAgenteVendasFiltro, txtaRazaoNomeClienteFiltro, ddlCidadesFiltro,
																			ddlEstadosFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

			if (!string.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View(listaPedidos);
		}

		public IActionResult RealatorioPendeteVistoria(int? ddlAgenteVendasFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro,
												string txtaRazaoNomeClienteFiltro, DateTime? dtpDataInicioFechamentoPedido, DateTime? dtpDataFinalFechamentoPedido)
		{

			RoteirizacaoBLL BLL = new RoteirizacaoBLL();
			string mensagemErro;

			List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosPendenteVistoria(ddlAgenteVendasFiltro, txtaRazaoNomeClienteFiltro, ddlCidadesFiltro,
																			ddlEstadosFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

			if (!string.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View(listaPedidos);
		}

		public IActionResult NovaRotaManual(int? codigoRota)
		{
			string mensagemErro;

			var vm = new RotaViewModel();

			if (codigoRota.HasValue && codigoRota > 0)
			{
				vm.Rota = RotaBLL.selectRotas(Convert.ToInt32(codigoRota), out mensagemErro).FirstOrDefault();
				vm.listaItensRota = ItemRotaBLL.selectItensRota(Convert.ToInt32(codigoRota), out mensagemErro);

				return View(vm);
			}
			else
			{
				return View(vm);
			}

			
		}

		public IActionResult RelatorioRotasCriadas()
		{
			return View(new List<Rota>());
		}

		public IActionResult ConsutarRotasInstrutor(int? ddlInstrutorFiltro)
		{
			string mensagemErro;

			List<Rota> listaRotas = RotaBLL.selectRotasByInstrutor(ddlInstrutorFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View(new List<Rota>());
			}

			if (listaRotas.Count == 0)
			{
				TempData["mensagemAlerta"] = "Nenhum registro encontrado para o filtro informado!";
			}

			return View("RelatorioRotasCriadas", listaRotas);
		}

		public IActionResult NovaRota()
		{
			return View();
		}

		public IActionResult NovaRota2(int? id)
		{
			Rota rota = new Rota();
			string mensagemErro;

			if (id.HasValue && id > 0)
			{
				rota = RotaBLL.selectRotas((int)id, out mensagemErro).FirstOrDefault();
			}

			return View(rota);
		}

		#endregion

		#region Services

		[HttpPost]
		public JsonResult AtualizarPedidoRota(int codigoPedido, int ddlInstrutorRota, string ddlSalaRota, string txtInformacoesAdicionais,
												DateTime dtpDataInicioTreinamento, DateTime dtpDataFimTreinamento, int codigoStatus, string detalheRetornoPedido = "")
		{
			RoteirizacaoBLL roteirizacaoBLL = new RoteirizacaoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
			string mensagemErro;

			var codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

			if (!roteirizacaoBLL.updatePedidoRota(codigoPedido, ddlInstrutorRota, ddlSalaRota, txtInformacoesAdicionais,
													dtpDataInicioTreinamento, dtpDataFimTreinamento, codigoStatus, out mensagemErro, detalheRetornoPedido))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

			//Grava Notificação
			CabecalhoPedido cabecalhoPedido = cabecalhoPedidoBLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);
			Notificacoes notificacao = new Notificacoes();
			notificacao.FuncionarioCriador = new Funcionario() { Codigo = codigoUsuario };
			notificacao.FuncionarioDestino = new Funcionario() { Codigo = cabecalhoPedido.FuncionarioVendedor.Codigo };
			notificacao.Mensagem = "Pedido " + cabecalhoPedido.Codigo + " incluído na rota!";
			notificacoesBLL.insertNotificacao(notificacao, out mensagemErro);

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult AtualizarRotaCompleta(int codigoInstrutor, string arrayPedidosRota)
		{
			RoteirizacaoBLL roteirizacaoBLL = new RoteirizacaoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
			string mensagemErro;
			List<RotaComplexaViewModel> listaPedidos = JsonConvert.DeserializeObject<List<RotaComplexaViewModel>>(arrayPedidosRota);

			var codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

			foreach (RotaComplexaViewModel pedido in listaPedidos)
			{
				if (!roteirizacaoBLL.updatePedidoRota(pedido.codigoPedido, codigoInstrutor, "", "", Convert.ToDateTime(pedido.dataInicioTreinamento), Convert.ToDateTime(pedido.dataFimTreinamento), 13, out mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}

				//Grava Notificação
				CabecalhoPedido cabecalhoPedido = cabecalhoPedidoBLL.GetPedidoByCodigo(pedido.codigoPedido, out mensagemErro);
				Notificacoes notificacao = new Notificacoes();
				notificacao.FuncionarioCriador = new Funcionario() { Codigo = codigoUsuario };
				notificacao.FuncionarioDestino = new Funcionario() { Codigo = cabecalhoPedido.FuncionarioVendedor.Codigo };
				notificacao.Mensagem = "Pedido " + cabecalhoPedido.Codigo + " incluído na rota!";
				notificacoesBLL.insertNotificacao(notificacao, out mensagemErro);

			}

			TempData["mensagemSucesso"] = "Rota criada com sucesso!";

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult AtualizarStatusPedido(int codigoStatus, List<int> pedidos)
		{
			RoteirizacaoBLL roteirizacaoBLL = new RoteirizacaoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
			string mensagemErro;

			StatusNegociacao status = new StatusNegociacao(codigoStatus);

			var codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

			foreach (int pedido in pedidos)
			{
				if (!roteirizacaoBLL.updateStatusPedido(pedido, codigoStatus, out mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}

				//Grava Notificação
				CabecalhoPedido cabecalhoPedido = cabecalhoPedidoBLL.GetPedidoByCodigo(pedido, out mensagemErro);
				Notificacoes notificacao = new Notificacoes();
				notificacao.FuncionarioCriador = new Funcionario() { Codigo = codigoUsuario };
				notificacao.FuncionarioDestino = new Funcionario() { Codigo = cabecalhoPedido.FuncionarioVendedor.Codigo };
				notificacao.Mensagem = "Status do pedido " + cabecalhoPedido.Codigo + " atualizado para " + status.Descricao + "!";
				notificacoesBLL.insertNotificacao(notificacao, out mensagemErro);
			}

			TempData["mensagemSucesso"] = "Pedidos atualizados com sucesso!";
			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult AtualizarRotaManual(int codigoInstrutor, string arrayPedidosRota, string arrayPedidosAprovados, string observacaoRota)
		{
			RoteirizacaoBLL roteirizacaoBLL = new RoteirizacaoBLL();
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
			string mensagemErro;
			List<RotaComplexaViewModel> listaPedidos = JsonConvert.DeserializeObject<List<RotaComplexaViewModel>>(arrayPedidosRota);
			List<RotaComplexaViewModel> listaPedidosAprovados = JsonConvert.DeserializeObject<List<RotaComplexaViewModel>>(arrayPedidosAprovados);

			var codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

			//INCLUIR CABECALHO ROTA
			Rota rota = new Rota();
			rota.Instrutor = new Funcionario() { Codigo = codigoInstrutor };
			rota.DataInicio = Convert.ToDateTime(listaPedidos.Min(x => x.dataInicioTreinamento));
			rota.DataFim = Convert.ToDateTime(listaPedidos.Min(x => x.dataFimTreinamento));
			rota.Observacao = observacaoRota;

			rota.Codigo = RotaBLL.insertRota(rota, out mensagemErro);

			if (rota.Codigo <= 0)
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

			//INCLUIR ITENS ROTA
			foreach (RotaComplexaViewModel pedido in listaPedidos)
			{
				ItemRota itemRota = new ItemRota();
				itemRota.Rota = rota;
				itemRota.CabecalhoPedido = new CabecalhoPedido() { Codigo = pedido.codigoPedido };
				itemRota.DataInicio = Convert.ToDateTime(pedido.dataFimTreinamento);
				itemRota.DataFim = Convert.ToDateTime(pedido.dataFimTreinamento);
				itemRota.Aprovado = (listaPedidosAprovados.Where(x => x.codigoPedido == pedido.codigoPedido).Count() > 0);

				if (ItemRotaBLL.insertItemRota(itemRota, out mensagemErro) <= 0)
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}

			TempData["mensagemSucesso"] = "Rota criada com sucesso!";

			return Json(new { sucesso = true });
		}

		[HttpPost]
		public JsonResult ConfirmarItemRota(int codigoRota, int codigoPedido, int codigoInstrutor, DateTime dataInicioTreinamento, DateTime dataFimTreinamento, int? codigoSala, int? codigoHotel, 
												string observacao, string ObservacaoInstrutor, string nomeCliente, string horarioAtendimento, string[] listaEmails)
		{
			string mensagemErro;
			ParceiroBLL parceiroBLL = new ParceiroBLL();
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();

			var usuario = new Funcionario(Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]));

			//ATUALIZAR PEDIDOS COM OS DADOS INFORMADOS
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			CabecalhoPedido cab = cabecalhoPedidoBLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

			if (cab != null)
			{
				cab.FuncionarioInstrutor = new Funcionario(codigoInstrutor);
				cab.DataInicioTreinamento = dataInicioTreinamento;
				cab.DataFinalTreinamento = dataFimTreinamento;
				if (codigoSala.HasValue && codigoSala > 0)
				{
					cab.ParceiraSalaTreinamento = parceiroBLL.getParceiros(codigoSala, null, null, null, null, out mensagemErro).First();
				}
				if (codigoHotel.HasValue && codigoHotel > 0)
				{
					cab.ParceiroHotel = parceiroBLL.getParceiros(codigoHotel, null, null, null, null, out mensagemErro).First();
				}
				cab.InfoTreinamento = ObservacaoInstrutor;
				cab.StatusNegociacao = new StatusNegociacao() { CodigoStatus = 13 }; //ENVIAR PARA AGUARDANDO GERAÇÃO DE DOCUMENTOS

				if (!cabecalhoPedidoBLL.updateCabecalhoPedido(cab, out mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}

				//Grava Notificação
				NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
				Notificacoes notificacao = new Notificacoes();
				notificacao.FuncionarioCriador = usuario;
				notificacao.FuncionarioDestino = new Funcionario() { Codigo = cab.FuncionarioVendedor.Codigo };
				notificacao.Mensagem = "Pedido " + cab.Codigo + " incluído na rota!";
				notificacoesBLL.insertNotificacao(notificacao, out mensagemErro);

				//ATUALIZA ITEM ROTA
				ItemRota itemRota = ItemRotaBLL.selectItensRota(codigoRota, out mensagemErro).Where(x => x.CabecalhoPedido.Codigo == codigoPedido).FirstOrDefault();
				if (itemRota != null)
				{
					itemRota.Aprovado = true;
					ItemRotaBLL.updateRota(itemRota, out mensagemErro);
				}

			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
			
			//BUSCAR ITENS PEDIDO
			var listaItens = itemPedidoBLL.getItemPedido(null, (int)cab.Codigo, out mensagemErro);
			var listaItensConcat = "";

			listaItensConcat = "<ul>";

			foreach (ItemPedido item in listaItens)
			{
				listaItensConcat += "<li>" + item.Produto.Descricao + "</li>";
			}

			listaItensConcat += "</ul>";

			//MONTAR LISTA COM HORÁRIOS
			string listHorarios = "<ul>";

			foreach (string item in horarioAtendimento.Split('#'))
			{
				listHorarios += "<li>" + item + "</li>";
			}

			listHorarios += "</ul>";

			//MONTAR EMAIL CLIENTE
			string templateEmail = System.IO.File.ReadAllText("./wwwRoot/Templates/TemplateEmailRoteirizacao.html");

			templateEmail = templateEmail.Replace("@nomeCliente", nomeCliente)
											.Replace("@razaoSocial", cab.Cliente.RazaoSocial)
											.Replace("@instrutor", cab.FuncionarioInstrutor.Nome)
											.Replace("@servicos", listaItensConcat)
											.Replace("@dataAtendimento", listHorarios)
											.Replace("@observacao", "<strong>2º Obs.:</strong> " + observacao)
											.Replace("@localAtendimento", (cab.ParceiraSalaTreinamento == null || cab.ParceiraSalaTreinamento.Codigo == 0 ? "Posto" : cab.ParceiraSalaTreinamento.Descricao + " - " + cab.ParceiraSalaTreinamento.Endereco + "," + cab.ParceiraSalaTreinamento.Cidade.Descricao + "-" + cab.ParceiraSalaTreinamento.Cidade.Estado));


			//INCLUIR RELATO
			string relatoAtendimento = System.IO.File.ReadAllText("./wwwRoot/Templates/TemplateRelatoRoteirizacao.txt");
			relatoAtendimento = relatoAtendimento.Replace("@nomeCliente", nomeCliente)
											.Replace("@razaoSocial", cab.Cliente.RazaoSocial)
											.Replace("@instrutor", cab.FuncionarioInstrutor.Nome)
											.Replace("@servicos", listaItensConcat)
											.Replace("@dataAtendimento", horarioAtendimento)
											.Replace("@observacao", "2º Obs.: " + observacao)
											.Replace("@localAtendimento", (cab.ParceiraSalaTreinamento == null || cab.ParceiraSalaTreinamento.Codigo == 0 ? "Posto" : cab.ParceiraSalaTreinamento.Descricao + " - " + cab.ParceiraSalaTreinamento.Endereco + "," + cab.ParceiraSalaTreinamento.Cidade.Descricao + "-" + cab.ParceiraSalaTreinamento.Cidade.Estado));

			if (!String.IsNullOrEmpty(relatoAtendimento))
			{
				Atendimentos atendimento = new Atendimentos();
				atendimento.CodigoPedido = (int)cab.Codigo;
				atendimento.DataRegistro = DateTime.Now;
				atendimento.Descricao = relatoAtendimento;
				atendimento.Funcionario = usuario;

				AtendimentosBLL atendimentosBLL = new AtendimentosBLL();

				if (!atendimentosBLL.insertAtendimento(atendimento, out mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}
			}

			if (listaEmails.Length > 0)
			{
				string emails = "";

				for (var i = 0; i < listaEmails.Length; i++)
				{
					emails += listaEmails[i] + ",";
				}

				emails += "rota@ciadotreinamento.com.br," + cab.FuncionarioVendedor.Email;
				
				//ENVIAR EMAIL PARA O CLIENTE
				string Assunto = "A/C " + nomeCliente  + " - Confirmação de Atendimento a " + cab.Cliente.Cidade.Descricao + " - " + cab.Cliente.Cidade.Estado + " / Cia do Treinamento";

				string retornoEmail = Uteis.SendMailRoteirizacao(usuario.Login, emails, Assunto, templateEmail, usuario.Email);

			}

			return Json(new { sucesso = true });
		}

		[HttpGet]
		public JsonResult BuscarItensPedido(int codigoPedido)
		{
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
			string mensagemErro;

			//BUSCAR ITENS DO PEDIDOS
			List<ItemPedido> itensPedido = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, mensagemErro = mensagemErro });
			}

			return Json(new { sucesso = true, itensPedido = itensPedido });
		}

		[HttpGet]
		public JsonResult BuscarParceirosSala(int codigoCidade)
		{
			ParceiroBLL parceiroBLL = new ParceiroBLL();
			string mensagemErro;
			List<Parceiro> parceirosSalas = parceiroBLL.getParceiros(null, null, null, codigoCidade, "Sala Alugada", out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
			else
			{
				return Json(new { sucesso = true, listaParceirosSala = parceirosSalas });
			}
		}

		[HttpGet]
		public JsonResult BuscarParceirosHotel(int codigoCidade)
		{
			ParceiroBLL parceiroBLL = new ParceiroBLL();
			string mensagemErro;
			List<Parceiro> parceirosHoteis = parceiroBLL.getParceiros(null, null, null, codigoCidade, "Hotéis", out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
			else
			{
				return Json(new { sucesso = true, listaParceirosSala = parceirosHoteis });
			}
		}

		[HttpGet]
		public JsonResult BuscarPedidoARoteirizar(int codigoPedido)
		{
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
			string mensagemErro;

			CabecalhoPedido cabecalho = cabecalhoPedidoBLL.GetPedidoByCodigo(codigoPedido, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
			else
			{
				//BUSCAR ITENS DO PEDIDOS
				List<ItemPedido> itensPedido = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);

				if (!String.IsNullOrEmpty(mensagemErro))
				{
					return Json(new { sucesso = false, mensagemErro = mensagemErro });
				}

				return Json(new { sucesso = true, temVistoria = (itensPedido.Where(x => x.Produto.TemVISTORIA == true).Count() > 0), pedido = cabecalho, itensPedido = itensPedido });
			}
		}

		[HttpGet]
		public JsonResult BuscarPedidosAgrupadosPorCidade(string codigoEstado, int? codigoCidade, int? codigoMeso, int? codigoMicro, int? codigoProduto)
		{
			string mensagemErro;
			RoteirizacaoBLL BLL = new RoteirizacaoBLL();

			var listaPedidos = BLL.BuscarPedidosRoteirizacao(null, null, null, codigoCidade, codigoEstado, null, null, codigoMeso, codigoMicro, null, codigoProduto, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

			var vm = (from item in listaPedidos
					  group item by item.Cliente.Cidade.Codigo into Group
					  select new PedidosRoteirizacaoViewModel() { cidade = Group.First().Cliente.Cidade.Descricao + " - " + Group.First().Cliente.Cidade.Estado, latitude = Group.First().Cliente.Cidade.Latitude, longitude = Group.First().Cliente.Cidade.Longitude, listaPedidos = Group.ToList() }).ToList();

			return Json(new { sucesso = true, listaPedidos = vm });
		}

		[HttpGet]
		public JsonResult BuscarPedidosRota(string estado, int? codigoCidade)
		{
			try
			{
				RoteirizacaoBLL BLL = new RoteirizacaoBLL();
				string mensagemErro;

				List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosRoteirizacao(null, null, null, codigoCidade, estado, null, null, null, null, null, null, out mensagemErro);

				return Json(new { sucesso = true, listaPedidos = listaPedidos });
			}catch(Exception ex){
				return Json(new { sucesso = false, mensagemErro = ex.Message });
			}
		}

		[HttpGet]
		public JsonResult BuscarCidadesRota(string estado, int? cidade, int? meso, int? micro, int? produto)
		{
			try
			{
				RoteirizacaoBLL BLL = new RoteirizacaoBLL();
				string mensagemErro;

				List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosRoteirizacao(estado, cidade, meso, micro, produto, out mensagemErro);

				List<CidadesRotaViewModel> vw = (from item in listaPedidos
												 group item by item.Cliente.Cidade.Codigo into Group
												 select new CidadesRotaViewModel() { cidade = Group.First().Cliente.Cidade, listaPedidos = Group.ToList(), qtdePedidos = Group.Count(), valorTotal = Group.Sum(x => x.ValorTotal) }).ToList(); ;

				return Json(new { sucesso = true, listaCidades = vw });
			}
			catch (Exception ex)
			{
				return Json(new { sucesso = false, mensagemErro = ex.Message });
			}
		}

		[HttpGet]
		public JsonResult BuscarDadosClienteRota(int codigoPedido)
		{
			try
			{
				string mensagemErro;
				ClienteBLL clienteBLL = new ClienteBLL();
				ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();
				AtendimentosBLL atendimentosBLL = new AtendimentosBLL();

				ClienteRotaViewModel vm = new ClienteRotaViewModel();

				vm.cliente = clienteBLL.getClientesDetalheRota(codigoPedido, out mensagemErro).First();
				vm.listaItens = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);
				vm.atendimentos = atendimentosBLL.getAtendimentosPedido(codigoPedido, out mensagemErro);

				return Json(new { sucesso = true, retorno = vm });
			}
			catch (Exception ex)
			{
				return Json(new { sucesso = false, mensagemErro = ex.Message });
			}
		}

		[HttpGet]
		public JsonResult BuscarItensRota(int codigoRota)
		{
			string mensagemErro;
			var listaItens = ItemRotaBLL.selectItensRota(codigoRota, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, listaItens = listaItens });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult CriarAtualizarNovaRota(int? codigoRota, int codigoInstrutor, string observacao)
		{
			string mensagemErro;

			Rota rota = new Rota();

			rota.Codigo = codigoRota;
			rota.Instrutor = new Funcionario() { Codigo = codigoInstrutor };
			rota.DataInicio = DateTime.Now;
			rota.DataFim = DateTime.Now;
			rota.Observacao = observacao;

			if (rota.Codigo.HasValue && rota.Codigo > 0)
			{
				RotaBLL.updateRota(rota, out mensagemErro);
			}
			else
			{
				codigoRota = RotaBLL.insertRota(rota, out mensagemErro);
			}

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, codigoRota = codigoRota });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult InserirNovoItemRota(int codigoRota, int codigoPedido, DateTime dataInicio, DateTime dataFim)
		{
			string mensagemErro;

			ItemRota itemRota = new ItemRota();
			itemRota.Rota = new Rota() { Codigo = codigoRota };
			itemRota.CabecalhoPedido = new CabecalhoPedido() { Codigo = codigoPedido };
			itemRota.DataInicio = dataInicio;
			itemRota.DataFim = dataFim;
			itemRota.Aprovado = false;

			ItemRotaBLL.insertItemRota(itemRota, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		[HttpPost]
		public JsonResult RemoverItemRota(int codigoRota, int codigoPedido)
		{
			string mensagemErro;

			ItemRotaBLL.deleteItemRota(codigoRota, codigoPedido, out mensagemErro);

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		#endregion
	}
}