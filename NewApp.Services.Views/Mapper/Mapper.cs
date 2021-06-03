using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using NewApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<CreateLevelView, Level>().ForMember("Position", opt=>opt.MapFrom(c=>c.Position));
            CreateMap<CreateMenteeView, Mentee>().ForMember("LevelId", opt => opt.MapFrom(c => c.Position));
            CreateMap<Level, DeleteLevelView>().ForMember("Position", opt => opt.MapFrom(c => c.Position));
            CreateMap<Mentee, DeleteMenteeView>().ForMember("Position", opt => opt.MapFrom(c => c.LevelId));
            CreateMap<Level, EditLevelView>().ForMember("Position", opt => opt.MapFrom(c => c.Position));
            CreateMap<EditLevelView, Level>().ForMember("Position", opt => opt.MapFrom(c => c.Position));
            CreateMap<Mentee, EditMenteeView>().ForMember("Position", opt => opt.MapFrom(c => c.LevelId));
            CreateMap<EditMenteeView, Mentee>().ForMember("LevelId", opt => opt.MapFrom(c => c.Position));
            CreateMap<Level, IndexLevelView>().ForMember("LevelId", opt => opt.MapFrom(c => c.LevelId));
            CreateMap<Mentee, IndexMenteeView>().ForMember("Position", opt => opt.MapFrom(c => c.LevelId));
        }
    }
}
