using Bunit;
using BlazorForge.Tests.TestComponents;

namespace BlazorForge.Tests.Forms;

public class ForgeTextFieldTests : BunitContext
{
    [Fact]
    public void TextField_RendersLabelAndPlaceholder()
    {
        var cut = Render<TextFieldTestHost>(parameters =>
        {
            parameters.Add(p => p.Label, "Full Name");
            parameters.Add(p => p.Placeholder, "Enter your name");
        });

        Assert.Contains("Full Name", cut.Markup);
        Assert.Contains("Enter your name", cut.Markup);
    }

    [Fact]
    public void TextField_RendersDisabledAttribute()
    {
        var cut = Render<TextFieldTestHost>(parameters =>
        {
            parameters.Add(p => p.Disabled, true);
        });

        Assert.True(cut.Find("input").HasAttribute("disabled"));
    }

    [Fact]
    public void TextField_RendersReadOnlyAttribute()
    {
        var cut = Render<TextFieldTestHost>(parameters =>
        {
            parameters.Add(p => p.ReadOnly, true);
        });

        Assert.True(cut.Find("input").HasAttribute("readonly"));
    }

    [Fact]
    public void TextField_UpdatesBoundModel_WhenInputChanges()
    {
        var model = new TextFieldTestHost.FormModel();

        var cut = Render<TextFieldTestHost>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        cut.Find("input").Change("Hemalatha");

        Assert.Equal("Hemalatha", model.Name);
    }
}