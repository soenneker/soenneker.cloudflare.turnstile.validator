using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Soenneker.Cloudflare.Turnstile.Validator.Responses;

public record TurnstileValidationResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    public DateTime? ChallengeTimestamp { get; set; }

    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    [JsonPropertyName("error-codes")]
    public List<string> ErrorCodes { get; set; } = default!;

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("cdata")]
    public string? Cdata { get; set; }
}