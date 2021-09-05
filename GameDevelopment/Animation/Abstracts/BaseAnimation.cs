using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDevelopment.Animation.HeroAnimations;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Entity.Interfaces;

namespace GameDevelopment.Animation.Abstracts
{
    public abstract class BaseAnimation: IAnimation
    {
        private AnimationHandler _animationHandler;

        private Texture2D _texture;

        private IDirection _direction;

        private ITransform _transform;

        protected BaseAnimation(IAnimationSheet animationSheet, ITransform transform)
        {
            _texture = animationSheet.GetTexture();
            _transform = transform;

            _animationHandler = new AnimationHandler();
            
            _animationHandler.AddFrames(animationSheet.GetTexture(), animationSheet.GetSpriteWidth(), animationSheet.GetSpriteHeight());
        }
        
        public void Update(GameTime gameTime, IDirection spriteDirection)
        {
            _direction = spriteDirection;

            _animationHandler.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_direction == null)
            {
                _direction = new Direction(SpriteEffects.None);
            }
            
            spriteBatch.Draw(_texture, _transform.Position, _animationHandler.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, this._direction.CurrentDirection, 0);
        }
    }
}