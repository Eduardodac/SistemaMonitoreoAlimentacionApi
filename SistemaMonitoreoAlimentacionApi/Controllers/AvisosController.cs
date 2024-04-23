using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class AvisosController : Controller
    {
        // GET: AvisoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AvisoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AvisoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvisoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvisoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AvisoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvisoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AvisoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
