using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLaunch.DAO;
using NewLaunch.Models;

namespace NewLaunch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DAO_Vote restaurante = new DAO_Vote();
            Restaurant rest = restaurante.GetTheMostVotedbyDay();
            return View(rest);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Um facilitador para a escolha mais importante da vida até o meio-dia!";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Márcia Abech";

            return View();
        }
    }
}