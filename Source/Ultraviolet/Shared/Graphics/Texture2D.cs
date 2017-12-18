﻿using System;
using Ultraviolet.Core;

namespace Ultraviolet.Graphics
{
    /// <summary>
    /// Represents a factory method which constructs instances of the <see cref="Texture2D"/> class.
    /// </summary>
    /// <param name="uv">The Ultraviolet context.</param>
    /// <param name="pixels">A pointer to the raw pixel data with which to populate the texture.</param>
    /// <param name="width">The texture's width in pixels.</param>
    /// <param name="height">The texture's height in pixels.</param>
    /// <param name="bytesPerPixel">The number of bytes which represent each pixel in the raw data.</param>
    /// <returns>The instance of <see cref="Texture2D"/> that was created.</returns>
    public delegate Texture2D Texture2DFromRawDataFactory(UltravioletContext uv, IntPtr pixels, Int32 width, Int32 height, Int32 bytesPerPixel);

    /// <summary>
    /// Represents a factory method which constructs instances of the <see cref="Texture2D"/> class.
    /// </summary>
    /// <param name="uv">The Ultraviolet context.</param>
    /// <param name="width">The texture's width in pixels.</param>
    /// <param name="height">The texture's height in pixels.</param>
    /// <param name="immutable">A value indicating whether the texture should be created with immutable storage.</param>
    /// <returns>The instance of <see cref="Texture2D"/> that was created.</returns>
    public delegate Texture2D Texture2DFactory(UltravioletContext uv, Int32 width, Int32 height, Boolean immutable);

