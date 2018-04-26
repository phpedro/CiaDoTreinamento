using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RotaBLL
    {
		//INSERT 
		public static int insertRota(Rota rota, out string mensagemErro)
		{
			return RotaDAL.insertRota(rota, out mensagemErro);
		}

		//UPDATE
		public static bool updateRota(Rota rota, out string mensagemErro)
		{
			return RotaDAL.updateRota(rota, out mensagemErro);
		}

		//DELETE
		public static bool deleteRota(int codigo, out string mensagemErro)
		{
			return ItemRotaDAL.deleteItensRota(codigo, out mensagemErro) && RotaDAL.deleteRota(codigo, out mensagemErro);
		}

		//SELECT
		public static List<Rota> selectRotas(int codigo, out string mensagemErro)
		{
			return RotaDAL.selectRotas(codigo, out mensagemErro);
		}

		public static List<Rota> selectRotasByInstrutor(int? codigoInstrutor, out string mensagemErro)
		{
			return RotaDAL.selectRotasByInstrutor(codigoInstrutor, out mensagemErro);
		}

		public static List<Rota> selectAllRotas(out string mensagemErro)
		{
			return RotaDAL.selectAllRotas(out mensagemErro);
		}

	}
}
