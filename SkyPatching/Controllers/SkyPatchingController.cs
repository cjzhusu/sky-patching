using SP.Common.ReturnCode;
using SP.Common.Util;
using System.Web.Mvc;

namespace SkyPatching.Controllers
{
    public class SkyPatchingController : Controller
    {
        // GET: SkyPatching
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetImg()
        {
            int width = 100;
            int height = 38;
            int fontsize = 18;
            string code = string.Empty;
            byte[] bytes = ValidateCodeHelper.CreateValidateGraphic(out code, 4, width, height, fontsize);
            Session["ValidateCode"] = code;
            return File(bytes, @"image/jpeg");
        }

        public ActionResult ValidateCode(string inputCode)
        {
            var packageCode = new Package();
            packageCode.Code = "-1";
            if (Session["ValidateCode"] == null)
            {
                packageCode.Message = "验证码过期!";
            } else
            {
                string sCode = Session["ValidateCode"].ToString();
                if (string.IsNullOrEmpty(inputCode))
                {
                    packageCode.Message = "请输入验证码!";
                }
                else if (inputCode.ToLower() != sCode.ToLower())
                {
                    packageCode.Message = "验证码不正确!";
                }
                else
                {
                    packageCode.Code = "1";
                }
            }

            return Json(packageCode);
        }
    }
}