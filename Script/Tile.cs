using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonQuest1.Script
{
    class Tile
    {
        public int Id { get; set; }
        public bool Collidable { get; set; }
        public SpriteEffects SpriteEffects { get { return Sprite.SpriteEffect; } set { Sprite.SpriteEffect ^= value; } }
        public float Rotation
        {
            get => Sprite.Rotation;
            set => Sprite.Rotation = value;
        }
        public Sprite Sprite { get; set; }
        public Sprite.Layer Layer { get => Sprite.LayerDepth; set => Sprite.LayerDepth = value; }
        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;
                Sprite.Position = new Vector2(value.X + value.Width / 2, value.Y + value.Height / 2);
            }
        }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable, float rotation, SpriteEffects effect, Sprite.Layer layer)
        {
            Id = id;
            Sprite = new Sprite(texture);
            Bounds = bounds;
            Collidable = collidable;
            Rotation = rotation;
            SpriteEffects = effect;
            Layer = layer;
        }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable, float rotation, SpriteEffects effect)
            : this(id, texture, bounds, collidable, rotation, effect, Sprite.Layer.background) { }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable, float rotation)
            : this(id, texture, bounds, collidable, rotation, SpriteEffects.None) { }
        public Tile(int id, TextureRegion texture, Rectangle bounds, bool collidable)
            : this(id, texture, bounds, collidable, 0f) { }
        public Tile(int id, TextureRegion texture, Rectangle bounds)
            : this(id, texture, bounds, false) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }
}
