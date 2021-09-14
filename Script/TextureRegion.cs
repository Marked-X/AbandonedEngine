using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class TextureRegion
    {
        public Texture2D Texture { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public TextureRegion(Texture2D texture, Rectangle rectangle) :this(texture, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height) { }
        public TextureRegion(Texture2D texture) :this(texture, 0, 0, texture.Width, texture.Height) { }
        public Vector2 Position { get => new Vector2(X, Y); }
        public Rectangle Bounds { get => new Rectangle(X, Y, Width, Height); }
    }
}
