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
    public class LevelEditDI: IDependencyConfigure<Level, EditLevelView>
    {
        UnitOfWork unitOfWork;
        public LevelEditDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public EditLevelView Configure(Level models, int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, EditLevelView>().
            ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = unitOfWork.Level.Get(id.Value);
            EditLevelView level = mapper.Map<Level, EditLevelView>(model);
            return level;
        }
        public List<SelectListItem> MakeViewPos()
        {
            List<SelectListItem> positions = new List<SelectListItem>();
            return positions;
        }
        public Level ConfigureLevel(EditLevelView model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditLevelView, Level>().
                 ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<EditLevelView, Level>(model);
            return level;
        }
    }
}
