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
    public class EditMenteeMapper:IEditConfig<EditMenteeView, Mentee>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public EditMenteeMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }
        public EditMenteeView Config_1(int? id)
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
        public Mentee Config_2(EditMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>().
                 ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
            return mentee;
        }
    }
}
