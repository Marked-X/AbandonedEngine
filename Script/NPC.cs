using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DragonQuest1.Script
{
    class NPC : Entity
    {
        public override Sprite Sprite { get; set; }
        public override Rectangle Bounds { get; set; }
        public override Vector2 Velocity { get; set; }
        public override Vector2 Position { get { return _position; } set { Bounds = new Rectangle((int)value.X, (int)value.Y, 16, 16); _position = value; } }
        private Vector2 _position;
        private bool _stationary = true;
        private int _speed = 16;
        public NPC(Sprite sprite, Rectangle bounds, bool stationary)
        {
            Sprite = sprite;
            Bounds = bounds;
            _stationary = stationary;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
            if(!_stationary)    
                AI();
        }
        private void AI()
        {
            var rand = new Random();
            int direction = rand.Next(0, 4);
            switch (direction)
            {
                case (0):
                    Velocity = new Vector2(0, -1) * _speed;
                    Sprite.PlayAnimation("Up");
                    break;
                case (1):
                    Velocity = new Vector2(0, 1) * _speed;
                    Sprite.PlayAnimation("Down");
                    break;
                case (2):
                    Velocity = new Vector2(-1, 0) * _speed;
                    Sprite.PlayAnimation("Left");
                    break;
                case (3):
                    Velocity = new Vector2(1, 0) * _speed;
                    Sprite.PlayAnimation("Right");
                    break;
            }
            if (CollisionManager.IsValidMove(this, Velocity))
                Position += Velocity;
            else Velocity = Vector2.Zero;
        }
        public void Execute()
        {
            //open shops GUI, choice dialog like in Inn or just regular dialog box
        }
    }
}
