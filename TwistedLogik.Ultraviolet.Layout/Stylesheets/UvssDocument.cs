﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.Layout.Elements;

namespace TwistedLogik.Ultraviolet.Layout.Stylesheets
{
    /// <summary>
    /// Represents an Ultraviolet Stylesheet (UVSS) document.
    /// </summary>
    public sealed partial class UvssDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UvssDocument"/> class.
        /// </summary>
        /// <param name="rules">A collection containing the document's rules.</param>
        internal UvssDocument(IEnumerable<UvssRule> rules)
        {
            Contract.Require(rules, "rules");

            this.rules = rules.ToList();
        }

        /// <summary>
        /// Loads an Ultraviolet Stylesheet (UVSS) document from the specified source text.
        /// </summary>
        /// <param name="source">The source text from which to load the document.</param>
        /// <returns>A new instance of <see cref="UvssDocument"/> that represents the loaded data.</returns>
        public static UvssDocument Parse(String source)
        {
            Contract.Require(source, "source");

            var tokens   = lexer.Lex(source);
            var document = parser.Parse(source, tokens);

            return document;
        }

        /// <summary>
        /// Loads an Ultraviolet Stylesheet (UVSS) document from the specified stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> that contains the document to load.</param>
        /// <returns>A new instance of <see cref="UvssDocument"/> that represents the loaded data.</returns>
        public static UvssDocument Load(Stream stream)
        {
            Contract.Require(stream, "stream");

            using (var reader = new StreamReader(stream))
            {
                var source   = reader.ReadToEnd();
                var tokens   = lexer.Lex(source);
                var document = parser.Parse(source, tokens);

                return document;
            }
        }

        /// <summary>
        /// Gets the document's rules.
        /// </summary>
        public IEnumerable<UvssRule> Rules
        {
            get { return rules; }
        }

        /// <summary>
        /// Applies styles to the specified element.
        /// </summary>
        /// <param name="element">The element to which to apply styles.</param>
        internal void ApplyStyles(UIElement element)
        {
            Contract.Require(element, "element");

            ApplyStylesInternal(element);
        }

        /// <summary>
        /// Recursively applies styles to the specified element and all of its descendants.
        /// </summary>
        /// <param name="element">The element to which to apply styles.</param>
        internal void ApplyStylesRecursively(UIElement element)
        {
            Contract.Require(element, "element");

            ApplyStylesInternal(element);

            var container = element as UIContainer;
            if (container != null)
            {
                foreach (var child in container.Children)
                {
                    ApplyStylesRecursively(child);
                }
            }
        }

        /// <summary>
        /// Applies styles to the specified element.
        /// </summary>
        /// <param name="element">The element to which to apply styles.</param>
        private void ApplyStylesInternal(UIElement element)
        {
            element.ClearStyledValues();

            // Gather styles from document
            var selector = default(UvssSelector);
            foreach (var rule in rules)
            {
                if (!rule.MatchesElement(element, out selector))
                    continue;

                foreach (var style in rule.Styles)
                {
                    const Int32 ImportantStylePriority = 1000000000;

                    var styleKey      = new UvssStyleKey(style.QualifiedName, selector.PseudoClass);
                    var stylePriority = selector.Priority + (style.IsImportant ? ImportantStylePriority : 0);

                    PrioritizedStyleData existingStyleData;
                    if (styleAggregator.TryGetValue(styleKey, out existingStyleData))
                    {
                        if (existingStyleData.Priority > stylePriority)
                            continue;
                    }
                    styleAggregator[styleKey] = new PrioritizedStyleData(style.Container, style.Name, style.Value, stylePriority);
                }
            }

            // Apply styles to element
            foreach (var kvp in styleAggregator)
            {
                ApplyStyleToElement(element, kvp.Value.Container, kvp.Value.Name, kvp.Key.PseudoClass, kvp.Value.Value);
            }
            styleAggregator.Clear();
        }

        /// <summary>
        /// Applies a style to the specified element.
        /// </summary>
        /// <param name="element">The element to which to apply the style.</param>
        /// <param name="container">The style's container, if it represents an attached property.</param>
        /// <param name="style">The name of the style to apply.</param>
        /// <param name="pseudoClass">The pseudo-class of the style to apply.</param>
        /// <param name="value">The styled value to apply.</param>
        private void ApplyStyleToElement(UIElement element, String container, String style, String pseudoClass, String value)
        {
            var styleIsForContainer = !String.IsNullOrEmpty(container);
            if (styleIsForContainer)
            {
                var styleMatchesContainer = element.Container != null && String.Equals(element.Container.Name, container, StringComparison.OrdinalIgnoreCase);
                if (styleMatchesContainer)
                {
                    element.ApplyStyle(style, pseudoClass, value, true);
                    return;
                }
            }

            element.ApplyStyle(style, pseudoClass, value, false);
        }

        // State values.
        private static readonly Dictionary<UvssStyleKey, PrioritizedStyleData> styleAggregator = 
            new Dictionary<UvssStyleKey, PrioritizedStyleData>();
        private static readonly UvssLexer lexer   = new UvssLexer();
        private static readonly UvssParser parser = new UvssParser();

        // Property values.
        private readonly List<UvssRule> rules;
    }
}
