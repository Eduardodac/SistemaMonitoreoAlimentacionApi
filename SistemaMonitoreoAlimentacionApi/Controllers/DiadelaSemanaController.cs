using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class DiadelaSemanaController : Controller
    {
        // GET: DiadelaSemanaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DiadelaSemanaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiadelaSemanaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiadelaSemanaController/Create
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

        // GET: DiadelaSemanaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiadelaSemanaController/Edit/5
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

        // GET: DiadelaSemanaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiadelaSemanaController/Delete/5
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
