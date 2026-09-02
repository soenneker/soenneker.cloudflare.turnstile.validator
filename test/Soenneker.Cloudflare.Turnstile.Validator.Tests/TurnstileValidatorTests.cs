using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Soenneker.Cloudflare.Turnstile.Validator.Abstract;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Cloudflare.Turnstile.Validator.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class TurnstileValidatorTests : HostedUnitTest
{
    private readonly ITurnstileValidator _validator;

    public TurnstileValidatorTests(Host host) : base(host)
    {
        _validator = Resolve<ITurnstileValidator>(true);
    }

    [Test]
    public async Task Validate_should_validate(CancellationToken cancellationToken)
    {
        var result = await _validator.Validate("XXXX.DUMMY.TOKEN.XXXX", cancellationToken: cancellationToken);
        result.Should().BeTrue();
    }
}
