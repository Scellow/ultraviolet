﻿using NUnit.Framework;
using TwistedLogik.Ultraviolet.Graphics;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Testing;

namespace TwistedLogik.Ultraviolet.Tests.Graphics.Graphics2D
{
    [TestFixture]
    public class SpriteBatchTests : UltravioletApplicationTestFramework
    {
        [Test]
        [Category("Rendering")]
        [Description("Ensures that the SpriteBatch class can render text using the DrawString() method.")]
        public void SpriteBatch_CanRenderSimpleStrings()
        {
            var spriteBatch = default(SpriteBatch);
            var spriteFont  = default(SpriteFont);

            var result = GivenAnUltravioletApplication()
                .WithContent(content =>
                {
                    spriteBatch = SpriteBatch.Create();
                    spriteFont  = content.Load<SpriteFont>("Fonts/SegoeUI");
                })
                .Render(uv =>
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    spriteBatch.DrawString(spriteFont.Regular, "Hello, world!", Vector2.Zero, Color.White);
                    spriteBatch.End();
                });

            TheResultingImage(result).ShouldMatch(@"Resources/Expected/Graphics/Graphics2D/SpriteBatch_CanRenderSimpleStrings.png");
        }
    }
}
