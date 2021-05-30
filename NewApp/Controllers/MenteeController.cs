using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using NewApp.Services.Views;
using NewApp.Services.Views.IMapper;

namespace NewApp.Controllers
{
    public class MenteeController : Controller
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IMapper myMapper;
        public MenteeController(IMenteeRepository _imenteeRepository, ILevelRepository _levelRepository,
            IMapper _myMapper)
        {
            this.menteeRepository = _imenteeRepository;
            this.levelRepository = _levelRepository;
            this.myMapper = _myMapper;
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            myMapper.IndexMenteeConfig();
            return View();
        }

        public async Task<IActionResult> ShowSearchResultsName(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Index", myMapper.IndexMenteeConfig());
            else
                return View("Index", myMapper.IndexMenteeConfig().Where(m => m.MenteeName.Contains(SearchLetter)));
        }

        public async Task<IActionResult> ShowSearchResultsPos(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Index", myMapper.IndexMenteeConfig());
            else
                return View("Index", myMapper.IndexMenteeConfig().Where(m => m.ViewPos.Contains(SearchLetter)));
        }

        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "name_des" ? "Name" : "name_des";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_des" : "Age";
            ViewBag.PosSortParm = sortOrder == "Pos_des" ? "Position" : "Pos_des";
            var mentee = from s in myMapper.IndexMenteeConfig()
                         select s;
            switch (sortOrder)
            {
                case "Name":
                    ViewBag.Name = " ▲";
                    mentee = mentee.OrderBy(s => s.MenteeName);
                    break;
                case "name_des":
                    ViewBag.Name = " ▼";
                    mentee = mentee.OrderByDescending(s => s.MenteeName);
                    break;
                case "Age":
                    ViewBag.Age = " ▲";
                    mentee = mentee.OrderBy(s => s.Age);
                    break;
                case "Age_des":
                    ViewBag.Age = " ▼";
                    mentee = mentee.OrderByDescending(s => s.Age);
                    break;
                case "Position":
                    ViewBag.Pos = " ▲";
                    mentee = mentee.OrderBy(s => s.Position);
                    break;
                case "Pos_des":
                    ViewBag.Pos = " ▼";
                    mentee = mentee.OrderByDescending(s => s.Position);
                    break;
                default:
                    mentee = mentee.OrderBy(s => s.MenteeId);
                    break;
            }
            return View(mentee.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Positions = myMapper.ListOfPositions();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMenteeView model)
        {
            ViewBag.Positions = myMapper.ListOfPositions();
            if (ModelState.IsValid)
            {
                menteeRepository.Create(myMapper.CreateMenteeConfig(model));
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Positions = myMapper.ListOfPositions();
            if (id == null)
                return View();
            return View(myMapper.EditMenteeConfig_1(id));
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            ViewBag.Positions = myMapper.ListOfPositions();
            if (ModelState.IsValid)
            {
                menteeRepository.Update(myMapper.EditMenteeConfig_2(model));
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            menteeRepository.Delete(myMapper.DeleteMenteeConfig(id).MenteeId);
            menteeRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (myMapper.EditMenteeConfig_1(id) == null)
            {
                return NotFound();
            }
            return View(myMapper.EditMenteeConfig_1(id));
        }
    }
}
