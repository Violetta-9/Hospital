using Authorization.Data.Repository;
using Documents.API.Application.Query.GetBlobDocuments;
using Documents.API.Application.Resourse;
using FluentValidation;

namespace Documents.API.Application.Validator.Query;

public class GetValidator : AbstractValidator<GetBlobDocumentsQuery>
{
    private readonly IDocumentsRepository _documentsRepository;

    public GetValidator(IDocumentsRepository documentsRepository)
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