    /// <summary>
    /// Represents a two-dimensional texture.
    /// </summary>
    public abstract class Texture2D : Texture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Texture2D"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        public Texture2D(UltravioletContext uv)
            : base(uv)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Texture2D"/> class.
        /// </summary>
        /// <param name="pixels">A pointer to the raw pixel data with which to populate the texture.</param>
        /// <param name="width">The texture's width in pixels.</param>
        /// <param name="height">The texture's height in pixels.</param>
        /// <param name="bytesPerPixel">The number of bytes which represent each pixel in the raw data.</param>
        /// <returns>The instance of <see cref="Texture2D"/> that was created.</returns>
        public static Texture2D Create(IntPtr pixels, Int32 width, Int32 height, Int32 bytesPerPixel)
        {
            Contract.EnsureRange(width > 0, nameof(width));
            Contract.EnsureRange(height > 0, nameof(height));

            var uv = UltravioletContext.DemandCurrent();
            return uv.GetFactoryMethod<Texture2DFromRawDataFactory>()(uv, pixels, width, height, bytesPerPixel);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Texture2D"/> class.
        /// </summary>
        /// <param name="width">The texture's width in pixels.</param>
        /// <param name="height">The texture's height in pixels.</param>
        /// <returns>The instance of <see cref="Texture2D"/> that was created.</returns>
        public static Texture2D Create(Int32 width, Int32 height)
        {
            return Create(width, height, true);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Texture2D"/> class.
        /// </summary>
        /// <param name="width">The texture's width in pixels.</param>
        /// <param name="height">The texture's height in pixels.</param>
        /// <param name="immutable">A value indicating whether to use immutable storage.</param>
        /// <returns>The instance of <see cref="Texture2D"/> that was created.</returns>
        public static Texture2D Create(Int32 width, Int32 height, Boolean immutable)
        {
            Contract.EnsureRange(width > 0, nameof(width));
            Contract.EnsureRange(height > 0, nameof(height));

            var uv = UltravioletContext.DemandCurrent();
            return uv.GetFactoryMethod<Texture2DFactory>()(uv, width, height, immutable);
        }

        /// <summary>
        /// Resizes the texture.
        /// </summary>
        /// <param name="width">The texture's new width in pixels.</param>
        /// <param name="height">The texture's new height in pixels.</param>
        public abstract void Resize(Int32 width, Int32 height);

        /// <summary>
        /// Sets the texture's data.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array being set as the texture's data.</typeparam>
        /// <param name="data">An array containing the data to set.</param>
        /// <param name="origin">A <see cref="SetDataOrigin"/> value specifying the origin of the texture data in <paramref name="data"/>.</param>
        public abstract void SetData<T>(T[] data, SetDataOrigin origin = SetDataOrigin.TopLeft) where T : struct;

        /// <summary>
        /// Sets the texture's data.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array being set as the texture's data.</typeparam>
        /// <param name="data">An array containing the data to set.</param>
        /// <param name="offset">The index of the first element to set.</param>
        /// <param name="count">The number of elements to set.</param>
        /// <param name="origin">A <see cref="SetDataOrigin"/> value specifying the origin of the texture data in <paramref name="data"/>.</param>
        public abstract void SetData<T>(T[] data, Int32 offset, Int32 count, SetDataOrigin origin = SetDataOrigin.TopLeft) where T : struct;

        /// <summary>
        /// Sets the texture's data.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array to set as the texture's data.</typeparam>
        /// <param name="level">The mipmap level for which to set data.</param>
        /// <param name="rect">A rectangle describing the position and size of the data to set, or <see langword="null"/> to set the entire texture.</param>
        /// <param name="data">An array containing the data to set.</param>
        /// <param name="offset">The index of the first element to set.</param>
        /// <param name="count">The number of elements to set.</param>
        /// <param name="origin">A <see cref="SetDataOrigin"/> value specifying the origin of the texture data in <paramref name="data"/>.</param>
        public abstract void SetData<T>(Int32 level, Rectangle? rect, T[] data, Int32 offset, Int32 count, SetDataOrigin origin = SetDataOrigin.TopLeft) where T : struct;

        /// <summary>
        /// Sets the texture's data.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array to set as the texture's data.</typeparam>
        /// <param name="level">The mipmap level for which to set data.</param>
        /// <param name="rect">A rectangle describing the position and size of the data to set, or <see langword="null"/> to set the entire texture.</param>
        /// <param name="data">An array containing the data to set.</param>
        /// <param name="offset">The index of the first element to set.</param>
        /// <param name="count">The number of elements to set.</param>
        /// <param name="stride">The number of elements in one row of data, or zero to use the width of <paramref name="rect"/>.</param>
        /// <param name="origin">A <see cref="SetDataOrigin"/> value specifying the origin of the texture data in <paramref name="data"/>.</param>
        public abstract void SetData<T>(Int32 level, Rectangle? rect, T[] data, Int32 offset, Int32 count, Int32 stride, SetDataOrigin origin = SetDataOrigin.TopLeft) where T : struct;

        /// <summary>
        /// Sets the texture's data.
        /// </summary>
        /// <param name="level">The mipmap level for which to set data.</param>
        /// <param name="rect">A rectangle describing the position and size of the data to set, or <see langword="null"/> to set the entire texture.</param>
        /// <param name="data">A pointer to the data to set.</param>
        /// <param name="offset">The index of the first element to set.</param>
        /// <param name="count">The number of elements to set.</param>
        /// <param name="stride">The number of elements in one row of data, or zero to use the width of <paramref name="rect"/>.</param>
        /// <param name="format">The format of the data being set.</param>
        /// <param name="origin">A <see cref="SetDataOrigin"/> value specifying the origin of the texture data in <paramref name="data"/>.</param>
        public abstract void SetData(Int32 level, Rectangle? rect, IntPtr data, Int32 offset, Int32 count, Int32 stride, TextureDataFormat format, SetDataOrigin origin = SetDataOrigin.TopLeft);

        /// <summary>
        /// Gets the texture's width in pixels.
        /// </summary>
        public abstract Int32 Width
        {
            get;
        }

        /// <summary>
        /// Gets the texture's height in pixels.
        /// </summary>
        public abstract Int32 Height
        {
            get;
        }
    }
}
