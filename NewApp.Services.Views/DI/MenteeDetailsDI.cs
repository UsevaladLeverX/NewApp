using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using NewApp.Infrastructure.Data;

namespace NewApp.Services.Views.DI
{
    public class MenteeDetailsDI : IDDeleteAndDetails<EditMenteeView>
    {
        private UnitOfWork unitOfWork;
        public MenteeDetailsDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public EditMenteeView Configure(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, EditMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id);
            EditMenteeView mentee = mapper.Map<Mentee, EditMenteeView>(model);
            return mentee;
        }
        public EditMenteeView ViewPos(int id)
        {
            var mentee = Configure(id);
            foreach (var pos in unitOfWork.Level.GetAll())
            {
                if (mentee.Position == pos.LevelId.ToString())
                    mentee.ViewPos = pos.Position;
            }
            return mentee;
        }
    }
}
