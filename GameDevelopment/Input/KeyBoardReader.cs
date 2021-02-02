using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevelopment.Input
{
    class KeyBoardReader : IInputReader
    {
        public bool ReadFollower()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.M))
                return true;

            return false;
        }

        public Vector2 ReadInput()
        {
            Vector2 direction = Vector2.Zero;

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left))
            {
                if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
                {
                    direction.X = -2;
                }
                else
                {
                    direction.X = -1;
                }
            }
            
            else if (state.IsKeyDown(Keys.Right))
            {
                if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
                {
                    direction.X = 2;
                }
                else
                {
                    direction.X = 1;
                }
            }

            if (state.IsKeyDown(Keys.Space))
            {
                direction.Y = -2;
            }
            
            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }

            return direction;
        }
    }
}
