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
    public static void AddTurnstileValidatorAsSingleton(this IServiceCollection services)
    {
        services.AddTurnstileClientAsSingleton();
        services.TryAddSingleton<ITurnstileValidator, TurnstileValidator>();
    }

    /// <summary>
    /// Adds <see cref="ITurnstileValidator"/> as a scoped service. <para/>
    /// </summary>
    public static void AddTurnstileValidatorAsScoped(this IServiceCollection services)
    {
        services.AddTurnstileClientAsScoped();
        services.TryAddScoped<ITurnstileValidator, TurnstileValidator>();
    }
}
