﻿using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Receptionists.DeleteReceptionist;

internal class DeleteReceptionistCommandHandler : IRequestHandler<DeleteReceptionistCommand, Response>
{
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly IReceptionistRepository _receptionistRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public DeleteReceptionistCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IReceptionistRepository receptionistRepository, IDocumentApiProxy documentApiProxy)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _receptionistRepository = receptionistRepository;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(DeleteReceptionistCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Receptionist;
        var receptionistId =
            await _receptionistRepository.GetReceptionistIdByAccountIdAsync(request.AccountId, cancellationToken);
        if (receptionistId == null) return Response.Error;

        await _receptionistRepository.DeleteAsync(receptionistId, cancellationToken);
        var user = await _userManager.FindByIdAsync(request.AccountId);
        if (user == null) return Response.Error;
        await _userManager.RemoveFromRoleAsync(user, role);

        if (user.PhotoId != null)
        {
            var documentId = (long)user.PhotoId;
            user.PhotoId = null;
            await _userManager.UpdateAsync(user);
            await _documentApiProxy.DeleteBlobAsync(documentId, cancellationToken);
        }

        return Response.Success;
    }
}