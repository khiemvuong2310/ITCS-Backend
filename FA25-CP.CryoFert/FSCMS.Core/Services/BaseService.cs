using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Exceptions;
using FSCMS.Core.Interfaces;

namespace FSCMS.Core.Services
{
    public abstract class BaseService
    {
        private readonly IValidationService _validationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="validationService">The validation service.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="validationService"/> is null.</exception>
        protected BaseService(IValidationService validationService)
        {
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        /// <summary>
        /// Validates the specified request and throws an exception if validation fails.
        /// </summary>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <param name="request">The request to validate.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="request"/> is null.</exception>
        public async Task ValidateAndThrowAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([NotNull] T? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await _validationService.ValidateAndThrowAsync(request);
        }

        /// <summary>
        /// Throws a <see cref="NotFoundException"/> if the specified entity is not found.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to check.</param>
        /// <param name="id">The ID of the entity.</param>
        /// <exception cref="NotFoundException">Thrown when the entity is not found.</exception>
        public void ThrowIfNotFound<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([NotNull] T? entity, Guid id)
        {
            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }
        }

        /// <summary>
        /// Throws a <see cref="NotFoundException"/> if the specified entity is not found.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to check.</param>
        /// <param name="id">The ID of the entity.</param>
        /// <exception cref="NotFoundException">Thrown when the entity is not found.</exception>
        public void ThrowIfNotFound<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([NotNull] T? entity, string field)
        {
            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, field);
            }
        }

        /// <summary>
        /// Throws a <see cref="BadRequestException"/> if the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="BadRequestException">Thrown when the condition is true.</exception>
        public void ThrowBadRequest(string message)
        {
            throw new BadRequestException(message);
        }

        /// <summary>
        /// Throws a <see cref="BadRequestException"/> if the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="BadRequestException">Thrown when the condition is true.</exception>
        public void ThrowBadRequestWithCondition(bool condition, string message)
        {
            if (condition)
            {
                ThrowBadRequest(message);
            }
        }

        public void ThrowTooManyRequests(string message)
        {
            throw new TooManyRequestsException(message);
        }

        public void ThrowTooManyRequestsWithCondition(bool condition, string message)
        {
            if (condition)
            {
                ThrowTooManyRequests(message);
            }
        }

        /// <summary>
        /// Throws a <see cref="ConflictException"/> with the specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <exception cref="ConflictException">Always thrown.</exception>
        public void ThrowConflict(string message)
        {
            throw new ConflictException(message);
        }

        /// <summary>
        /// Throws a <see cref="ConflictException"/> if the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message.</param>
        /// <exception cref="ConflictException">Thrown when the condition is true.</exception>
        public void ThrowConflictWithCondition(bool condition, string message)
        {
            if (condition)
            {
                ThrowConflict(message);
            }
        }

        public void ThrowNotFound(string message)
        {
            throw new NotFoundException(message);
        }

        public void ThrowNotFoundWithCondition(bool condition, string message)
        {
            if (condition)
            {
                ThrowNotFound(message);
            }
        }

        public void ThrowForbiddenWithCondition(bool condition, string message)
        {
            if (condition)
            {
                throw new ForbiddenException(message);
            }
        }
    }
}
