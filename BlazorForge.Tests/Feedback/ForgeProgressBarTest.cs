using Bunit;
using BlazorForge.Components.Feedback;

namespace BlazorForge.Tests.Feedback;

public class ForgeProgressBarTests : BunitContext
{
    [Fact]
    public void ProgressBar_RendersLabel()
    {
        var cut = Render<ForgeProgressBar>(parameters =>
            parameters
                .Add(p => p.Label, "Uploading document")
                .Add(p => p.Value, 50));

        Assert.Contains("Uploading document", cut.Markup);
    }

    [Fact]
    public void ProgressBar_DisplaysPercentage()
    {
        var cut = Render<ForgeProgressBar>(parameters =>
            parameters.Add(p => p.Value, 65));

        Assert.Contains("65%", cut.Markup);
    }

    [Fact]
    public void ProgressBar_ClampsValueAbove100()
    {
        var cut = Render<ForgeProgressBar>(parameters =>
            parameters.Add(p => p.Value, 150));

        Assert.Contains("aria-valuenow=\"100\"", cut.Markup);
        Assert.Contains("100%", cut.Markup);
    }

    [Fact]
    public void ProgressBar_ClampsValueBelowZero()
    {
        var cut = Render<ForgeProgressBar>(parameters =>
            parameters.Add(p => p.Value, -10));

        Assert.Contains("aria-valuenow=\"0\"", cut.Markup);
        Assert.Contains("0%", cut.Markup);
    }

    [Fact]
    public void ProgressBar_HasAccessibleAttributes()
    {
        var cut = Render<ForgeProgressBar>(parameters =>
            parameters
                .Add(p => p.Label, "Upload progress")
                .Add(p => p.Value, 40));

        var progressBar = cut.Find("[role='progressbar']");

        Assert.Equal("40", progressBar.GetAttribute("aria-valuenow"));
        Assert.Equal("0", progressBar.GetAttribute("aria-valuemin"));
        Assert.Equal("100", progressBar.GetAttribute("aria-valuemax"));
        Assert.Equal("Upload progress", progressBar.GetAttribute("aria-label"));
    }

    [Fact]
    public void ProgressBar_RendersThreeGreenSegments()
    {
        var cut = Render<ForgeProgressBar>();

        Assert.Single(cut.FindAll(".forge-progress-light"));
        Assert.Single(cut.FindAll(".forge-progress-medium"));
        Assert.Single(cut.FindAll(".forge-progress-dark"));
    }

    [Fact]
    public void ProgressBar_HidesPercentageWhenDisabled()
    {
        var cut = Render<ForgeProgressBar>(parameters => parameters
            .Add(p => p.Value, 50)
            .Add(p => p.ShowPercentage, false));

        Assert.Empty(cut.FindAll(".forge-progress-header"));
    }
}