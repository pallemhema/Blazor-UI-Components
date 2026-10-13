using Bunit;
using BlazorForge.Tests.TestComponents;

namespace BlazorForge.Tests.Forms;

public class ForgeSelectTests : BunitContext
{
    [Fact]
    public void Select_RendersLabelAndPlaceholder()
    {
        var cut = Render<SelectTestHost>(parameters =>
        {
            parameters.Add(p => p.Label, "Country");
            parameters.Add(p => p.Placeholder, "Select your country");
        });

        Assert.Contains("Country", cut.Markup);
        Assert.Contains("Select your country", cut.Markup);
    }

    [Fact]
    public void Select_RendersAllOptions()
    {
        var cut = Render<SelectTestHost>();

        Assert.Contains("India", cut.Markup);
        Assert.Contains("Japan", cut.Markup);
        Assert.Contains("United States", cut.Markup);
    }

    [Fact]
    public void Select_RendersDisabledAttribute()
    {
        var cut = Render<SelectTestHost>(parameters =>
        {
            parameters.Add(p => p.Disabled, true);
        });

        Assert.True(cut.Find("select").HasAttribute("disabled"));
    }

    [Fact]
    public void Select_UpdatesBoundModel_WhenSelectionChanges()
    {
        var model = new SelectTestHost.FormModel();

        var cut = Render<SelectTestHost>(parameters =>
        {
            parameters.Add(p => p.Model, model);
        });

        cut.Find("select").Change("JP");

        Assert.Equal("JP", model.Country);
    }
}