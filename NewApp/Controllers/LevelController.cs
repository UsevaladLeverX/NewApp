using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using NewApp.Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewApp.Controllers
{
    public class LevelController : Controller
    {
        private readonly ILevelRepository levelRepository;
        private readonly IMapper mapper;
        public LevelController(ILevelRepository _levelRepository, IMapper _mapper)
        {
            this.levelRepository = _levelRepository;
            this.mapper = _mapper;
        }
        public IActionResult Index()
        {
            var level = levelRepository.GetAll();
            var levelDTO=mapper.Map<List<IndexLevelView>>(level);
            return View(levelDTO);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateLevelView model)
        {
            if (ModelState.IsValid)
            {
                var level = mapper.Map<Level>(model);
                levelRepository.Create(level);
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View();
            var level = levelRepository.Get(id.Value);
            var levelDTO = mapper.Map<EditLevelView>(level);
            return View(levelDTO);
        }
        [HttpPost]
        public ActionResult Edit(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                var level = mapper.Map<Level>(model);
                levelRepository.Update(level);
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var level = levelRepository.Get(id);
            var levelDTO = mapper.Map<DeleteLevelView>(level);
            levelRepository.Delete(levelDTO.LevelId);
            levelRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var level = levelRepository.Get(id);
            var levelDTO = mapper.Map<EditLevelView>(level);
            if (levelDTO == null)
            {
                return NotFound();
            }
            return View(levelDTO);
        }
    }
}
