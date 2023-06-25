using FluentValidation;
using TT.Deliveries.Application.Features.Delivery.Commands;

namespace TT.Deliveries.Application.Validators
{
    public class UpdateDeliveryCommandValidator : AbstractValidator<UpdateDeliveryCommand>
    {
        public UpdateDeliveryCommandValidator() 
        {
            RuleFor(x => x.updateDeliveryDto.OrderId).GreaterThan(0).WithMessage("Invalid)");
            RuleFor(x => x.updateDeliveryDto.Roles).IsInEnum();
        }
    }
}
