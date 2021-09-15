using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DragonQuest1.Script
{
    /// <summary>
    /// static class used to create levels, can be used to paint tiles, create and place entities and triggers
    /// </summary>
    static class LevelBuilder
    {
        //builder
        static private ContentManager content;
        static private bool _builderActive = false;
        static private SpriteFont _builderFont;
        static private Texture2D _builderTexture;
        static private Level _level;
        static private bool _gridSnapping = true;
        static private int _layer = 0;

        //tiles
        static private TextureAtlas _textureAtlas;
        static private List<Tile> _paletteTiles;
        static private bool _addingTile = false;
        static private bool _builderTileCollidable = false;
        static private Tile _builderTile;
        static private Rectangle _paletteLeftButton;
        static private Rectangle _paletteRightButton;

        //triggers
        static private bool _triggerDebug = false;
        static private int _triggerActiveState = 0;
        // 0 - trigger target, 1 - starting pos X, 2 - starting pos Y
        static private List<string> _triggerParam = new List<string>() { "", "", ""};
        static private bool _addingTrigger = false;
        static private Rectangle _paletteTrigger;
        static public bool AddingTrigger { get => _addingTrigger; }

        static public void Initialize(ContentManager cont, GraphicsDevice graphicsDevice)
        {
            content = cont;
            _builderTexture = content.Load<Texture2D>("Sprites/builderBackground");
            _builderFont = content.Load<SpriteFont>("Arial");

            _paletteTiles = new List<Tile>();
            _textureAtlas = new TextureAtlas(content.Load<Texture2D>("Sprites/Tiles/Castle"), Game1.TILE_SIZE, Game1.TILE_SIZE);
            _paletteLeftButton = new Rectangle(0, Game1.TILE_SIZE / 2, Game1.TILE_SIZE, Game1.TILE_SIZE);
            _paletteRightButton = new Rectangle(graphicsDevice.Viewport.Width - Game1.TILE_SIZE, Game1.TILE_SIZE / 2, Game1.TILE_SIZE, Game1.TILE_SIZE);
            for (int i = 0; i < _textureAtlas.Count; i++)
            {
                _paletteTiles.Add(new Tile(i,
                                           _textureAtlas.TextureRegions[i],
                                           new Rectangle(Game1.TILE_SIZE + i * Game1.TILE_SIZE * 2,
                                                         Game1.TILE_SIZE - Game1.TILE_SIZE / 2,
                                                         Game1.TILE_SIZE,
                                                         Game1.TILE_SIZE),
                                           false));
            }
            _paletteTrigger = new Rectangle(0,
                                            Game1.TILE_SIZE * 2,
                                            Game1.TILE_SIZE * 4,
                                            Game1.TILE_SIZE * 2);

            //------------

        }
        static public void StartBuilder(Level level, CommandList commands)
        {
            _level = level;
            commands.SwitchBuilder += c_SwitchBuilder;
        }
        static public void SwitchBuilder()
        {
            if (_builderActive)
                _builderActive = false;
            else _builderActive = true;
        }
        static public void c_SwitchBuilder(object sender, EventArgs e)
        {
            _builderActive = !_builderActive;
        }
        static public void Update()
        {
            if (_builderActive)
            {
                if (InputManager.isKeyPressed(Keys.S) && InputManager.IsKeyDown(Keys.LeftControl))
                    Save();
                else if (InputManager.isKeyPressed(Keys.C) && InputManager.IsKeyDown(Keys.LeftControl))
                    Clear();
                else if (InputManager.isKeyPressed(Keys.L) && InputManager.IsKeyDown(Keys.LeftControl))
                    Load();
                else if (InputManager.isKeyPressed(Keys.R) && InputManager.IsKeyDown(Keys.LeftControl))
                    CollisionManager.ReloadTiles(_level.Tiles);
                else if (InputManager.isKeyPressed(Keys.T) && InputManager.IsKeyDown(Keys.LeftControl))
                    _triggerDebug = !_triggerDebug;
                else if (InputManager.isKeyPressed(Keys.S) && InputManager.IsKeyDown(Keys.LeftShift))
                    _gridSnapping = !_gridSnapping;
                else if (InputManager.isKeyPressed(Keys.Right))
                {
                    if (_layer > 0)
                        _layer--;
                }
                else if (InputManager.isKeyPressed(Keys.Left))
                    if (_layer < (int)Sprite.Layer.MAX_LAYER - 1)
                        _layer++;

                if (InputManager.isLeftClicked() && !_addingTile && !_addingTrigger)
                {
                    if (InputManager.mousePosition.Intersects(_paletteLeftButton))
                        foreach (Tile tile in _paletteTiles)
                            tile.Bounds = new Rectangle(tile.Bounds.X - tile.Bounds.Width, tile.Bounds.Y, tile.Bounds.Width, tile.Bounds.Height);
                    else if (InputManager.mousePosition.Intersects(_paletteRightButton))
                        foreach (Tile tile in _paletteTiles)
                            tile.Bounds = new Rectangle(tile.Bounds.X + tile.Bounds.Width, tile.Bounds.Y, tile.Bounds.Width, tile.Bounds.Height);
                    else if (InputManager.mousePosition.Intersects(_paletteTrigger))
                    {
                        _addingTrigger = true;
                        for (int i = 0; i < 3; i++)
                            _triggerParam[i] = "";
                    }
                    else foreach (Tile tile in _paletteTiles)
                    {
                        if (InputManager.mousePosition.Intersects(tile.Bounds))
                        {
                            _builderTile = new Tile(tile.Id, tile.Sprite.TextureRegion, tile.Bounds, tile.Collidable);
                            _builderTileCollidable = false;
                            _addingTile = true;
                        }
                    }
                }
                else if (_addingTile)
                    TileUpdate();
                else if (_addingTrigger)
                    TriggerUpdate();
            }
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (_builderActive)
            {
                if (_addingTile)
                {
                    _builderTile.Draw(spriteBatch);
                    //Tile debug
                    spriteBatch.DrawString(_builderFont, "Collidable: " + _builderTileCollidable.ToString(), new Vector2(600, 120), Color.White);
                }
                else if (_addingTrigger)
                {
                    spriteBatch.Draw(_builderTexture,
                                     new Rectangle(InputManager.globalMousePosition.X - Game1.TILE_SIZE / 2,
                                                   InputManager.globalMousePosition.Y - Game1.TILE_SIZE / 2,
                                                   Game1.TILE_SIZE,
                                                   Game1.TILE_SIZE),
                                     Color.Green);
                    //Trigger debug
                    spriteBatch.DrawString(_builderFont, "Target id: " + _triggerParam[0], new Vector2(600, 120), Color.White);
                    spriteBatch.DrawString(_builderFont, "Pos X: " + _triggerParam[1], new Vector2(600, 150), Color.White);
                    spriteBatch.DrawString(_builderFont, "Pos Y: " + _triggerParam[2], new Vector2(600, 180), Color.White);
                }

                spriteBatch.DrawString(_builderFont, "MousePosX: " + InputManager.globalMousePosition.X.ToString(), new Vector2(600, 30), Color.White);
                spriteBatch.DrawString(_builderFont, "MousePosY: " + InputManager.globalMousePosition.Y.ToString(), new Vector2(600, 60), Color.White);
                spriteBatch.DrawString(_builderFont, "Layer: " + (Sprite.Layer)_layer, new Vector2(600, 90), Color.White);

                //Drawing palette
                spriteBatch.Draw(_builderTexture, new Rectangle(0, 0, _paletteTiles.Count * Game1.TILE_SIZE * 2, Game1.TILE_SIZE * 2), new Color(Color.White, 0.5f));
                foreach (Tile tile in _paletteTiles)
                {
                    tile.Draw(spriteBatch);
                }               
                spriteBatch.Draw(_builderTexture, _paletteTrigger, Color.Red);
                spriteBatch.Draw(_builderTexture, _paletteLeftButton, Color.Green);
                spriteBatch.Draw(_builderTexture, _paletteRightButton, Color.Green);
                spriteBatch.DrawString(_builderFont, "Trigger", new Vector2(Game1.TILE_SIZE / 2, Game1.TILE_SIZE * 2.5f), Color.White);
            }
            //more trigger debug
            if(_triggerDebug)
                foreach (Trigger trigger in _level.Triggers)
                {
                    spriteBatch.Draw(_builderTexture, trigger.bounds, Color.Lime);
                }
            spriteBatch.End();
        }
        static public void Save()
        {
            string saved = "";
            foreach (Tile tile in _level.Tiles)
            {
                saved += tile.Id + ", " + tile.Bounds.X + ", " + tile.Bounds.Y + ", " + tile.Collidable + ", " + tile.Rotation + ", " + tile.SpriteEffects + ", " + tile.Layer + "\n";
            }
            File.WriteAllText("Content/Levels/" + _level.Id, saved);
        }
        static public void Load()
        {
            string load = File.ReadAllText("Content/Levels/" + _level.Id);
            if (!string.IsNullOrEmpty(load))
            {
                string[] tileInfos = load.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

                foreach (string tileInfo in tileInfos)
                {
                    string[] temp = tileInfo.Split(',');
                    _level.Tiles.Add(new Tile(int.Parse(temp[0]),
                                              _textureAtlas.TextureRegions[int.Parse(temp[0])],
                                              new Rectangle(int.Parse(temp[1]), int.Parse(temp[2]), Game1.TILE_SIZE, Game1.TILE_SIZE),
                                              bool.Parse(temp[3]),
                                              float.Parse(temp[4]),
                                              (SpriteEffects)Enum.Parse(typeof(SpriteEffects), temp[5]),
                                              (Sprite.Layer)int.Parse(temp[6])));
                }
            }
        }
        static public void Clear()
        {
            _level.Tiles.Clear();
        }
        
        static private void TileUpdate()
        {
            //Rotating, flipping and changing other Tile properties
            if (InputManager.isKeyPressed(Keys.W) && InputManager.IsKeyDown(Keys.LeftControl))
                _builderTileCollidable = !_builderTileCollidable;
            if (InputManager.isKeyPressed(Keys.Q) && InputManager.IsKeyDown(Keys.LeftControl))
                _builderTile.Rotation -= MathHelper.ToRadians(90);
            if (InputManager.isKeyPressed(Keys.E) && InputManager.IsKeyDown(Keys.LeftControl))
                _builderTile.Rotation += MathHelper.ToRadians(90);
            if (InputManager.isKeyPressed(Keys.Q) && InputManager.IsKeyDown(Keys.LeftShift))
                _builderTile.SpriteEffects = SpriteEffects.FlipHorizontally;
            if (InputManager.isKeyPressed(Keys.E) && InputManager.IsKeyDown(Keys.LeftShift))
                _builderTile.SpriteEffects = SpriteEffects.FlipVertically;

            //Moving "Builder" Tile
            _builderTile.Bounds = new Rectangle(InputManager.mousePosition.X - Game1.TILE_SIZE / 2, 
                                                InputManager.mousePosition.Y - Game1.TILE_SIZE / 2, 
                                                Game1.TILE_SIZE, 
                                                Game1.TILE_SIZE);

            //Placing "Builder" tile or cancel painting
            if (InputManager.isLeftClicked() || InputManager.isLeftHold())
            {
                for (int i = 0; i < _level.Tiles.Count; i++)
                {
                    if (InputManager.globalMousePosition.Intersects(_level.Tiles[i].Bounds))
                    {
                        if(_level.Tiles[i].Layer == (Sprite.Layer)_layer)
                        {
                            _level.Tiles.RemoveAt(i);
                            break;
                        }
                    }
                }
                Point tilePos;
                //TODO: Refactor negative number snap calculation
                if (_gridSnapping)
                {
                    tilePos = new Point();
                    if (InputManager.globalMousePosition.X > 0)
                        tilePos.X = InputManager.globalMousePosition.X - (InputManager.globalMousePosition.X % Game1.TILE_SIZE);
                    else
                        tilePos.X = InputManager.globalMousePosition.X - (InputManager.globalMousePosition.X % Game1.TILE_SIZE) - Game1.TILE_SIZE;
                    if (InputManager.globalMousePosition.Y > 0)
                        tilePos.Y = InputManager.globalMousePosition.Y - (InputManager.globalMousePosition.Y % Game1.TILE_SIZE);
                    else
                        tilePos.Y = InputManager.globalMousePosition.Y - (InputManager.globalMousePosition.Y % Game1.TILE_SIZE) - Game1.TILE_SIZE;
                }
                else
                    tilePos = new Point(InputManager.globalMousePosition.X - Game1.TILE_SIZE / 2, InputManager.globalMousePosition.Y - Game1.TILE_SIZE / 2);
                _level.Tiles.Add(new Tile(_builderTile.Id,
                                          _builderTile.Sprite.TextureRegion,
                                          new Rectangle(tilePos, new Point(Game1.TILE_SIZE, Game1.TILE_SIZE)), 
                                          _builderTileCollidable,
                                          _builderTile.Rotation,
                                          _builderTile.SpriteEffects,
                                          (Sprite.Layer)_layer));
            }
            else if (InputManager.isRightClicked())
            {
                _addingTile = false;
                _builderTile = null;
            };
        }

        static private void TriggerUpdate()
        {
            TriggerParamTyping();
            if (InputManager.isLeftClicked())
            {
                for (int i = 0; i < _level.Triggers.Count; i++)
                {
                    if (InputManager.globalMousePosition.Intersects(_level.Triggers[i].bounds))
                    {
                        _level.Triggers.RemoveAt(i);
                        break;
                    }
                }
                Point triggerPos = new Point((InputManager.globalMousePosition.X - (InputManager.globalMousePosition.X % Game1.TILE_SIZE)), (InputManager.globalMousePosition.Y - (InputManager.globalMousePosition.Y % Game1.TILE_SIZE)));
                _level.Triggers.Add(new Trigger(new Rectangle(triggerPos, new Point(Game1.TILE_SIZE, Game1.TILE_SIZE)),
                                                int.Parse(_triggerParam[0]),
                                                new Vector2(float.Parse(_triggerParam[1]), float.Parse(_triggerParam[2]))));
            }
            else if (InputManager.isRightClicked())
                _addingTrigger = false;
        }
        static private void TriggerParamTyping()
        {
            //refactor this junk
            if (InputManager.isKeyPressed(Keys.NumPad0))
                _triggerParam[_triggerActiveState] += '0';
            else if (InputManager.isKeyPressed(Keys.NumPad1))
                _triggerParam[_triggerActiveState] += '1';
            else if (InputManager.isKeyPressed(Keys.NumPad2))
                _triggerParam[_triggerActiveState] += '2';
            else if (InputManager.isKeyPressed(Keys.NumPad3))
                _triggerParam[_triggerActiveState] += '3';
            else if (InputManager.isKeyPressed(Keys.NumPad4))
                _triggerParam[_triggerActiveState] += '4';
            else if (InputManager.isKeyPressed(Keys.NumPad5))
                _triggerParam[_triggerActiveState] += '5';
            else if (InputManager.isKeyPressed(Keys.NumPad6))
                _triggerParam[_triggerActiveState] += '6';
            else if (InputManager.isKeyPressed(Keys.NumPad7))
                _triggerParam[_triggerActiveState] += '7';
            else if (InputManager.isKeyPressed(Keys.NumPad8))
                _triggerParam[_triggerActiveState] += '8';
            else if (InputManager.isKeyPressed(Keys.NumPad9))
                _triggerParam[_triggerActiveState] += '9';
            else if (InputManager.isKeyPressed(Keys.Enter))
                if (_triggerActiveState != 2)
                    _triggerActiveState++;
                else _triggerActiveState = 0;
            else if (InputManager.isKeyPressed(Keys.Back))
                _triggerParam[_triggerActiveState].Remove(_triggerParam[_triggerActiveState].Length - 1);
        }
    }
}
