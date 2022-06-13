using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanesaMoraga_EVA2.Models;

namespace VanesaMoraga_EVA2.Views.Estadistica
{
    public class EstadisticaController : Controller
    {
        private FabricaDB db = new FabricaDB();
        // GET: Estadistica
        public ActionResult Index()
        {
            return View();
        }
}
}