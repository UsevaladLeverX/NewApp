using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NewApp.Services.Views.Validator
{
    public class MenteeEditValidator : AbstractValidator<EditMenteeView>
    {
        public MenteeEditValidator()
        {
            RuleFor(m => m.MenteeName).NotEmpty();
            RuleFor(m => m.MenteeName)
                .Must(m => m.All(Char.IsLetter)).When(m => m.MenteeName != null);
            RuleFor(m => m.Age)
                .GreaterThan(17)
                .LessThan(80);
            RuleFor(m => m.Position).NotEmpty();
        }
    }
}
