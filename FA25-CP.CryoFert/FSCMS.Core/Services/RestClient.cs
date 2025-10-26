using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FSCMS.Core.Common;
using FSCMS.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace FSCMS.Core.Services
{
    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RestClient> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestClient(IHttpClientFactory httpClientFactory, ILogger<RestClient> logger)
        {
            Guard.Against.Null(httpClientFactory, nameof(httpClientFactory));
            Guard.Against.Null(logger, nameof(logger));

            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                // Consider adding other options like:
                // PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // If your APIs use camelCase
            };
        }

        private HttpClient CreateClientWithHeaders(Dictionary<string, string>? headers)
        {
            var client = _httpClientFactory.CreateClient(); // Or a named client
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            // Ensure Accept header for JSON is present for GET requests expecting JSON
            if (!client.DefaultRequestHeaders.Accept.Any(h => h.MediaType == "application/json"))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return client;
        }

        public async Task<TResponse?> GetAsync<TResponse>(string uri, Dictionary<string, string>? headers = null)
        {
            Guard.Against.NullOrWhiteSpace(uri, nameof(uri));
            var httpClient = CreateClientWithHeaders(headers);

            try
            {
                _logger.LogInformation("GET request to {Uri}", uri);
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TResponse>(stream, _jsonSerializerOptions);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during GET to {Uri}. Status Code: {StatusCode}", uri, ex.StatusCode);
                // Optionally, you could return a Result<TResponse> here with error details
                return default;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error during GET from {Uri}", uri);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during GET from {Uri}", uri);
                return default;
            }
        }

        private async Task<TResponse?> SendAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest? data, Dictionary<string, string>? headers = null)
        {
            Guard.Against.NullOrWhiteSpace(uri, nameof(uri));
            var httpClient = CreateClientWithHeaders(headers);

            HttpContent? httpContent = null;
            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var request = new HttpRequestMessage(method, uri) { Content = httpContent };

            try
            {
                _logger.LogInformation("{Method} request to {Uri}", method, uri);
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                if (typeof(TResponse) == typeof(Unit)) // For methods that don't expect a content response
                {
                    return default; // Or some representation of success like (TResponse)(object)Unit.Value
                }

                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TResponse>(stream, _jsonSerializerOptions);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during {Method} to {Uri}. Status Code: {StatusCode}", method, uri, ex.StatusCode);
                return default;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON serialization/deserialization error during {Method} to {Uri}", method, uri);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during {Method} to {Uri}", method, uri);
                return default;
            }
        }
        private async Task SendAsync<TRequest>(HttpMethod method, string uri, TRequest? data, Dictionary<string, string>? headers = null)
        {
            Guard.Against.NullOrWhiteSpace(uri, nameof(uri));
            var httpClient = CreateClientWithHeaders(headers);

            HttpContent? httpContent = null;
            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var request = new HttpRequestMessage(method, uri) { Content = httpContent };

            try
            {
                _logger.LogInformation("{Method} request to {Uri}", method, uri);
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during {Method} to {Uri}. Status Code: {StatusCode}", method, uri, ex.StatusCode);
                // Consider how to propagate this error if not returning a TResponse
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON serialization error during {Method} to {Uri}", method, uri);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during {Method} to {Uri}", method, uri);
                throw;
            }
        }


        public Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data, Dictionary<string, string>? headers = null)
        {
            return SendAsync<TRequest, TResponse>(HttpMethod.Post, uri, data, headers);
        }

        public Task PostAsync<TRequest>(string uri, TRequest data, Dictionary<string, string>? headers = null)
        {
            return SendAsync(HttpMethod.Post, uri, data, headers);
        }

        public Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data, Dictionary<string, string>? headers = null)
        {
            return SendAsync<TRequest, TResponse>(HttpMethod.Put, uri, data, headers);
        }

        public Task PutAsync<TRequest>(string uri, TRequest data, Dictionary<string, string>? headers = null)
        {
            return SendAsync(HttpMethod.Put, uri, data, headers);
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string uri, Dictionary<string, string>? headers = null)
        {
            Guard.Against.NullOrWhiteSpace(uri, nameof(uri));
            var httpClient = CreateClientWithHeaders(headers);

            try
            {
                _logger.LogInformation("DELETE request to {Uri}", uri);
                var response = await httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();

                if (typeof(TResponse) == typeof(Unit)) // For methods that don't expect a content response
                {
                    return default;
                }

                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TResponse>(stream, _jsonSerializerOptions);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during DELETE to {Uri}. Status Code: {StatusCode}", uri, ex.StatusCode);
                return default;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error during DELETE from {Uri}", uri);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during DELETE from {Uri}", uri);
                return default;
            }
        }

        public async Task DeleteAsync(string uri, Dictionary<string, string>? headers = null)
        {
            Guard.Against.NullOrWhiteSpace(uri, nameof(uri));
            var httpClient = CreateClientWithHeaders(headers);

            try
            {
                _logger.LogInformation("DELETE request to {Uri}", uri);
                var response = await httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during DELETE to {Uri}. Status Code: {StatusCode}", uri, ex.StatusCode);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during DELETE from {Uri}", uri);
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            Guard.Against.Null(requestMessage, nameof(requestMessage));
            var httpClient = _httpClientFactory.CreateClient(); // Use a fresh client or one configured for general purpose

            // If headers are attached to requestMessage.Headers, they will be used.
            // If you want to merge with some default headers, logic would be needed here.

            _logger.LogInformation("Sending {Method} request to {Uri}", requestMessage.Method, requestMessage.RequestUri);
            try
            {
                var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                // We don't call EnsureSuccessStatusCode here as the caller might want to handle various statuses.
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error during SendAsync to {Uri}. Status Code: {StatusCode}", requestMessage.RequestUri, ex.StatusCode);
                // It's often better to let the original exception propagate or wrap it if you add more context.
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during SendAsync to {Uri}", requestMessage.RequestUri);
                throw;
            }
        }
    }

    // Represents a type for methods that don't return a value (similar to void but usable as a generic type argument)
    public readonly struct Unit : IEquatable<Unit>
    {
        public static readonly Unit Value = new Unit();
        public bool Equals(Unit other) => true;
        public override bool Equals(object? obj) => obj is Unit;
        public override int GetHashCode() => 0;
        public static bool operator ==(Unit left, Unit right) => true;
        public static bool operator !=(Unit left, Unit right) => false;
    }
}
