using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Commands
{
    public class MoveCommand : IGameCommand
    {
        public Vector2 speed;

        public Vector2 lastPosition;

        public MoveCommand()
        {
            this.speed = new Vector2(1, 1);
        }

        public void Execute(ITransform transform, Vector2 direction)
        {
            lastPosition = transform.Position;
            direction *= speed;
            transform.Position += direction;
        }

        public void Undo(ITransform transform)
        {
            transform.Position = lastPosition;
        }
    }
}
