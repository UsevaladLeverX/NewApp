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
    public class EditLevelView
    {
        public int LevelId { get; set; }
        public string Position { get; set; }
    }
    public class LevelEditValidator : AbstractValidator<EditLevelView>
    {
        public LevelEditValidator()
        {
            var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";
            RuleFor(m => m.Position).NotEmpty();
            RuleFor(m => m.Position)
               .Must(m => m.All(Char.IsLetter)).WithMessage(msg);
        }
    }
}
