using FluentValidation;
using TT.Deliveries.Application.Features.Delivery.Command;

namespace TT.Deliveries.Application.Validators
{
    public class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
    {
        public CreateDeliveryCommandValidator()
        {
            RuleFor(x => x.deliveryDto.Order.OrderNumber).NotEmpty().GreaterThan(0);
            RuleFor(x => x.deliveryDto.Order.Sender).NotEmpty().MinimumLength(3);
            RuleFor(x=> x.deliveryDto.Recipient.Address).NotEmpty().MinimumLength(3);
            RuleFor(x => x.deliveryDto.Recipient.Email).NotEmpty().MinimumLength(3);
            RuleFor(x => x.deliveryDto.Recipient.PhoneNumber).NotEmpty().MinimumLength(10);
            RuleFor(x => x.deliveryDto.Recipient.Name).NotEmpty().MinimumLength(3);
        }
    }
}
