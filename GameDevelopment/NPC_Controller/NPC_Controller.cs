using System;
using System.Collections.Generic;
using GameDevelopment.Animation.HeroAnimations;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Entity;
using GameDevelopment.Entity.Abstracts;
using GameDevelopment.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevelopment.NPC_Controller
{
    public class NPC_Controller
    {
        private List<Tuple<Vector2, Hostile_NPC>> _entities;
        
        public NPC_Controller(
            List<Tuple<Vector2, Hostile_NPC>> entities
            )
        {
            this._entities = entities;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var NPC in _entities)
            {
                Console.WriteLine("___________");
                Console.WriteLine("Updating NPC...");
                
                Vector2 direction = new Vector2(NPC.Item1.X - NPC.Item2.Position.X, 0);
                
                Console.WriteLine("Waypoint: ");
                Console.WriteLine(NPC.Item1);
                
                Console.WriteLine("Current position: ");
                Console.WriteLine(NPC.Item2.Position);
                
                Console.WriteLine("Calculated direction: ");
                Console.WriteLine(direction);
                
                IInputReader inputController = NPC.Item2.GetInputReader();
                
                if (Math.Abs(direction.X) > float.Epsilon)
                {
                    Console.WriteLine("Still have to move... ");

                    inputController.ReadInput(direction);
                }
                else
                {
                    Console.WriteLine("I'm done moving for now.");
                    inputController.ReadInput(new Vector2(0, 0));
                    Console.WriteLine("+++++++++++");
                }

                NPC.Item2.Update(gameTime);
                
                Console.WriteLine("Updated NPC.");

                Console.WriteLine("___________");
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var NPC in _entities)
            {
                NPC.Item2.Draw(spriteBatch);
            } 
        }
    }
}