using Microsoft.AspNetCore.Mvc;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using NewApp.Services.Views;
using NewApp.Services.Views.IMapper;
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
            return View(mapper.IndexLevelConfig());
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
                levelRepository.Create(mapper.CreateLevelConfig(model));
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View();
            return View(mapper.EditLevelConfig_1(id));
        }
        [HttpPost]
        public ActionResult Edit(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                levelRepository.Update(mapper.EditLevelConfig_2(model));
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            levelRepository.Delete(mapper.DeleteLevelConfig(id).LevelId);
            levelRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (mapper.EditLevelConfig_1(id) == null)
            {
                return NotFound();
            }
            return View(mapper.EditLevelConfig_1(id));
        }
    }
}
