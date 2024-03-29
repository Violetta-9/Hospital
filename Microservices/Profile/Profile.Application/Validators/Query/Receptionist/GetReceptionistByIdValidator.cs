﻿using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Receptionist.GetReceptionistById;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Receptionist;

internal class GetReceptionistByIdValidator : AbstractValidator<GetReceptionistByIdQuery>
{
    private readonly IReceptionistRepository _receptionistRepository;

    public GetReceptionistByIdValidator(IReceptionistRepository receptionistRepository)
    {
        _receptionistRepository = receptionistRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.ReceptionistId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsReceptionist)
            .WithMessage(opt => string.Format(Messages.NotFoundReceptionist, opt.ReceptionistId));
    }

    private async Task<bool> ExistsReceptionist(long receptionistId, CancellationToken cancellationToken)
    {
        return await _receptionistRepository.ExistsAsync(receptionistId, cancellationToken);
    }
}