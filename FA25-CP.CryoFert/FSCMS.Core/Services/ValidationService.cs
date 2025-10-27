using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FSCMS.Core.Exceptions;
using FSCMS.Core.Interfaces;

namespace FSCMS.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IValidationService"/> interface.
    /// </summary>
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> ValidateAsync<T>(T model)
        {
            if (model == null)
            {
                throw new BadRequestException("Model is invalid");
            }

            var validator = _serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;

            if (validator == null)
            {
                throw new InvalidOperationException($"No validator found for type {typeof(T).Name}");
            }

            return await validator.ValidateAsync(model);
        }

        /// <inheritdoc/>
        public async Task ValidateAndThrowAsync<T>(T model)
        {
            var validationResult = await ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new BadRequestException(errors);
            }
        }
    }
}
