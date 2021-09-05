using GameDevelopment.Animation.Abstracts;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Entity.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Animation.HeroAnimations
{
    public class JumpAnimation : BaseAnimation
    {
        public JumpAnimation(IAnimationSheet animationSheet, ITransform transform) : base(animationSheet, transform)
        {

        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public new void Update(GameTime gameTime, IDirection spriteDirection)
        {
            base.Update(gameTime, spriteDirection);
        }
    }
}