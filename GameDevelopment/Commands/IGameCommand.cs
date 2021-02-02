using GameDevelopment.Entity.Interfaces;

using Microsoft.Xna.Framework;

namespace GameDevelopment.Commands
{
    public interface IGameCommand
    {
        void Execute(ITransform transform, Vector2 direction);

        void Undo(ITransform transform);
    }
}
