namespace ExCSS
{
    public interface IChildSelector : ISelector
    {
        string Name { get; }
        int Step { get; }
        int Offset { get; }
    }
}
