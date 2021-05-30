using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;

namespace NewApp.Services.Views
{
    public class IndexMenteeMapper:IIndexConfig<IndexMenteeView>
    {
        private readonly IMenteeRepository menteeRepository;
        private readonly ILevelRepository levelRepository;

        public IndexMenteeMapper(IMenteeRepository _menteeRepository, ILevelRepository _levelRepository)
        {
            this.menteeRepository = _menteeRepository;
            this.levelRepository = _levelRepository;
        }

        public List<IndexMenteeView> Config()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Mentee, IndexMenteeView>().
            ForMember("Position", opt => opt.MapFrom(c => c.LevelId)));
            var mapper = new Mapper(config);
            var mentees = mapper.Map<List<IndexMenteeView>>(menteeRepository.GetAll());
            foreach (var item in mentees)
            {
                foreach (var pos in levelRepository.GetAll())
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
