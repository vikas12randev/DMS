using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using TT.Deliveries.Application.ExceptionHandlers;

namespace TT.Deliveries.Web.Api.Middleware
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var errors = (await Task.WhenAll(_validators
                .Select(async x => await x.ValidateAsync(context))))
                .SelectMany(x => x.Errors);

                if (errors.Any())
                    throw new TTException(errors);
            }

            return await next();
        }
    }
}
