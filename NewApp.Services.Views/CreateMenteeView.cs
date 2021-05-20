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
    public class MenteeCreateValidator : AbstractValidator<CreateMenteeView>
    {
        public MenteeCreateValidator()
        {
            RuleFor(m => m.MenteeName).NotEmpty();
            RuleFor(m => m.MenteeName)
                .Must(m => m.All(Char.IsLetter));
            RuleFor(m => m.Age)
                .GreaterThan(17)
                .LessThan(80);
            RuleFor(m => m.Position).NotEmpty();
        }
    }
}
