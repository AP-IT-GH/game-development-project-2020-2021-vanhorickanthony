using System;
using System.Collections.Generic;
using GameDevelopment.Animation.HeroAnimations;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Collision;
using GameDevelopment.Collision.Interfaces;
using GameDevelopment.Entity;
using GameDevelopment.Entity.Abstracts;
using GameDevelopment.Entity.Interfaces;
using GameDevelopment.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace GameDevelopment.NPC_Controller
{
    public class NPC_Controller
    {
        private List<Tuple<Vector2, Hostile_NPC>> _entities;
        
        public NPC_Controller(
            List<Tuple<Vector2, Hostile_NPC>> entities
            )
        {
            _entities = entities;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var NPC in _entities)
            {
                Vector2 direction = new Vector2(NPC.Item1.X - NPC.Item2.Position.X, 0);

                IInputReader inputController = NPC.Item2.GetInputReader();
                
                if (Math.Abs(direction.X) > float.Epsilon)
                {
                    inputController.ReadInput(direction);
                }
                else
                {
                    inputController.ReadInput(new Vector2(0, 0));
                }

                NPC.Item2.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var NPC in _entities)
            {
                NPC.Item2.Draw(spriteBatch);
            } 
        }
        
        public void ApplyGravity(CollisionManager collisionManager, TiledMapTileLayer collisionLayer)
        {
            foreach (var NPC in _entities)
            {
                if (collisionManager.CheckBottomCollision(NPC.Item2, collisionLayer))
                {
                    NPC.Item2.Gravity = new Vector2(0, 0);
                }
                else
                {
                    NPC.Item2.Gravity = new Vector2(
                        0f,
                        (NPC.Item2.Gravity.Y != 0f)
                            ? NPC.Item2.Gravity.Y * GameSettings.Default.GravityVelocity
                            : GameSettings.Default.GravityBase
                    );
                }
            }
        }
        
        public void CheckCollision(CollisionManager collisionManager, TiledMapTileLayer collisionLayer)
        {
            foreach (var NPC in _entities)
            {
                if (collisionManager.CheckCollision(NPC.Item2, collisionLayer))
                {
                    NPC.Item2.Undo();
                }
            }
        }
        
        public bool CheckCollision(CollisionManager collisionManager, ICollision entity)
        {
            foreach (var NPC in _entities)
            {
                if (collisionManager.CheckCollision(NPC.Item2, entity))
                {
                    return true;
                }
            }

            return false;
        }
    }
}