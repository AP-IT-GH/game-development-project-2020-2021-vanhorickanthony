using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Animation.HeroAnimations
{
    public class IdleAnimation: IEntityAnimation
    {
        private AnimationHandler Animation;
        private IEntityDirection Direction;

        Texture2D texture;
        ITransform transform;

        public IdleAnimation(Texture2D texture, ITransform transform)
        {
            this.transform = transform;
            this.texture = texture;

            Animation = new AnimationHandler();

            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 32, 40)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 40, 32, 40)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 80, 32, 40)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 120, 32, 40)));

        }

        public AnimationHandler Animatie
        {
            get { return Animation; }
            set { Animation = value; }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, transform.Position, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, this.Direction.Direction, 0);
        }

        public void Update(GameTime gameTime, IEntityDirection spriteDirection)
        {
            this.Direction = spriteDirection;
            this.Animation.Update(gameTime);
        }
    }
}
