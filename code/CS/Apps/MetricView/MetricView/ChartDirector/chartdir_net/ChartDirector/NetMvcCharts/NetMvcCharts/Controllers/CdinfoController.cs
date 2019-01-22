using System.Web.Mvc;
using ChartDirector;

namespace WebApplication1.Controllers
{
    public class CdinfoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Description = Chart.getDescription();
            ViewBag.MajorVersion = (Chart.getVersion() & 0xff000000) / 0x1000000;
            ViewBag.MinorVersion = (Chart.getVersion() & 0xff0000) / 0x10000;
            ViewBag.MicroVersion = Chart.getVersion() & 0xffff;
            ViewBag.Copyright = Chart.getCopyright();
            ViewBag.BootLog = Chart.getBootLog();
            ViewBag.FontTest = Chart.libgTTFTest();

            return View();
        }
    }
}