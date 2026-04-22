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
    public async Task Validate_should_validate()
    {
        var result = await _validator.Validate("XXXX.DUMMY.TOKEN.XXXX");
        result.Should().BeTrue();
    }
}
