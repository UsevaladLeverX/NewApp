using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using AutoMapper;

namespace NewApp.Services.Views
{
    public class CreateMenteeMapper:ICreateConfig<Mentee, CreateMenteeView>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public CreateMenteeMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
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
        public Mentee Config(CreateMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateMenteeView, Mentee>().
               ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<CreateMenteeView, Mentee>(model);
            return mentee;
        }
    }
}
