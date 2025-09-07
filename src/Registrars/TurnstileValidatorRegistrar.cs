using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.Turnstile.Client.Registrars;
using Soenneker.Cloudflare.Turnstile.Validator.Abstract;

namespace Soenneker.Cloudflare.Turnstile.Validator.Registrars;

/// <summary>
/// A validation module checking Cloudflare Turnstile tokens
/// </summary>
public static class TurnstileValidatorRegistrar
{
    /// <summary>
    /// Adds <see cref="ITurnstileValidator"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTurnstileValidatorAsSingleton(this IServiceCollection services)
    {
        services.AddTurnstileClientAsSingleton().TryAddSingleton<ITurnstileValidator, TurnstileValidator>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ITurnstileValidator"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTurnstileValidatorAsScoped(this IServiceCollection services)
    {
        services.AddTurnstileClientAsScoped().TryAddScoped<ITurnstileValidator, TurnstileValidator>();

        return services;
    }
}