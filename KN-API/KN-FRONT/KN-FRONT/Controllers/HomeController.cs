using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_FRONT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Principal()
        {
            /*LLAMAR AL API PARA VALIDAR LOS CREDENCIALES*/

            return View();
        }

    }
}