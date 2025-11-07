using DomainModels.Entities.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http.Headers;

namespace E_Club.WebAPI.Extensions.HealthCheck
{
    public class AuthFlowHealthCheck : IHealthCheck
    {
        private readonly string _loginUrl;
        private readonly object _loginBody;
        private readonly string _protectedUrl;

        public AuthFlowHealthCheck(
            string loginUrl,
            object loginBody,
            string protectedUrl,
            HttpMethod protectedMethod,
            object? protectedBody = null)
        {
            _loginUrl = loginUrl;
            _loginBody = loginBody;
            _protectedUrl = protectedUrl;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient();
            var loginResp = await client.PostAsJsonAsync(_loginUrl, _loginBody, cancellationToken);

            if (!loginResp.IsSuccessStatusCode)
                return HealthCheckResult.Unhealthy($"Connexion impossible ... : {(int)loginResp.StatusCode} {loginResp.ReasonPhrase}");

            var loginJson = await loginResp.Content.ReadFromJsonAsync<ResponseApi<object>>(cancellationToken: cancellationToken);

            if ((bool)loginJson!.Success!)
            {
                var token = loginJson!.Data!.ToString();

                if (string.IsNullOrWhiteSpace(token))
                    return HealthCheckResult.Unhealthy("Connexion impossible ... : Json Web Token introuvable dans la réponse");

                using var client2 = new HttpClient();
                using var req = new HttpRequestMessage(HttpMethod.Get, _protectedUrl);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp2 = await client2.SendAsync(req, cancellationToken);

                if (!resp2.IsSuccessStatusCode)
                    return HealthCheckResult.Unhealthy($"Connexion réussie ! Problème sur l'endpoint protégé : {(int)resp2.StatusCode} {resp2.ReasonPhrase}");

                return HealthCheckResult.Healthy("Connexion réussie et Endpoint atteint !");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Connexion impossible ... : Erreurs liées au formulaire de login");
            }
        }
    }
}