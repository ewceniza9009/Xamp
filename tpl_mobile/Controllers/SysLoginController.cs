using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tpl_mobile.Controllers
{
    [RoutePrefix("api")]
    public class SysLoginController : Controller
    {
        private Data.tplDataContext tpl;

        // GET: SysLogin
        public ActionResult Index()
        {
            
            return View();
        }

        [Route("login/{user}/{pass}")]
        public JsonResult login(string user, string pass) {
            tpl = new Data.tplDataContext();
            var canLogin = false;

            var password = tpl.sysUsers.Where(x => x.username == user);

            if (pass.Count() > 0) {
                if (password.FirstOrDefault().password == pass) {
                    canLogin = true;
                }
            }

            return Json(canLogin, JsonRequestBehavior.AllowGet);
        }
    }
}