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
    public class LevelIndexDI:DILists<IndexLevelView>
    {
        UnitOfWork unitOfWork;
        public LevelIndexDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<IndexLevelView> Configure()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, IndexLevelView>().
            ForMember("LevelId", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var levels = mapper.Map<List<IndexLevelView>>(unitOfWork.Level.GetAll());
            return levels;
        }
    }
}
