using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XMBOXING.Backstage.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {

            List<string> list = new List<string>();
            list.Add("主页");
            list.Add("功能管理");
            list.Add("权限管理");
            ViewBag.data = list;
            return View();
        }

        public ActionResult Function() {
            return View();
        }


        public ActionResult TreeView() {


            return View();
        }
    }
}