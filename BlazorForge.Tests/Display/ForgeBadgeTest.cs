using Bunit;
using BlazorForge.Components.Display;

namespace BlazorForge.Tests.Display;

public class ForgeBadgeTests : BunitContext
{
    [Fact]
    public void Badge_RendersChildContent()
    {
        var cut = Render<ForgeBadge>(parameters =>
        {
            parameters.AddChildContent("Active");
        });

        Assert.Contains("Active", cut.Markup);
    }

    [Theory]
    [InlineData(BadgeVariant.Primary, "forge-badge-primary")]
    [InlineData(BadgeVariant.Success, "forge-badge-success")]
    [InlineData(BadgeVariant.Warning, "forge-badge-warning")]
    [InlineData(BadgeVariant.Danger, "forge-badge-danger")]
    [InlineData(BadgeVariant.Neutral, "forge-badge-neutral")]
    public void Badge_RendersCorrectVariant(
        BadgeVariant variant,
        string expectedClass)
    {
        var cut = Render<ForgeBadge>(parameters =>
        {
            parameters.Add(p => p.Variant, variant);
        });

        Assert.Contains(expectedClass, cut.Markup);
    }

    [Theory]
    [InlineData(BadgeSize.Small, "forge-badge-small")]
    [InlineData(BadgeSize.Medium, "forge-badge-medium")]
    public void Badge_RendersCorrectSize(
        BadgeSize size,
        string expectedClass)
    {
        var cut = Render<ForgeBadge>(parameters =>
        {
            parameters.Add(p => p.Size, size);
        });

        Assert.Contains(expectedClass, cut.Markup);
    }

    [Fact]
    public void Badge_DoesNotRenderDotByDefault()
    {
        var cut = Render<ForgeBadge>();

        Assert.Empty(cut.FindAll(".forge-badge-dot"));
    }

    [Fact]
    public void Badge_RendersDotWhenEnabled()
    {
        var cut = Render<ForgeBadge>(parameters =>
        {
            parameters.Add(p => p.ShowDot, true);
        });

        Assert.Single(cut.FindAll(".forge-badge-dot"));
    }
}