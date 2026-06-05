using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Soenneker.Cloudflare.Turnstile.Validator.Responses;

/// <summary>
/// Represents the turnstile validation response record.
/// </summary>
public record TurnstileValidationResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether success.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets challenge timestamp.
    /// </summary>
    [JsonPropertyName("challenge_ts")]
    public DateTime? ChallengeTimestamp { get; set; }

    /// <summary>
    /// Gets or sets hostname.
    /// </summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    /// <summary>
    /// Gets or sets error codes.
    /// </summary>
    [JsonPropertyName("error-codes")]
    public List<string> ErrorCodes { get; set; } = default!;

    /// <summary>
    /// Gets or sets action.
    /// </summary>
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    /// <summary>
    /// Gets or sets cdata.
    /// </summary>
    [JsonPropertyName("cdata")]
    public string? Cdata { get; set; }
}