using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemRotaBLL
    {
		//INSERT 
		public static int insertItemRota(ItemRota itemRota, out string mensagemErro)
		{
			return ItemRotaDAL.insertItemRota(itemRota, out mensagemErro);
		}

		//UPDATE
		public static bool updateRota(ItemRota itemRota, out string mensagemErro)
		{
			return ItemRotaDAL.updateRota(itemRota, out mensagemErro);
		}

		//DELETE
		public static bool deleteItemRota(int codigoRota, int codigoPedido, out string mensagemErro)
		{
			return ItemRotaDAL.deleteItemRota(codigoRota, codigoPedido, out mensagemErro);
		}

		public static bool deleteItensRota(int codigoRota, out string mensagemErro)
		{
			return ItemRotaDAL.deleteItensRota(codigoRota, out mensagemErro);
		}

		//SELECT
		public static List<ItemRota> selectItensRota(int codigoRota, out string mensagemErro)
		{
			return ItemRotaDAL.selectItensRota(codigoRota, out mensagemErro);
		}
	}
}
