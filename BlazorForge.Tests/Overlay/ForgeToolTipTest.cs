using Bunit;
using BlazorForge.Components.Overlay;
namespace BlazorForge.Tests.Overlay;
public class ForgeTooltipTests : BunitContext
{
    [Fact]
    public void Tooltip_ShouldNotRenderInitially()
    {
        var cut = Render<ForgeTooltip>(parameters =>
            parameters.Add(p => p.Text, "Save your changes"));

        Assert.Empty(cut.FindAll("[role='tooltip']"));
    }

    [Fact]
    public void Tooltip_ShouldRenderOnMouseEnter()
    {
        var cut = Render<ForgeTooltip>(parameters => parameters
            .Add(p => p.Text, "Save your changes")
            .AddChildContent("<button>Save</button>"));

        cut.Find(".forge-tooltip-container").MouseEnter();

        Assert.Equal(
            "Save your changes",
            cut.Find("[role='tooltip']").TextContent);
    }

    [Fact]
    public void Tooltip_ShouldHideOnMouseLeave()
    {
        var cut = Render<ForgeTooltip>(parameters =>
            parameters.Add(p => p.Text, "Save your changes"));

        var container = cut.Find(".forge-tooltip-container");

        container.MouseEnter();

        Assert.Single(cut.FindAll("[role='tooltip']"));

        container.MouseLeave();

        Assert.Empty(cut.FindAll("[role='tooltip']"));
    }

    [Fact]
    public void Tooltip_ShouldDisplayChildContent()
    {
        var cut = Render<ForgeTooltip>(parameters => parameters
            .Add(p => p.Text, "Save your changes")
            .AddChildContent("Save"));

        Assert.Contains("Save", cut.Markup);
    }

    [Fact]
    public void Tooltip_ShouldUseTopPlacementByDefault()
    {
        var cut = Render<ForgeTooltip>(parameters =>
            parameters.Add(p => p.Text, "Tooltip text"));

        cut.Find(".forge-tooltip-container").MouseEnter();

        Assert.Contains(
            "forge-tooltip-top",
            cut.Find("[role='tooltip']").ClassName);
    }

    [Theory]
    [InlineData(TooltipPlacement.Top, "forge-tooltip-top")]
    [InlineData(TooltipPlacement.Bottom, "forge-tooltip-bottom")]
    [InlineData(TooltipPlacement.Left, "forge-tooltip-left")]
    [InlineData(TooltipPlacement.Right, "forge-tooltip-right")]
    public void Tooltip_ShouldUseConfiguredPlacement(
        TooltipPlacement placement,
        string expectedClass)
    {
        var cut = Render<ForgeTooltip>(parameters => parameters
            .Add(p => p.Text, "Tooltip text")
            .Add(p => p.Placement, placement));

        cut.Find(".forge-tooltip-container").MouseEnter();

        Assert.Contains(
            expectedClass,
            cut.Find("[role='tooltip']").ClassName);
    }

    [Fact]
    public void Tooltip_ShouldNotRender_WhenTextIsEmpty()
    {
        var cut = Render<ForgeTooltip>(parameters =>
            parameters.Add(p => p.Text, string.Empty));

        cut.Find(".forge-tooltip-container").MouseEnter();

        Assert.Empty(cut.FindAll("[role='tooltip']"));
    }

    [Fact]
    public void Tooltip_ShouldRenderOnFocus()
    {
        var cut = Render<ForgeTooltip>(parameters => parameters
            .Add(p => p.Text, "Save your changes")
            .AddChildContent("<button>Save</button>"));

        cut.Find(".forge-tooltip-container").FocusIn();

        Assert.Single(cut.FindAll("[role='tooltip']"));
    }

    [Fact]
    public void Tooltip_ShouldHideOnFocusOut()
    {
        var cut = Render<ForgeTooltip>(parameters =>
            parameters.Add(p => p.Text, "Save your changes"));

        var container = cut.Find(".forge-tooltip-container");

        container.FocusIn();

        Assert.Single(cut.FindAll("[role='tooltip']"));

        container.FocusOut();

        Assert.Empty(cut.FindAll("[role='tooltip']"));
    }
}