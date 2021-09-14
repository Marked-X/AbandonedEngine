using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DragonQuest1.Script
{
    /// <summary>
    /// Game's state machine, controls levels which are game's "states"
    /// </summary>
    class LevelStateManager
    {
        private Level _currentLevel;
        private ContentManager _content;
        private Camera _camera;
        public LevelStateManager(ContentManager content, Viewport viewport)
        {
            _content = content;
            _camera = new Camera(viewport);
        }
        public void ChangeState(Level level, Vector2 heroPos)
        {
            if(_currentLevel != null)
                _currentLevel.Exit();
            _currentLevel = level;
            _currentLevel.StartingHeroPosition = heroPos;
            _currentLevel.Initialize(_content, _camera);
            LevelBuilder.StartBuilder(_currentLevel);
        }
        public void Update(GameTime gameTime)
        {
            _currentLevel.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentLevel.Draw(spriteBatch);
        }
    }
}
