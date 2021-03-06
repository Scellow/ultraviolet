﻿using System;
using System.Text;
using Ultraviolet.Core.Text;

namespace Ultraviolet.Graphics.Graphics2D
{
    /// <summary>
    /// Represents the base class for font faces.
    /// </summary>
    public abstract class UltravioletFontFace : UltravioletResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UltravioletFontFace"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        public UltravioletFontFace(UltravioletContext uv)
            : base(uv)
        { }

        /// <summary>
        /// Gets the information required to draw the specified glyph.
        /// </summary>
        /// <param name="c">The 32-bit Unicode value of the glyph to draw.</param>
        /// <param name="info">The rendering information for the specified glyph.</param>
        public abstract void GetGlyphRenderInfo(Int32 c, out GlyphRenderInfo info);

        /// <summary>
        /// Measures the size of the specified string of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>The size of the specified string of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(String text);

        /// <summary>
        /// Measures the size of the specified substring of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="start">The index of the first character of the substring to measure.</param>
        /// <param name="count">The number of characters in the substring to measure.</param>
        /// <returns>The size of the specified substring of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(String text, Int32 start, Int32 count);

        /// <summary>
        /// Measures the size of the specified string of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>The size of the specified string of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(StringBuilder text);

        /// <summary>
        /// Measures the size of the specified substring of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="start">The index of the first character of the substring to measure.</param>
        /// <param name="count">The number of characters in the substring to measure.</param>
        /// <returns>The size of the specified substring of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(StringBuilder text, Int32 start, Int32 count);

        /// <summary>
        /// Measures the size of the specified string of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>The size of the specified string of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(ref StringSegment text);

        /// <summary>
        /// Measures the size of the specified substring of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="start">The index of the first character of the substring to measure.</param>
        /// <param name="count">The number of characters in the substring to measure.</param>
        /// <returns>The size of the specified substring of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(ref StringSegment text, Int32 start, Int32 count);

        /// <summary>
        /// Measures the size of the specified string of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>The size of the specified string of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(ref StringSource text);

        /// <summary>
        /// Measures the size of the specified substring of text when rendered using this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="start">The index of the first character of the substring to measure.</param>
        /// <param name="count">The number of characters in the substring to measure.</param>
        /// <returns>The size of the specified substring of text when rendered using this font.</returns>
        public abstract Size2 MeasureString(ref StringSource text, Int32 start, Int32 count);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning into account.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyph(String text, Int32 ix);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning into account.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyph(StringBuilder text, Int32 ix);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning into account.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyph(ref StringSegment text, Int32 ix);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning with a hypothetical second character into account.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <param name="c2">The glyph that comes immediately after the glyph being measured.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyphWithHypotheticalKerning(ref StringSegment text, Int32 ix, Int32 c2);

        /// <summary>
        /// Measures the specified glyph in a string, ignoring kerning.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyphWithoutKerning(ref StringSegment text, Int32 ix);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning into account.
        /// </summary>
        /// <param name="source">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyph(ref StringSource source, Int32 ix);

        /// <summary>
        /// Measures the specified glyph in a string, taking kerning with a hypothetical second character into account.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <param name="c2">The glyph that comes immediately after the glyph being measured.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyphWithHypotheticalKerning(ref StringSource text, Int32 ix, Int32 c2);

        /// <summary>
        /// Measures the specified glyph in a string, ignoring kerning.
        /// </summary>
        /// <param name="text">The text that contains the glyph to measure.</param>
        /// <param name="ix">The index of the glyph to measure.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyphWithoutKerning(ref StringSource text, Int32 ix);

        /// <summary>
        /// Measures the specified glyph, taking kerning into account.
        /// </summary>
        /// <param name="c1">The glyph to measure.</param>
        /// <param name="c2">The glyph that comes immediately after the glyph being measured.</param>
        /// <returns>The size of the specified glyph.</returns>
        public abstract Size2 MeasureGlyph(Int32 c1, Int32? c2 = null);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="c1">The first Unicode code point in the pair to evaluate.</param>
        /// <param name="c2">The second Unicode code point in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetKerningInfo(Int32 c1, Int32 c2);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text">The string segment that contains the character pair.</param>
        /// <param name="ix">The index of the first character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetKerningInfo(ref StringSource text, Int32 ix);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text">The string segment that contains the character pair.</param>
        /// <param name="ix">The index of the first character in the pair to evaluate.</param>
        /// <param name="c2">The second character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetHypotheticalKerningInfo(ref StringSource text, Int32 ix, Int32 c2);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text1">The string segment that contains the first character in the pair.</param>
        /// <param name="ix1">The index of the first character in the pair to evaluate.</param>
        /// <param name="text2">The string segment that contains the second character in the pair.</param>
        /// <param name="ix2">The index of the second character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetKerningInfo(ref StringSource text1, Int32 ix1, ref StringSource text2, Int32 ix2);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text">The string segment that contains the character pair.</param>
        /// <param name="ix">The index of the first character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetKerningInfo(ref StringSegment text, Int32 ix);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text">The string segment that contains the character pair.</param>
        /// <param name="ix">The index of the first character in the pair to evaluate.</param>
        /// <param name="c2">The second character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetHypotheticalKerningInfo(ref StringSegment text, Int32 ix, Int32 c2);

        /// <summary>
        /// Gets this font's kerning offset for the specified character pair.
        /// </summary>
        /// <param name="text1">The string segment that contains the first character in the pair.</param>
        /// <param name="ix1">The index of the first character in the pair to evaluate.</param>
        /// <param name="text2">The string segment that contains the second character in the pair.</param>
        /// <param name="ix2">The index of the second character in the pair to evaluate.</param>
        /// <returns>The kerning offset for the specified character pair.</returns>
        public abstract Size2 GetKerningInfo(ref StringSegment text1, Int32 ix1, ref StringSegment text2, Int32 ix2);

        /// <summary>
        /// Gets a value indicating whether this font face contains the specified glyph.
        /// </summary>
        /// <param name="c">The UTF-32 Unicode code point to evaluate.</param>
        /// <returns><see langword="true"/> if the font face contains the specified glyph; otherwise, <see langword="false"/>.</returns>
        public abstract Boolean ContainsGlyph(Int32 c);

        /// <summary>
        /// Gets the number of characters in the font face.
        /// </summary>
        public abstract Int32 Characters { get; }

        /// <summary>
        /// Gets the width of a space in this font face.
        /// </summary>
        public abstract Int32 SpaceWidth { get; }

        /// <summary>
        /// Gets the width of a tab in this font face.
        /// </summary>
        public abstract Int32 TabWidth { get; }

        /// <summary>
        /// Gets the size of the font face's ascender metric in pixels.
        /// </summary>
        public abstract Int32 Ascender { get; }

        /// <summary>
        /// Gets the size of the font face's descender metric in pixels. Values below the baseline are negative.
        /// </summary>
        public abstract Int32 Descender { get; }

        /// <summary>
        /// Gets the height of a line written with this font face.
        /// </summary>
        public abstract Int32 LineSpacing { get; }

        /// <summary>
        /// Gets the character that corresponds to the font face's substitution glyph.
        /// </summary>
        /// <remarks>The substitution glyph is used as a replacement for characters which do not exist in the collection.</remarks>
        public abstract Char SubstitutionCharacter { get; }
    }
}
