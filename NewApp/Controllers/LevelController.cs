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
        private readonly IIndexConfig<IndexLevelView> indexConfig;
        private readonly ICreateConfig<Level, CreateLevelView> createConfig;
        private readonly IEditConfig<EditLevelView, Level> editConfig;
        private readonly IDeleteConfig<DeleteMenteeView> deleteConfig;
        public LevelController(ILevelRepository _levelRepository,
            IIndexConfig<IndexLevelView> _indexConfig,
            ICreateConfig<Level, CreateLevelView> _createConfig,
            IEditConfig<EditLevelView, Level> _editConfig,
            IDeleteConfig<DeleteMenteeView> _deleteConfig)
        {
            this.indexConfig = _indexConfig;
            this.levelRepository = _levelRepository;
            this.createConfig = _createConfig;
            this.editConfig = _editConfig;
            this.deleteConfig = _deleteConfig;
        }
        public IActionResult Index()
        {
            return View(indexConfig.Config());
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
                
                levelRepository.Create(createConfig.Config(model));
                levelRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View();
            return View(editConfig.Config_1(id));
        }
        [HttpPost]
        public ActionResult Edit(EditLevelView model)
        {
            if (ModelState.IsValid)
            {
                levelRepository.Update(editConfig.Config_2(model));
                levelRepository.Save();
                return RedirectToAction("Level");
            }
            return View(model);
        }
    }
}
