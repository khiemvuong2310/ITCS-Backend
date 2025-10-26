using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Service for validating models using FluentValidation.
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validates a model using the appropriate validator.
        /// </summary>
        /// <typeparam name="T">The type of model to validate.</typeparam>
        /// <param name="model">The model to validate.</param>
        /// <returns>The validation result.</returns>
        Task<ValidationResult> ValidateAsync<T>(T model);

        /// <summary>
        /// Validates a model and throws a BadRequestException if validation fails.
        /// </summary>
        /// <typeparam name="T">The type of model to validate.</typeparam>
        /// <param name="model">The model to validate.</param>
        /// <exception cref="Exceptions.BadRequestException">Thrown when validation fails.</exception>
        Task ValidateAndThrowAsync<T>(T model);
    }
}
