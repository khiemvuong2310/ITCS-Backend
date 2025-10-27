using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Defines a client for making REST API calls to external services.
    /// This interface abstracts the HTTP client to enable better testability and consistency across the application.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Sends a GET request to the specified URI and deserializes the response content to the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response content to.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deserialized response content.</returns>
        Task<TResponse?> GetAsync<TResponse>(string uri, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a POST request with the specified content to the specified URI and deserializes the response content to the specified type.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request content.</typeparam>
        /// <typeparam name="TResponse">The type to deserialize the response content to.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="data">The request content to send.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deserialized response content.</returns>
        Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a POST request with the specified content to the specified URI without expecting a typed response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request content.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="data">The request content to send.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task PostAsync<TRequest>(string uri, TRequest data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a PUT request with the specified content to the specified URI and deserializes the response content to the specified type.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request content.</typeparam>
        /// <typeparam name="TResponse">The type to deserialize the response content to.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="data">The request content to send.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deserialized response content.</returns>
        Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a PUT request with the specified content to the specified URI without expecting a typed response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request content.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="data">The request content to send.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task PutAsync<TRequest>(string uri, TRequest data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a DELETE request to the specified URI and deserializes the response content to the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response content to.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the deserialized response content.</returns>
        Task<TResponse?> DeleteAsync<TResponse>(string uri, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a DELETE request to the specified URI without expecting a typed response.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(string uri, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends an HTTP request message with the provided configuration and returns the raw HTTP response message.
        /// </summary>
        /// <param name="requestMessage">The HTTP request message to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the HTTP response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);
    }
}
