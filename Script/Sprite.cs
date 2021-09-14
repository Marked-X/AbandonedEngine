using DragonQuest1.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace DragonQuest1
{
    class Sprite
    {
        public enum Layer
        {
            background,
            entities,
            foreground,
            ui,
            MAX_LAYER
        }
        private Dictionary<string, Animation> _animations;
        private AnimationManager _animationManager;
        private Vector2 _position;
        public TextureRegion TextureRegion { get; }
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public float Rotation { get; set; } = 0f;
        public Layer LayerDepth { get; set; } = Layer.background;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public Sprite(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
        }
        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }
        public void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (TextureRegion != null)
            {
                spriteBatch.Draw(TextureRegion.Texture,
                                 new Rectangle((int)_position.X, (int)_position.Y, Game1.TILE_SIZE, Game1.TILE_SIZE),
                                 TextureRegion.Bounds,
                                 Color.White,
                                 Rotation,
                                 new Vector2(TextureRegion.Width / 2f, TextureRegion.Height / 2f),
                                 SpriteEffect,
                                 (float)LayerDepth/(float)Layer.MAX_LAYER);
            }
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
        }
        public void PlayAnimation(string animation)
        {
            _animationManager.Play(_animations[animation]);
        }
    }
}
