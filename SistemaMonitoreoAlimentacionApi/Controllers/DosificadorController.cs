using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class DosificadorController : Controller
    {
        // GET: DosificadorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DosificadorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DosificadorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DosificadorController/Create
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

        // GET: DosificadorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DosificadorController/Edit/5
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

        // GET: DosificadorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DosificadorController/Delete/5
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
