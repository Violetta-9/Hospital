using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Office.Application.Contracts.Incoming;

namespace Office.Application.Command.CreateOffice
{
    public class CreateOfficeCommand : IRequest<long>
    {
        public CreateOfficeDTO OfficeDto { get; set; }

        public CreateOfficeCommand(CreateOfficeDTO officeDto)
        {
            OfficeDto=officeDto;
        }
    }
}
