using GameDevelopment.Animation.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation.HeroAnimations
{
    public class Direction: IDirection
    {
        public Direction(SpriteEffects currentDirection)
        {
            CurrentDirection = currentDirection;
        }

        public SpriteEffects CurrentDirection { get; set; }

        public void Update(Vector2 newDirection)
        {
            if (newDirection.X >= 1)
            {
                CurrentDirection = SpriteEffects.None;
            }
            else if (newDirection.X <= -1)
            {
                CurrentDirection = SpriteEffects.FlipHorizontally;
            }
        }
    }
}
