using Authorization.Data.Repository;
using FluentValidation;
using Services.API.Application.Command.CreateService;
using Services.API.Resources;

namespace Services.API.Application.Validator.Command;

public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
{
    private readonly IServiceCategoryRepository _categoryRepository;
    private readonly IServiceRepository _serviceRepository;

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