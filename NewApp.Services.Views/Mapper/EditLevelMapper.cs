using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public class EditLevelMapper : IEditConfig<EditLevelView, Level>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public EditLevelMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }
        public EditLevelView Config_1(int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = levelRepository.Get(id.Value);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            return level;
        }
        public List<SelectListItem> Positions()
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
        public Level Config_2(EditLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditLevelView, Level>().
                  ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<EditLevelView, Level>(model);
            return level;
        }
    }
}
