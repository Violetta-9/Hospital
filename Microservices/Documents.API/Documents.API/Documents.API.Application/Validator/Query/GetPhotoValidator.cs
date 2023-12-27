using Authorization.Data.Repository;
using Documents.API.Application.Query.GetBlobDocuments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Query.GetBlobPhoto;
using Documents.API.Application.Resourse;

namespace Documents.API.Application.Validator.Query
{
    public class GetPhotoValidator: AbstractValidator<GetBlobPhotoQuery>
    {
        private readonly IPhotosRepository _documentsRepository;

            public GetPhotoValidator(IPhotosRepository documentsRepository)
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
