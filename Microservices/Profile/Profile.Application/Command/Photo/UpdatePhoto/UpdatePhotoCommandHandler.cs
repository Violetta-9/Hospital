﻿using System;
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

namespace Profile.Application.Command.Photo.UpdatePhoto
{
    internal class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand,Response>
    {
        private readonly IDocumentApiProxy _documentApiProxy;
        private readonly UserManager<Account> _userManager;

        public UpdatePhotoCommandHandler(IDocumentApiProxy documentApiProxy,UserManager<Account> userManager)
        {
            _documentApiProxy=documentApiProxy;
            _userManager=userManager;
        }
        public async Task<Response> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AccountId);
            long docId=new long();
            if (user != null && user.PhotoId.HasValue)
            {
                docId = user.PhotoId.Value;
            }
        
            if (user != null && docId>0 )
            {
                Console.WriteLine(docId);
                await _documentApiProxy.UpdateBlobAsync(new FileParameter(request.File.OpenReadStream(),request.File.FileName,request.File.ContentType),docId,(SubjectUpdate)request.SubjectUpdate,cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }
    }
}
