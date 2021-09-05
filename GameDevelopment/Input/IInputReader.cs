using Microsoft.Xna.Framework;

namespace GameDevelopment.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();
        
        Vector2 ReadInput(Vector2 direction);

        bool ReadFollower();
    }
}
