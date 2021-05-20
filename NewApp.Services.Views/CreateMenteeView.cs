using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using FluentValidation;

namespace NewApp.Services.Views
{ 
    public class CreateMenteeView
    {
        public int Position{ get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
    }
}
