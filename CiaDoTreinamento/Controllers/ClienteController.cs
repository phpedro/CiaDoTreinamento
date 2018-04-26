using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

		public IActionResult Edit(int id)
		{
			if (id != 0)
			{
				ClienteBLL BLL = new ClienteBLL();

				Cliente cliente = BLL.GetClientes(id, out string mensagemErro).FirstOrDefault();

				if (cliente != null)
				{
					return View(cliente);
				}
				else
				{
					return RedirectToAction("List");
				}
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtRazaoSocialFiltro, string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtNomeClienteFiltro,
										string txtCpfClienteFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro, int? ddlMicrosFiltro,
										int? ddlRedesFiltro, string txtEmailFiltro, string txtTelefoneFiltro)
		{

			ClienteBLL BLL = new ClienteBLL();

			List<Cliente.ClienteTela> listaClientes = BLL.GetClientes(null, txtRazaoSocialFiltro, txtCnpjFiltro, txtCodigoPedidoFiltro, txtNomeClienteFiltro, txtCpfClienteFiltro,
																		ddlEstadosFiltro, ddlCidadesFiltro, ddlMicrosFiltro, ddlRedesFiltro, txtEmailFiltro, txtTelefoneFiltro,
																		out string mensagemErro);

			return View("List", listaClientes);
		}

		public IActionResult Salvar(Cliente cliente, string hfListaAlunos, string hfListaEmails, string hfListaTelefones, string hfListaAtendimentos,
			string hfListaConcorrentes, string hfListaContabilidades, string hfListaLicenciamentoAmbiental)
		{

			string mensagemErro = "";
			ClienteBLL clienteBLL = new ClienteBLL();
			AlunoBLL alunoBLL = new AlunoBLL();
			EmailClienteBLL emailClienteBLL = new EmailClienteBLL();
			TelefoneClienteBLL telefoneClienteBLL = new TelefoneClienteBLL();
			AtendimentosBLL atendimentosBLL = new AtendimentosBLL();
			RelacaoClienteConcorrenteBLL relacaoClienteConcorrenteBLL = new RelacaoClienteConcorrenteBLL();
			RelacaoClienteContabilidadeBLL relacaoClienteContabilidadeBLL = new RelacaoClienteContabilidadeBLL();
			RelacaoClienteLicenciamentoAmbientalBLL relacaoClienteLicenciamentoAmbientalBLL = new RelacaoClienteLicenciamentoAmbientalBLL();

			//CADASTRO / ATUALIZAÇÃO CLIENTE
			if (cliente.Codigo == null)
			{
				cliente.DataCadastro = DateTime.Now;

				if (clienteBLL.InsertCliente(cliente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Cliente cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
					return RedirectToAction("List");
				}
			}
			else
			{
				if (clienteBLL.UpdateCliente(cliente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Cliente atualizado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
					return RedirectToAction("List");
				}
			}

			//CADASTRO/ATUALIZAÇÃO DE ALUNOS
			if (!String.IsNullOrEmpty(hfListaAlunos))
			{
				List<Aluno> listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(hfListaAlunos);

				if (listaAlunos.Count > 0)
				{
					foreach (Aluno item in listaAlunos)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.Cliente = cliente;
							if (!alunoBLL.InserAluno(item, out mensagemErro))
							{
								mensagemErro = "Um ou mais alunos não foram cadastrados. Consulte o suporte!";
							}

						}
						else if (item.tipo == Enumeradores.Tipo.Edit)
						{
							if (!alunoBLL.UpdateAluno(item, out mensagemErro))
							{
								mensagemErro = "Um ou mais alunos não foram atualizados. Consulte o suporte!";
							}
						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO/ATUALIZAÇÃO DE EMAILS
			if (!String.IsNullOrEmpty(hfListaEmails))
			{
				List<EmailCliente> listaEmails = JsonConvert.DeserializeObject<List<EmailCliente>>(hfListaEmails);

				if (listaEmails.Count > 0)
				{
					foreach (EmailCliente item in listaEmails)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.Cliente = (int)cliente.Codigo;
							if (!emailClienteBLL.insertEmail(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais emails não foram cadastrados. Consulte o suporte!";
							}

						}
						else if (item.tipo == Enumeradores.Tipo.Edit)
						{
							if (!emailClienteBLL.updateEmail(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais emails não foram atualizados. Consulte o suporte!";
							}
						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO/ATUALIZAÇÃO DE TELEFONES
			if (!String.IsNullOrEmpty(hfListaTelefones))
			{
				List<TelefoneCliente> listaTelefones = JsonConvert.DeserializeObject<List<TelefoneCliente>>(hfListaTelefones);

				if (listaTelefones.Count > 0)
				{
					foreach (TelefoneCliente item in listaTelefones)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.cliente = cliente;
							if (!telefoneClienteBLL.InserTelefoneCliente(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais telefones não foram cadastrados. Consulte o suporte!";
							}

						}
						else if (item.tipo == Enumeradores.Tipo.Edit)
						{
							if (!telefoneClienteBLL.UpdateTelefoneCliente(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais telefones não foram atualizados. Consulte o suporte!";
							}
						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO/ATUALIZAÇÃO DE ATENDIMENTOS
			if (!String.IsNullOrEmpty(hfListaAtendimentos))
			{
				List<Atendimentos> listaAtendimentos = JsonConvert.DeserializeObject<List<Atendimentos>>(hfListaAtendimentos);

				if (listaAtendimentos.Count > 0)
				{
					//GET USUÁRIO CORRENTE
					var usuario = HttpContext.Request.Cookies["USUARIO"];
					Pessoa pessoa = PessoaBLL.getPessoaByLogin(usuario, out mensagemErro);

					//GET ÚLTIMO PEDIDO CLIENTE
					int codigoPedido = clienteBLL.getMaxCodigoPedido((int)cliente.Codigo, out mensagemErro);

					if (codigoPedido != -1)
					{
						foreach (Atendimentos item in listaAtendimentos)
						{
							if (item.tipo == Enumeradores.Tipo.New)
							{
								item.Funcionario = new Funcionario((int)pessoa.Codigo);
								item.CodigoPedido = codigoPedido;
								if (!atendimentosBLL.insertAtendimento(item, out mensagemErro))
								{
									mensagemErro += " Um ou mais atendimentos não foram cadastrados. Consulte o suporte!";
								}

							}
							else if (item.tipo == Enumeradores.Tipo.Edit)
							{
								if (!atendimentosBLL.updateAtendimento(item, out mensagemErro))
								{
									mensagemErro += " Um ou mais atendimentos não foram atualizados. Consulte o suporte!";
								}
							}
						}
					}
					else
					{
						mensagemErro = "Não foi possível cadastrar o atendimento para o cliente! O cliente deve possuir pelo menos um pedido cadastrado!";
					}

				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO DE RELAÇÃO CLIENTE CONCORRENTE
			if (!String.IsNullOrEmpty(hfListaConcorrentes))
			{
				List<RelacaoClienteConcorrente> listaConcorrentes = JsonConvert.DeserializeObject<List<RelacaoClienteConcorrente>>(hfListaConcorrentes);

				if (listaConcorrentes.Count > 0)
				{
					foreach (RelacaoClienteConcorrente item in listaConcorrentes)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.CodigoCliente = (int)cliente.Codigo;
							if (!relacaoClienteConcorrenteBLL.insertRelacaoClienteConcorrente(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais relações não foram cadastrados. Consulte o suporte!";
							}

						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO DE RELAÇÃO CLIENTE CONTABILIDADE
			if (!String.IsNullOrEmpty(hfListaContabilidades))
			{
				List<RelacaoClienteContabilidade> listaConcorrentes = JsonConvert.DeserializeObject<List<RelacaoClienteContabilidade>>(hfListaContabilidades);

				if (listaConcorrentes.Count > 0)
				{
					foreach (RelacaoClienteContabilidade item in listaConcorrentes)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.CodigoCliente = (int)cliente.Codigo;
							if (!relacaoClienteContabilidadeBLL.insertRelacaoClienteContabilidade(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais relações não foram cadastrados. Consulte o suporte!";
							}

						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			//CADASTRO DE RELAÇÃO CLIENTE LICENCIAMENTO AMBIENTAL
			if (!String.IsNullOrEmpty(hfListaLicenciamentoAmbiental))
			{
				List<RelacaoClienteLicenciamentoAmbiental> listaConcorrentes = JsonConvert.DeserializeObject<List<RelacaoClienteLicenciamentoAmbiental>>(hfListaLicenciamentoAmbiental);

				if (listaConcorrentes.Count > 0)
				{
					foreach (RelacaoClienteLicenciamentoAmbiental item in listaConcorrentes)
					{
						if (item.tipo == Enumeradores.Tipo.New)
						{
							item.CodigoCliente = (int)cliente.Codigo;
							if (!relacaoClienteLicenciamentoAmbientalBLL.insertRelacaoClienteLicenciamento(item, out mensagemErro))
							{
								mensagemErro += " Um ou mais relações não foram cadastrados. Consulte o suporte!";
							}

						}
					}
				}

				if (mensagemErro.Length > 0)
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");
		}

		public JsonResult AtualizarStatus(int id, int novoStatus)
		{
			ClienteBLL BLL = new ClienteBLL();
			if (BLL.UpdateStatus(id, novoStatus, out string mensagemErro))
			{
				return Json(new { sucesso = true});
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		public JsonResult BuscarCliente(int? codigo, string cnpj, int? codigoPedido)
		{
			ClienteBLL clienteBLL = new ClienteBLL();
			string mensagemErro;

			Cliente.ClienteTela cliente = clienteBLL.GetClientes(codigo, null, cnpj, codigoPedido, null, null, null, null, null, null, null, null, out mensagemErro).FirstOrDefault();

			if (cliente != null)
			{
				return Json(new { sucesso = true, cliente = cliente });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		public JsonResult BuscarClientesResumido(string search)
		{

			ClienteBLL BLL = new ClienteBLL();

			List<Cliente.ClienteTela> listaClientes = BLL.GetClienteResumido(search, out string mensagemErro);

			if (listaClientes != null)
			{
				return Json(new { sucesso = true, listaClientes = listaClientes });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}

		}

		public JsonResult GetAlunos(int? codigoCliente)
		{
			string mensagemErro;
			string listaAlunos = "";

			if (codigoCliente.HasValue && codigoCliente > 0)
			{
				listaAlunos = AlunoBLL.GetAlunosJson(codigoCliente, out mensagemErro);
			}

			return Json(listaAlunos);
		}

		public JsonResult GetAtendimentos(int codigoCliente)
		{
			string mensagemErro;
			AtendimentosBLL atendimentosBLL = new AtendimentosBLL();
			List<Atendimentos> listaAtendimentos = new List<Atendimentos>();

			if (codigoCliente > 0)
			{
				listaAtendimentos = atendimentosBLL.getAtendimentos(codigoCliente, out mensagemErro);
			}

			return Json(listaAtendimentos);
		}

		public JsonResult GetConcorrentes(int codigoCliente)
		{
			string mensagemErro;
			RelacaoClienteConcorrenteBLL relacaoClienteConcorrenteBLL = new RelacaoClienteConcorrenteBLL();
			List<RelacaoClienteConcorrente> listaConcorrentes = new List<RelacaoClienteConcorrente>();

			if (codigoCliente > 0)
			{
				listaConcorrentes = relacaoClienteConcorrenteBLL.getConcorrentesByCliente(codigoCliente, out mensagemErro);
			}

			return Json(listaConcorrentes);
		}

		public JsonResult GetContabilidades(int codigoCliente)
		{
			string mensagemErro;

			RelacaoClienteContabilidadeBLL relacaoClienteContabilidadeBLL = new RelacaoClienteContabilidadeBLL();
			List<RelacaoClienteContabilidade> listaConcorrentes = new List<RelacaoClienteContabilidade>();

			if (codigoCliente > 0)
			{
				listaConcorrentes = relacaoClienteContabilidadeBLL.getContabilidadesByCliente(codigoCliente, out mensagemErro);
			}

			return Json(listaConcorrentes);
		}

		public JsonResult GetEmails(int codigoCliente)
		{

			EmailClienteBLL BLL = new EmailClienteBLL();
			List<EmailCliente> listaEmails = new List<EmailCliente>();

			if (codigoCliente > 0)
			{
				listaEmails = BLL.GetEmails(codigoCliente, out string mensagemErro);
			}

			return Json(listaEmails);

		}

		public JsonResult GetLicenciamentos(int codigoCliente)
		{
			string mensagemErro;

			RelacaoClienteLicenciamentoAmbientalBLL relacaoClienteLicenciamentoAmbientalBLL = new RelacaoClienteLicenciamentoAmbientalBLL();
			List<RelacaoClienteLicenciamentoAmbiental> listaConcorrentes = new List<RelacaoClienteLicenciamentoAmbiental>();

			if (codigoCliente > 0)
			{
				listaConcorrentes = relacaoClienteLicenciamentoAmbientalBLL.getLicenciamentosByCliente(codigoCliente, out mensagemErro);
			}

			return Json(listaConcorrentes);
		}

		public JsonResult GetOrcamentos(int? codigoCliente)
		{
			string mensagemErro = "";
			List<CabecalhoOrcamento.CabecalhoOrcamentoTela> listaPedidos = null;

			if (codigoCliente.HasValue && codigoCliente > 0)
			{
				CabecalhoOrcamentoBLL cabecalhoOrcamentoBLL = new CabecalhoOrcamentoBLL();
				listaPedidos = cabecalhoOrcamentoBLL.GetPedidoByCliente(codigoCliente, out mensagemErro);
			}

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, resultado = listaPedidos });
			}
			else
			{
				return Json(new { sucesso = false, resultado = mensagemErro });
			}


		}

		public JsonResult GetPedidos(int? codigoCliente)
		{
			string mensagemErro = "";
			List<CabecalhoPedido.CabecalhoPedidoTela> listaPedidos = null;

			if (codigoCliente.HasValue && codigoCliente > 0)
			{
				CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
				listaPedidos = cabecalhoPedidoBLL.GetPedidoByCliente(codigoCliente, out mensagemErro);
			}

			if (String.IsNullOrEmpty(mensagemErro))
			{
				return Json(new { sucesso = true, resultado = listaPedidos });
			}
			else
			{
				return Json(new { sucesso = false, resultado = mensagemErro });
			}


		}

		public JsonResult GetProdutosExpirando(int codigoCliente)
		{
			ClienteBLL BLL = new ClienteBLL();
			List<Cliente.ProdutoExpirando> listaProdutos = new List<Cliente.ProdutoExpirando>();

			if (codigoCliente > 0)
			{
				listaProdutos = BLL.GetProdutosExpirando(codigoCliente, out string mensagemErro);
			}

			return Json(listaProdutos);
		}

		public JsonResult GetProdutosVencidos(int codigoCliente)
		{
			string mensagemErro;

			ClienteBLL clienteBLL = new ClienteBLL();
			List<Cliente.ProdutoExpirando> listaProdutos = new List<Cliente.ProdutoExpirando>();

			if (codigoCliente > 0)
			{
				listaProdutos = clienteBLL.GetProdutosVencidos(codigoCliente, out mensagemErro);
			}

			return Json(listaProdutos);
		}

		public JsonResult GetTelefones(int codigoCliente)
		{
			string mensagemErro;

			TelefoneClienteBLL telefoneClienteBLL = new TelefoneClienteBLL();
			List<TelefoneCliente> listaTelefones = new List<TelefoneCliente>();

			if (codigoCliente > 0)
			{
				listaTelefones = telefoneClienteBLL.GetTelefonesCliente(codigoCliente, out mensagemErro);
			}

			return Json(listaTelefones);

		}

		public JsonResult DeleteRelacaoConcorrente(int codigo)
		{
			string mensagemErro;

			RelacaoClienteConcorrenteBLL relacaoClienteConcorrenteBLL = new RelacaoClienteConcorrenteBLL();

			if (relacaoClienteConcorrenteBLL.deleteRelacaoClienteConcorrente(codigo, out mensagemErro))
			{
				return Json(new { sucesso = true, mensagem = "Relação cliente concorrente removida com sucesso!" });
			}
			else
			{
				return Json(new { sucesso = false, mensagem = "Não foi possível remover a relação cliente concorrente. Contate o suporte!" });
			}
		}

		public JsonResult DeleteRelacaoContabilidade(int codigo)
		{
			string mensagemErro;

			RelacaoClienteContabilidadeBLL relacaoClienteContabilidadeBLL = new RelacaoClienteContabilidadeBLL();

			if (relacaoClienteContabilidadeBLL.deleteRelacaoClienteContabilidade(codigo, out mensagemErro))
			{
				return Json(new { sucesso = true, mensagem = "Relação cliente contabilidade removida com sucesso!" });
			}
			else
			{
				return Json(new { sucesso = false, mensagem = "Não foi possível remover a relação cliente contabilidade. Contate o suporte!" });
			}
		}

		public JsonResult DeleteRelacaoLicenciamento(int codigo)
		{
			string mensagemErro;

			RelacaoClienteLicenciamentoAmbientalBLL relacaoClienteLicenciamentoAmbientalBLL = new RelacaoClienteLicenciamentoAmbientalBLL();

			if (relacaoClienteLicenciamentoAmbientalBLL.deleteRelacaoClienteLicenciamento(codigo, out mensagemErro))
			{
				return Json(new { sucesso = true, mensagem = "Relação cliente licenciamento ambiental removida com sucesso!" });
			}
			else
			{
				return Json(new { sucesso = false, mensagem = "Não foi possível remover a relação cliente licenciamento ambiental. Contate o suporte!" });
			}
		}



	}
}