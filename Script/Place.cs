using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DragonQuest1.Script
{
    /// <summary>
    /// Places are things like towns, castles, shrines, etc.
    /// </summary>
    class Place : Level
    {
        private Hero hero;
        private Camera _camera;
        private TextureAtlas _tileSheet;
        public override int Id { get; set; }
        public override Vector2 StartingHeroPosition { get; set; }
        public override List<Tile> Tiles { get; set; }
        public override List<Trigger> Triggers { get; set; }
        public Place(int id)
        {
            this.Id = id;
        }

        public override void Initialize(ContentManager content, Camera camera)
        {
            hero = new Hero(content, StartingHeroPosition);
            _camera = camera;
            //Loading level tiles
            Tiles = new List<Tile>();
            _tileSheet = new TextureAtlas(content.Load<Texture2D>("Sprites/Tiles/Castle"), Game1.TILE_SIZE, Game1.TILE_SIZE);

            if(File.Exists("Content/Levels/" + Id))
            {
                string load = File.ReadAllText("Content/Levels/" + Id);
                if (!string.IsNullOrEmpty(load))
                {
                    string[] tileInfos = load.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

                    foreach (string tileInfo in tileInfos)
                    {
                        string[] temp = tileInfo.Split(',');
                        
                        Tiles.Add(new Tile(int.Parse(temp[0]), 
                                           _tileSheet.TextureRegions[int.Parse(temp[0])], 
                                           new Rectangle(int.Parse(temp[1]), int.Parse(temp[2]), Game1.TILE_SIZE, Game1.TILE_SIZE),
                                           bool.Parse(temp[3]),
                                           float.Parse(temp[4]),
                                           (SpriteEffects)Enum.Parse(typeof(SpriteEffects), temp[5]),
                                           (Sprite.Layer)int.Parse(temp[6])));
                    }
                }
            }
            Triggers = new List<Trigger>();

            CollisionManager.Fill(new List<Entity> { hero }, Tiles, Triggers);
        }

        public override void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            _camera.Update(hero.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              _camera.Transform);
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
            hero.Draw(spriteBatch);
            spriteBatch.End();
        }
        public override void Exit()
        {
            CollisionManager.Clear();
        }
    }
}
