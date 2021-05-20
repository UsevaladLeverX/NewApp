using Microsoft.Extensions.Logging;
using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NewApp.Services.Views;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages.Html;
using System.Globalization;
using PagedList;
using NewApp.Services.Views.DI;

namespace NewApp.Controllers
{
    public class HomeController : Controller
    {
        private MenteeIndexDI menteeIndexDI;
        private MenteeCreateDI menteeCreateDI;
        private MenteeEditDI menteeEditDI;
        private MenteeDeleteDI menteeDeleteDI;
        private MenteeDetailsDI menteeDetailsDI;
        private LevelIndexDI levelIndexDI;
        private LevelCreateDI levelCreateDI;
        private LevelEditDI levelEditDI;
        private LevelDeleteDI levelDeleteDI;
        private LevelDetailsDI levelDetailsDI;
        private UnitOfWork unitOfWork;
        public HomeController()
        {
            unitOfWork = new UnitOfWork();
            menteeIndexDI = new MenteeIndexDI();
            menteeCreateDI = new MenteeCreateDI();
            menteeEditDI = new MenteeEditDI();
            menteeDeleteDI = new MenteeDeleteDI();
            menteeDetailsDI = new MenteeDetailsDI();
            levelIndexDI = new LevelIndexDI();
            levelCreateDI = new LevelCreateDI();
            levelEditDI = new LevelEditDI();
            levelDeleteDI = new LevelDeleteDI();
            levelDetailsDI = new LevelDetailsDI();
        }

        public ActionResult Mentee(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name_des" ? "Name" : "name_des";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_des" : "Age";
            ViewBag.PosSortParm = sortOrder == "Pos_des" ? "Position" : "Pos_des";
           
            var mentee = from s in menteeIndexDI.MakeViewPos()
                         select s;
            switch (sortOrder)
            {
                case "Name":
                    ViewBag.ArrowName = " ▲";
                    mentee = mentee.OrderBy(s => s.MenteeName);
                    break;
                case "name_des":
                    ViewBag.ArrowName = " ▼";
                    mentee = mentee.OrderByDescending(s => s.MenteeName);
                    break;
                case "Age":
                    ViewBag.ArrowAge = " ▲";
                    mentee = mentee.OrderBy(s => s.Age);
                    break;
                case "Age_des":
                    ViewBag.ArrowAge = " ▼";
                    mentee = mentee.OrderByDescending(s => s.Age);
                    break;
                case "Position":
                    ViewBag.ArrowPosition = " ▲";
                    mentee = mentee.OrderBy(s => s.ViewPos);
                    break;
                case "Pos_des":
                    ViewBag.ArrowPosition = " ▼";
                    mentee = mentee.OrderByDescending(s => s.ViewPos);
                    break;
                default:
                    mentee = mentee.OrderBy(s => s.MenteeId);
                    break;
            }
            return View(mentee.ToList());
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            menteeIndexDI.MakeViewPos();
            return View();
        }

        public async Task<IActionResult> ShowSearchResultsName(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Mentee", menteeIndexDI.MakeViewPos());
            else
                return View("Mentee", menteeIndexDI.MakeViewPos().Where(m => m.MenteeName.Contains(SearchLetter)));
        }

        public async Task<IActionResult> ShowSearchResultsPos(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Mentee", menteeIndexDI.MakeViewPos());           
            else
                return View("Mentee", menteeIndexDI.MakeViewPos().Where(m => m.ViewPos.Contains(SearchLetter)));
        }
        
        public IActionResult Create()
        {
            ViewBag.Positions = menteeCreateDI.MakeViewPos();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMenteeView model)
        {
            ViewBag.Positions = menteeCreateDI.MakeViewPos();
            if (ModelState.IsValid)
            {
                unitOfWork.Mentee.Create(menteeCreateDI.Configure(model, null));
                unitOfWork.Save();
                return RedirectToAction("Mentee");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Positions = menteeEditDI.MakeViewPos();
            if (id == null)
                return View();
            return View(menteeEditDI.Configure(null, id));
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            ViewBag.Positions = menteeEditDI.MakeViewPos();
            if (ModelState.IsValid)
            {
                unitOfWork.Mentee.Update(menteeEditDI.ConfigureMentee(model));
                unitOfWork.Save();
                return RedirectToAction("Mentee");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            unitOfWork.Mentee.Delete(menteeDeleteDI.Configure(id).MenteeId);
            unitOfWork.Save();
            return RedirectToAction(nameof(Mentee));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (menteeDetailsDI.ViewPos(id) == null)
            {
                return NotFound();
            }
            return View(menteeDetailsDI.ViewPos(id));
        }

        public IActionResult Level()
        {
            return View(levelIndexDI.Configure());
        }

        public IActionResult CreateLevel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLevel(CreateLevelView model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Level.Create(levelCreateDI.Configure(model, null));
                unitOfWork.Save();
                return RedirectToAction("Level");
            }
            return View();
        }

        public ActionResult EditLevel(int? id)
        {
            if (id == null)
                return View();
            return View(levelEditDI.Configure(null, id));
        }
        [HttpPost]
        public ActionResult EditLevel(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Level.Update(levelEditDI.ConfigureLevel(model));
                unitOfWork.Save();
                return RedirectToAction("Level");
            }
            return View(model);
        }

        public IActionResult DeleteLevel(int id)
        {
            unitOfWork.Level.Delete(levelDeleteDI.Configure(id).LevelId);
            unitOfWork.Save();
            return RedirectToAction(nameof(Level));
        }

        public async Task<IActionResult> DetailsLevel(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (levelDetailsDI.Configure(id) == null)
            {
                return NotFound();
            }
            return View(levelDetailsDI.Configure(id));
        }
               
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
