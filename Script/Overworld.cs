using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class Overworld : Level
    {
        public override Vector2 StartingHeroPosition { get; set; }
        public override int Id { get; set; }
        //public override List<Texture2D> TileTextures { get; set; }
        public override List<Tile> Tiles { get; set; }
        public override List<Trigger> Triggers { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(ContentManager content, Camera camera)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
