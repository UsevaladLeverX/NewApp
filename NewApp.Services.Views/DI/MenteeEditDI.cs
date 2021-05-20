using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Infrastructure.Data;
using NewApp.Domain.Core;
using AutoMapper;

namespace NewApp.Services.Views.DI
{
    public class MenteeEditDI:IDependencyConfigure<Mentee, EditMenteeView>
    {
        private UnitOfWork unitOfWork;
        public MenteeEditDI()
        {
            unitOfWork = new UnitOfWork();
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
        public EditMenteeView Configure(Mentee models, int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
           ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id.Value);
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            return mentee;
        }
        public Mentee ConfigureMentee(EditMenteeView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditMenteeView, Mentee>().
            ForMember("LevelId", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Mentee mentee = mapper.Map<EditMenteeView, Mentee>(model);
            return mentee;
        }
    }
}
