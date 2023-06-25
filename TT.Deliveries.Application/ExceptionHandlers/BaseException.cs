namespace TT.Deliveries.Application.ExceptionHandlers
{    public abstract class BaseException : Exception
    {
        protected BaseException()
        {
        }

        protected BaseException(string? message, Exception innerException) : base(message, innerException)
        {
        }

        public IEnumerable<AppError>? Errors { get; set; }
    }
}
