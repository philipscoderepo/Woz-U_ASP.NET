using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;

namespace SecurityFromScratch.Controllers
{
    public class SecurityController : Controller
    {
        IDataProtectionProvider provider;

        public SecurityController(IDataProtectionProvider DISuppliedProvider)
        {
            provider = DISuppliedProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Protect(Models.SecurityModel postedData)
        {
            postedData.ProtectedText = provider.CreateProtector("Confidentiality").Protect(postedData.PlainText);
            postedData.PlainText = "Data is now encrypted";
            postedData.MethodText = "Protect called...";
            return View("index", postedData);
        }

        [HttpPost]
        public ActionResult Unprotect(Models.SecurityModel postedData)
        {
            postedData.PlainText = provider.CreateProtector("Confidentiality").Unprotect(postedData.ProtectedText);
            postedData.ProtectedText = "";
            postedData.MethodText = "Unprotect called...";
            return View("index", postedData);
        }
    }
}
