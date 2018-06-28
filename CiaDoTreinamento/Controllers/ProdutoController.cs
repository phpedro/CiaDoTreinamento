using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CiaDoTreinamento.Controllers
{
    public class ProdutoController : Controller
    {
		#region Atributos e propriedades

		private readonly IHostingEnvironment _hostingEnvironment;

		#endregion

		#region Construtores

		public ProdutoController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region Eventos

		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			ProdutoBLL BLL = new ProdutoBLL();
			string mensagemErro;
			List<Produto.ProdutoTela> listaProdutos = BLL.GetProdutosTela(out mensagemErro);

			return View(listaProdutos);
		}

		public IActionResult Edit(int? codigoProduto)
		{
			ProdutoBLL BLL = new ProdutoBLL();
			string mensagemErro;

			if (codigoProduto != null && codigoProduto != 0)
			{
				Produto produtoCorrente = BLL.GetProdutoById(codigoProduto, out mensagemErro);

				return View(produtoCorrente);
			}
			else
			{
				return View();
			}
		}

		public async Task<IActionResult> Salvar(Produto produto, IFormFile ArquivoImagem, IFormFile ModeloCertificadoFrente, IFormFile ModeloCertificadoVerso,
									IFormFile ModeloProposta, IFormFile ModeloListaPresenca)
		{

			string mensagemErro;
			ProdutoBLL BLL = new ProdutoBLL();

			//UPLOAD IMAGEM
			if (ArquivoImagem != null && ArquivoImagem.Length > 0)
			{

				string webRootPathImage = _hostingEnvironment.WebRootPath + "/ImagensProdutos/" + ArquivoImagem.FileName;

				if (System.IO.File.Exists(webRootPathImage + "/ImagensProdutos/" + ArquivoImagem.FileName))
				{
					System.IO.File.Delete(webRootPathImage + "/ImagensProdutos/" + ArquivoImagem.FileName);
				}

				using (var fileStream = new FileStream(webRootPathImage, FileMode.Create))
				{
					await ArquivoImagem.CopyToAsync(fileStream);
				}

				produto.NomeImagem = ArquivoImagem.FileName;
			}

			string webRootPath = _hostingEnvironment.WebRootPath + "/ModelosDocumentos/";

			//UPLOAD CERTIFICADO FRENTE
			if (ModeloCertificadoFrente != null && ModeloCertificadoFrente.Length > 0)
			{

				if (System.IO.File.Exists(webRootPath + ModeloCertificadoFrente.FileName))
				{
					System.IO.File.Delete(webRootPath + ModeloCertificadoFrente.FileName);
				}

				using (var fileStream = new FileStream(webRootPath + ModeloCertificadoFrente.FileName, FileMode.Create))
				{
					await ModeloCertificadoFrente.CopyToAsync(fileStream);
				}

				produto.NomeModeloCertificado = ModeloCertificadoFrente.FileName;
			}

			//UPLOAD CERTIFICADO VERSO
			if (ModeloCertificadoVerso != null && ModeloCertificadoVerso.Length > 0)
			{

				if (System.IO.File.Exists(webRootPath + ModeloCertificadoVerso.FileName))
				{
					System.IO.File.Delete(webRootPath + ModeloCertificadoVerso.FileName);
				}

				using (var fileStream = new FileStream(webRootPath + ModeloCertificadoVerso.FileName, FileMode.Create))
				{
					await ModeloCertificadoVerso.CopyToAsync(fileStream);
				}

				produto.NomeModeloVerso = ModeloCertificadoVerso.FileName;
			}

			//UPLOAD MODELO PROPOSTA
			if (ModeloProposta != null && ModeloProposta.Length > 0)
			{

				if (System.IO.File.Exists(webRootPath + ModeloProposta.FileName))
				{
					System.IO.File.Delete(webRootPath + ModeloProposta.FileName);
				}

				using (var fileStream = new FileStream(webRootPath + ModeloProposta.FileName, FileMode.Create))
				{
					await ModeloProposta.CopyToAsync(fileStream);
				}

				produto.NomeModeloProposta = ModeloProposta.FileName;
			}

			//UPLOAD MODELO LISTA PRESENÇA
			if (ModeloListaPresenca != null && ModeloListaPresenca.Length > 0)
			{

				if (System.IO.File.Exists(webRootPath + ModeloListaPresenca.FileName))
				{
					System.IO.File.Delete(webRootPath + ModeloListaPresenca.FileName);
				}

				using (var fileStream = new FileStream(webRootPath + ModeloListaPresenca.FileName, FileMode.Create))
				{
					await ModeloListaPresenca.CopyToAsync(fileStream);
				}

				produto.NomeModeloListaPresenca = ModeloListaPresenca.FileName;
			}

			//INSERT PRODUTO
			if (produto.Codigo == null)
			{
				if (BLL.insertProduto(produto, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Produto cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateProduto(produto, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Produto atualizado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");
		}

		public FileResult DownloadImagem(string nomeImagem)
		{
			string webRootPath = _hostingEnvironment.WebRootPath;

			if (!String.IsNullOrEmpty(nomeImagem) && System.IO.File.Exists(webRootPath + "/ImagensProdutos/" + nomeImagem))
			{
				return base.File(Path.Combine(webRootPath, "/ImagensProdutos/" + nomeImagem), "image/png");
			}

			return File(Path.Combine(webRootPath, "/images/sem_imagem.jpg"), "image/jpg");
		}

		[HttpPost]
		public JsonResult getProdutoById(int codigoProduto)
		{
			string mensagemErro;

			ProdutoBLL produtoBLL = new ProdutoBLL();

			Produto produto = produtoBLL.GetProdutoById(codigoProduto, out mensagemErro);

			return Json(produto);
		}

		#endregion

		#region Servicos

		[HttpGet]
		public JsonResult BuscarArgumentacoesVenda(int codigoProduto)
		{
			string mensagemErro;

			ProdutoBLL produtoBLL = new ProdutoBLL();

			Produto produto = produtoBLL.GetProdutoById(codigoProduto, out mensagemErro);

			if (produto != null)
			{
				if (String.IsNullOrEmpty(produto.ArgumentacaoVenda.Trim()))
				{
					produto.ArgumentacaoVenda = "O produto não possui argumentos de venda cadastrado!";
				}

				return Json(new { sucesso = true, argumento = produto.ArgumentacaoVenda });
			}
			else
			{
				return Json(new { sucesso = false, mensagemErro = mensagemErro });
			}
		}

		#endregion

	}
}