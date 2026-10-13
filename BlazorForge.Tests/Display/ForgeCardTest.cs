using Bunit;
using BlazorForge.Components.Display;
using Microsoft.AspNetCore.Components;
namespace BlazorForge.Tests.Display;

public class ForgeCardTests : BunitContext
{
    [Fact]
    public void Card_RendersChildContent()
    {
        var cut = Render<ForgeCard>(parameters =>
            parameters.AddChildContent("<p>Course description</p>"));

        Assert.Contains("Course description", cut.Markup);
    }

    [Fact]
    public void Card_RendersHeader()
    {
        RenderFragment HeaderContent = builder =>
            builder.AddContent(0, "Course Title");
        var cut = Render<ForgeCard>(parameters =>
        parameters.Add(p => p.Header, HeaderContent ));

        Assert.Contains("Course Title", cut.Markup);
        Assert.Single(cut.FindAll(".forge-card-header"));
    }

    [Fact]
    public void Card_RendersFooter()
    {
        RenderFragment footerContent = builder =>
            builder.AddContent(0, "Course actions");

        var cut = Render<ForgeCard>(parameters =>
            parameters.Add(p => p.Footer, footerContent));

        Assert.Contains("Course actions", cut.Markup);
        Assert.Single(cut.FindAll(".forge-card-footer"));
    }

    [Fact]
    public void Card_DoesNotRenderHeaderWhenNotProvided()
    {
        var cut = Render<ForgeCard>();

        Assert.Empty(cut.FindAll(".forge-card-header"));
    }

    [Fact]
    public void Card_DoesNotRenderFooterWhenNotProvided()
    {
        var cut = Render<ForgeCard>();

        Assert.Empty(cut.FindAll(".forge-card-footer"));
    }

    [Theory]
    [InlineData("Small", "forge-card-padding-small")]
    [InlineData("Medium", "forge-card-padding-medium")]
    [InlineData("Large", "forge-card-padding-large")]
    public void Card_AppliesPaddingVariant(
        string padding,
        string expectedClass)
    {
        var cut = Render<ForgeCard>(parameters =>
            parameters.Add(p => p.Padding, padding));

        Assert.Contains(expectedClass, cut.Find("article").ClassName);
    }

    [Fact]
    public void Card_HasShadowByDefault()
    {
        var cut = Render<ForgeCard>();

        Assert.Contains(
            "forge-card-shadow",
            cut.Find("article").ClassName);
    }

    [Fact]
    public void Card_CanDisableShadow()
    {
        var cut = Render<ForgeCard>(parameters =>
            parameters.Add(p => p.HasShadow, false));

        Assert.DoesNotContain(
            "forge-card-shadow",
            cut.Find("article").ClassName);
    }

    [Fact]
    public void Card_HasBorderByDefault()
    {
        var cut = Render<ForgeCard>();

        Assert.Contains(
            "forge-card-border",
            cut.Find("article").ClassName);
    }

    [Fact]
    public void Card_CanDisableBorder()
    {
        var cut = Render<ForgeCard>(parameters =>
            parameters.Add(p => p.HasBorder, false));

        Assert.DoesNotContain(
            "forge-card-border",
            cut.Find("article").ClassName);
    }
}