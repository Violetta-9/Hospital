using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Response = Profile.Application.Contracts.Outgoing.Response;
using SubjectUpdate = Profile.Application.Contracts.Enum.SubjectUpdate;

namespace Profile.Application.Command.Photo.AddPhoto
{
    internal class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, Response>
    {
        private readonly IDocumentApiProxy _documentApiProxy;
        private readonly UserManager<Account> _userManager;
        public AddPhotoCommandHandler(IDocumentApiProxy documentApiProxy,UserManager<Account> usrManager)
        {
            _documentApiProxy = documentApiProxy;
            _userManager = usrManager;
        }

        public async Task<Response> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {

           var id= await _documentApiProxy.UploadBlobAsync(
                new FileParameter(request.AddPhotoDTO.File.OpenReadStream(), request.AddPhotoDTO.File.FileName,
                    request.AddPhotoDTO.File.ContentType), request.AddPhotoDTO.EntityType, (Documents.API.Client.GeneratedClient.SubjectUpdate)request.AddPhotoDTO.SubjectUpdate,
                cancellationToken);
         var user= await _userManager.FindByIdAsync(request.AddPhotoDTO.AccountId);
         if (user != null && id>0)
         {
             user.DocumentationId=id;
             await _userManager.UpdateAsync(user);
             return Response.Success;
         }
         return Response.Error;
        }


    }
}