using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DragonQuest1.Script
{
    static class CollisionManager
    {
        static public LevelStateManager levelManager { get; set; }
        static private List<Entity> _entities;
        static private List<Tile> _tiles;
        static private List<Trigger> _triggers;

        static public void Fill(List<Entity> entities, List<Tile> tiles, List<Trigger> triggers)
        {
            _entities = entities;
            _triggers = triggers;
            _tiles = new List<Tile>();
            foreach (Tile tile in tiles)
                if (tile.Collidable)
                    _tiles.Add(tile);
        }
        static public void ReloadTiles(List<Tile> tiles)
        {
            _tiles = new List<Tile>();
            foreach (Tile tile in tiles)
                if (tile.Collidable)
                    _tiles.Add(tile);
        }
        static public bool IsValidMove(Entity entity, Vector2 velocity)
        {
            Rectangle temp = new Rectangle(entity.Bounds.X + (int)velocity.X, entity.Bounds.Y + (int)velocity.Y, 16, 16);
            if(_tiles != null)    
                foreach (Tile tile in _tiles)
                    if (Vector2.Distance(entity.Sprite.Position, tile.sprite.Position) < 32)
                        if (temp.Intersects(tile.Bounds))
                            return false;
            if(_entities != null)
                foreach (Entity entityF in _entities)
                    if(entity != entityF)
                        if (Vector2.Distance(entity.Sprite.Position, entityF.Sprite.Position) < 32)
                            if (temp.Intersects(entityF.Bounds))
                                return false;
            if (_triggers != null)
                foreach (Trigger trigger in _triggers)
                    if (Vector2.Distance(entity.Sprite.Position, new Vector2(trigger.bounds.X, trigger.bounds.Y)) < 32)
                        if (temp.Intersects(trigger.bounds))
                        {
                            if (trigger.targetLevel == 0)
                                levelManager.ChangeState(new Overworld(), trigger.targetPosition);
                            else levelManager.ChangeState(new Place(trigger.targetLevel), trigger.targetPosition);
                            return true;
                        }
            return true;
        }
        static public void Clear()
        {
            _entities.Clear();
            _tiles.Clear();
            _triggers.Clear();
        }
    }
}
