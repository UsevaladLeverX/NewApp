using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NewApp.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using FluentValidation;


namespace NewApp.Services.Views
{
    public class EditMenteeView
    {
        public int MenteeId { get; set; }
        public string Position { get; set; }
        public string ViewPos { get; set; }
        public string MenteeName { get; set; }
        public int Age { get; set; }
    }
    public class MenteeEditValidator : AbstractValidator<EditMenteeView>
    {
        public MenteeEditValidator()
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
