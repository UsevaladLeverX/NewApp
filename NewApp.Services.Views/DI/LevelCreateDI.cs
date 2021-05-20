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
    public class LevelCreateDI:IDependencyConfigure<CreateLevelView, Level>
    {
        UnitOfWork unitOfWork;
        public LevelCreateDI()
        {
            unitOfWork = new UnitOfWork();
        }
        public Level Configure(CreateLevelView model, int? id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateLevelView, Level>().
                ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            Level level = mapper.Map<CreateLevelView, Level>(model);
            return level;
        }
        public List<SelectListItem> MakeViewPos()
        {
            List<SelectListItem> positions = new List<SelectListItem>();
            return positions;
        }
    }
}
