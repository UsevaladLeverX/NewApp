using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using NewApp.Services.Views;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewApp.Controllers
{
    public class MenteeController : Controller
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IMapper mapper;
        public MenteeController(IMenteeRepository _imenteeRepository, ILevelRepository _levelRepository,
            IMapper _mapper)
        {
            this.menteeRepository = _imenteeRepository;
            this.levelRepository = _levelRepository;
            this.mapper = _mapper;
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            var mentees = menteeRepository.GetAll();
            var menteesDTO = mapper.Map<List<IndexMenteeView>>(mentees);
            foreach (var item in menteesDTO)
            {
                foreach (var pos in levelRepository.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> ShowSearchResultsName(string SearchLetter)
        {
            var mentees = menteeRepository.GetAll();
            var menteesDTO = mapper.Map<List<IndexMenteeView>>(mentees);
            foreach (var item in menteesDTO)
            {
                foreach (var pos in levelRepository.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            if (SearchLetter == null)
                return View("Index", menteesDTO);
            else
                return View("Index", menteesDTO.Where(m => m.MenteeName.Contains(SearchLetter)));
        }

        public async Task<IActionResult> ShowSearchResultsPos(string SearchLetter)
        {
            var mentees = menteeRepository.GetAll();
            var menteesDTO = mapper.Map<List<IndexMenteeView>>(mentees);
            foreach (var item in menteesDTO)
            {
                foreach (var pos in levelRepository.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            if (SearchLetter == null)
                return View("Index", menteesDTO);
            else
                return View("Index", menteesDTO.Where(m => m.ViewPos.Contains(SearchLetter)));
        }

        public IActionResult Index(string sortOrder)
        {
            var mentees = menteeRepository.GetAll();
            var menteesDTO = mapper.Map<List<IndexMenteeView>>(mentees);
            foreach (var item in menteesDTO)
            {
                foreach (var pos in levelRepository.GetAll())
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
            var mentee = from s in menteesDTO
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
            var productsList = (from product in levelRepository.GetAll()
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
            var productsList = (from product in levelRepository.GetAll()
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
                var mentee = mapper.Map<Mentee>(model);
                menteeRepository.Create(mentee);
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            var productsList = (from product in levelRepository.GetAll()
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
            var mentee = menteeRepository.Get(id.Value);
            var menteeDTO = mapper.Map<EditMenteeView>(mentee);
            if (id == null)
                return View();
            return View(menteeDTO);
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            var productsList = (from product in levelRepository.GetAll()
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
                var mentee = mapper.Map<Mentee>(model);
                menteeRepository.Update(mentee);
                menteeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var mentee = menteeRepository.Get(id);
            var menteeDTO = mapper.Map<DeleteMenteeView>(mentee);
            menteeRepository.Delete(menteeDTO.MenteeId);
            menteeRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mentee = menteeRepository.Get(id);
            var menteeDTO = mapper.Map<EditMenteeView>(mentee);

            foreach (var pos in levelRepository.GetAll())
            {
                if (menteeDTO.Position == pos.LevelId.ToString())
                {
                    menteeDTO.ViewPos = pos.Position;
                }
            }

            if (menteeDTO == null)
            {
                return NotFound();
            }
            return View(menteeDTO);
        }
    }
}
