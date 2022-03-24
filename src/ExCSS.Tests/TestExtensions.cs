using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ExCSS;

namespace ExCSS.Tests
{
    static class TestExtensions
    {
        public static Stylesheet ToCssStylesheet(this string sourceCode)
        {
            var parser = new StylesheetParser();
            return parser.Parse(sourceCode);
        }

        public static IEnumerable<Comment> GetComments(this StylesheetNode node)
        {
            return node.GetAll<Comment>();
        }

        public static IEnumerable<T> GetAll<T>(this IStylesheetNode node)
            where T : IStyleFormattable
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node is T)
            {
                yield return (T)node;
            }

            foreach (var entity in node.Children.SelectMany(m => m.GetAll<T>()))
            {
                yield return entity;
            }
        }

        public static IEnumerable<T> GetAllSelectors<T>(this IStylesheetNode node)
            where T : ISelector
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node is T)
            {
                yield return (T)node;
            }

            if (node is ISelectorEnumerable selectors)
            {
                foreach (var selector in selectors.SelectMany(m => m.GetAllSelectors<T>()))
                {
                    yield return selector;
                }
            }

            if (node is IComplexSelector combinatorSelector)
            {
                foreach (var selector in combinatorSelector.Selectors.SelectMany(m => m.Selector.GetAllSelectors<T>()))
                {
                    yield return selector;
                }
            }

            foreach (var selector in node.Children.SelectMany(m => m.GetAllSelectors<T>()))
            {
                yield return selector;
            }
        }

        public static Stylesheet ToCssStylesheet(this Stream content)
        {
            var parser = new StylesheetParser();
            return parser.Parse(content);
        }
    }
}