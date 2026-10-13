using Bunit;
using BlazorForge.Components.Feedback;

namespace BlazorForge.Tests.Feedback;

public class ForgeSpinnerTests : BunitContext
{
    [Fact]
    public void Spinner_RendersStatusRole()
    {
        var cut = Render<ForgeSpinner>();

        Assert.Equal("status", cut.Find("[role='status']")
                                  .GetAttribute("role"));
    }

    [Fact]
    public void Spinner_RendersLabel()
    {
        var cut = Render<ForgeSpinner>(parameters =>
        {
            parameters.Add(p => p.Label, "Loading users...");
        });

        Assert.Contains("Loading users...", cut.Markup);
    }

    [Theory]
    [InlineData(SpinnerSize.Small, "forge-spinner-small")]
    [InlineData(SpinnerSize.Medium, "forge-spinner-medium")]
    [InlineData(SpinnerSize.Large, "forge-spinner-large")]
    public void Spinner_RendersCorrectSize(
        SpinnerSize size,
        string expectedClass)
    {
        var cut = Render<ForgeSpinner>(parameters =>
        {
            parameters.Add(p => p.Size, size);
        });

        Assert.Contains(expectedClass, cut.Markup);
    }
}