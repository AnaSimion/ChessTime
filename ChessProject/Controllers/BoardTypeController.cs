using ChessProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ChessProject.Models.ApplicationUser;

namespace ChessProject.Controllers
{
    public class BoardTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]

        public ActionResult Index()
        {
            ViewBag.BoardTypes = db.BoardTypes.ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            BoardType boardType = new BoardType();
            return View(boardType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(BoardType boardTypeRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.BoardTypes.Add(boardTypeRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(boardTypeRequest);
            }
            catch (Exception e)
            {
                return View(boardTypeRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                BoardType boardType = db.BoardTypes.Find(id);

                if (boardType == null)
                {
                    return HttpNotFound("Couldn't find the board type with id " + id.ToString() + "!");
                }
                return View(boardType);
            }
            return HttpNotFound("Couldn't find the board type with id " + id.ToString() + "!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, BoardType boardTypeRequestor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BoardType boardType = db.BoardTypes.Find(id);
                    if (TryUpdateModel(boardType))
                    {
                        boardType.Name = boardTypeRequestor.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(boardTypeRequestor);
            }
            catch (Exception e)
            {
                return View(boardTypeRequestor);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                BoardType boardType = db.BoardTypes.Find(id);
                if (boardType != null)
                {
                    db.BoardTypes.Remove(boardType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound("Couldn't find the board type with id " + id.ToString() + "!");
            }
            return HttpNotFound("Board type id parameter is missing!");
        }
    }
}