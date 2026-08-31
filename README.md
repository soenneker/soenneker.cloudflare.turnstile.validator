[![](https://img.shields.io/nuget/v/soenneker.cloudflare.turnstile.validator.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.turnstile.validator/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.turnstile.validator/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.turnstile.validator/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.turnstile.validator.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.turnstile.validator/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.turnstile.validator/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.turnstile.validator/actions/workflows/codeql.yml)

# Soenneker.Cloudflare.Turnstile.Validator

Validates Cloudflare Turnstile tokens server-side through the Siteverify API.

## Installation

```bash
dotnet add package Soenneker.Cloudflare.Turnstile.Validator
```

## Configuration

```json
{
  "Cloudflare": {
    "Turnstile": {
      "Secret": "your-widget-secret"
    }
  }
}
```

Keep the secret on the server. It must never be sent to a browser or committed to source control.

## Registration

```csharp
using Soenneker.Cloudflare.Turnstile.Validator.Registrars;

services.AddTurnstileValidatorAsScoped();
```

The scoped validator borrows a singleton Turnstile HTTP client. Disposing a request scope does not tear down that shared client. Singleton validator registration is also available.

## Usage

```csharp
using Soenneker.Cloudflare.Turnstile.Validator.Abstract;

bool valid = await turnstileValidator.Validate(
    tokenFromForm,
    remoteIp: httpContext.Connection.RemoteIpAddress?.ToString(),
    cancellationToken);
```

`Validate` returns `true` only when Cloudflare returns `success: true`; an empty or unsuccessful response returns `false`. Transport and deserialization failures are not converted into validation failures and may propagate from the HTTP layer.

Use `GetResponse` when the application relies on widget actions, host restrictions, or diagnostic error codes:

```csharp
using Soenneker.Cloudflare.Turnstile.Validator.Responses;

TurnstileValidationResponse? response =
    await turnstileValidator.GetResponse(token, remoteIp, cancellationToken);

bool accepted = response is { Success: true, Action: "checkout", Hostname: "example.com" };
```

A successful Siteverify response alone does not enforce your expected `Action` or `Hostname`; compare those fields yourself when they are part of the security decision. Tokens are single-use and short-lived, so validate them at the point of the protected server operation rather than pre-validating and reusing the result later.

The optional remote IP is sent to Cloudflare as validation context. Pass the actual client IP only when your proxy/header trust configuration is correct; do not blindly trust a forwarded header from the public internet.
