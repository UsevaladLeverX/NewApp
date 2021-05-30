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
    public class DeleteMenteeMapper:IDeleteConfig<DeleteMenteeView>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public DeleteMenteeMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }
        public DeleteMenteeView Config(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, DeleteMenteeView>().
       ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var model = menteeRepository.Get(id);
            DeleteMenteeView mentee = mapper.Map<Mentee, DeleteMenteeView>(model);
            return mentee;
        }
    }
}
