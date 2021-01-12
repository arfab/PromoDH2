using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PromoDH.Controllers
{
    public class BasesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}