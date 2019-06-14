using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XMBOXING.Backstage.Controllers
{
    public class BaseController : Controller
    {

        protected override IActionInvoker CreateActionInvoker()
        {
            return new MyActionInvoker();
        }

    }
}