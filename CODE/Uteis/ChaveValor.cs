using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ChaveValor
    {
		public enum TipoObjDocumento
		{
			NENHUM,
			CELULA,
			CAIXA_TEXTO
		}

		public string Chave { get; set; }
		public string Valor { get; set; }
		public TipoObjDocumento TipoObjeto { get; set; }

		public ChaveValor()
		{
			TipoObjeto = TipoObjDocumento.NENHUM;
		}
		public ChaveValor(string Chave, string Valor)
			: this()
		{
			this.Chave = Chave;
			this.Valor = Valor;
		}

		public ChaveValor(string Chave, string Valor, TipoObjDocumento tipoObjeto)
		{
			this.Chave = Chave;
			this.Valor = Valor;
			TipoObjeto = tipoObjeto;
		}
	}
}
