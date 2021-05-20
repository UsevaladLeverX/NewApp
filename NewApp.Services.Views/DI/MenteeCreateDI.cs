using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Infrastructure.Data;
using NewApp.Domain.Core;

namespace NewApp.Services.Views.DI
{
    public class MenteeCreateDI:IDependencyConfigure<CreateMenteeView, Mentee>
    {
        UnitOfWork unitOfWork;
        public MenteeCreateDI()
        {
            unitOfWork = new UnitOfWork();
        }

        public Mentee Configure(CreateMenteeView model, int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateMenteeView, Mentee>().
               ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<CreateMenteeView, Mentee>(model);
            return mentee;
        }

        public List<SelectListItem> MakeViewPos()
        {
            var productsList = (from product in unitOfWork.Level.GetAll()
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
    }
}
