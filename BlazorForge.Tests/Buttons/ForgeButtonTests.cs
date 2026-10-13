using Bunit;
using BlazorForge.Components.Buttons;

namespace BlazorForge.Tests.Buttons;

public class ForgeButtonTests : BunitContext
{
    [Fact]
    public void Button_RendersPrimaryVariant_ByDefault()
    {
        // Arrange & Act
        var cut = Render<ForgeButton>();

        // Assert
        var button = cut.Find("button");

        Assert.Contains(
            "forge-button-primary",
            button.ClassList
        );
    }

    [Fact]
    public void Button_RendersChildContent()
    {
        // Arrange & Act
        var cut = Render<ForgeButton>(parameters =>
            parameters.AddChildContent("Save Changes")
        );

        // Assert
        Assert.Equal(
            "Save Changes",
            cut.Find("button").TextContent
        );
    }

    [Fact]
    public void Button_RendersLargeSize()
    {
        // Arrange & Act
        var cut = Render<ForgeButton>(parameters =>
            parameters.Add(
                button => button.Size,
                ButtonSize.Large
            )
        );

        // Assert
        Assert.Contains(
            "forge-button-large",
            cut.Find("button").ClassList
        );
    }

    [Fact]
    public void Button_InvokesCallback_WhenClicked()
    {
        // Arrange
        var clicked = false;

        var cut = Render<ForgeButton>(parameters =>
            parameters.Add(
                button => button.OnClick,
                _ => clicked = true
            )
        );

        // Act
        cut.Find("button").Click();

        // Assert
        Assert.True(clicked);
    }

    [Fact]
    public void Button_DoesNotInvokeCallback_WhenDisabled()
    {
        // Arrange
        var clicked = false;

        var cut = Render<ForgeButton>(parameters =>
        {
            parameters.Add(
                button => button.Disabled,
                true
            );

            parameters.Add(
                button => button.OnClick,
                _ => clicked = true
            );
        });

        var button = cut.Find("button");

        // Assert that the native HTML button is disabled.
        Assert.True(button.HasAttribute("disabled"));

        // Act
        button.Click();

        // Assert that the callback was not invoked.
        Assert.False(clicked);
    }
}