using System;
using System.Collections.Generic;

namespace ExCSS
{
    public interface IComplexSelector : ISelector
    {
        IEnumerable<ICombinatorSelector> Selectors { get; }
    }
}
