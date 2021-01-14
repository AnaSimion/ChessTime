using ChessProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessProject.Controllers
{
    public class MaterialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]

        public ActionResult Index()
        {
            ViewBag.Materials = db.Materials.ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            Material material = new Material();
            return View(material);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Material materialRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Materials.Add(materialRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(materialRequest);
            }
            catch (Exception e)
            {
                return View(materialRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Material material = db.Materials.Find(id);

                if (material == null)
                {
                    return HttpNotFound("Couldn't find the material with id " + id.ToString() + "!");
                }
                return View(material);
            }
            return HttpNotFound("Couldn't find the material with id " + id.ToString() + "!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Material materialRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Material material = db.Materials.Find(id);
                    if (TryUpdateModel(material))
                    {
                        material.Name = materialRequestor.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(materialRequestor);
            }
            catch (Exception e)
            {
                return View(materialRequestor);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Material material = db.Materials.Find(id);
                if (material != null)
                {
                    db.Materials.Remove(material);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the material with id " + id.ToString() + "!");
            }
            return HttpNotFound("Material id parameter is missing!");
        }
    }
}