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
    public class CreateLevelMapper:ICreateConfig<Level, CreateLevelView>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public CreateLevelMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
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
        public Level Config(CreateLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateLevelView, Level>().
                ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<CreateLevelView, Level>(model);
            return level;
        }
    }
}
