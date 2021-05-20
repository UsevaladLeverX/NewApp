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
    public class LevelDeleteDI:IDDeleteAndDetails<DeleteLevelView>
    {
        UnitOfWork unitOfWork;
        public LevelDeleteDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public DeleteLevelView Configure(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, DeleteLevelView>().
          ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id);
            DeleteLevelView level = mapper.Map<Level, DeleteLevelView>(model);
            return level;
        }
    }
}
