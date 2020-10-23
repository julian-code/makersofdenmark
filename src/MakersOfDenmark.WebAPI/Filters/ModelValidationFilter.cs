using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MakersOfDenmark.WebAPI.Filters
{
    public class ModelValidationActionFilter : IAsyncActionFilter
    {
        private readonly IValidatorFactory _validatorFactory;
        public ModelValidationActionFilter(IValidatorFactory validatorFactory) => _validatorFactory = validatorFactory;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var allErrors = new Dictionary<string, object>();

            // Short-circuit if there's nothing to validate
            if (context.ActionArguments.Count == 0)
            {
                await next();
                return;
            }

            foreach (var (key, value) in context.ActionArguments)
            {
                // skip null values
                if (value == null)
                    continue;

                var validator = _validatorFactory.GetValidator(value.GetType());

                // skip objects with no validators
                if (validator == null)
                    continue;
                
                validator.CanValidateInstancesOfType(value.GetType());
                var validationContext = new ValidationContext<object>(value);
                // validate
                var result = await validator.ValidateAsync(validationContext);

                // if it's valid, continue
                if (result.IsValid) continue;

                // if there are errors, copy to the response dictonary
                var dict = new Dictionary<string, Error>();

                foreach (var e in result.Errors)
                    dict[e.PropertyName] = new Error { Message = e.ErrorMessage, AttemptedValue = e.AttemptedValue };

                allErrors.Add(key, dict);
            }

            if (allErrors.Any())
            {
                    context.Result = new BadRequestObjectResult(allErrors);
            }
            else
                await next();
        }
    }

    internal class Error
    {
        public string Message { get; set; }
        public object AttemptedValue { get; set; }
    }
}