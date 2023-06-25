using FluentValidation.Results;
namespace TT.Deliveries.Application.ExceptionHandlers
{
    public class TTException : BaseException
    {
        public TTException(string code, string message)
        {
            Errors = new List<AppError> { new AppError(code, message) };
        }
        public TTException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.Select(e => new AppError(e.ErrorCode, e.ErrorMessage));
        }
    }
}
