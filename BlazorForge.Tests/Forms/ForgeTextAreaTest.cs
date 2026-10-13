using Bunit;
using BlazorForge.Tests.TestComponents;

namespace BlazorForge.Tests.Forms;

public class ForgeTextAreaTests : BunitContext
{
    [Fact]
    public void TextArea_RendersLabelAndPlaceholder()
    {
        var cut = Render<TextAreaTestHost>(parameters =>
        {
            parameters.Add(p => p.Label, "Description");
            parameters.Add(p => p.Placeholder, "Enter description");
        });

        Assert.Contains("Description", cut.Markup);
        Assert.Contains("Enter description", cut.Markup);
    }

    [Fact]
    public void TextArea_RendersConfiguredRows()
    {
        var cut = Render<TextAreaTestHost>(parameters =>
        {
            parameters.Add(p => p.Rows, 6);
        });

        Assert.Equal("6", cut.Find("textarea").GetAttribute("rows"));
    }

    [Fact]
    public void TextArea_RendersDisabledAttribute()
    {
        var cut = Render<TextAreaTestHost>(parameters =>
        {
            parameters.Add(p => p.Disabled, true);
        });

        Assert.True(cut.Find("textarea").HasAttribute("disabled"));
    }

    [Fact]
    public void TextArea_RendersReadOnlyAttribute()
    {
        var cut = Render<TextAreaTestHost>(parameters =>
        {
            parameters.Add(p => p.ReadOnly, true);
        });

        Assert.True(cut.Find("textarea").HasAttribute("readonly"));
    }

    [Fact]
    public void TextArea_UpdatesBoundModel_WhenInputChanges()
    {
        var model = new TextAreaTestHost.FormModel();

        var cut = Render<TextAreaTestHost>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        cut.Find("textarea").Change("This is my description.");

        Assert.Equal("This is my description.", model.Description);
    }
}