﻿using System;
using TwistedLogik.Nucleus;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Media
{
    /// <summary>
    /// Represents an identity transformation.
    /// </summary>
    [Preserve(AllMembers = true)]
    [UvmlKnownType]
    public sealed class IdentityTransform : Transform
    {
        /// <inheritdoc/>
        public override Matrix Value
        {
            get { return Matrix.Identity; }
        }

        /// <inheritdoc/>
        public override Matrix? Inverse
        {
            get { return Matrix.Identity; }
        }

        /// <inheritdoc/>
        public override Boolean IsIdentity
        {
            get { return true; }
        }
    }
}