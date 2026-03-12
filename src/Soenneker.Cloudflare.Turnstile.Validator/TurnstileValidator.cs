using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Turnstile.Client.Abstract;
using Soenneker.Cloudflare.Turnstile.Validator.Abstract;
using Soenneker.Cloudflare.Turnstile.Validator.Requests;
using Soenneker.Cloudflare.Turnstile.Validator.Responses;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.HttpClient;
using System;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Cloudflare.Turnstile.Validator;

/// <inheritdoc cref="ITurnstileValidator"/>
public sealed class TurnstileValidator : Validators.Validator.Validator, ITurnstileValidator
{
    private readonly ITurnstileClient _turnstileClient;

    private readonly string _secret;

    public TurnstileValidator(ILogger<TurnstileValidator> logger, ITurnstileClient turnstileClient, IConfiguration configuration) : base(logger)
    {
        _turnstileClient = turnstileClient;
        _secret = configuration.GetValueStrict<string>("Cloudflare:Turnstile:Secret");
    }

    public async ValueTask<bool> Validate(string token, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        TurnstileValidationResponse? response = await GetResponse(token, remoteIp, cancellationToken).NoSync();

        if (response is not { Success: true })
            return false;

        return true;
    }

    public async ValueTask<TurnstileValidationResponse?> GetResponse(string token, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        var idempotencyKey = Guid.NewGuid().ToString();

        HttpClient client = await _turnstileClient.Get(cancellationToken).NoSync();

        var request = new TurnstileValidationRequest
        {
            RemoteIp = remoteIp,
            Response = token,
            Secret = _secret,
            IdempotencyKey = idempotencyKey
        };

        return await client.SendToTypeWithRetry<TurnstileValidationResponse>(HttpMethod.Post, "https://challenges.cloudflare.com/turnstile/v0/siteverify", request, 2, Logger,
            TimeSpan.FromSeconds(1), true, cancellationToken).NoSync();
    }
}