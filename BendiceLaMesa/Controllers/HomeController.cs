using BendiceLaMesa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BendiceLaMesa.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            OracionesController oracionControler = new OracionesController();
            Oracion frase = oracionControler.GetRandomFrase();
            
            try
            {
                ViewBag.FraseRandom = frase.Texto;
                ViewBag.IdFraseRandom = frase.ID;
                ViewBag.PuntuacionFraseRandowm = frase.Puntuacion;

                ViewBag.Autor = "-Desconocido-";
                if (frase.Autor.Length > 0)
                {
                    ViewBag.Autor = "-" + frase.Autor + "-";
                }
                
            }
            catch
            {
                ViewBag.FraseRandom = "upss!! Ninguna Frase encontrada";
            }

            return View();
        }
    }
}
