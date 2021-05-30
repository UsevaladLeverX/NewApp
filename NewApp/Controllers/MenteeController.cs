using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using NewApp.Services.Views;

namespace NewApp.Controllers
{
    public class MenteeController : Controller
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IIndexConfig<IndexMenteeView> indexConfig;
        private readonly ICreateConfig<Mentee, CreateMenteeView> createConfig;
        private readonly IEditConfig<EditMenteeView, Mentee> editConfig;
        private readonly IDeleteConfig<DeleteMenteeView> deleteConfig;
        public MenteeController(IMenteeRepository _imenteeRepository, ILevelRepository _levelRepository,
            IIndexConfig<IndexMenteeView> _indexConfig, 
            ICreateConfig<Mentee, CreateMenteeView> _createConfig,
            IEditConfig<EditMenteeView, Mentee> _editConfig,
            IDeleteConfig<DeleteMenteeView> _deleteConfig)
        {
            this.menteeRepository = _imenteeRepository;
            this.indexConfig = _indexConfig;
            this.levelRepository = _levelRepository;
            this.createConfig = _createConfig;
            this.editConfig = _editConfig;
            this.deleteConfig = _deleteConfig;
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            indexConfig.Config();
            return View();
        }

        public async Task<IActionResult> ShowSearchResultsName(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Index", indexConfig.Config());
            else
                return View("Index", indexConfig.Config().Where(m => m.MenteeName.Contains(SearchLetter)));
        }

        public async Task<IActionResult> ShowSearchResultsPos(string SearchLetter)
        {
            if (SearchLetter == null)
                return View("Index", indexConfig.Config());
            else
                return View("Index", indexConfig.Config().Where(m => m.ViewPos.Contains(SearchLetter)));
        }

        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "name_des" ? "Name" : "name_des";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_des" : "Age";
            ViewBag.PosSortParm = sortOrder == "Pos_des" ? "Position" : "Pos_des";
            var mentee = from s in indexConfig.Config()
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
            ViewBag.Positions = createConfig.Positions();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMenteeView model)
        {
            ViewBag.Positions = createConfig.Positions();
            if (ModelState.IsValid)
            {
                var mentee=createConfig.Config(model);
                menteeRepository.Create(mentee);
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Positions = editConfig.Positions();
            if (id == null)
                return View();
            return View(editConfig.Config_1(id));
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            ViewBag.Positions = editConfig.Positions();
            if (ModelState.IsValid)
            {
                menteeRepository.Update(editConfig.Config_2(model));
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            menteeRepository.Delete(deleteConfig.Config(id).MenteeId);
            menteeRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (editConfig.Config_1(id) == null)
            {
                return NotFound();
            }
            return View(editConfig.Config_1(id));
        }
    }
}
