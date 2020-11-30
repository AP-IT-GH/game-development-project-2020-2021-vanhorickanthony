using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Animation.HeroAnimations
{
    public class EntityDirection: IEntityDirection
    {
        private SpriteEffects currentDirection;

        public EntityDirection(SpriteEffects Direction)
        {
            currentDirection = Direction;
        }

        public SpriteEffects Direction
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }

       public void Update(Vector2 newDirection)
        {
            if (newDirection.X >= 1)
            {
                this.currentDirection = SpriteEffects.None;
            }
            else if (newDirection.X <= -1)
            {
                this.currentDirection = SpriteEffects.FlipHorizontally;
            }
        }
    }
}
