﻿using System.IO;
using System.Xml.Linq;

namespace ExCSS
{
    internal sealed class ListSelector : Selectors, ISelectorEnumerable
    {
        public bool IsInvalid { get; internal set; }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_selectors.Count <= 0) return;
            writer.Write(_selectors[0].Text);

            for (var i = 1; i < _selectors.Count; i++)
            {
                writer.Write(Symbols.Comma);
                writer.Write(_selectors[i].Text);
            }
        }
    }
}