using System.Collections.Generic;

namespace ExCSS
{
    public interface IEnumerableSelector : ISelector, IEnumerable<ISelector>
    {
    }
}
