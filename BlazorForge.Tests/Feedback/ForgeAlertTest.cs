using Bunit;
using BlazorForge.Components.Feedback;

namespace BlazorForge.Tests.Feedback;

public class ForgeAlertTests : BunitContext
{
    [Fact]
    public void Alert_RendersChildContent()
    {
        var cut = Render<ForgeAlert>(parameters =>
        {
            parameters.AddChildContent("Profile updated successfully.");
        });

        Assert.Contains("Profile updated successfully.", cut.Markup);
    }

    [Fact]
    public void Alert_RendersTitle()
    {
        var cut = Render<ForgeAlert>(parameters =>
        {
            parameters.Add(p => p.Title, "Success");
        });

        Assert.Contains("Success", cut.Markup);
    }

    [Theory]
    [InlineData(AlertVariant.Success, "forge-alert-success")]
    [InlineData(AlertVariant.Info, "forge-alert-info")]
    [InlineData(AlertVariant.Warning, "forge-alert-warning")]
    [InlineData(AlertVariant.Error, "forge-alert-error")]
    public void Alert_RendersCorrectVariant(
        AlertVariant variant,
        string expectedClass)
    {
        var cut = Render<ForgeAlert>(parameters =>
        {
            parameters.Add(p => p.Variant, variant);
        });

        Assert.Contains(expectedClass, cut.Markup);
    }

    [Fact]
    public void Alert_HidesDismissButton_WhenNotDismissible()
    {
        var cut = Render<ForgeAlert>();

        Assert.Empty(cut.FindAll(".forge-alert-dismiss"));
    }

    [Fact]
    public void Alert_DismissesAndInvokesCallback_WhenDismissed()
    {
        var callbackInvoked = false;

        var cut = Render<ForgeAlert>(parameters =>
        {
            parameters.Add(p => p.Dismissible, true);
            parameters.Add(
                p => p.OnDismiss,
                () => callbackInvoked = true);
        });

        cut.Find(".forge-alert-dismiss").Click();

        Assert.Empty(cut.FindAll(".forge-alert"));
        Assert.True(callbackInvoked);
    }
}