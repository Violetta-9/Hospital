using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Photo.DeletePhoto;

internal class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Response>
{
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly UserManager<Account> _userManager;

    public DeletePhotoCommandHandler(IDocumentApiProxy documentApiProxy, UserManager<Account> userManager)
    {
        _documentApiProxy = documentApiProxy;
        _userManager = userManager;
    }

    public async Task<Response> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId);
        var docId = (long)user.PhotoId;
        user.PhotoId = null;
        await _userManager.UpdateAsync(user);
        if (user != null)
        {
            await _documentApiProxy.DeleteBlobAsync(docId, cancellationToken);
            return Response.Success;
        }

        return Response.Error;
    }
}