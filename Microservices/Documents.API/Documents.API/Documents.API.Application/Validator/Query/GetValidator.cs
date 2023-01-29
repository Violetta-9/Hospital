using Authorization.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Query.GetBlob;
using Documents.API.Application.Resourse;
using FluentValidation;

namespace Documents.API.Application.Validator.Query
{
    public class GetValidator:AbstractValidator<GetBlobQuery>
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
}
