using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issuemoa.Controllers
{
    using Models.Sponsorship;
    public class SponsorshipController : Controller
    {
        public int selectedId { get; set; }
        // GET: Sponsorship
        [HttpGet]
        public ActionResult Index(int? itemId)
        {
            //defalut
            selectedId = 1;
            if(itemId.HasValue)
            {
                selectedId = itemId.Value;
            }

            ViewBag.Title = "후원하기";
            return View(new SponsorshipModel(selectedId));
        }
        

        [HttpPost]
        public ActionResult Index(string postAction, SponsorshipModel postedValues)
        {
            if(postedValues.Donate())
                return RedirectToAction("Thanks");
            return View(postedValues);
        }


        [HttpGet]
        public ActionResult Thanks()
        {
            return View(new ThanksModel());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
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

        [HttpGet]
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
