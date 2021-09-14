using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonQuest1
{
    public class AnimationManager
    {
        private Animation _animation;
        private float _timer;
        public Vector2 Position;
        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }
        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;
            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }
        public void Stop()
        {
            _timer = 0;
            _animation.CurrentFrame = 0;
        }
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;
                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBach)
        {
            spriteBach.Draw(_animation.Texture,
                            Position,
                            new Rectangle(_animation.CurrentFrame * _animation.FrameWidth + _animation.StartPosition,
                                          0,
                                          _animation.FrameWidth,
                                          _animation.FrameHeight),
                            Color.White);
        }
    }
}
