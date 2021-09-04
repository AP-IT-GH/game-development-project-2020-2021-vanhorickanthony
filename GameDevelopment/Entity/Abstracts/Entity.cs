using System;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Diagnostics;
using GameDevelopment.Animation.HeroAnimations;
using GameDevelopment.Entity.Interfaces;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Collision.Interfaces;

using GameDevelopment.Commands;
using GameDevelopment.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.Entity.Abstracts
{
    public abstract class BaseEntity: ITransform, ICollision, IGravity
    {
        public int Health;
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        
        public Vector2 Gravity { get; set; }
        
        protected IInputReader InputReader;

        protected IGameCommand MoveCommand;

        public IDirection EntityDirection;

        protected Dictionary<Vector2, IAnimation> Animations;

        protected IAnimation SelectedAnimation;

        private float _collisionOffsetX;
        private float _collisionOffsetY;
        
        protected BaseEntity(
            Vector2 spawnPosition,
            IInputReader inputReader, 
            Rectangle collisionRectangle,
            float collisionOffsetX = 0f,
            float collisionOffsetY = 0f
        )
        {
            Position = spawnPosition;

            Animations = new Dictionary<Vector2, IAnimation>();

            EntityDirection = new Direction(SpriteEffects.None);
            
            InputReader = inputReader;

            MoveCommand = new MoveCommand();

            CollisionRectangle = collisionRectangle;

            _collisionOffsetX = collisionOffsetX;
            _collisionOffsetY = collisionOffsetY;

            Health = 100;
        }

        public void Update(GameTime gameTime)
        {
            var direction = InputReader.ReadInput();

            EntityDirection.Update(direction);

            Move(direction);
            
            SelectedAnimation.Update(gameTime, EntityDirection);

            Rectangle collisionRectangle = CollisionRectangle;

            collisionRectangle.X = (int) (Position.X + _collisionOffsetX);
            collisionRectangle.Y = (int) (Position.Y + _collisionOffsetY);

            CollisionRectangle = collisionRectangle;
        }
        
        private void Move(Vector2 direction)
        {
            Vector2 animationVector = new Vector2(0, 0); 
            
            if ( (Math.Abs(direction.Y) > 0) || Math.Abs(Gravity.Y) > 0)
            {
                animationVector = new Vector2(0, 5);
            }
            else if (Math.Abs(direction.X) > 0)
            {
                animationVector = new Vector2(Math.Abs(direction.X), 0);
            }
            
            if (Animations.ContainsKey(animationVector))
            {
                SelectedAnimation = Animations[animationVector];
            }
            else
            {
                SelectedAnimation = Animations[new Vector2(0, 0)];
            }

            MoveCommand.Execute(this, direction + Gravity);
        }

        private void MoveHorizontal(Vector2 direction)
        {
            Vector2 currentVector = new Vector2(direction.X, 0);

            if (Animations.ContainsKey(currentVector))
            {
                SelectedAnimation = Animations[new Vector2(Math.Abs(direction.X), 0)];
            }
            else
            {
                SelectedAnimation = Animations[new Vector2(0, 0)];
            }

            MoveCommand.Execute(this, new Vector2(direction.X, 0));
        }
        
        private void MoveVertical(Vector2 direction)
        {
            Vector2 currentVector = new Vector2(0, Math.Abs(direction.Y));

            if (Animations.ContainsKey(currentVector))
            {
                SelectedAnimation = Animations[currentVector];
            }
            else
            {
                SelectedAnimation = Animations[new Vector2(0, 0)];
            }

            MoveCommand.Execute(this, new Vector2(0, direction.Y + Gravity.Y));
        }
        
        private void ApplyGravity(Vector2 direction)
        {
            Vector2 currentVector = new Vector2(0, Math.Abs(direction.Y));

            if (Animations.ContainsKey(currentVector))
            {
                SelectedAnimation = Animations[currentVector];
            }
            else
            {
                SelectedAnimation = Animations[new Vector2(0, 0)];
            }

            MoveCommand.Execute(this, Gravity);
        }

        public void Undo()
        {
            MoveCommand.Undo(this);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            SelectedAnimation.Draw(spriteBatch);
        }

    }

}