using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tfs_dashboard.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult NavigationBar()
        {
            return View();
        }

        public ActionResult ConnectionModal()
        {
            return View();
        }

        public ActionResult TaskPopover()
        {
            return View();
        }

        public ActionResult ConfigurationModal()
        {
            return View();
        }
    }
}
