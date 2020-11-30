using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.Interfaces;

namespace TestGame.Commands
{
    public class MoveToCommando : IGameCommand
    {
        private Vector2 snelheid;

        public MoveToCommando()
        {
            snelheid = new Vector2(1, 0);
        }
      
        public void Execute(ITransform transform, Vector2 direction)
        {
            direction = Vector2.Add(direction, -transform.Position);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 0.1f);

            snelheid += direction;
            snelheid = Limit(snelheid, 6);
            transform.Position += snelheid;
        }

        public void Undo(ITransform transform)
        {
            throw new NotImplementedException();
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }
    }

   

}
