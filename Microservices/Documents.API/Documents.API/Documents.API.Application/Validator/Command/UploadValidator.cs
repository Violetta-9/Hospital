using Documents.API.Application.Command.Upload;
using Documents.API.Application.Resourse;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Validator.Command;

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