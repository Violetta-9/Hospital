﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllFreeServices
{
    public class GetAllFreeServicesQuery : IRequest<OutServicesDto[]>
    {
    }
}
