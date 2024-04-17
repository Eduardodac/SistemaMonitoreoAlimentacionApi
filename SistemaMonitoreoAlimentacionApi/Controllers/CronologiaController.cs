﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaMonitoreoAlimentacionApi.Controllers
{
    public class CronologiaController : Controller
    {
        // GET: CronologiaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CronologiaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CronologiaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CronologiaController/Create
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

        // GET: CronologiaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CronologiaController/Edit/5
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

        // GET: CronologiaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CronologiaController/Delete/5
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
