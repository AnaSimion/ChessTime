using ChessProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ChessProject.Models.ApplicationUser;

namespace ChessProject.Controllers
{
    public class BoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public ActionResult Index()
        {
            List<Board> boards = db.Boards.ToList();
            ViewBag.Boards = boards;
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Board board = db.Boards.Find(id);
                if (board != null)
                {
                    return View(board);
                }
                return HttpNotFound("Couldn't find the board with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing board id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Board board = new Board();
            board.BoardTypeList = GetAllBoardTypes();
            board.ProducerList = GetAllProducers();
            board.MaterialsList = GetAllMaterials();
            board.Materials = new List<Material>();
            return View(board);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Board boardRequest)
        {
            boardRequest.BoardTypeList = GetAllBoardTypes();
            boardRequest.ProducerList = GetAllProducers();

            // memoram intr-o lista doar genurile care au fost selectate
            var selectedMaterials = boardRequest.MaterialsList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    boardRequest.Materials = new List<Material>();
                    for (int i = 0; i < selectedMaterials.Count(); i++)
                    {
                        // cartii pe care vrem sa o adaugam in baza de date ii 
                        // asignam genurile selectate 
                        Material material = db.Materials.Find(selectedMaterials[i].Id);
                        boardRequest.Materials.Add(material);
                    }
                    db.Boards.Add(boardRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(boardRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(boardRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Board board = db.Boards.Find(id);
                board.BoardTypeList = GetAllBoardTypes();
                board.ProducerList = GetAllProducers();
                board.MaterialsList = GetAllMaterials();

                foreach (Material checkedMaterial in board.Materials)
                {   // iteram prin genurile care erau atribuite cartii inainte de momentul accesarii formularului
                    // si le selectam/bifam  in lista de checkbox-uri
                    board.MaterialsList.FirstOrDefault(g => g.Id == checkedMaterial.MaterialId).Checked = true;
                }
                if (board == null)
                {
                    return HttpNotFound("Coludn't find the board with id " + id.ToString() + "!");
                }
                return View(board);
            }
            return HttpNotFound("Missing board id parameter!");
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Board boardRequest)
        {
            boardRequest.BoardTypeList = GetAllBoardTypes();
            boardRequest.ProducerList = GetAllProducers();

            // preluam cartea pe care vrem sa o modificam din baza de date
            Board board = db.Boards.Include("Producer").Include("BoardType")
                        .SingleOrDefault(b => b.BoardId.Equals(id));

            // memoram intr-o lista doar genurile care au fost selectate din formular
            var selectedMaterials = boardRequest.MaterialsList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(board))
                    {
                        board.Name = boardRequest.Name;
                        board.Owner = boardRequest.Owner;
                        board.Description = boardRequest.Description;
                        board.Price = boardRequest.Price;
                        board.Year = boardRequest.Year;
                        board.ProducerId = boardRequest.ProducerId;
                        board.BoardTypeId = boardRequest.BoardTypeId;

                        board.Materials.Clear();
                        board.Materials = new List<Material>();

                        for (int i = 0; i < selectedMaterials.Count(); i++)
                        {
                            // cartii pe care vrem sa o editam ii asignam genurile selectate 
                            Material material = db.Materials.Find(selectedMaterials[i].Id);
                            board.Materials.Add(material);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(boardRequest);
            }
            catch (Exception)
            {
                return View(boardRequest);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Board board = db.Boards.Find(id);
            if (board != null)
            {
                db.Boards.Remove(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the board with id " + id.ToString() + "!");
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllMaterials()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var material in db.Materials.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = material.MaterialId,
                    Name = material.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllBoardTypes()
        {
            var selectList = new List<SelectListItem>();
            foreach (var cover in db.BoardTypes.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = cover.BoardTypeId.ToString(),
                    Text = cover.Name
                });
            }
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllProducers()
        {
            var selectList = new List<SelectListItem>();
            foreach (var producer in db.Producers.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = producer.ProducerId.ToString(),
                    Text = producer.Name
                });
            }
            return selectList;
        }
    }
}