using System.Web;
using System.Web.Mvc;

namespace starteAlkemy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerifySession());


        }
    }
}
