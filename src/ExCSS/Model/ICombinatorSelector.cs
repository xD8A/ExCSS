namespace ExCSS
{
    public interface ICombinatorSelector
    {
        string Delimiter { get; }
        ISelector Selector { get; }
    }
}
