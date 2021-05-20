using Microsoft.Extensions.Logging;
using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NewApp.Infrastructure.Data;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using AutoMapper;
using NewApp.Services.Views;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages.Html;

namespace NewApp.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;
        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in unitOfWork.Level.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> ShowSearchResultsName(String SearchLetter)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in unitOfWork.Level.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            return View("Mentee", mentees.Where(m => m.MenteeName.Contains(SearchLetter)));
        }

        public async Task<IActionResult> ShowSearchResultsPos(String SearchLetter)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in unitOfWork.Level.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            return View("Mentee", mentees.Where(m => m.ViewPos.Contains(SearchLetter)));
        }

        public ActionResult Mentee(string sortOrder)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
               ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in unitOfWork.Level.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            ViewBag.NameSortParm = sortOrder == "name_des" ? "Name" : "name_des";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_des" : "Age";
            ViewBag.PosSortParm = sortOrder == "Pos_des" ? "Position" : "Pos_des";
            var mentee = from s in mentees
                          select s;
            switch (sortOrder)
            {
                case "Name":
                    mentee = mentee.OrderBy(s => s.MenteeName);
                    break;
                case "name_des":
                    mentee = mentee.OrderByDescending(s => s.MenteeName);
                    break;
                case "Age":
                    mentee = mentee.OrderBy(s => s.Age);
                    break;
                case "Age_des":
                    mentee = mentee.OrderByDescending(s => s.Age);
                    break;
                case "Position":
                    mentee = mentee.OrderBy(s => s.ViewPos);
                    break;
                case "Pos_des":
                    mentee = mentee.OrderByDescending(s => s.ViewPos);
                    break;
                default:
                    mentee = mentee.OrderBy(s => s.MenteeId);
                    break;
            }
            return View(mentee.ToList());
        }
        
        public IActionResult Create()
        {
            var productsList = (from product in unitOfWork.Level.GetAll()
                                select new SelectListItem()
                                {
                                    Text = product.Position,
                                    Value = product.LevelId.ToString(),
                                }).ToList();

            productsList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.Positions = productsList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMenteeView model)
        {
            var productsList = (from product in unitOfWork.Level.GetAll()
                                select new SelectListItem()
                                {
                                    Text = product.Position,
                                    Value = product.LevelId.ToString(),
                                }).ToList();

            productsList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.Positions = productsList;
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateMenteeView, Mentee>().
                ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
                var mapper = new Mapper(config);
                Mentee mentee = mapper.Map<CreateMenteeView, Mentee>(model);
                unitOfWork.Mentee.Create(mentee);
                unitOfWork.Save();
                return RedirectToAction("Mentee");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var productsList = (from product in unitOfWork.Level.GetAll()
                                select new SelectListItem()
                                {
                                    Text = product.Position,
                                    Value = product.LevelId.ToString(),
                                }).ToList();

            productsList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.Positions = productsList;
            if (id == null)
                return View();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id.Value);
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            return View(mentee);
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            var productsList = (from product in unitOfWork.Level.GetAll()
                                select new SelectListItem()
                                {
                                    Text = product.Position,
                                    Value= product.LevelId.ToString(),
                                }).ToList();
            productsList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.Positions = productsList;
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>().
                 ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
                var mapper = new Mapper(config);
                Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
                unitOfWork.Mentee.Update(mentee);
                unitOfWork.Save();
                return RedirectToAction("Mentee");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, DeleteMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id);
            DeleteMenteeView mentee = mapper.Map<Mentee, DeleteMenteeView>(model);
            foreach (var pos in unitOfWork.Level.GetAll())
            {
                if (mentee.Position == pos.LevelId.ToString())
                    mentee.ViewPos = pos.Position;
            }
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, DeleteMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeleteMenteeView, Mentee>().
             ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<DeleteMenteeView, Mentee>(model);
            unitOfWork.Mentee.Delete(mentee.MenteeId);
            unitOfWork.Save();
            return RedirectToAction(nameof(Mentee));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id);
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            foreach (var pos in unitOfWork.Level.GetAll())
            {
                if (mentee.Position == pos.LevelId.ToString())
                    mentee.ViewPos = pos.Position;
            }
            if (mentee == null)
            {
                return NotFound();
            }
            return View(mentee);
        }

        public IActionResult Level()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, IndexLevelView>().
            ForMember("LevelId", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var levels = mapper.Map<List<IndexLevelView>>(unitOfWork.Level.GetAll());
            return View(levels);
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
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateLevelView, Level>().
                ForMember("Position", opt => opt.MapFrom(c => c.Position)));
                var mapper = new Mapper(config);
                Level level = mapper.Map<CreateLevelView, Level>(model);
                unitOfWork.Level.Create(level);
                unitOfWork.Save();
                return RedirectToAction("Level");
            }
            return View();
        }

        public ActionResult EditLevel(int? id)
        {
            if (id == null)
                return View();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id.Value);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            return View(level);
        }
        [HttpPost]
        public ActionResult EditLevel(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EditLevelView, Level>().
                 ForMember("Position", opt => opt.MapFrom(c => c.Position)));
                var mapper = new Mapper(config);
                Level level = mapper.Map<EditLevelView, Level>(model);
                unitOfWork.Level.Update(level);
                unitOfWork.Save();
                return RedirectToAction("Level");
            }
            return View(model);
        }

        public IActionResult DeleteLevel(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, DeleteLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id);
            DeleteLevelView level = mapper.Map<Level, DeleteLevelView>(model);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        [HttpPost, ActionName("DeleteLevel")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedLevel(int id, DeleteLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeleteLevelView, Level>().
             ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<DeleteLevelView, Level>(model);
            unitOfWork.Level.Delete(level.LevelId);
            unitOfWork.Save();
            return RedirectToAction(nameof(Level));
        }

        public async Task<IActionResult> DetailsLevel(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
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
