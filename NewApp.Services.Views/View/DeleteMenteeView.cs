using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Infrastructure.Data;

namespace NewApp.Services.Views
{
    public class DeleteMenteeView
    {
        public int MenteeId { get; set; }
        public string Position { get; set; }
        public string ViewPos { get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
    }
}
