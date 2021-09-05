using Microsoft.Xna.Framework;

namespace GameDevelopment.Animation
{
    public class AnimationFrame
    {

        public Rectangle SourceRectangle { get; set; }

        public AnimationFrame(Rectangle rectangle)
        {
            SourceRectangle = rectangle;
        }
    }
}
