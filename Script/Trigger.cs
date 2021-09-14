using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    /// <summary>
    /// Triggers only used for changing level, put them on things like ladders, doors and level bounds
    /// </summary>
    class Trigger
    {
        public Rectangle bounds;
        public int targetLevel;
        public Vector2 targetPosition;

        public Trigger(Rectangle bounds, int targetLevel, Vector2 targetPosition)
        {
            this.bounds = bounds;
            this.targetLevel = targetLevel;
            this.targetPosition = targetPosition;
        }
    }
}
