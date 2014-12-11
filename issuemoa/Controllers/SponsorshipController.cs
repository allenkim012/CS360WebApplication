using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issuemoa.Controllers
{
    public class SponsorshipController : Controller
    {
        // GET: Sponsorship
        public ActionResult Index()
        {
            ViewBag.Title = "후원하기";
            return View();
        }

        // GET: Sponsorship/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sponsorship/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sponsorship/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sponsorship/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sponsorship/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sponsorship/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sponsorship/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
