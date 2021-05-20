using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
//using NewApp.Domain.Core;
using NewApp.Infrastructure.Data;

namespace NewApp.Services.Views
{
    public class IndexMenteeView
    {
        public int MenteeId { get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
        public int Position { get; set; }
       // public virtual Level Level { get; set; }
        //public IndexMenteeView NewLevel()
        //{
        //    IndexMenteeView levelmodel = new IndexMenteeView();
        //    using (OnionAppContext db = new OnionAppContext())
        //    {
        //        levelmodel.LevelId = db.Level.ToList<Level>();
        //    }
        //    return levelmodel;
        //}
        //[NotMapped]
        //public List<Level> LevelId { get; set; } 

    }
}
