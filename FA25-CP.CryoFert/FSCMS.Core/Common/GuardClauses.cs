using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Common
{
    /// <summary>
    /// Provides a simple API for performing parameter validation through guard clauses.
    /// </summary>
    /// <example>
    /// Guard.Against.Null(parameter, nameof(parameter));
    /// Guard.Against.NullOrEmpty(stringParameter, nameof(stringParameter));
    /// </example>
    public static class Guard
    {
        /// <summary>
        /// Gets the guard clause instance.
        /// </summary>
        public static IGuardClause Against { get; } = new GuardClause();

        /// <summary>
        /// Implementation of guard clauses for parameter validation.
        /// </summary>
        public class GuardClause : IGuardClause
        {
            /// <summary>
            /// Guards against null parameter values.
            /// </summary>
            /// <param name="input">The value to check.</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
            public void Null(object? input, string parameterName, string? message = null)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(parameterName, message ?? $"Parameter {parameterName} cannot be null.");
                }
            }

            /// <summary>
            /// Guards against null or empty string parameter values.
            /// </summary>
            /// <param name="input">The string to check.</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
            public void NullOrEmpty(string? input, string parameterName, string? message = null)
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException(message ?? $"Parameter {parameterName} cannot be null or empty.", parameterName);
                }
            }

            /// <summary>
            /// Guards against null, empty, or whitespace-only string parameter values.
            /// </summary>
            /// <param name="input">The string to check.</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentException">Thrown when the input is null, empty, or whitespace-only.</exception>
            public void NullOrWhiteSpace(string? input, string parameterName, string? message = null)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException(message ?? $"Parameter {parameterName} cannot be null or whitespace.", parameterName);
                }
            }

            /// <summary>
            /// Guards against null or empty collection parameter values.
            /// </summary>
            /// <typeparam name="T">The type of elements in the collection.</typeparam>
            /// <param name="input">The collection to check.</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
            public void NullOrEmpty<T>(IEnumerable<T>? input, string parameterName, string? message = null)
            {
                if (input == null || !input.Any())
                {
                    throw new ArgumentException(message ?? $"Parameter {parameterName} cannot be null or empty.", parameterName);
                }
            }

            /// <summary>
            /// Guards against integer parameter values outside a specified range.
            /// </summary>
            /// <param name="input">The value to check.</param>
            /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
            /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
            public void OutOfRange(int input, int rangeFrom, int rangeTo, string parameterName, string? message = null)
            {
                if (input < rangeFrom || input > rangeTo)
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Parameter {parameterName} was out of range. Must be between {rangeFrom} and {rangeTo}.");
                }
            }

            /// <summary>
            /// Guards against long parameter values outside a specified range.
            /// </summary>
            /// <param name="input">The value to check.</param>
            /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
            /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
            public void OutOfRange(long input, long rangeFrom, long rangeTo, string parameterName, string? message = null)
            {
                if (input < rangeFrom || input > rangeTo)
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Parameter {parameterName} was out of range. Must be between {rangeFrom} and {rangeTo}.");
                }
            }

            /// <summary>
            /// Guards against decimal parameter values outside a specified range.
            /// </summary>
            /// <param name="input">The value to check.</param>
            /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
            /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
            public void OutOfRange(decimal input, decimal rangeFrom, decimal rangeTo, string parameterName, string? message = null)
            {
                if (input < rangeFrom || input > rangeTo)
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Parameter {parameterName} was out of range. Must be between {rangeFrom} and {rangeTo}.");
                }
            }

            /// <summary>
            /// Guards against double parameter values outside a specified range.
            /// </summary>
            /// <param name="input">The value to check.</param>
            /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
            /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
            public void OutOfRange(double input, double rangeFrom, double rangeTo, string parameterName, string? message = null)
            {
                if (input < rangeFrom || input > rangeTo)
                {
                    throw new ArgumentOutOfRangeException(parameterName, message ?? $"Parameter {parameterName} was out of range. Must be between {rangeFrom} and {rangeTo}.");
                }
            }

            /// <summary>
            /// Guards against struct parameter values that are set to their default value.
            /// </summary>
            /// <typeparam name="T">The type of the struct.</typeparam>
            /// <param name="input">The value to check.</param>
            /// <param name="parameterName">The name of the parameter being checked.</param>
            /// <param name="message">Optional custom error message.</param>
            /// <exception cref="ArgumentException">Thrown when the input is the default value for its type.</exception>
            public void Default<T>(T input, string parameterName, string? message = null) where T : struct
            {
                if (EqualityComparer<T>.Default.Equals(input, default))
                {
                    throw new ArgumentException(message ?? $"Parameter {parameterName} cannot be the default value.", parameterName);
                }
            }
        }
    }

    /// <summary>
    /// Defines the contract for guard clause operations for parameter validation.
    /// </summary>
    public interface IGuardClause
    {
        /// <summary>
        /// Guards against null parameter values.
        /// </summary>
        /// <param name="input">The value to check.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        void Null(object? input, string parameterName, string? message = null);

        /// <summary>
        /// Guards against null or empty string parameter values.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
        void NullOrEmpty(string? input, string parameterName, string? message = null);

        /// <summary>
        /// Guards against null, empty, or whitespace-only string parameter values.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentException">Thrown when the input is null, empty, or whitespace-only.</exception>
        void NullOrWhiteSpace(string? input, string parameterName, string? message = null);

        /// <summary>
        /// Guards against null or empty collection parameter values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="input">The collection to check.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
        void NullOrEmpty<T>(IEnumerable<T>? input, string parameterName, string? message = null);

        /// <summary>
        /// Guards against integer parameter values outside a specified range.
        /// </summary>
        /// <param name="input">The value to check.</param>
        /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
        /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
        void OutOfRange(int input, int rangeFrom, int rangeTo, string parameterName, string? message = null);

        /// <summary>
        /// Guards against long parameter values outside a specified range.
        /// </summary>
        /// <param name="input">The value to check.</param>
        /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
        /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
        void OutOfRange(long input, long rangeFrom, long rangeTo, string parameterName, string? message = null);

        /// <summary>
        /// Guards against decimal parameter values outside a specified range.
        /// </summary>
        /// <param name="input">The value to check.</param>
        /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
        /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
        void OutOfRange(decimal input, decimal rangeFrom, decimal rangeTo, string parameterName, string? message = null);

        /// <summary>
        /// Guards against double parameter values outside a specified range.
        /// </summary>
        /// <param name="input">The value to check.</param>
        /// <param name="rangeFrom">The minimum allowed value (inclusive).</param>
        /// <param name="rangeTo">The maximum allowed value (inclusive).</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the input is outside the specified range.</exception>
        void OutOfRange(double input, double rangeFrom, double rangeTo, string parameterName, string? message = null);

        /// <summary>
        /// Guards against struct parameter values that are set to their default value.
        /// </summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="input">The value to check.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentException">Thrown when the input is the default value for its type.</exception>
        void Default<T>(T input, string parameterName, string? message = null) where T : struct;
    }
}
