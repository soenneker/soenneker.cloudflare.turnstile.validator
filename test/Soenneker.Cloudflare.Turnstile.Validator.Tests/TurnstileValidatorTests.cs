using System.Threading.Tasks;
using AwesomeAssertions;
using Soenneker.Cloudflare.Turnstile.Validator.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Cloudflare.Turnstile.Validator.Tests;

[Collection("Collection")]
public class TurnstileValidatorTests : FixturedUnitTest
{
    private readonly ITurnstileValidator _validator;

    public TurnstileValidatorTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _validator = Resolve<ITurnstileValidator>(true);
    }

    [Fact]
    public async Task Validate_should_validate()
    {
        var result = await _validator.Validate("XXXX.DUMMY.TOKEN.XXXX");
        result.Should().BeTrue();
    }
}
