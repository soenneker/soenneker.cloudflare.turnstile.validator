using System.Text.Json.Serialization;

namespace Soenneker.Cloudflare.Turnstile.Validator.Requests;

/// <summary>
/// Represents the turnstile validation request record.
/// </summary>
public record TurnstileValidationRequest
{
    /// <summary>
    /// Gets or sets secret.
    /// </summary>
    [JsonPropertyName("secret")]
    public string Secret { get; set; } = default!;

    /// <summary>
    /// Gets or sets response.
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; set; } = default!;

    /// <summary>
    /// Gets or sets remote ip.
    /// </summary>
    [JsonPropertyName("remoteip")]
    public string? RemoteIp { get; set; }

    /// <summary>
    /// Gets or sets idempotency key.
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }
}