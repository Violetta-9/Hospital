using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.DeleteReceptionist;

public class DeleteReceptionistCommand : IRequest<Response>
{
    public string AccountId { get; set; }

    public DeleteReceptionistCommand(string accountId)
    {
        AccountId = accountId;
    }
}