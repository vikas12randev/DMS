using FluentValidation;
using TT.Deliveries.Application.Features.Delivery.Queries;

namespace TT.Deliveries.Application.Validators
{
    public class GetDeliveryByOrderIdQueryValidator : AbstractValidator<GetDeliveryByOrderIdQuery>
    {
        public GetDeliveryByOrderIdQueryValidator() 
        { 
            RuleFor(x=> x.orderId).NotEmpty().GreaterThan(0);
        }
    }
}
