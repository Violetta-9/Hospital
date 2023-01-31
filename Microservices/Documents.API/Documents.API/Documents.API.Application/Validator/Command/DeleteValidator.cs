using Authorization.Data.Repository;
using Documents.API.Application.Command.Delete;
using Documents.API.Application.Resourse;
using FluentValidation;

namespace Documents.API.Application.Validator.Command;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    private readonly IDocumentsRepository _documentsRepository;

    public DeleteValidator(IDocumentsRepository documentsRepository)
    {
        _documentsRepository = documentsRepository;
        CreateRule();
    }

    private void CreateRule()
    {
        RuleFor(x => x.DocumentId)
            .MustAsync(_documentsRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundDocumentId, opt.DocumentId));
    }
}