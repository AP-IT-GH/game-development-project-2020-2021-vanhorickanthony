using GameDevelopment.Entity.Interfaces;

using Microsoft.Xna.Framework;

namespace GameDevelopment.Commands
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
            this.lastPosition = transform.Position;
            
            direction *= speed;
            
            transform.Position += direction;
        }

        public void Undo(ITransform transform)
        {
            transform.Position = lastPosition;
        }
    }
}
