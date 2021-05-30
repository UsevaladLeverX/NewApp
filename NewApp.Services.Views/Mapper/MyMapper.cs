using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Services.Views.IMapper;

namespace NewApp.Services.Views
{
    public class MyMapper:IMyMapper
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;
        public MyMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }
        public List<SelectListItem> ListOfPositions()
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
            return productsList;
        }
        public Level CreateLevelConfig(CreateLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateLevelView, Level>().
                ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<CreateLevelView, Level>(model);
            return level;
        }
        public Mentee CreateMenteeConfig(CreateMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateMenteeView, Mentee>().
               ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<CreateMenteeView, Mentee>(model);
            return mentee;
        }
        public DeleteLevelView DeleteLevelConfig(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, DeleteLevelView>().
           ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = levelRepository.Get(id);
            DeleteLevelView level = mapper.Map<Level, DeleteLevelView>(model);
            return level;
        }
        public DeleteMenteeView DeleteMenteeConfig(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, DeleteMenteeView>().
       ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = menteeRepository.Get(id);
            DeleteMenteeView mentee = mapper.Map<Mentee, DeleteMenteeView>(model);
            return mentee;
        }
        public EditLevelView EditLevelConfig_1(int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = levelRepository.Get(id.Value);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            return level;
        }
        public Level EditLevelConfig_2(EditLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditLevelView, Level>().
                  ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<EditLevelView, Level>(model);
            return level;
        }
        public EditMenteeView EditMenteeConfig_1(int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = menteeRepository.Get(id.Value);
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            foreach (var pos in levelRepository.GetAll())
            {
                if (mentee.Position == pos.LevelId.ToString())
                    mentee.ViewPos = pos.Position;
            }
            return mentee;
        }
        public Mentee EditMenteeConfig_2(EditMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>().
                 ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
            return mentee;
        }
        public List<IndexLevelView> IndexLevelConfig()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, IndexLevelView>().
            ForMember("LevelId", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var levels = mapper.Map<List<IndexLevelView>>(levelRepository.GetAll());
            return levels;
        }
        public List<IndexMenteeView> IndexMenteeConfig()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(menteeRepository.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in levelRepository.GetAll())
                {
                    if (item.Position == pos.LevelId.ToString())
                    {
                        item.ViewPos = pos.Position;
                    }
                }
            }
            return mentees;
        }
    }
}
