using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento.BancoDados
{
    public class Connection
    {

        public static DbTransaction Transaction { get; set; }
        public static DbCommand DbCommand { get; set; }
    }
}
