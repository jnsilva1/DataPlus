﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataPlusMVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return RedirectToAction("Index");
        }
    }
}