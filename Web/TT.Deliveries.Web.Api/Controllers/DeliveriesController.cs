namespace TT.Deliveries.Web.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using TT.Deliveries.Application.Features.Delivery.Command;
    using TT.Deliveries.Application.Features.Delivery.Commands;
    using TT.Deliveries.Application.Features.Delivery.Queries;
    using TT.Deliveries.Data.Dto.Response;
    using TT.Deliveries.Data.Enums;
    using TT.Deliveries.Web.Api.Filters;

    [Route("deliveries")]
    [ApiExceptionFiter]
    [ApiController]
    [Produces("application/json")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{orderId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(DeliveryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<DeliveryDto> GetDelieveryByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetDeliveryByOrderIdQuery(orderId, cancellationToken));
        }

        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(typeof(DeliveryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateDelievery(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
             var response = await _mediator.Send(request, cancellationToken);
             return Ok(response);
        }

        [Route("Update")]
        [HttpPut]
        [ProducesResponseType(typeof(DeliveryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DeliveryDto>> UpdateDelievery(UpdateDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [Route("{orderId:int}")]
        [HttpDelete]
        [ProducesResponseType(typeof(DeliveryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteDelievery(int orderId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteDeliveryCommand(orderId));
            return Ok(response);
        }
    }
}


