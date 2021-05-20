using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Infrastructure.Data;
using NewApp.Domain.Core;

namespace NewApp.Services.Views.DI
{
    public class MenteeIndexDI:DILists<IndexMenteeView>
    {
        private UnitOfWork unitOfWork;
        public MenteeIndexDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<IndexMenteeView> Configure()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
               ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(unitOfWork.Mentee.GetAll());
            return mentees;
        }
        public List<IndexMenteeView> MakeViewPos()
        {
            var mentees = Configure();
            foreach (var item in mentees)
            {
                foreach (var pos in unitOfWork.Level.GetAll())
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
