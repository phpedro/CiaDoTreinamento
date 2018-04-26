using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CODE
{
    public class CabecalhoOrcamentoBLL
    {
		public bool GetInsertCabecalhoOrcamento(CabecalhoOrcamento cabecalhoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CabecalhoOrcamentoDAL.GetInsertCabecalhoOrcamento(cabecalhoOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool GetUpdateCabecalhoOrcamento(CabecalhoOrcamento cabecalhoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CabecalhoOrcamentoDAL.GetUpdateCabecalhoOrcamento(cabecalhoOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<CabecalhoOrcamento.CabecalhoOrcamentoTela> GetPedidoByCliente(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CabecalhoOrcamentoDAL.GetPedidoByCliente(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public CabecalhoOrcamento GetCabecalhoOrcamento(int codigoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CabecalhoOrcamentoDAL.GetCabecalhoOrcamento(codigoOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public bool GerarPdfOrcamento(int codigoOrcamento,  bool removerColunaDesconto, string caminhoSaida, string caminhoWebRoot, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				//Lista de produtos vendidos
				CabecalhoOrcamentoBLL cabecalhoOrcamentoBLL = new CabecalhoOrcamentoBLL();
				ItemOrcamentoBLL itemOrcamentoBLL = new ItemOrcamentoBLL();

				CabecalhoOrcamento cabecalhoOrcamento = cabecalhoOrcamentoBLL.GetCabecalhoOrcamento(codigoOrcamento, out mensagemErro);
				List<ItemOrcamento> produtos = itemOrcamentoBLL.getItensOrcamento(codigoOrcamento, null, out mensagemErro);

				using (MemoryStream myMemoryStream = new MemoryStream())
				{
					Document doc = new Document(PageSize.A4);
					doc.SetMargins(20, 20, 40, 80);
					doc.AddCreationDate();

					PdfWriter writer = PdfWriter.GetInstance(doc, myMemoryStream);

					doc.Open();

					string caminhoImagem = caminhoWebRoot + "/images/LogoCIA.png";

					Image imagem = Image.GetInstance(caminhoImagem);
					imagem.ScaleAbsolute(400, 200);
					imagem.Alignment = 1;
					doc.Add(imagem);

					doc.Add(new Paragraph("\n"));

					//Ajustar espaço entre a logo e a tabela

					float[] columnWidths = { 2, 2, 1, 1 };

					PdfPTable table = new PdfPTable(columnWidths);
					table.HorizontalAlignment = 0;
					table.TotalWidth = 560f;
					table.LockedWidth = true;

					PdfPCell cellMeta = new PdfPCell();
					PdfPCell cellData = new PdfPCell();

					//Dados do Cliente
					cellMeta.Phrase = new Phrase("Data:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = Rectangle.TOP_BORDER;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.DataCriacao.ToString("dd/MM/yyyy"));
					cellData.Colspan = 3;
					cellData.Border = Rectangle.TOP_BORDER;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta = new PdfPCell();
					cellMeta.Phrase = new Phrase("Razão Social:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.Cliente.RazaoSocial);
					cellData.Colspan = 3;
					cellData.Border = 0;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("CNPJ:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.Cliente.CNPJ.ToString());
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Cidade:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.Cliente.Cidade.Descricao);
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Estado:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.Cliente.Cidade.Estado);
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Telefone:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.TelefoneContato);
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					//Dados do orcamento

					cellMeta.Phrase = new Phrase("Repres.:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = Rectangle.TOP_BORDER;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.FuncionarioVendedor.Nome);
					cellData.Border = Rectangle.TOP_BORDER;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Fixo:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = Rectangle.TOP_BORDER;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase("(34) 3253-0533");
					cellData.Border = Rectangle.TOP_BORDER;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Email:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.FuncionarioVendedor.Email);
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Celular:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.FuncionarioVendedor.Telefone);
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Val. da Proposta:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.ValidadeOrcamento.ToString() + " Dias");
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Condições de pag:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase(cabecalhoOrcamento.CondicaoPagamento.Descricao);
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					cellMeta.Phrase = new Phrase("Prazo de entrega:", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMeta.Border = 0;
					cellMeta.BorderWidth = 2;
					table.AddCell(cellMeta);

					cellData = new PdfPCell();
					cellData.Phrase = new Phrase("30 Dias úteis");
					cellData.Colspan = 3;
					cellData.Border = 0;
					cellData.BorderWidth = 2;
					table.AddCell(cellData);

					doc.Add(table);

					doc.Add(new Paragraph("\n"));
					Paragraph p = new Paragraph("ORÇAMENTO", FontFactory.GetFont("Times New Roman", 15, Font.BOLD, BaseColor.Black));
					p.Alignment = Element.ALIGN_CENTER;
					doc.Add(p);
					doc.Add(new Paragraph("\n"));

					//Dados dos produtos vendidos
					float[] columnWidths4 = { 3, 2, 2, 2, 2 };
					float[] columnWidths5 = { 3, 2, 2, 2, 2, 2 };

					PdfPTable tableProduct;

					if (removerColunaDesconto)
					{
						tableProduct = new PdfPTable(columnWidths4);
					}
					else
					{
						tableProduct = new PdfPTable(columnWidths5);
					}
					tableProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.TotalWidth = 500f;
					tableProduct.LockedWidth = true;


					PdfPCell cellMetaProduct = new PdfPCell();
					PdfPCell cellDataProduct = new PdfPCell();

					BaseColor myColor = WebColors.GetRgbColor("#00CD00");

					cellMetaProduct.Phrase = new Phrase("Produto", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMetaProduct.BackgroundColor = myColor;
					cellMetaProduct.BorderWidth = 2;
					cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.AddCell(cellMetaProduct);

					cellMetaProduct.Phrase = new Phrase("Unitário", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMetaProduct.BackgroundColor = myColor;
					cellMetaProduct.BorderWidth = 2;
					cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.AddCell(cellMetaProduct);

					cellMetaProduct.Phrase = new Phrase("Quantidade", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMetaProduct.BackgroundColor = myColor;
					cellMetaProduct.BorderWidth = 2;
					cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.AddCell(cellMetaProduct);

					cellMetaProduct.Phrase = new Phrase("Total", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMetaProduct.BackgroundColor = myColor;
					cellMetaProduct.BorderWidth = 2;
					cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.AddCell(cellMetaProduct);

					if (!removerColunaDesconto)
					{
						cellMetaProduct.Phrase = new Phrase("Desconto", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
						cellMetaProduct.BackgroundColor = myColor;
						cellMetaProduct.BorderWidth = 2;
						cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellMetaProduct);
					}

					cellMetaProduct.Phrase = new Phrase("Valor Final", FontFactory.GetFont("Times New Roman", 11, Font.BOLD, BaseColor.Black));
					cellMetaProduct.BackgroundColor = myColor;
					cellMetaProduct.BorderWidth = 2;
					cellMetaProduct.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct.AddCell(cellMetaProduct);

					//@foreach

					foreach (ItemOrcamento item in produtos)
					{
						cellDataProduct.Phrase = new Phrase(item.produto.Descricao);
						cellDataProduct.BorderWidth = 2;
						cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellDataProduct);

						cellDataProduct.Phrase = new Phrase(string.Format("{0:C}", item.produto.ValorPorPessoa + item.acrescimo));
						cellDataProduct.BorderWidth = 2;
						cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellDataProduct);

						cellDataProduct.Phrase = new Phrase(item.quantidade.ToString());
						cellDataProduct.BorderWidth = 2;
						cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellDataProduct);

						cellDataProduct.Phrase = new Phrase(string.Format("{0:C}", ((item.produto.ValorPorPessoa + item.acrescimo) * item.quantidade)));
						cellDataProduct.BorderWidth = 2;
						cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellDataProduct);

						if (!removerColunaDesconto)
						{
							cellDataProduct.Phrase = new Phrase(Convert.ToDecimal(item.percentualDesconto) + "%");
							cellDataProduct.BorderWidth = 2;
							cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
							tableProduct.AddCell(cellDataProduct);
						}

						cellDataProduct.Phrase = new Phrase(string.Format("{0:C}", item.subtotal));
						cellDataProduct.BorderWidth = 2;
						cellDataProduct.HorizontalAlignment = Element.ALIGN_CENTER;
						tableProduct.AddCell(cellDataProduct);
					}
					//#@End Foreach

					doc.Add(tableProduct);

					Paragraph p1 = new Paragraph("INVESTIMENTO", FontFactory.GetFont("Times New Roman", 15, Font.BOLD, BaseColor.Black));
					p1.Alignment = Element.ALIGN_CENTER;
					doc.Add(p1);

					string valorPorExtenso = Uteis.toExtenso(cabecalhoOrcamento.ValorOrcamento);

					Paragraph p2 = new Paragraph(string.Format("{0:C}", cabecalhoOrcamento.ValorOrcamento) + " - " + valorPorExtenso, FontFactory.GetFont("Times New Roman", 15, BaseColor.Black));
					p2.Alignment = Element.ALIGN_CENTER;
					doc.Add(p2);

					int totalParcelas = cabecalhoOrcamento.CondicaoPagamento.Descricao.Split('/').Length;

					string texto = totalParcelas + " X " + string.Format("{0:C}", cabecalhoOrcamento.ValorOrcamento / totalParcelas) + " No Boleto Bancário";

					Paragraph p3 = new Paragraph(texto, FontFactory.GetFont("Times New Roman", 13, Font.BOLD, BaseColor.Black));
					p3.Alignment = Element.ALIGN_CENTER;
					doc.Add(p3);

					texto = "\n*Vale ressaltar que o recolhimento da taxa da ART é de responsabilidade do contratante e o valor para pagamento de R$ 82,94 será entregue em boleto separado. " +
								"\nOBS: Será cobrado 5 % sobre o valor total do investimento referente a despesas acessórias.";

					Paragraph p4 = new Paragraph(texto, FontFactory.GetFont("Times New Roman", 15, BaseColor.Black));
					p4.Alignment = Element.ALIGN_JUSTIFIED;
					doc.Add(p4);

					texto = "I. É de responsabilidade do Cliente:\n" +
						"* Disponibilizar os colaboradores para participação das aulas teórico - práticas.\n" +
						"* Arcar com os custos relativos às taxas geradas junto aos órgãos oficiais e CREA / ART.\n" +
						"* Arcar com os custos das recargas e manutenções dos extintores que por ventura possam ser utilizados no curso.\n" +
						"* Disponibilizar o cofee break e espaço físico para a realização dos treinamentos.\n" +
						"* Disponibilizar toda a documentação necessária para elaboração dos laudos.\n\n" +
						"II. É de responsabilidade Cia do Treinamento:\n" +
						"* Executar os serviços relativos à elaboração, montagem e treinamentos propostos;\n" +
						"* Aplicar Avaliação para os colaboradores.\n" +
						"* Emitir Certificados referente aos cursos ministrados.\n";

					Paragraph p5 = new Paragraph(texto, FontFactory.GetFont("Times New Roman", 15, BaseColor.Black));
					p5.Alignment = Element.ALIGN_JUSTIFIED;
					doc.Add(p5);

					texto = "Serviços disponibilizados pela Cia do Treinamento";

					Paragraph p6 = new Paragraph(texto, FontFactory.GetFont("Times New Roman", 15, BaseColor.Black));
					p6.Alignment = Element.ALIGN_CENTER;
					doc.Add(p6);

					doc.Add(new Paragraph("\n"));

					PdfPTable tableProduct2 = new PdfPTable(1);
					tableProduct2.HorizontalAlignment = Element.ALIGN_CENTER;
					tableProduct2.TotalWidth = 300f;
					tableProduct2.LockedWidth = true;

					PdfPCell cell = new PdfPCell();

					cell.Phrase = new Phrase("NR20");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR20 - Motoristas (24h)");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR20 - Motoristas (32h)");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Treinamento em Segurança do Trabalho, Basico em Meio Ambiente");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NBR-14.276 Brigada de Incêndio - 12 Horas");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NBR-14.276 Brigada de Incêndio - 8 Horas");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Treinamento Benzeno");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR06");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("PPP");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR35");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR33 - 40 Horas");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR33 - 8 Horas");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Certificado Cipa");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR17");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("NR10");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Prontuario NR20");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Plano de Atendimento a Emergencias ou Plano de Resposta a Emergencias");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("LTCAT");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("PPRA");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("PCMSO");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Mapa de Risco");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Pasta Cipa");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("PET - Plano de Emergencia e Transporte");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("PGR - Programa de Gerencimento de Riscos");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("BTXE (Medição)");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Estudo de classificação de áreas");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Laudo de aterramento e SPDA que discorra também sobre potencialização e eletricidade estática");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Laudo de inspeção detalhada das instalações elétricas-Laudo de inspeção não elétrica em atm.explosiv");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Inspeção no vaso de pressão - Acima 500 L");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Inspeção no vaso de pressão - Ate 500 L");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Teste de Estanqueidade");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Layout de planta baixa");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					cell = new PdfPCell();
					cell.Phrase = new Phrase("Dosimetria de ruído");
					cell.BorderWidth = 2;
					tableProduct2.AddCell(cell);

					doc.Add(tableProduct2);

					doc.Close();

					byte[] content = myMemoryStream.ToArray();

					// Write out PDF from memory stream.
					using (FileStream fs = File.Create(caminhoSaida))
					{
						fs.Write(content, 0, (int)content.Length);

						fs.Close();
					}

					myMemoryStream.Close();
				}

			}
			catch (Exception e)
			{
				mensagemErro = e.ToString();
				return false;
			}

			return true;
		}

	}
}
