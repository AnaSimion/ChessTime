using ChessProject.Models;
using static ChessProject.Models.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessProject.Controllers
{
    public class ProducerController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]

        // GET: Producer
        public ActionResult Index()
        {
            ViewBag.Producers = ctx.Producers.ToList();
            return View();
        }
        [Authorize(Roles = "Admin,User")]

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Producer producer = ctx.Producers.Find(id);
                if (producer != null)
                {
                    return View(producer);
                }
                return HttpNotFound("Couldn't find the producer with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing producer id parameter!");
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public ActionResult New()
        {

            ProducerContactViewModel pc = new ProducerContactViewModel();
            return View(pc);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(ProducerContactViewModel pcViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfo contact = new ContactInfo
                    {
                        PhoneNumber = pcViewModel.PhoneNumber,
                        Email = pcViewModel.Email,
                    };
                    // vom adauga in baza de date ambele obiecte 
                    ctx.ContactsInfo.Add(contact);
                    Producer producer = new Producer
                    {
                        Name = pcViewModel.Name,
                        ContactInfo = contact
                    };
                    ctx.Producers.Add(producer);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pcViewModel);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(pcViewModel);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Producer producer = ctx.Producers.Find(id);
            ContactInfo contact = ctx.ContactsInfo.Find(producer.ContactInfo.ContactInfoId);

            if (producer != null)
            {
                ctx.Producers.Remove(producer);
                ctx.ContactsInfo.Remove(contact);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the producer with id " + id.ToString() + "!");
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Producer producer = ctx.Producers.Find(id);

                if (producer == null)
                {
                    return HttpNotFound("Couldn't find the producer with id " + id.ToString() + "!");
                }
                return View(producer);
            }
            return HttpNotFound("Id missing");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Producer producerRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Producer producer = ctx.Producers.Find(id);
                    if (TryUpdateModel(producer))
                    {
                        producer.Name = producerRequestor.Name;
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(producerRequestor);
            }
            catch (Exception e)
            {
                return View(producerRequestor);
            }
        }

    }
}