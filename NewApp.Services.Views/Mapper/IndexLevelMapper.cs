using AutoMapper;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public class IndexLevelMapper:IIndexConfig<IndexLevelView>
    {
        private readonly ILevelRepository levelRepository;
        public IndexLevelMapper(ILevelRepository _levelRepository)
        {
            levelRepository = _levelRepository;
        }
        public List<IndexLevelView> Config()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, IndexLevelView>().
            ForMember("LevelId", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var levels = mapper.Map<List<IndexLevelView>>(levelRepository.GetAll());
            return levels;
        }
    }
}
