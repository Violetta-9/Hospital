using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.API.Application.Command.UpdateServiceStatus;
using Authorization.Data.Repository;
using Services.API.Resources;

namespace Services.API.Application.Validator.Command
{
    public class UpdateServiceStatusValidator: AbstractValidator<UpdateServiceStatusCommand>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceStatusValidator(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.UpdateServiceStatusDTO.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateServiceStatusDTO.Id)))
                .MustAsync(ExistsServiceAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundService, opt.UpdateServiceStatusDTO.Id));

            RuleFor(x => x.UpdateServiceStatusDTO.IsActive)
                .NotEmpty()
                .WithMessage(opt=>string.Format(Messages.NotEmptyField,nameof(opt.UpdateServiceStatusDTO.IsActive)));
        }

        private async Task<bool> ExistsServiceAsync(long serviceId, CancellationToken cancellationToken)
        {
            return await _serviceRepository.ExistsAsync(serviceId, cancellationToken);
        }
    }
}
