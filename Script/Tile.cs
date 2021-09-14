using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonQuest1.Script
{
    class Tile
    {
        public int Id { get; set; }
        public bool Collidable { get; set; }
        public SpriteEffects SpriteEffects { get { return sprite.SpriteEffect; } set { sprite.SpriteEffect ^= value; } }
        public float Rotation
        {
            get => sprite.Rotation;
            set => sprite.Rotation = value;
        }
        public Sprite sprite;
        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;
                sprite.Position = new Vector2(value.X + value.Width / 2, value.Y + value.Height / 2);
            }
        }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable, float rotation, SpriteEffects effect)
        {
            Id = id;
            sprite = new Sprite(texture);
            Bounds = bounds;
            Collidable = collidable;
            Rotation = rotation;
            SpriteEffects = effect;
        }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable, float rotation)
            :this(id, texture, bounds, collidable, rotation, SpriteEffects.None) { }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable)
            : this(id, texture, bounds, collidable, 0f, SpriteEffects.None) { }
        public Tile(int id, TextureRegion texture, Rectangle bounds)
            : this(id, texture, bounds, false, 0f, SpriteEffects.None) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
