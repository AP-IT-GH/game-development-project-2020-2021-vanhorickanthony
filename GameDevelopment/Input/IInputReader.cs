using Microsoft.Xna.Framework;

namespace GameDevelopment.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();

        bool ReadFollower();
    }
}
