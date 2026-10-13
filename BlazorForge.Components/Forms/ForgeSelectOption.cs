namespace BlazorForge.Components.Forms;

public sealed class ForgeSelectOption<TValue>
{
    public required string Text { get; init; }

    public required TValue Value { get; init; }
}