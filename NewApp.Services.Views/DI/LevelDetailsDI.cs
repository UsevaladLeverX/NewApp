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
    public class LevelDetailsDI: IDDeleteAndDetails<EditLevelView>
    {
        private UnitOfWork unitOfWork;
        public LevelDetailsDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public EditLevelView Configure(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
           ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            return level;
        }
    }
}
