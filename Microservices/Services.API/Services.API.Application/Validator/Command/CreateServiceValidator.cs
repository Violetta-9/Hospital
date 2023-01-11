using FluentValidation;
using Services.API.Application.Command.UpdateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.API.Application.Command.CreateService;
using Authorization.Data.Repository;
using Services.API.Resources;

namespace Services.API.Application.Validator.Command
{
    public class CreateServiceValidator: AbstractValidator<CreateServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceCategoryRepository _categoryRepository;

        public CreateServiceValidator(IServiceRepository serviceRepository, IServiceCategoryRepository serviceCategory)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = serviceCategory;
            CreateRules();
        }
        private void CreateRules()
        {
           
            RuleFor(x => x.CreateServiceDto.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.CreateServiceDto.Price)))
                .Must(CorrectPrice)
                .WithMessage(Messages.InCorrectPrice);
            RuleFor(x => x.CreateServiceDto.ServiceCategoryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt =>
                    string.Format(Messages.NotEmptyField, nameof(opt.CreateServiceDto.ServiceCategoryId)))
                .MustAsync(ExistsServiceCategory)
                .WithMessage(opt =>
                    string.Format(Messages.NotFoundServiceCategory, opt.CreateServiceDto.ServiceCategoryId));

        }

        private async Task<bool> ExistsServiceCategory(long categoryId, CancellationToken cancellationToken)
        {
            return await _categoryRepository.ExistsAsync(categoryId, cancellationToken);
        }

        private bool CorrectPrice(double price)
        {
            return price > 0;
        }

       
    }
}
