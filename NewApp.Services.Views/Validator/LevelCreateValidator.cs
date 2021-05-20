﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NewApp.Services.Views.Validator
{
    public class LevelCreateValidator : AbstractValidator<CreateLevelView>
    {
        public LevelCreateValidator()
        {
            RuleFor(m => m.Position).NotEmpty();
            RuleFor(m => m.Position)
               .Must(m => m.All(Char.IsLetter)).When(m => m.Position != null);
        }
    }
}