using DragonQuest1.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DragonQuest1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelStateManager _levelStateManager;
        public const int TILE_SIZE = 16;
        public Rectangle ViewportSize { get => GraphicsDevice.Viewport.Bounds;}
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _levelStateManager = new LevelStateManager(this.Content, GraphicsDevice.Viewport);
            LevelBuilder.Initialize(this.Content, this.GraphicsDevice);
            CollisionManager.levelManager = _levelStateManager;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _levelStateManager.ChangeState(new Place(1), new Vector2(32, 32));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update(gameTime);
            _levelStateManager.Update(gameTime);
            /*if (InputManager.isKeyPressed(Keys.F))
                LevelBuilder.SwitchBuilder();
            */
            LevelBuilder.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _levelStateManager.Draw(_spriteBatch);
            LevelBuilder.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
