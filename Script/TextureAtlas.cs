using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class TextureAtlas
    {
        public Texture2D Texture { get; }
        public List<TextureRegion> TextureRegions { get; }
        public int Count { get => TextureRegions.Count; }
        public TextureAtlas(Texture2D texture, List<Rectangle> regions)
        {
            Texture = texture;
            TextureRegions = new List<TextureRegion>();
            foreach (Rectangle region in regions)
            {
                TextureRegions.Add(new TextureRegion(texture, region));
            }
        }
        public TextureAtlas(Texture2D texture, int height, int width)
        {
            Texture = texture;
            TextureRegions = new List<TextureRegion>();
            for(int i = 0; i < texture.Height; i += height)
            {
                for(int j = 0; j < texture.Width; j += width)
                {
                    TextureRegions.Add(new TextureRegion(texture, new Rectangle(j, i, height, width)));
                }
            }
        }
    }
}
