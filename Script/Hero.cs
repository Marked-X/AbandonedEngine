using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DragonQuest1.Script
{
    class Hero : Entity
    {
        public override Sprite Sprite { get; set; }
        private Vector2 _position;
        public override Vector2 Position { get { return _position; } set { Bounds = new Rectangle((int)value.X, (int)value.Y, 16, 16); _position = value; } }
        public override Vector2 Velocity { get; set; }
        public override Rectangle Bounds { get; set; }
        private int _step = 8;
        private float _speed = 0.25f;

        #region stats
        private int _hp { get; set; } = 20;
        private int _mp { get; set; } = 20;
        private int _hpMax { get; set; } = 20;
        private int _mpMax { get; set; } = 20;
        private int _strength { get; set; } = 5;
        private int _agility { get; set; } = 5;
        #endregion

        public Hero(ContentManager content)
        {
            Sprite = new Sprite(new Dictionary<string, Animation>() {
                {"Down", new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 0) },
                {"Up",  new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 32)},
                {"Left", new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 64) },
                {"Right",  new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 96)},
            });
            Bounds = new Rectangle(0, 0, 16, 16);
            Position = Vector2.Zero;
        }

        public Hero(ContentManager content, Vector2 pos)
        {
            Sprite = new Sprite(new Dictionary<string, Animation>() {
                {"Down", new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 0) },
                {"Up",  new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 32)},
                {"Left", new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 64) },
                {"Right",  new Animation(content.Load<Texture2D>("Sprites/Hero"), 2, 16, 16, 96)},
            });
            Bounds = new Rectangle(0, 0, 16, 16);
            Position = pos;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            CheckInput();
            Sprite.Position = Position;
            Sprite.Update(gameTime);
        }

        private void CheckInput()
        {
            if (InputManager.IsKeyDown(Keys.W))
                Move(Directions.Up);
            else if (InputManager.IsKeyDown(Keys.S))
                Move(Directions.Down);
            else if (InputManager.IsKeyDown(Keys.A))
                Move(Directions.Left);
            else if (InputManager.IsKeyDown(Keys.D))
                Move(Directions.Right);
            else Velocity = Vector2.Zero;
        }

        private void Move(Directions direction)
        {
            switch (direction)
            {
                case (Directions.Up):
                    Velocity = new Vector2(0, -1) * _step * _speed;
                    Sprite.PlayAnimation("Up");
                    break;
                case (Directions.Down):
                    Velocity = new Vector2(0, 1) * _step * _speed;
                    Sprite.PlayAnimation("Down");
                    break;
                case (Directions.Left):
                    Velocity = new Vector2(-1, 0) * _step * _speed;
                    Sprite.PlayAnimation("Left");
                    break;
                case (Directions.Right):
                    Velocity = new Vector2(1, 0) * _step * _speed;
                    Sprite.PlayAnimation("Right");
                    break;
            }
            if (CollisionManager.IsValidMove(this, Velocity))
                Position += Velocity;
            else Velocity = Vector2.Zero;
        }
    }
}
