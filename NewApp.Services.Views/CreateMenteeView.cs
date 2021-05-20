using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Domain.Core;
using AutoMapper;
using System.Web.Mvc;
using NewApp.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace NewApp.Services.Views
{ 
    public class CreateMenteeView
    {
        public int Position{ get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
        public CreateMenteeView NewLevel()
        {
            CreateMenteeView levelmodel = new CreateMenteeView();
            using (OnionAppContext db = new OnionAppContext())
            {
                levelmodel.LevelId = db.Level.ToList<Level>();
            }
            return levelmodel;
        }
        [NotMapped]
        public List<Level> LevelId { get; set; }
    }
}
