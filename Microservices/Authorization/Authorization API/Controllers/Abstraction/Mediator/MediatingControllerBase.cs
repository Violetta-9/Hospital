using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Authorization_API.Controllers.Abstraction.Mediator
{
    public class MediatingControllerBase:ControllerBase
    {
        protected readonly IMediator _mediator;
        public MediatingControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> SendRequestAsync<T>(T request,CancellationToken cancellation=default)
        {

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
