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
    public class IndexLevelView
    {
        public int LevelId { get; set; }
        public string Position { get; set; }
    }
}
