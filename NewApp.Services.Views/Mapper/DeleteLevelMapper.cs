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
    public class DeleteLevelMapper:IDeleteConfig<DeleteLevelView>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public DeleteLevelMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }
        public DeleteLevelView Config(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Level, DeleteLevelView>().
           ForMember("Position", opt => opt.MapFrom(c => c.Position)));
            var mapper = new Mapper(config);
            var model = levelRepository.Get(id);
            DeleteLevelView level = mapper.Map<Level, DeleteLevelView>(model);
            return level;
        }
    }
}
