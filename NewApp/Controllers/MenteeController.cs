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
        public MenteeController(IMenteeRepository _imenteeRepository,ILevelRepository _levelRepository, IIndexConfig<IndexMenteeView> _indexConfig)
        {
            this.menteeRepository = _imenteeRepository;
            this.indexConfig = _indexConfig;
            this.levelRepository = _levelRepository;
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            indexConfig.Config();
            return View();
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
    }
}
