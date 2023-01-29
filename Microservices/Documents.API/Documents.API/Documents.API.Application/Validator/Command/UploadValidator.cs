using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Command.Upload;
using Microsoft.AspNetCore.Http;
using Documents.API.Application.Resourse;

namespace Documents.API.Application.Validator.Command
{
    public class UploadValidator : AbstractValidator<UploadCommand>
    {
        public UploadValidator()
        {
            CreateRule();
        }

        private void CreateRule()
        {
            
            RuleFor(x => x.File)
                .Cascade(CascadeMode.Stop)
                .Must(IsImageType)
                .WithMessage(Messages.NotValidType);
        }

       

        private bool IsImageType(IFormFile arg)
        {
            return arg.ContentType.Contains("image");
        }
    }
}
