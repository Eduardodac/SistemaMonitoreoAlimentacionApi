using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class CollaresController : Controller
    {
        // GET: CollarController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CollarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollarController/Create
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

        // GET: CollarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CollarController/Edit/5
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

        // GET: CollarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CollarController/Delete/5
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
