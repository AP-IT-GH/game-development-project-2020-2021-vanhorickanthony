using System;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Entity.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            this._texture = animationSheet.GetTexture();
            this._transform = transform;

            _animationHandler = new AnimationHandler();
            
            _animationHandler.AddFrames(animationSheet.GetTexture(), animationSheet.GetSpriteWidth(), animationSheet.GetSpriteHeight());
        }
        
        public void Update(GameTime gameTime, IDirection spriteDirection)
        {
            this._direction = spriteDirection;
            
            _animationHandler.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _transform.Position, _animationHandler.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, this._direction.CurrentDirection, 0);
        }
    }
}