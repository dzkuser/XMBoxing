using System.Web;
using System.Web.Mvc;
using XMBOXING.Backstage.Controllers;

namespace XMBOXING.Backstage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PowerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
