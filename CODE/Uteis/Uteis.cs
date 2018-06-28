using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CODE
{
    public class Uteis
    {
		private static string MapPath = "/CiaTreinamentoNovo";

		/// <summary>
		/// Calcula o Hash MD5 de uma string
		/// </summary>
		/// <param name="strEntrada">String para calcular o hash</param>
		/// <returns>O hash MD5 da string</returns>
		public static string GeraHashMD5(string strEntrada)
		{
			if (String.IsNullOrEmpty(strEntrada))
			{
				return "";
			}

			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(strEntrada));

				StringBuilder sBuilder = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				return sBuilder.ToString();
			}
		}

		public static string SendMail(string name, string emailTo, string title, string conteudo)
		{
			if (!String.IsNullOrEmpty(emailTo))
			{
				//String host = ConfigurationManager.AppSettings["host"].ToString();
				//String porta = ConfigurationManager.AppSettings["port"].ToString();
				//String user = ConfigurationManager.AppSettings["user"].ToString();
				//String password = ConfigurationManager.AppSettings["password"].ToString();
				//String ssl = ConfigurationManager.AppSettings["ssl"].ToString();

				String host = "email-ssl.com.br";
				String porta = "587";
				String user = "ciacorreios@ciadotreinamento.com";
				String password = "treinamento2019";
				String ssl = "false";

				MailMessage message = new MailMessage();

				message.From = new MailAddress(user, name);

				string[] emails = emailTo.Split(';');
				foreach (string email in emails)
				{
					if (!String.IsNullOrEmpty(email.Trim()))
					{
						message.To.Add(email.Trim());
					}
				}

				message.Subject = title;
				message.IsBodyHtml = true;

				message.Body = conteudo;

				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Credentials = new System.Net.NetworkCredential(user, password);
				smtpClient.Port = Convert.ToInt32(porta);
				smtpClient.Host = host;
				smtpClient.EnableSsl = Convert.ToBoolean(ssl);

				try
				{
					smtpClient.Send(message);
					return null;
				}
				catch (Exception ex)
				{
					string msgErro = ex.Message.ToString() + "<br>";

					if (ex.InnerException != null)
					{
						msgErro = msgErro + " - Inner Exception: " + ex.InnerException.ToString() + "<br>";

						if (ex.InnerException.Message != null)
						{
							msgErro = msgErro + " - Inner Exception Message: " + ex.InnerException.Message.ToString() + "<br>";
						}
					}

					return msgErro;
				}
			}
			else
			{
				return "Não foi possível localizar o email de destino.";
			}
		}

		public static string SendMailRoteirizacao(string name, string emailTo, string title, string conteudo, string emailSmtp)
		{
			if (!String.IsNullOrEmpty(emailTo))
			{
				String host = "email-ssl.com.br";
				String porta = "587";
				String user = "roteirizacao@ciadotreinamento.com.br";
				String password = "treinamento@2018";
				String ssl = "false";

				if (!String.IsNullOrEmpty(emailSmtp))
				{
					user = emailSmtp;

					if (emailSmtp == "marcospaulo@ciadotreinamento.com.br")
					{	
						password = "neto0589";
					}
					else
					{
						password = "treinamento2019";
					}
				}

				MailMessage message = new MailMessage();

				message.From = new MailAddress(user, name);

				string[] emails = emailTo.Split(';');
				foreach (string email in emails)
				{
					if (!String.IsNullOrEmpty(email.Trim()))
					{
						message.To.Add(email.Trim());
					}
				}

				message.Subject = title;
				message.IsBodyHtml = true;

				message.Body = conteudo;

				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Credentials = new System.Net.NetworkCredential(user, password);
				smtpClient.Port = Convert.ToInt32(porta);
				smtpClient.Host = host;
				smtpClient.EnableSsl = Convert.ToBoolean(ssl);

				try
				{
					smtpClient.Send(message);
					return null;
				}
				catch (Exception ex)
				{
					string msgErro = ex.Message.ToString() + "<br>";

					if (ex.InnerException != null)
					{
						msgErro = msgErro + " - Inner Exception: " + ex.InnerException.ToString() + "<br>";

						if (ex.InnerException.Message != null)
						{
							msgErro = msgErro + " - Inner Exception Message: " + ex.InnerException.Message.ToString() + "<br>";
						}
					}

					return msgErro;
				}
			}
			else
			{
				return "Não foi possível localizar o email de destino.";
			}
		}

		public static string SendMailLogSistema(string title, string conteudo)
		{
			String host = "email-ssl.com.br";
			String porta = "587";
			String user = "errosistema@ciadotreinamento.com";
			String password = "sistema@cia2017";
			String ssl = "false";

			MailMessage message = new MailMessage();

			message.From = new MailAddress(user, "Erro Sistema CIA");

			message.To.Add("errosistema@ciadotreinamento.com");
			message.To.Add("lukscamilo@live.com");

			message.Subject = title;
			message.IsBodyHtml = true;

			message.Body = conteudo;

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Credentials = new System.Net.NetworkCredential(user, password);
			smtpClient.Port = Convert.ToInt32(porta);
			smtpClient.Host = host;
			smtpClient.EnableSsl = Convert.ToBoolean(ssl);

			try
			{
				smtpClient.Send(message);
				return null;
			}
			catch (Exception ex)
			{
				string msgErro = ex.Message.ToString() + "<br>";

				if (ex.InnerException != null)
				{
					msgErro = msgErro + " - Inner Exception: " + ex.InnerException.ToString() + "<br>";

					if (ex.InnerException.Message != null)
					{
						msgErro = msgErro + " - Inner Exception Message: " + ex.InnerException.Message.ToString() + "<br>";
					}
				}

				return msgErro;
			}
		}

		public static bool GravarLogErro(string title, string mensagem)
		{

			//using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Sites\CiaTreinamentoNovo\LogErros.txt"))
			//{
			//	file.WriteLine(title + " - " + mensagem + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

			//	SendMailLogSistema(title, mensagem);
			//}

			return true;
		}

		public static List<SelectListItem> RetornarEstadosComboCompleto()
		{

			List<SelectListItem> estados = new List<SelectListItem>();

			estados.Add(new SelectListItem() { Value = "AC", Text = "ACRE" });
			estados.Add(new SelectListItem() { Value = "AL", Text = "ALAGOAS" });
			estados.Add(new SelectListItem() { Value = "AM", Text = "AMAZONAS" });
			estados.Add(new SelectListItem() { Value = "AP", Text = "AMAPÁ" });
			estados.Add(new SelectListItem() { Value = "BA", Text = "BAHIA" });
			estados.Add(new SelectListItem() { Value = "CE", Text = "CEARA" });
			estados.Add(new SelectListItem() { Value = "DF", Text = "DISTRITO FEDERAL" });
			estados.Add(new SelectListItem() { Value = "ES", Text = "ESPITO SANTO" });
			estados.Add(new SelectListItem() { Value = "GO", Text = "GOIÁS" });
			estados.Add(new SelectListItem() { Value = "MA", Text = "MARANHÃO" });
			estados.Add(new SelectListItem() { Value = "MG", Text = "MINAS GERAIS" });
			estados.Add(new SelectListItem() { Value = "MS", Text = "MATO GROSSO DO SUL" });
			estados.Add(new SelectListItem() { Value = "MT", Text = "MATO GROSSO" });
			estados.Add(new SelectListItem() { Value = "PA", Text = "PARÁ" });
			estados.Add(new SelectListItem() { Value = "PB", Text = "PARAÍBA" });
			estados.Add(new SelectListItem() { Value = "PE", Text = "PERNAMBUCO" });
			estados.Add(new SelectListItem() { Value = "PI", Text = "PIAUÍ" });
			estados.Add(new SelectListItem() { Value = "PR", Text = "PARANÁ" });
			estados.Add(new SelectListItem() { Value = "RJ", Text = "RIO DE JANEITO" });
			estados.Add(new SelectListItem() { Value = "RN", Text = "RIO GRANDE DO NORTE" });
			estados.Add(new SelectListItem() { Value = "RO", Text = "RONDÔNIA" });
			estados.Add(new SelectListItem() { Value = "RR", Text = "RORAIMA" });
			estados.Add(new SelectListItem() { Value = "RS", Text = "RIO GRANDE DO SUL" });
			estados.Add(new SelectListItem() { Value = "SC", Text = "SANTA CATARINA" });
			estados.Add(new SelectListItem() { Value = "SE", Text = "SERGIPE" });
			estados.Add(new SelectListItem() { Value = "SP", Text = "SÃO PAULO" });
			estados.Add(new SelectListItem() { Value = "TO", Text = "TOCANTINS" });

			return estados;

		}

		//Números por extenso
		//Créditos ao autor: https://ivanmeirelles.wordpress.com/2012/10/27/escrever-valores-por-extenso-em-c/
		// O método toExtenso recebe um valor do tipo decimal
		public static string toExtenso(decimal valor)
		{
			if (valor <= 0 | valor >= 1000000000000000)
				return "Valor não suportado pelo sistema.";
			else
			{
				string strValor = valor.ToString("000000000000000.00");
				string valor_por_extenso = string.Empty;

				for (int i = 0; i <= 15; i += 3)
				{
					valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
					if (i == 0 & valor_por_extenso != string.Empty)
					{
						if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
							valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
						else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
							valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
					}
					else if (i == 3 & valor_por_extenso != string.Empty)
					{
						if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
							valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
						else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
							valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
					}
					else if (i == 6 & valor_por_extenso != string.Empty)
					{
						if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
							valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
						else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
							valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
					}
					else if (i == 9 & valor_por_extenso != string.Empty)
						if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
							valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

					if (i == 12)
					{
						if (valor_por_extenso.Length > 8)
							if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
								valor_por_extenso += " DE";
							else
								if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
								valor_por_extenso += " DE";
							else
									if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
								valor_por_extenso += " DE";

						if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
							valor_por_extenso += " REAL";
						else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
							valor_por_extenso += " REAIS";

						if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
							valor_por_extenso += " E ";
					}

					if (i == 15)
						if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
							valor_por_extenso += " CENTAVO";
						else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
							valor_por_extenso += " CENTAVOS";
				}
				return valor_por_extenso;
			}
		}

		static string escreva_parte(decimal valor)
		{
			if (valor <= 0)
				return string.Empty;
			else
			{
				string montagem = string.Empty;
				if (valor > 0 & valor < 1)
				{
					valor *= 100;
				}
				string strValor = valor.ToString("000");
				int a = Convert.ToInt32(strValor.Substring(0, 1));
				int b = Convert.ToInt32(strValor.Substring(1, 1));
				int c = Convert.ToInt32(strValor.Substring(2, 1));

				if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
				else if (a == 2) montagem += "DUZENTOS";
				else if (a == 3) montagem += "TREZENTOS";
				else if (a == 4) montagem += "QUATROCENTOS";
				else if (a == 5) montagem += "QUINHENTOS";
				else if (a == 6) montagem += "SEISCENTOS";
				else if (a == 7) montagem += "SETECENTOS";
				else if (a == 8) montagem += "OITOCENTOS";
				else if (a == 9) montagem += "NOVECENTOS";

				if (b == 1)
				{
					if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
					else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
					else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
					else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
					else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
					else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
					else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
					else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
					else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
					else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
				}
				else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
				else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
				else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
				else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
				else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
				else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
				else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
				else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

				if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

				if (strValor.Substring(1, 1) != "1")
					if (c == 1) montagem += "UM";
					else if (c == 2) montagem += "DOIS";
					else if (c == 3) montagem += "TRÊS";
					else if (c == 4) montagem += "QUATRO";
					else if (c == 5) montagem += "CINCO";
					else if (c == 6) montagem += "SEIS";
					else if (c == 7) montagem += "SETE";
					else if (c == 8) montagem += "OITO";
					else if (c == 9) montagem += "NOVE";

				return montagem;
			}
		}

	}
}

