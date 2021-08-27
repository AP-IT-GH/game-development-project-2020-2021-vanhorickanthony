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
    public abstract class BaseEntity: ITransform, ICollision
    {
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }


        protected IInputReader InputReader;

        protected IGameCommand MoveCommand;

        protected IDirection EntityDirection;

        protected Dictionary<Vector2, IAnimation> Animations;

        protected IAnimation SelectedAnimation;

        protected BaseEntity(Vector2 spawnPosition, IInputReader inputReader, Rectangle collisionRectangle)
        {
            this.Position = spawnPosition;

            this.Animations = new Dictionary<Vector2, IAnimation>();

            this.EntityDirection = new Direction(SpriteEffects.None);

            this.InputReader = inputReader;

            this.MoveCommand = new MoveCommand();

            this.CollisionRectangle = collisionRectangle;
        }

        public void Update(GameTime gameTime)
        {
            this.EntityDirection.Update(InputReader.ReadInput());

            var direction = InputReader.ReadInput();

            MoveHorizontal(direction);

            SelectedAnimation.Update(gameTime, EntityDirection);

            Rectangle collisionRectangle = CollisionRectangle;

            collisionRectangle.X = (int) Position.X + collisionRectangle.Width / 2;
            collisionRectangle.Y = (int) Position.Y;

            CollisionRectangle = collisionRectangle;
        }

        private void MoveHorizontal(Vector2 direction)
        {
            Vector2 currentVector = new Vector2(Math.Abs(direction.X), Math.Abs(direction.Y));

            Debug.WriteLine(currentVector);

            if (Animations.ContainsKey(currentVector))
            {
                SelectedAnimation = Animations[currentVector];
            }
            else
            {
                SelectedAnimation = Animations[new Vector2(0, 0)];
            }

            MoveCommand.Execute(this, direction);
        }

        public void Undo()
        {
            this.MoveCommand.Undo(this);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            SelectedAnimation.Draw(spriteBatch);
        }

    }

}