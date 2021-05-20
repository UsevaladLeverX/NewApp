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
    public class MenteeDeleteDI:IDDeleteAndDetails<DeleteMenteeView>
    {
        UnitOfWork unitOfWork;
        public MenteeDeleteDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public DeleteMenteeView Configure(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, DeleteMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Mentee.Get(id);
            DeleteMenteeView mentee = mapper.Map<Mentee, DeleteMenteeView>(model);
            return mentee;
        }
    }
}
