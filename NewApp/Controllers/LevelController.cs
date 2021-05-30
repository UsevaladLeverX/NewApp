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
        private readonly IMapper myMapper;
        public LevelController(ILevelRepository _levelRepository, IMapper _myMapper)
        {
            this.levelRepository = _levelRepository;
            this.myMapper = _myMapper;
        }
        public IActionResult Index()
        {
            return View(myMapper.IndexLevelConfig());
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
                levelRepository.Create(myMapper.CreateLevelConfig(model));
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View();
            return View(myMapper.EditLevelConfig_1(id));
        }
        [HttpPost]
        public ActionResult Edit(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                levelRepository.Update(myMapper.EditLevelConfig_2(model));
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            levelRepository.Delete(myMapper.DeleteLevelConfig(id).LevelId);
            levelRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (myMapper.EditLevelConfig_1(id) == null)
            {
                return NotFound();
            }
            return View(myMapper.EditLevelConfig_1(id));
        }
    }
}
