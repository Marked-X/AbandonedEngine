using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DragonQuest1.Script
{
    /// <summary>
    /// "State" of a game, things like overworld, places, battles and menus
    /// </summary>
    abstract class Level
    {
        public abstract Vector2 StartingHeroPosition { get; set; }
        public abstract int Id { get; set; }
        public abstract List<Tile> Tiles { get; set; }
        public abstract List<Trigger> Triggers { get; set; }
        public abstract void Initialize(ContentManager content, Camera camera);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Exit();
    }
}
