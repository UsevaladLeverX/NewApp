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

namespace NewApp.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;//= new UnitOfWork();

        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public IActionResult Mentee()
        {
            // Создание конфигурации сопоставления
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));//.ForMember("Position",opt=>opt.MapFrom(c=>c.Level.Position)));//.IncludeMembers(m=>m.Level.Position));//.ForMember("Position", op=>op.MapFrom(m=>m.Level.Position)));
            // Настройка AutoMapper
            var mapper = new Mapper(config);
            // сопоставление

            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            //IndexMenteeView menteeView = new IndexMenteeView();
            //var mentee = menteeView.NewLevel();
            return View(mentees);
        }
        public IActionResult Create()
        {
            CreateMenteeView create = new CreateMenteeView();
            return View(create.NewLevel());
        }
        [HttpPost]
        public ActionResult Create(CreateMenteeView model)
        {
            //if (ModelState.IsValid)
            //{
                //Mentee mentee = new Mentee
                //{
                //    MenteeName = model.MenteeName,
                //    Age = model.Age,
                //    LevelId = model.Position
                //};
                // Настройка конфигурации AutoMapper
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateMenteeView, Mentee>().
                ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
                var mapper = new Mapper(config);
                // Выполняем сопоставление
                //var mentees = unitOfWork.Mentee.GetAll();
                Mentee mentee = mapper.Map<CreateMenteeView, Mentee>(model);
                //unitOfWork.Mentee.Create(mentee);
                               
           // }
            unitOfWork.Mentee.Create(mentee);
            CreateMenteeView mView = new CreateMenteeView();
            mView= model.NewLevel();
            //mentee.MenteeId= unitOfWork.Mentee.GetAll().ToList().Last().MenteeId;
            //++mentee.MenteeId;
            //if (mentee.MenteeId > 0)
            //{
                unitOfWork.Save();
                return RedirectToAction("Mentee");
           // }
            return View(mView);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View();
            // Настройка конфигурации AutoMapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id.Value);
            // Выполняем сопоставление
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            //mentee.NewLevel();
            return View(mentee);
        }
        [HttpPost]
        public ActionResult Edit(EditMenteeView model)
        {
            if (ModelState.IsValid)
            {
                // Настройка конфигурации AutoMapper
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>());
                    //.ForMember("Email", opt => opt.MapFrom(src => src.Login)));
                var mapper = new Mapper(config);
                // Выполняем сопоставление
                Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
                unitOfWork.Mentee.Update(mentee);
                unitOfWork.Save();
                return RedirectToAction("Mentee");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id);
            // Выполняем сопоставление
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            //var mentee = unitOfWork.Mentee.Get(id);
            //var mentee = await _context.Mentee
            //    .Include(m => m.Level)
            //    .FirstOrDefaultAsync(m => m.MenteeId == id);
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        // POST: Mentees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, EditMenteeView model)
        {
            // Настройка конфигурации AutoMapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>());
            //.ForMember("Email", opt => opt.MapFrom(src => src.Login)));
            var mapper = new Mapper(config);
            // Выполняем сопоставление
            Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
            unitOfWork.Mentee.Delete(mentee.MenteeId);
            unitOfWork.Save();
            //var mentee = repo.GetMentee(id);//await _context.Mentee.FindAsync(id);
            //unitOfWork.Mentee.Delete(id);// _context.Mentee.Remove(mentee);
            //unitOfWork.Save();//await _context.SaveChangesAsync();
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
            // Выполняем сопоставление
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            //var mentee = unitOfWork.Mentee.Get(id);
            //var mentee = await _context.Mentee
            //    .Include(m => m.Level)
            //    .FirstOrDefaultAsync(m => m.MenteeId == id);
            if (mentee == null)
            {
                return NotFound();
            }
            return View(mentee);
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
