using ChessProject.Models;
using static ChessProject.Models.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessProject.Controllers
{
    public class ContactInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]

        public ActionResult Index()
        {
            ViewBag.ContactInfos = db.ContactsInfo.ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public ActionResult New()
        {
            ContactInfo contact = new ContactInfo();
            return View(contact);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult New(ContactInfo contactRequest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.ContactsInfo.Add(contactRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ContactInfo");
                }
                return View(contactRequest);
            }
            catch (Exception e)
            {
                return View(contactRequest);
            }
        }
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                ContactInfo contact = db.ContactsInfo.Find(id);

                if (contact == null)
                {
                    return HttpNotFound("Couldn't find the contact with id " + id.ToString() + "!");
                }
                return View(contact);
            }
            return HttpNotFound("Couldn't find the contact with id " + id.ToString() + "!");
        }
        [Authorize(Roles = "Admin")]

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, ContactInfo contactRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfo contact = db.ContactsInfo.Find(id);
                    if (TryUpdateModel(contact))
                    {
                        contact.PhoneNumber = contactRequestor.PhoneNumber;
                        contact.Email = contactRequestor.Email;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(contactRequestor);
            }
            catch (Exception e)
            {
                return View(contactRequestor);
            }
        }
        [Authorize(Roles = "Admin")]


        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                ContactInfo contact = db.ContactsInfo.Find(id);
                if (contact != null)
                {
                    db.ContactsInfo.Remove(contact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the contact with id " + id.ToString() + "!");
            }
            return HttpNotFound("Contact id parameter is missing!");
        }
    }
}


