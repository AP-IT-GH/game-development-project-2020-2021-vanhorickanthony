using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TestGame.Animation;
using TestGame.Animation.HeroAnimations;
using TestGame.Commands;
using TestGame.Input;
using TestGame.Interfaces;

namespace TestGame
{
    public class Hero :ITransform, ICollision
    {
        private Texture2D idleTexture;
        private Texture2D runningTexture;
        private Texture2D climbingTexture;

        private Texture2D heroTexture;

        private AnimationHandler animatie;       
       
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get ; set ; }
        private Rectangle _collisionRectangle;

        private IInputReader inputReader;
        private IInputReader mouseReader;

        private IGameCommand moveCommand;
        private IGameCommand moveToCommand;

        private IEntityDirection heroDirection;


        IEntityAnimation idle, walk, run, currentAnimation;

       
        public Hero(Vector2 spawnPosition, Texture2D IdleTexture, Texture2D WalkingTexture, Texture2D RunningTexture, IInputReader reader)
        {
            this.Position = spawnPosition;

            this.idleTexture = IdleTexture;
            this.runningTexture = RunningTexture;

            idle = new IdleAnimation(IdleTexture, this);

            walk = new WalkAnimation(WalkingTexture, this);

            run = new RunAnimation(RunningTexture, this);

            heroDirection = new EntityDirection(SpriteEffects.None);

            currentAnimation = idle;

            //Read input for my hero class
            this.inputReader = reader;
            mouseReader = new MouseReader();

            moveCommand = new MoveCommand();
            moveToCommand = new MoveToCommando();

            _collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 280, 385);

        }

        public void Update(GameTime gameTime)
        {
            this.heroDirection.Update(inputReader.ReadInput());

            var direction = inputReader.ReadInput();

            MoveHorizontal(direction);
         
            if(inputReader.ReadFollower())
                Move(mouseReader.ReadInput());


            //animatie.Update(gameTime);
            currentAnimation.Update(gameTime, heroDirection);

            _collisionRectangle.X = (int)Position.X;
            CollisionRectangle = _collisionRectangle;


        }

        private void MoveHorizontal(Vector2 _direction)
        {
            if (Math.Abs(_direction.X) == 2)
            {
                currentAnimation = run;
            }
            else if (Math.Abs(_direction.X) == 1)
            {
                currentAnimation = walk;
            }
            else if (Math.Abs(_direction.X) == 0)
            {
                currentAnimation = idle;
            }
            else
            {
                currentAnimation = idle;
            }

            moveCommand.Execute(this, _direction);
        }    

       

        private void Move(Vector2 mouse) 
        {
            moveToCommand.Execute(this, mouse);

        }

   

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }
    }
}
