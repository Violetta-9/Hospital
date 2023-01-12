﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetSpecializationById
{
    public class GetSpecializationByIdQueryHandler : IRequestHandler<GetSpecializationByIdQuery, SpecializationDTO>
    {
        private readonly ISpecializationRepository _specializationRepository;
        public GetSpecializationByIdQueryHandler(ISpecializationRepository repository)
        {
            _specializationRepository = repository;
        }

        public async Task<SpecializationDTO> Handle(GetSpecializationByIdQuery request, CancellationToken cancellationToken)
        {
           return await _specializationRepository.GetSpecializationByIdAsync(request.SpecializationId, cancellationToken);
            //todo:
           
        }
    }
}