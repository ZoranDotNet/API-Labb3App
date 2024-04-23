using FluentValidation;

namespace API_Labb3.Filter
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
           EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is null)
            {
                return await next(context);
            }

            var obj = context.Arguments.OfType<T>().FirstOrDefault();

            if (obj is null)
            {
                return Results.Problem("The object to validate could not be found");
            }

            var validationsResult = await validator.ValidateAsync(obj);

            if (!validationsResult.IsValid)
            {
                return Results.ValidationProblem(validationsResult.ToDictionary());
            }

            return await next(context);
        }
    }
}
