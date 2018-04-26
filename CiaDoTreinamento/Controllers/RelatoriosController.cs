using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CiaDoTreinamento.Controllers
{
    public class RelatoriosController : Controller
    {
        public IActionResult ClientesNaoAtendidos()
        {
            return View();
        }
    }
}