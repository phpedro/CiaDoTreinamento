using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace CiaDoTreinamento.BancoDados
{
    public class DbCommandGeneric
    {
        public DbCommand DbCommand { get; set; }
        public Object TableObject { get; set; }
    }
}
