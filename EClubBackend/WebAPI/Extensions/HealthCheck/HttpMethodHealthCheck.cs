using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace E_Club.WebAPI.Extensions.HealthCheck
{
    public class HttpMethodHealthCheck : IHealthCheck
    {
        private readonly string _url;
        private readonly HttpMethod _method;
        private readonly object? _body;

        public HttpMethodHealthCheck(string url, HttpMethod method, object? body = null)
        {
            _url = url;
            _method = method;
            _body = body;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = new HttpClient();
                using var request = new HttpRequestMessage(_method, _url);

                if (_body != null && _method != HttpMethod.Get && _method != HttpMethod.Delete)
                {
                    request.Content = JsonContent.Create(_body);
                }

                var response = await client.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy($"{_method} {_url} OK ({(int)response.StatusCode})");

                return HealthCheckResult.Unhealthy($"{_method} {_url} → {(int)response.StatusCode} {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"{_method} {_url} failed: {ex.Message}");
            }
        }
    }
}
