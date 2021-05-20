using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NewApp.Domain.Core;
using AutoMapper;
using System.Web.Mvc;
using NewApp.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace NewApp.Services.Views
{
    public class EditMenteeView
    {
        public int MenteeId { get; set; }
        public int Position { get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
        //public EditMenteeView NewLevel()
        //{
        //    EditMenteeView levelmodel = new EditMenteeView();
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
