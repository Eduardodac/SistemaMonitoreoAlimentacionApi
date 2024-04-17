using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class GatoController : Controller
    {
        // GET: GatoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GatoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GatoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GatoController/Create
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

        // GET: GatoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GatoController/Edit/5
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

        // GET: GatoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GatoController/Delete/5
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
