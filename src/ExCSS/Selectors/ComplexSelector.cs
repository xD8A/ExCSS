﻿using System.Collections.Generic;
using System.IO;

namespace ExCSS
{
    internal sealed class ComplexSelector : StylesheetNode, IComplexSelector
    {
        private readonly List<CombinatorSelector> _selectors;

        public ComplexSelector()
        {
            _selectors = new List<CombinatorSelector>();
        }

        private struct CombinatorSelector : ICombinatorSelector
        {
            public string Delimiter;
            public ISelector Selector;
            string ICombinatorSelector.Delimiter => Delimiter;
            ISelector ICombinatorSelector.Selector => Selector;
        }


        public string Text => this.ToCss();
        public int Length => _selectors.Count;
        public IEnumerable<ICombinatorSelector> Selectors {
            get {
                foreach (var selector in _selectors)
                {
                    yield return (ICombinatorSelector)selector;
                }
            }
        }
        public bool IsReady { get; private set; }

        public Priority Specificity
        {
            get
            {
                var sum = new Priority();
                var n = _selectors.Count;

                for (var i = 0; i < n; i++) sum += _selectors[i].Selector.Specificity;

                return sum;
            }
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_selectors.Count <= 0) return;

            var n = _selectors.Count - 1;

            for (var i = 0; i < n; i++)
            {
                writer.Write(_selectors[i].Selector.Text);
                writer.Write(_selectors[i].Delimiter);
            }

            writer.Write(_selectors[n].Selector.Text);
        }

        public void ConcludeSelector(ISelector selector)
        {
            if (IsReady) return;

            _selectors.Add(new CombinatorSelector
            {
                Selector = selector,
                Delimiter = null
            });
            IsReady = true;
        }

        public void AppendSelector(ISelector selector, Combinator combinator)
        {
            if (!IsReady)
                _selectors.Add(new CombinatorSelector
                {
                    Selector = combinator.Change(selector),
                    Delimiter = combinator.Delimiter
                });
        }
    }
}