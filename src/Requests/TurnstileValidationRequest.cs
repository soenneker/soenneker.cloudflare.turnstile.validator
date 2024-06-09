using System.Text.Json.Serialization;

namespace Soenneker.Cloudflare.Turnstile.Validator.Requests;

public record TurnstileValidationRequest
{
    [JsonPropertyName("secret")]
    public string Secret { get; set; } = default!;

    [JsonPropertyName("response")]
    public string Response { get; set; } = default!;

    [JsonPropertyName("remoteip")]
    public string? RemoteIp { get; set; }

    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }
}