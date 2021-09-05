using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation
{
    public class AnimationHandler
    {
        public AnimationFrame CurrentFrame { get; set; }

        private List<AnimationFrame> frames;

        private int counter;

        private double frameMovement = 0;
        
        public AnimationHandler()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrames(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            for (int x = 0; x < texture.Width; x += spriteWidth)
            {
                for (int y = 0; y < texture.Height; y += spriteHeight)
                {
                    this.AddFrame(new AnimationFrame(new Rectangle(x, y, spriteWidth, spriteHeight)));
                }
            }
        }

        public void AddFrame(AnimationFrame animationFrame)
        {
            frames.Add(animationFrame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {

            CurrentFrame = frames[counter];

            frameMovement += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.TotalSeconds;

            if (frameMovement >= CurrentFrame.SourceRectangle.Width/8)
            {
                counter++;
                frameMovement = 0;
            }

            if (counter >= frames.Count)
                counter = 0;
        }
        
    }
}
