using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using NewApp.Infrastructure.Data;
using NewApp.Services.Views.View;

namespace NewApp.Services.Views
{
    public class IndexMenteeView
    {
        public int MenteeId { get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
        public int Position { get; set; }
        public string ViewPos { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<IndexMenteeView> Mentees { get; set; }
    }
}
