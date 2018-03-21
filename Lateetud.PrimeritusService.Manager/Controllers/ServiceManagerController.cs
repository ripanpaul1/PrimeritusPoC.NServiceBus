using Lateetud.PrimeritusService.Manager.Models;
using Lateetud.PrimeritusService.Manager.LibClasses;
using System.Web;
using System.Web.Mvc;
using Lateetud.Utilities.Models;
using System.Collections.Generic;

namespace Lateetud.PrimeritusService.Manager.Controllers
{
    public class ServiceManagerController : Controller
    {
        #region Excel Service
        public ActionResult ExcelService()
        {
            ViewBag.Title = "Excel Service";
            return View(new VMExcel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcelService(HttpPostedFileBase file, string SheetName)
        {
            ViewBag.Title = "Excel Service";
            return View("ExcelService", new LateetudService().ExcelToXml(file, SheetName, Server.MapPath("~/"), "TempFiles", "XmlFiles"));
        }
        #endregion

        #region Xml Service
        public ActionResult XmlService()
        {
            ViewBag.Title = "Simulator - Future State Prototype using NServiceBus";
            return View(new List<FileModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XmlService(HttpPostedFileBase[] files)
        {
            ViewBag.Title = "Simulator - Future State Prototype using NServiceBus";
            List<FileModel> fileModel = null;
            if (files[0] != null) fileModel = new LateetudService().ReadXml(files, Server.MapPath("~/TempFiles"));
            return View("XmlService", fileModel);
        }
        #endregion
    }
}