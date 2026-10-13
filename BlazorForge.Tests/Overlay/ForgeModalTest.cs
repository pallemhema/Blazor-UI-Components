using Bunit;
using BlazorForge.Components.Overlay;
using Microsoft.AspNetCore.Components;

namespace BlazorForge.Tests.Overlay;

public class ForgeModalTests : BunitContext
{
    public ForgeModalTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Modal_ShouldRender_WhenIsOpenIsTrue()
    {
        var cut = Render<ForgeModal>(parameters =>
            parameters.Add(p => p.IsOpen, true));

        Assert.Single(cut.FindAll("section[role='dialog']"));
    }

    [Fact]
    public void Modal_ShouldNotRender_WhenIsOpenIsFalse()
    {
        var cut = Render<ForgeModal>(parameters =>
            parameters.Add(p => p.IsOpen, false));

        Assert.Empty(cut.FindAll("section[role='dialog']"));
    }

    [Fact]
    public void Modal_ShouldDisplayTitle()
    {
        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.Title, "Course Details"));

        Assert.Equal(
            "Course Details",
            cut.Find("h2").TextContent);
    }

    [Fact]
    public void Modal_ShouldDisplayChildContent()
    {
        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .AddChildContent("<p>Welcome to BlazorForge</p>"));

        Assert.Equal(
            "Welcome to BlazorForge",
            cut.Find(".forge-modal-body p").TextContent);
    }

    [Fact]
    public void Modal_ShouldDisplayFooter_WhenFooterIsProvided()
    {
        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.Footer, (RenderFragment)(builder =>
                builder.AddContent(0, "Save Changes"))));

        Assert.Equal(
            "Save Changes",
            cut.Find(".forge-modal-footer").TextContent);
    }

    [Fact]
    public void Modal_ShouldNotRenderFooter_WhenFooterIsNotProvided()
    {
        var cut = Render<ForgeModal>(parameters =>
            parameters.Add(p => p.IsOpen, true));

        Assert.Empty(cut.FindAll(".forge-modal-footer"));
    }

    [Fact]
    public void CloseButton_ShouldInvokeIsOpenChangedWithFalse()
    {
        bool? newOpenState = null;

        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.IsOpenChanged,
                EventCallback.Factory.Create<bool>(
                    this,
                    value => newOpenState = value)));

        cut.Find("button.forge-modal-close").Click();

        Assert.False(newOpenState);
    }

    [Fact]
    public void Modal_ShouldHaveDialogAccessibilityAttributes()
    {
        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true));

        var dialog = cut.Find("section[role='dialog']");

        Assert.Equal("true", dialog.GetAttribute("aria-modal"));
        Assert.NotNull(dialog.GetAttribute("aria-labelledby"));
    }

    [Fact]
    public void Modal_ShouldNotClose_WhenBackdropClickIsDisabled()
    {
        bool? newOpenState = null;

        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.CloseOnBackdropClick, false)
            .Add(p => p.IsOpenChanged,
                EventCallback.Factory.Create<bool>(
                    this,
                    value => newOpenState = value)));

        cut.Find(".forge-modal-backdrop").Click();

        Assert.Null(newOpenState);
        Assert.Single(cut.FindAll("section[role='dialog']"));
    }

    [Fact]
    public void ClickingInsideModal_ShouldNotCloseModal()
    {
        bool? newOpenState = null;

        RenderFragment content = builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "onclick",
                EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(
                    this, () => { }));
            builder.AddContent(2, "Inside");
            builder.CloseElement();
        };

        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.ChildContent, content)
            .Add(p => p.IsOpenChanged,
                EventCallback.Factory.Create<bool>(
                    this, value => newOpenState = value)));

        cut.Find(".forge-modal-body button").Click();

        Assert.Null(newOpenState);
        Assert.Single(cut.FindAll("section[role='dialog']"));
    }
    [Fact]
    public void HandleEscape_ShouldCloseModal_WhenCloseOnEscapeIsEnabled()
    {
        bool? newOpenState = null;

        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.CloseOnEscape, true)
            .Add(p => p.IsOpenChanged,
                EventCallback.Factory.Create<bool>(
                    this,
                    value => newOpenState = value)));

        cut.Instance.HandleEscape();

        Assert.False(newOpenState);
    }

    [Fact]
    public void HandleEscape_ShouldNotCloseModal_WhenCloseOnEscapeIsDisabled()
    {
        bool? newOpenState = null;

        var cut = Render<ForgeModal>(parameters => parameters
            .Add(p => p.IsOpen, true)
            .Add(p => p.CloseOnEscape, false)
            .Add(p => p.IsOpenChanged,
                EventCallback.Factory.Create<bool>(
                    this,
                    value => newOpenState = value)));

        cut.Instance.HandleEscape();

        Assert.Null(newOpenState);
    }
}