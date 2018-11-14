using Colmart.Model_Manager;
using Colmart.View_Models;
using System.Web.Mvc;

namespace Colmart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            return View();
        }

        public ActionResult HowToOrder()
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult ReturnPolicy()
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult _HomeCarousel()
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            var clsHomeCarousel = new clsHomeCarousel();
            var clsProductsManager = new clsProductsManager();
            clsHomeCarousel.lstTShirts = clsProductsManager.getLimitedProductsByCategoryID(6);
            clsHomeCarousel.lstBottoms = clsProductsManager.getLimitedProductsByCategoryID(10);
            clsHomeCarousel.lstJackets = clsProductsManager.getLimitedProductsByCategoryID(7);
            clsHomeCarousel.lstFormal = clsProductsManager.getLimitedProductsByCategoryID(5);
            return PartialView(clsHomeCarousel);
        }

    }
}