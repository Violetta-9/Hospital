using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using Profile.Application.Command.Photo.AddPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Response = Profile.Application.Contracts.Outgoing.Response;

namespace Profile.Application.Command.Photo.DeletePhoto
{
    internal class DeletePhotoCommandHandler:IRequestHandler<DeletePhotoCommand,Response>
    {
        private readonly IDocumentApiProxy _documentApiProxy;
        private readonly UserManager<Account> _userManager;
        public DeletePhotoCommandHandler(IDocumentApiProxy documentApiProxy,UserManager<Account> userManager)
        {
            _documentApiProxy = documentApiProxy;
            _userManager = userManager;
        }

        public async Task<Response> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
           var user=await _userManager.FindByIdAsync(request.AccountId);
           var docId = (long)user.DocumentationId;
           user.DocumentationId = null;
           await _userManager.UpdateAsync(user);
           if (user != null)
           {
              await _documentApiProxy.DeleteBlobAsync(docId, cancellationToken);
              return Response.Success;
           }
           return Response.Error;
            
        }
    }
}
