using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class Camera
    {
        private Matrix _transform;
        public Matrix Transform { get => _transform; }

        private Vector2 _centre;
        private Viewport _viewport;

        private float _zoom = 1;
        private float _rotation = 0;

        public float X
        {
            get { return _centre.X; }
            set { _centre.X = value; }
        }
        public float Y
        {
            get { return _centre.Y; }
            set { _centre.Y = value; }
        }
        public float Zoom
        {
            get { return _zoom; }
            set 
            { 
                _zoom = value;
                if (_zoom < 0.1f) 
                    _zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
        }
        public void Update(Vector2 position)
        {
            _centre = new Vector2(position.X + Game1.TILE_SIZE / 2, position.Y + Game1.TILE_SIZE / 2);

            _transform = Matrix.CreateTranslation(new Vector3(-_centre.X, -_centre.Y, 0)) *
                                                  Matrix.CreateRotationZ(Rotation) *
                                                  Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                  Matrix.CreateTranslation(new Vector3(_viewport.Width / 2, _viewport.Height / 2, 0));
            InputManager.MousePos(Matrix.Invert(Transform));
        }
    }
}
