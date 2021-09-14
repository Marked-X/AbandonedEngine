using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DragonQuest1.Script
{
    abstract class Entity
    {
        protected enum Directions
        {
            Up, Right, Down, Left
        }
        public abstract Sprite Sprite { get; set; }
        public abstract Rectangle Bounds { get; set; }
        public abstract Vector2 Velocity { get; set; }
        public abstract Vector2 Position { get; set; }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
