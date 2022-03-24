﻿using System.IO;

namespace ExCSS
{
    internal abstract class ChildSelector : StylesheetNode, IChildSelector
    {
        private readonly string _name;
        protected int Step;
        protected int Offset;
        protected ISelector Kind;

        protected ChildSelector(string name)
        {
            _name = name;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var a = Step.ToString();

            var b = Offset switch
            {
                > 0 => "+" + Offset,
                < 0 => Offset.ToString(),
                _ => string.Empty
            };

            writer.Write(":{0}({1}n{2})", _name, a, b);
        }

        public Priority Specificity => Priority.OneClass;
        public string Text => this.ToCss();
        public string Name => _name;
        int IChildSelector.Step => Step;
        int IChildSelector.Offset => Offset;

        internal ChildSelector With(int step, int offset, ISelector kind)
        {
            Step = step;
            Offset = offset;
            Kind = kind;
            return this;
        }
    }
}